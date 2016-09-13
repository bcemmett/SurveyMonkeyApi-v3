using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class Sorting
    {
        public SortingType Type { get; set; }
        public bool IgnoreLast { get; set; }
    }
}