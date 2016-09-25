using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Message
    {
        public long Id { get; set; }
        public string Href { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}