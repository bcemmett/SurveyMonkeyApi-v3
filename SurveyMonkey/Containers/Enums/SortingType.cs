using Newtonsoft.Json;

namespace SurveyMonkey.Containers.Enums
{
    [JsonConverter(typeof(LaxEnumJsonConverter))]
    public enum SortingType
    {
        Default,
        TextAsc,
        TextDesc,
        RespCountAsc,
        RespCountDesc,
        Random,
        Flip
    }
}