using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using SurveyMonkey.Containers;
using SurveyMonkey.Enums;
using SurveyMonkey.ProcessedAnswers;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi
    {
        public Survey GetMissingSurveyInformation(long surveyId)
        {
            Survey survey = GetSurveyDetails(surveyId);
            survey.Responses = GetResponseDetailList(surveyId, ObjectType.Survey);

            foreach (var response in survey.Responses)
            {
                MatchResponsesToSurveyStructure(survey, response);
            }

            return survey;
        }

        private void MatchResponsesToSurveyStructure(Survey survey, Response response)
        {
            foreach (var question in survey.Questions)
            {
                question.Answers?.PopulatedItemLookup();
            }
            Dictionary<long, Question> questionsLookup = survey.Questions.Where(q => q.Id.HasValue).ToDictionary(q => q.Id.Value, q => q);
            MatchIndividualResponseToSurveyStructure(response, questionsLookup);
        }

        private void MatchIndividualResponseToSurveyStructure(Response response, Dictionary<long, Question> questionsLookup)
        {
            if (response.Questions == null) //In rare cases there can be a respondent with no responses to questions //todo is this still true?
            {
                return;
            }
            foreach (var responseQuestion in response.Questions)
            {
                //First try to match the ResponseQuestion with the survey structure
                if (questionsLookup.ContainsKey(responseQuestion.Id.Value))
                {
                    responseQuestion.ProcessedAnswer = new ProcessedAnswer
                    {
                        QuestionFamily = questionsLookup[responseQuestion.Id.Value].Family,
                        QuestionSubtype = questionsLookup[responseQuestion.Id.Value].Subtype,
                        Response = MatchResponseQuestionToSurveyStructure(questionsLookup[responseQuestion.Id.Value], responseQuestion.Answers)
                        //todo original version stored QuestionHeading
                    };
                }

                //todo Try to retrived from custom variables
                //todo Deleted questions?
                //If there's still no match (probably because the question's been deleted), leave ProcessedAnswer as null
            }
        }

        private IProcessedResponse MatchResponseQuestionToSurveyStructure(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            switch (question.Family)
            {
                case QuestionFamily.SingleChoice:
                    return MatchSingleChoiceAnswer(question, responseAnswers);

                case QuestionFamily.MultipleChoice:
                    return MatchMultipleChoiceAnswer(question, responseAnswers);
/*
                case QuestionFamily.OpenEnded:
                    switch (question.Subtype)
                    {
                        case QuestionSubtype.Essay:
                        case QuestionSubtype.Single:
                            return MatchOpenEndedSingleAnswer(question, responseAnswers);

                        case QuestionSubtype.Multi:
                        case QuestionSubtype.Numerical:
                            return MatchOpenEndedMultipleAnswer(question, responseAnswers);
                    }
                    break;

                case QuestionFamily.Demographic:
                    return MatchDemographicAnswer(question, responseAnswers);

                case QuestionFamily.DateTime:
                    return MatchDateTimeAnswer(question, responseAnswers);

                case QuestionFamily.Matrix:
                    switch (question.Subtype)
                    {
                        case QuestionSubtype.Menu:
                            return MatchMatrixMenuAnswer(question, responseAnswers);
                        case QuestionSubtype.Ranking:
                            return MatchMatrixRankingAnswer(question, responseAnswers);
                        case QuestionSubtype.Rating:
                            return MatchMatrixRatingAnswer(question, responseAnswers);
                        case QuestionSubtype.Single:
                            return MatchMatrixSingleAnswer(question, responseAnswers);
                        case QuestionSubtype.Multi:
                            return MatchMatrixMultiAnswer(question, responseAnswers);
                    }
                    break;*/
            }
            return null;
        }

        private SingleChoiceAnswer MatchSingleChoiceAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new SingleChoiceAnswer();

            foreach (var responseAnswer in responseAnswers)
            {
                if (responseAnswer.OtherId.HasValue)
                {
                    reply.OtherText = responseAnswer.Text;
                }
                else if (responseAnswer.ChoiceId.HasValue)
                {
                    reply.Choice = question.Answers.ItemLookup[responseAnswer.ChoiceId.Value];
                }

            }
            return reply;
        }

        private MultipleChoiceAnswer MatchMultipleChoiceAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new MultipleChoiceAnswer
            {
                Choices = new List<string>()
            };

            foreach (var responseAnswer in responseAnswers)
            {
                //The API occasionally returns an invalid empty answer like "answers":[{"row":"0"},{"row":"123456789"}]
                //Confirmed by SM as their problem, but need to ignore in the library to avoid a KeyNotFoundException which blows up data processing
                if (responseAnswer.OtherId.HasValue)
                {
                    reply.OtherText = responseAnswer.Text;
                }
                if (responseAnswer.ChoiceId.HasValue && responseAnswer.ChoiceId != 0)
                {
                    reply.Choices.Add(question.Answers.ItemLookup[responseAnswer.ChoiceId.Value]);
                }
            }
            return reply;
        }
        /*
        private OpenEndedSingleAnswer MatchOpenEndedSingleAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new OpenEndedSingleAnswer
            {
                Text = responseAnswers.First().Text
            };

            return reply;
        }

        private OpenEndedMultipleAnswer MatchOpenEndedMultipleAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new OpenEndedMultipleAnswer
            {
                Rows = new List<OpenEndedMultipleAnswerRow>()
            };

            foreach (var responseAnswer in responseAnswers)
            {
                reply.Rows.Add(new OpenEndedMultipleAnswerRow
                {
                    RowName = question.AnswersLookup[responseAnswer.Row].Text,
                    Text = responseAnswer.Text
                });
            }

            return reply;
        }

        private DemographicAnswer MatchDemographicAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new DemographicAnswer();

            foreach (var responseAnswer in responseAnswers)
            {
                var propertyName = question.AnswersLookup[responseAnswer.Row].Type.ToString();
                if (typeof(DemographicAnswer).GetProperty(propertyName, (BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)) != null)
                {
                    typeof(DemographicAnswer).GetProperty(propertyName, (BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)).SetValue(reply, responseAnswer.Text);
                }
            }
            return reply;
        }

        private DateTimeAnswer MatchDateTimeAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new DateTimeAnswer
            {
                Rows = new List<DateTimeAnswerRow>()
            };

            foreach (var responseAnswer in responseAnswers)
            {
                var dateTimeAnswerReply = new DateTimeAnswerRow
                {
                    RowName = question.AnswersLookup[responseAnswer.Row].Text,
                    TimeStamp = DateTime.MinValue
                };

                DateTime timeStamp = DateTime.Parse(responseAnswer.Text, CultureInfo.CreateSpecificCulture("en-US"));
                if (question.Type.Subtype == QuestionSubtype.TimeOnly) //Where only a time is given, use date component from DateTime.MinValue
                {
                    dateTimeAnswerReply.TimeStamp = dateTimeAnswerReply.TimeStamp.AddHours(timeStamp.Hour);
                    dateTimeAnswerReply.TimeStamp = dateTimeAnswerReply.TimeStamp.AddMinutes(timeStamp.Minute);
                }
                else
                {
                    dateTimeAnswerReply.TimeStamp = timeStamp;
                }

                reply.Rows.Add(dateTimeAnswerReply);
            }

            return reply;
        }

        private MatrixMenuAnswer MatchMatrixMenuAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new MatrixMenuAnswer
            {
                Rows = new Dictionary<long, MatrixMenuAnswerRow>()
            };
            Dictionary<long, string> choicesLookup = question.AnswersLookup
                .Where(answerItem => answerItem.Value.Items != null)
                .SelectMany(answerItem => answerItem.Value.Items)
                .ToDictionary(item => item.AnswerId, item => item.Text);

            foreach (var responseAnswer in responseAnswers)
            {
                if (responseAnswer.Row == 0)
                {
                    reply.OtherText = responseAnswer.Text;
                }
                else
                {
                    if (!reply.Rows.ContainsKey(responseAnswer.Row))
                    {
                        reply.Rows.Add(responseAnswer.Row, new MatrixMenuAnswerRow
                        {
                            Columns = new Dictionary<long, MatrixMenuAnswerColumn>()
                        });
                    }
                    if (!reply.Rows[responseAnswer.Row].Columns.ContainsKey(responseAnswer.Col))
                    {
                        reply.Rows[responseAnswer.Row].Columns.Add(responseAnswer.Col, new MatrixMenuAnswerColumn());
                    }

                    reply.Rows[responseAnswer.Row].RowName = question.AnswersLookup[responseAnswer.Row].Text;
                    reply.Rows[responseAnswer.Row].Columns[responseAnswer.Col].ColumnName = question.AnswersLookup[responseAnswer.Col].Text;
                    reply.Rows[responseAnswer.Row].Columns[responseAnswer.Col].Choice = choicesLookup[responseAnswer.ColChoice];
                }
            }

            return reply;
        }

        private MatrixRankingAnswer MatchMatrixRankingAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new MatrixRankingAnswer
            {
                Ranking = new List<Tuple<int, string>>(),
                NotApplicable = new List<string>()
            };

            foreach (var responseAnswer in responseAnswers)
            {
                if (question.AnswersLookup[responseAnswer.Col].Weight == 0)
                {
                    reply.NotApplicable.Add(question.AnswersLookup[responseAnswer.Row].Text);
                }
                else
                {
                    reply.Ranking.Add(
                        new Tuple<int, string>(
                            question.AnswersLookup[responseAnswer.Col].Weight,
                            question.AnswersLookup[responseAnswer.Row].Text
                        )
                    );
                }
            }

            return reply;
        }

        private MatrixRatingAnswer MatchMatrixRatingAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new MatrixRatingAnswer
            {
                Rows = new List<MatrixRatingAnswerRow>()
            };

            foreach (var responseAnswer in responseAnswers)
            {
                if (responseAnswer.Row == 0)
                {
                    reply.OtherText = responseAnswer.Text;
                }
                else
                {
                    var row = new MatrixRatingAnswerRow
                    {
                        RowName = question.AnswersLookup[responseAnswer.Row].Text
                    };

                    if (responseAnswer.Col != 0)
                    {
                        row.Choice = question.AnswersLookup[responseAnswer.Col].Text;
                    }

                    if (!String.IsNullOrEmpty(responseAnswer.Text))
                    {
                        row.OtherText = responseAnswer.Text;
                    }
                    reply.Rows.Add(row);
                }
            }

            return reply;
        }

        private MatrixSingleAnswer MatchMatrixSingleAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new MatrixSingleAnswer
            {
                Rows = new List<MatrixSingleAnswerRow>()
            };

            foreach (var responseAnswer in responseAnswers)
            {
                if (responseAnswer.Row == 0)
                {
                    reply.OtherText = responseAnswer.Text;
                }
                else
                {
                    reply.Rows.Add(new MatrixSingleAnswerRow
                    {
                        RowName = question.AnswersLookup[responseAnswer.Row].Text,
                        Choice = question.AnswersLookup[responseAnswer.Col].Text
                    });
                }
            }

            return reply;
        }

        private MatrixMultiAnswer MatchMatrixMultiAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new MatrixMultiAnswer();

            var rows = new Dictionary<long, MatrixMultiAnswerRow>();

            foreach (var responseAnswer in responseAnswers)
            {
                if (responseAnswer.Row == 0)
                {
                    reply.OtherText = responseAnswer.Text;
                }
                else
                {
                    if (!rows.ContainsKey(responseAnswer.Row))
                    {
                        rows.Add(responseAnswer.Row, new MatrixMultiAnswerRow
                        {
                            RowName = question.AnswersLookup[responseAnswer.Row].Text,
                            Choices = new List<string>()
                        });
                    }
                    rows[responseAnswer.Row].Choices.Add(question.AnswersLookup[responseAnswer.Col].Text);
                }
            }

            reply.Rows = rows.Values.ToList();

            return reply;
        }*/
    }
}