using System;
using SurveyMonkey.Enums;

namespace SurveyMonkey.RequestSettings
{
    public class GetSurveyListSettings
    {
        public int? Page { get; set; }
        public int? PerPage { get; set; }
        public GetSurveyListSortBy? SortBy { get; set; }
        public SortOrder? SortOrder { get; set; }
        public DateTime? StartModifiedAt { get; set; }
        public DateTime? EndModifiedAt { get; set; }
    }
}