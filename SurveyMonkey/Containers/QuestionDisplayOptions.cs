using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class QuestionDisplayOptions
    {
        public string MiddleLabel { get; set; }
        public bool? ShowDisplayNumber { get; set; }
        public string DisplaySubtype { get; set; }
        public string RightLabel { get; set; }
        public string DisplayType { get; set; }
        public string LeftLabel { get; set; }
        public QuestionDisplayOptionsCustomOptions CustomOptions { get; set; }
    }
}