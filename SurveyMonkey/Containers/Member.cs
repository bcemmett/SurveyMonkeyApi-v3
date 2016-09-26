using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Member : IPageableContainer
    {
        public long? Id { get; set; }
        public string Username { get; set; }
        public string Href { get; set; }
    }
}