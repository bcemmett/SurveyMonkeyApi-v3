using System;
using SurveyMonkey.Enums;

namespace SurveyMonkey.RequestSettings
{
    public class GetCollectorListSettings : IPagingSettings
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

        public int? Page { get; set; }
        public int? PerPage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public SortOrderOption? SortOrder { get; set; }
        public SortByOption? SortBy { get; set; }
        public string Name { get; set; }
    }
}