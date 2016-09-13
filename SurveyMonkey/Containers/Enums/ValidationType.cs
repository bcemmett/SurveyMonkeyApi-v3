using Newtonsoft.Json;

namespace SurveyMonkey.Containers.Enums
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