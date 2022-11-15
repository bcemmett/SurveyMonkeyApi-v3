using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Button
    {
        public string BgColor { get; set; }
        public string Text { get; set; }
        public string TextColor { get; set; }
    }
}