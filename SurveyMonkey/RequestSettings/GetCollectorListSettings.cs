using System;
using SurveyMonkey.Enums;

namespace SurveyMonkey.RequestSettings
{
    public class GetCollectorListSettings : PagingSettings
    {
        public enum SortOrderOption
        {
            ASC,
            DESC
        }

        public enum SortByOption
        {
            Id,
            DateModified,
            Type,
            Status,
            Name
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public SortOrderOption? SortOrder { get; set; }
        public SortByOption? SortBy { get; set; }
        public string Name { get; set; }
    }
}