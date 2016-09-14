using System.Collections.Generic;
using Newtonsoft.Json;
using SurveyMonkey.Containers.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class Question
    {
        public long Id { get; set; }
        public QuestionFamily Family { get; set; }
        public QuestionSubtype Subtype { get; set; }
        public int Position { get; set; }
        public Required Required { get; set; }
        public Sorting Sorting { get; set; }
        public Validation Validation { get; set; }
        public List<Headings> Headings { get; set; }
        public bool Visible { get; set; }
        public bool ForcedRanking { get; set; }
        public string Href { get; set; }
        public QuestionAnswers Answers { get; set; }
    }
}