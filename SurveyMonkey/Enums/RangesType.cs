using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum RangesType
    {
        Percentage,
        Points,
        Disabled
    }
}
