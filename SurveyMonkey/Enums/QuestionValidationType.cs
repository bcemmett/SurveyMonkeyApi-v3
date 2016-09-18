using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum QuestionValidationType
    {
        Any,
        Integer,
        Decimal,
        DateUs,
        DateIntl,
        Regex,
        Email,
        TextLength
    }
}