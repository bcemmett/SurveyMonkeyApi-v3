using Newtonsoft.Json;
using SurveyMonkey.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class QuizOptions
    {
        public bool? ScoringEnabled { get; set; }
        public Feedback Feedback { get; set; }
        public int? Score { get; set; }
        public ShowResultsType? ShowResultsType { get; set; }
        public bool? IsQuizMode { get; set; }
        public DefaultQuestionFeedback DefaultQuestionFeedback {get; set;}
    }
}
