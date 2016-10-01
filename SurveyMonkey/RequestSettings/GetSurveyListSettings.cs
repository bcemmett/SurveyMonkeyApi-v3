using System;

namespace SurveyMonkey.RequestSettings
{
    public class GetSurveyListSettings : IPagingSettings
    {
        public enum SortByOption
        {
            Title,
            DateModified,
            NumResponses
        }

        public enum SortOrderOption
        {
            ASC,
            DESC
        }

        public int? Page { get; set; }
        public int? PerPage { get; set; }
        public SortByOption? SortBy { get; set; }
        public SortOrderOption? SortOrder { get; set; }
        public DateTime? StartModifiedAt { get; set; }
        public DateTime? EndModifiedAt { get; set; }
    }
}