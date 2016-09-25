using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum MessageStatus
    {
        Sent,
        NotSent,
        Processing
    }
}