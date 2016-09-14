using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(LaxEnumJsonConverter))]
    public enum QuestionRequiredType
    {
        All,
        AtLeast,
        AtMost,
        Exactly,
        Range
    }
}