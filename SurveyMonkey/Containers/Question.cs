using System.Collections.Generic;
using Newtonsoft.Json;
using SurveyMonkey.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Question : IPageableContainer
    {
        public long? Id { get; set; }
        public QuestionFamily? Family { get; set; }
        public QuestionSubtype? Subtype { get; set; }
        public int? Position { get; set; }
        public QuestionRequired Required { get; set; }
        public QuestionSorting Sorting { get; set; }
        public QuestionValidation Validation { get; set; }
        public List<Headings> Headings { get; set; }
        public string Heading { get; set; } //todo it's a bit messy that GetSurveyDetails populates the list put GetQuestionDetails populates just the string
        public bool? Visible { get; set; }
        public bool? ForcedRanking { get; set; }
        internal string Href { get; set; }
        public QuestionAnswers Answers { get; set; }
        public string Nickname { get; set; }
        public QuestionDisplayOptions DisplayOptions { get; set; }
        public QuizOptions QuizOptions { get; set; }
        [JsonIgnore]
        internal object Layout { get; set; }
    }
}