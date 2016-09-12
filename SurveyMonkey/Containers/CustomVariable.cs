using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class CustomVariable
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}