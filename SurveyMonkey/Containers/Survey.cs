using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Survey : IPageableContainer
    {
        public long? Id { get; set; }
        public string Title { get; set; }
        public string Nickname { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public bool? IsOwner { get; set; }
        public int? PageCount { get; set; }
        public int? QuestionCount { get; set; }
        public int? ResponseCount { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public ButtonsText ButtonsText { get; set; }
        internal string Href { get; set; }
        public long? FolderId { get; set; }
        public string Preview { get; set; }
        public string EditUrl { get; set; }
        public string CollectUrl { get; set; }
        public string AnalyzeUrl { get; set; }
        public string SummaryUrl { get; set; }
        public bool? Footer { get; set; }
        public Dictionary<string, string> CustomVariables { get; set; }
        public List<Page> Pages { get; set; }
        public List<Collector> Collectors { get; set; }
        public List<Response> Responses { get; set; }
        public QuizOptions QuizOptions { get; set; }

        public List<Question> Questions {
            get { return Pages?.SelectMany(page => page.Questions).ToList(); }
        }

        internal Dictionary<long, Question> QuestionsLookup { get; set; }
    }
}