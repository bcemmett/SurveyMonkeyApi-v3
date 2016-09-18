using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class OtherAnswer
    {
        public long? Id { get; set; }
        public int? Position { get; set; }
        public string Text { get; set; }
        public bool? Visible { get; set; }
        public bool? ApplyAllRows { get; set; }
        public string ErrorText { get; set; }
        public bool? IsAnswerChoice { get; set; }
        public int? NumChars { get; set; }
        public int? NumLines { get; set; }
    }
}