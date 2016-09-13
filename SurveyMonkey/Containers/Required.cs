using Newtonsoft.Json;
using SurveyMonkey.Containers.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class Required
    {
        public string Text { get; set; }
        public RequiredType Type { get; set; }
        public string Amount { get; set; }
    }
}