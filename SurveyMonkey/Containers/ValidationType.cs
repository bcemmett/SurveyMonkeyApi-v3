using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxEnumJsonConverter))]
    public enum ValidationType
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