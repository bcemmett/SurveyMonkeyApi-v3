using System;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class PrimaryButton
    {
        public string BgColor { get; set; }
        internal string Text { get; set; }
        public string TextColor { get; set; }
    }
}
