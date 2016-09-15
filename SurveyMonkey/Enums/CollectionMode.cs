using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(LaxEnumJsonConverter))]
    public enum CollectionMode
    {
        Default,
        Preview,
        DataEntry,
        SurveyPreview,
        Edit
    }
}