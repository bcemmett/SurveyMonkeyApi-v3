using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(LaxEnumJsonConverter))]
    public enum ResponseStatus
    {
        Completed,
        Partial,
        Overquota,
        Disqualified
    }
}