using Newtonsoft.Json;
using SurveyMonkey.Containers.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class Validation
    {
        public ValidationType Type { get; set; }
        public string Text { get; set; }
        public object Min { get; set; }
        public object Max { get; set; }
        public int Sum { get; set; }
        public string SumText { get; set; }
    }
}