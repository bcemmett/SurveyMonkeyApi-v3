using Newtonsoft.Json;

namespace SurveyMonkey
{
    [JsonConverter(typeof(LaxEnumJsonConverter))]
    public enum QuestionSubtype
    {
        NotSet = 0,
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
