using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum QuestionRequiredType
    {
        All,
        AtLeast,
        AtMost,
        Exactly,
        Range
    }
}