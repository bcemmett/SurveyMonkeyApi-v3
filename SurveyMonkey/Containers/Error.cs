using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    internal class Error
    {
        public string Docs { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int? HttpStatusCode { get; set; }
    }
}