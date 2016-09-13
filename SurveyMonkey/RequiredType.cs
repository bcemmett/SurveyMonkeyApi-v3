using Newtonsoft.Json;

namespace SurveyMonkey
{
    [JsonConverter(typeof(LaxEnumJsonConverter))]
    public enum RequiredType
    {
        All,
        AtLeast,
        AtMost,
        Exactly,
        Range
    }
}