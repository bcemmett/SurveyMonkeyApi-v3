using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum QuestionSubtype
    {
        Menu,
        Vertical,
        VerticalTwoCol,
        VerticalThreeCol,
        Horiz,
        Ranking,
        Rating,
        Single,
        Multi,
        Essay,
        International,
        US,
        Both,
        DateOnly,
        TimeOnly,
        DescriptiveText,
        Image,
        Numerical,
        CustomVariable
    }
}