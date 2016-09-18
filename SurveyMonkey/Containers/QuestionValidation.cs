using Newtonsoft.Json;
using SurveyMonkey.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class QuestionValidation
    {
        public QuestionValidationType? Type { get; set; }
        public string Text { get; set; }
        public object Min { get; set; }
        public object Max { get; set; }
        public int? Sum { get; set; }
        public string SumText { get; set; }
    }
}