using Newtonsoft.Json;

namespace SurveyMonkey.Containers.Enums
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