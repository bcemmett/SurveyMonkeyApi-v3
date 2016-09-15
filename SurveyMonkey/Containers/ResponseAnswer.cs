using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class ResponseAnswer
    {
        public long? ChoiceId { get; set; }
        public long? RowId { get; set; }
        public long? ColId { get; set; }
        public long? OtherId { get; set; }
        public string Text { get; set; }
    }
}