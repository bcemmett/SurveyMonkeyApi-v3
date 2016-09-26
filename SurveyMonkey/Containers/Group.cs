using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Group : IPageableContainer
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
    }
}