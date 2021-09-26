using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum WebhookEventType
    {
        ResponseCompleted,
        ResponseUpdated,
        ResponseDisqualified,
        ResponseCreated,
        ResponseDeleted,
        ResponseOverquota,
        CollectorCreated,
        CollectorUpdated,
        CollectorDeleted,
        SurveyCreated,
        SurveyUpdated,
        SurveyDeleted
    }
}