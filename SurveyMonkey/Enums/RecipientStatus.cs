using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum RecipientStatus
    {
        Reminder,
        ThankYou
    }
}