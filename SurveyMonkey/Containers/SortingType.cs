using Newtonsoft.Json;

namespace SurveyMonkey.Containers
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