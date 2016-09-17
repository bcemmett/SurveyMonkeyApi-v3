using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class Collector
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
    }
}