using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class Choice
    {
        public long Id { get; set; }
        public int Position { get; set; }
        public string Text { get; set; }
        public int Weight { get; set; }
        public bool Visible { get; set; }
    }
}