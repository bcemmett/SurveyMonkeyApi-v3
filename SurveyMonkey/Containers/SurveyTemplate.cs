using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class SurveyTemplate : IPageable
    {
        public string Name { get; set; }
        public long? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool? Available { get; set; }
        public int? NumQuestions { get; set; }
        public string PreviewLink { get; set; }
    }
}