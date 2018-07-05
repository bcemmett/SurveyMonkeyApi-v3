using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class DefaultQuestionFeedback
    {
        public string CorrectText { get; set; }
        public string IncorrectText { get; set; }
        public string PartialText { get; set; }
    }
}
