using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum RecipientSurveyResponseStatus
    {
        NotResponded,
        PartiallyResponded,
        CompletelyResponded
    }
}