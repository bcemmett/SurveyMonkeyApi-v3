using Newtonsoft.Json;
using SurveyMonkey.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Range
    {
        public int? Min { get; set; }
        public int? Max { get; set; }
        public string Message { get; set; }
    }
}
