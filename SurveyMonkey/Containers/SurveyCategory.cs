using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class SurveyCategory : IPageable
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}