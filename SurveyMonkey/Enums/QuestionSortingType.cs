using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
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