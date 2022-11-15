using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class ChoiceMetadata
    {
        // Api docs say this is a string rather than an integer - probably an error but treating it as a string here just in case there's some scenario it can be
        public string Weight { get; set; }
    }
}