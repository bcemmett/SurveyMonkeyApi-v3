using System;
using System.Collections.Generic;

namespace SurveyMonkey.Containers
{
    public class Survey
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public int QuestionCount { get; set; }
        public int PageCount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public long Id { get; set; }
        public string Href { get; set; }
        public ButtonsText ButtonsText { get; set; }
        public List<CustomVariable> CustomVariables { get; set; }
        public string Preview { get; set; }
        public string EditUrl { get; set; }
        public string CollectUrl { get; set; }
        public string AnalyzeUrl { get; set; }
        public string SummaryUrl { get; set; }
    }
}