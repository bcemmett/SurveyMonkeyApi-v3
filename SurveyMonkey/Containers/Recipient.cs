using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Recipient : IPageable
    {
        public long? Id { get; set; }
        public string Href { get; set; }
        public string Email { get; set; }
    }
}