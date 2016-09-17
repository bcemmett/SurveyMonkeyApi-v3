using System;

namespace SurveyMonkey.RequestSettings
{
    public class GetSurveyListSettings : PagingSettings
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

        public SortByOption? SortBy { get; set; }
        public SortOrderOption? SortOrder { get; set; }
        public DateTime? StartModifiedAt { get; set; }
        public DateTime? EndModifiedAt { get; set; }
    }
}