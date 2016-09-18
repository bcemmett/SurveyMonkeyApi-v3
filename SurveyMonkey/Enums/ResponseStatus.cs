using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum ResponseStatus
    {
        Completed,
        Partial,
        Overquota,
        Disqualified
    }
}