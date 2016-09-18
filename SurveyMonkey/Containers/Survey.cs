using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Survey
    {
        public long? Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public int? PageCount { get; set; }
        public int? QuestionCount { get; set; }
        public int? ResponseCount { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public ButtonsText ButtonsText { get; set; }
        public string Href { get; set; }
        public string Preview { get; set; }
        public string EditUrl { get; set; }
        public string CollectUrl { get; set; }
        public string AnalyzeUrl { get; set; }
        public string SummaryUrl { get; set; }
        public Dictionary<string, string> CustomVariables { get; set; }
        public List<Page> Pages { get; set; }
    }
}