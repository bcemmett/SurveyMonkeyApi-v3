using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum ShowResultsType
    {
        Disabled,
        ResultsOnly,
        ResultsAndAnswers
    }
}
