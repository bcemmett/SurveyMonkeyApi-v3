using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class RandomAssignment
    {
        public int? Percent { get; set; }
        public int? Position { get; set; }
        public string VariableName { get; set; }
    }
}