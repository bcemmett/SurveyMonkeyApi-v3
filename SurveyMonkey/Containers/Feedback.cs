using Newtonsoft.Json;
using SurveyMonkey.Enums;
using System.Collections.Generic;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Feedback
    {
        public string CorrectText { get; set; }
        public string IncorrectText { get; set; }
        public string PartialText { get; set; }
        public List<Range> Ranges { get; set; }
        public RangesType? RangesType { get; set; }
    }
}
