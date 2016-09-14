using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(LaxEnumJsonConverter))]
    public enum QuestionSortingType
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