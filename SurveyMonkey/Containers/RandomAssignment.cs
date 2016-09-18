using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class RandomAssignment
    {
        public int? Percent { get; set; }
        public int? Position { get; set; }
        public string VariableName { get; set; }
    }
}