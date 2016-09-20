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
                        /*case QuestionSubtype.Ranking:
                            return MatchMatrixRankingAnswer(question, responseAnswers);
                        case QuestionSubtype.Rating:
                            return MatchMatrixRatingAnswer(question, responseAnswers);
                        case QuestionSubtype.Single:
                            return MatchMatrixSingleAnswer(question, responseAnswers);
                        case QuestionSubtype.Multi:
                            return MatchMatrixMultiAnswer(question, responseAnswers);*/
                    }
                    break;
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

        private OpenEndedSingleAnswer MatchOpenEndedSingleAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new OpenEndedSingleAnswer
            {
                Text = responseAnswers.FirstOrDefault()?.Text
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
                if (responseAnswer.RowId.HasValue)
                {
                    reply.Rows.Add(new OpenEndedMultipleAnswerRow
                    {
                        RowName = question.Answers.ItemLookup[responseAnswer.RowId.Value],
                        Text = responseAnswer.Text
                    });
                }
            }

            return reply;
        }

        private DemographicAnswer MatchDemographicAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new DemographicAnswer();

            //todo it's a shame to unnecessarily re-generate this dictionary for each response. Could perhaps cache it as per the general ItemLookup
            Dictionary<long, string> typeLookup = question.Answers.Rows.Where(r => r.Id.HasValue).ToDictionary(r => r.Id.Value, r => r.Type);
            foreach (var responseAnswer in responseAnswers)
            {
                if (responseAnswer.RowId.HasValue)
                {
                    string propertyName = typeLookup[responseAnswer.RowId.Value];

                    PropertyInfo property = typeof(DemographicAnswer).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (property != null)
                    {
                        property.SetValue(reply, responseAnswer.Text);
                    }
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
                if (responseAnswer.RowId.HasValue)
                {
                    var dateTimeAnswerReply = new DateTimeAnswerRow
                    {
                        RowName = question.Answers.ItemLookup[responseAnswer.RowId.Value],
                        TimeStamp = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc)
                    };

                    DateTime timeStamp = DateTime.Parse(responseAnswer.Text, CultureInfo.CreateSpecificCulture("en-US"));
                    DateTime utcTimeStamp = DateTime.SpecifyKind(timeStamp, DateTimeKind.Utc);
                    if (question.Subtype == QuestionSubtype.TimeOnly) //Where only a time is given, use date component from DateTime.MinValue
                    {
                        dateTimeAnswerReply.TimeStamp = dateTimeAnswerReply.TimeStamp.AddHours(utcTimeStamp.Hour);
                        dateTimeAnswerReply.TimeStamp = dateTimeAnswerReply.TimeStamp.AddMinutes(utcTimeStamp.Minute);
                    }
                    else
                    {
                        dateTimeAnswerReply.TimeStamp = utcTimeStamp;
                    }
                    reply.Rows.Add(dateTimeAnswerReply);
                }
            }

            return reply;
        }

        private MatrixMenuAnswer MatchMatrixMenuAnswer(Question question, IEnumerable<ResponseAnswer> responseAnswers)
        {
            var reply = new MatrixMenuAnswer
            {
                Rows = new Dictionary<string, MatrixMenuAnswerRow>()
            };
            
            Dictionary<long, string> choicesLookup = question.Answers.Cols //todo would ideally build + cache this once, but profile to see if it matters
                .Where(answerItem => answerItem.Choices != null)
                .SelectMany(a => a.Choices)
                .ToDictionary(item => item.Id.Value, item => item.Text);

            foreach (var responseAnswer in responseAnswers)
            {
                if (!String.IsNullOrEmpty(responseAnswer.Text))
                {
                    reply.OtherText = responseAnswer.Text;
                }
                else if (responseAnswer.ChoiceId.HasValue)
                {
                    if (!reply.Rows.ContainsKey(question.Answers.ItemLookup[responseAnswer.RowId.Value]))
                    {
                        reply.Rows.Add(question.Answers.ItemLookup[responseAnswer.RowId.Value], new MatrixMenuAnswerRow {Columns = new Dictionary<string, string>()});
                    }
                    reply.Rows[question.Answers.ItemLookup[responseAnswer.RowId.Value]].Columns.Add(question.Answers.ItemLookup[responseAnswer.ColId.Value], choicesLookup[responseAnswer.ChoiceId.Value]);
                }
            }
            
            return reply;
        }
/*
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