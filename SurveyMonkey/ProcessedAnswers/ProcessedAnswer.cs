using SurveyMonkey.Enums;

namespace SurveyMonkey.ProcessedAnswers
{
    public class ProcessedAnswer
    {
        public string QuestionHeading { get; set; }
        public QuestionFamily? QuestionFamily { get; set; }
        public QuestionSubtype? QuestionSubtype { get; set; }
        public IProcessedResponse Response { get; set; }
    }
}