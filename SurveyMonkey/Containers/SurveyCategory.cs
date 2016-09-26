using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class SurveyCategory : IPageableContainer
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}