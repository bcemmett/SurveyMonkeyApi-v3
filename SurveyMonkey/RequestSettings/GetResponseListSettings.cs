using System;
using SurveyMonkey.Enums;

namespace SurveyMonkey.RequestSettings
{
    public class GetResponseListSettings
    {
        public enum TotalTimeUnitsOption
        {
            Second,
            Minute,
            Hour
        }

        public enum SortOrderOption
        {
            ASC,
            DESC
        }

        public enum SortByOption
        {
            DateModified
        }

        public int? Page { get; set; }
        public int? PerPage { get; set; }
        public string CollectorIds { get; set; }
        public DateTime? StartCreatedAt { get; set; }
        public DateTime? EndCreatedAt { get; set; }
        public DateTime? StartModifiedAt { get; set; }
        public DateTime? EndModifiedAt { get; set; }
        public ResponseStatus? Status { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ip { get; set; }
        public string Custom { get; set; }
        public int? TotalTimeMax { get; set; }
        public int? TotalTimeMin { get; set; }
        public TotalTimeUnitsOption? TotalTimeUnits { get; set; }
        public SortOrderOption? SortOrder { get; set; }
        public SortByOption? SortBy { get; set; }
    }
}