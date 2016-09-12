using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class ButtonsText
    {
        public string DoneButton { get; set; }
        public string PrevButton { get; set; }
        public string ExitButton { get; set; }
        public string NextButton { get; set; }
    }
}