using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum MessageType
    {
        Invite,
        Reminder,
        ThankYou
    }
}