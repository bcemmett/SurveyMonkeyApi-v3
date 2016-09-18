using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum CollectionMode
    {
        Default,
        Preview,
        DataEntry,
        SurveyPreview,
        Edit
    }
}