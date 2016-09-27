using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Column
    {
        public long? Id { get; set; }
        public int? Position { get; set; }
        public string Text { get; set; }
        public bool? Visible { get; set; }
        public List<Choice> Choices { get; set; }
        public string Description { get; set; }
        public bool? IsNa { get; set; }
    }
}