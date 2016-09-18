using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Row
    {
        public int? Position { get; set; }
        public string Text { get; set; }
        public bool? Visible { get; set; }
        public bool? Required { get; set; }
        public string Type { get; set; }
    }
}