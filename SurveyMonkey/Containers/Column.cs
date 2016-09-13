using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class Column
    {
        public string Text { get; set; }
        public List<Choice> Choices { get; set; }
    }
}
