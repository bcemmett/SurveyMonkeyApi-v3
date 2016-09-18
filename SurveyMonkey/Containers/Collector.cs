using System;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class Collector
    {
        [JsonConverter(typeof(LaxEnumJsonConverter))]
        public enum StatusType
        {
            Open,
            Closed
        }

        [JsonConverter(typeof(LaxEnumJsonConverter))]
        public enum CollectorType
        {
            Weblink,
            Email
        }

        [JsonConverter(typeof(LaxEnumJsonConverter))]
        public enum EditResponseOption
        {
            UntilComplete,
            Never,
            Always
        }

        [JsonConverter(typeof(LaxEnumJsonConverter))]
        public enum AnonymousOption
        {
            NotAnonymous,
            PartiallyAnonymous,
            FullyAnonymous
        }

        public StatusType? Status { get; set; }
        public long? Id { get; set; }
        public string Name { get; set; }
        public CollectorType? Type { get; set; }
        public string ThankYouMessage { get; set; }
        public string DisqualificationMessage { get; set; }
        public DateTime? CloseDate { get; set; }
        public string ClosedPageMessage { get; set; }
        public string RedirectUrl { get; set; }
        public bool? DisplaySurveyResults { get; set; }
        public EditResponseOption? EditResponseType { get; set; }
        public AnonymousOption? AnonymousType { get; set; }
        public bool? AllowMultipleResponses { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Url { get; set; }
        public bool? PasswordEnabled { get; set; }
        public string SenderEmail { get; set; }
        public string Href { get; set; }
        public int? ResponseCount { get; set; }
    }
}