using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class SurveyCategory
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}