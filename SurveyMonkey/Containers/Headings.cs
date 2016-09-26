using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Headings
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public Image Image { get; set; }
        public RandomAssignment RandomAssignment { get; set; }
    }
}