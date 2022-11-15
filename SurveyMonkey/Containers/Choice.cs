using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Choice
    {
        public long? Id { get; set; }
        public int? Position { get; set; }
        public string Text { get; set; }
        public int? Weight { get; set; }
        public bool? Visible { get; set; }
        public string Description { get; set; }
        public bool? IsNa { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public List<object> Items { get; set; }
        public string ResourceUrl { get; set; }
        public QuizOptions QuizOptions { get; set; }
    }
}