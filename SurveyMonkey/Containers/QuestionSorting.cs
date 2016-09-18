using Newtonsoft.Json;
using SurveyMonkey.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class QuestionSorting
    {
        public QuestionSortingType? Type { get; set; }
        public bool? IgnoreLast { get; set; }
    }
}