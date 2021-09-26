using System;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Collector : IPageableContainer
    {
        [JsonConverter(typeof(TolerantJsonConverter))]
        public enum StatusType
        {
            New,
            Open,
            Closed
        }

        [JsonConverter(typeof(TolerantJsonConverter))]
        public enum CollectorType
        {
            Weblink,
            Email,
            Sms,
            PopupInvitation,
            PopupSurvey,
            EmbeddedSurvey
        }

        [JsonConverter(typeof(TolerantJsonConverter))]
        public enum EditResponseOption
        {
            UntilComplete,
            Never,
            Always
        }

        [JsonConverter(typeof(TolerantJsonConverter))]
        public enum AnonymousOption
        {
            NotAnonymous,
            PartiallyAnonymous,
            FullyAnonymous
        }

        [JsonConverter(typeof(TolerantJsonConverter))]
        public enum RedirectOption
        {
            Url,
            Close,
            Loop
        }

        public StatusType? Status { get; set; }
        public long? Id { get; set; }
        public string Name { get; set; }
        public CollectorType? Type { get; set; }
        public string ThankYouMessage { get; set; }
        public string DisqualificationMessage { get; set; }
        public string DisqualificationUrl { get; set; }
        public string DisqualificationType { get; set; }
        public DateTime? CloseDate { get; set; }
        public string ClosedPageMessage { get; set; }
        public string RedirectUrl { get; set; }
        public bool? DisplaySurveyResults { get; set; }
        public EditResponseOption? EditResponseType { get; set; }
        public AnonymousOption? AnonymousType { get; set; }
        public RedirectOption? RedirectType { get; set; }
        public int? ResponseLimit { get; set; }
        public bool? AllowMultipleResponses { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Url { get; set; }
        public bool? PasswordEnabled { get; set; }
        public string SenderEmail { get; set; }
        internal string Href { get; set; }
        public int? ResponseCount { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string BorderColor { get; set; }
        public bool? IsBrandingEnabled { get; set; }
        public string Headline { get; set; }
        public string Message { get; set; }
        public int? SampleRate { get; set; }
        public Button PrimaryButton { get; set; }
        public Button SecondaryButton { get; set; }
        public long? SurveyId { get; set; }
    }
}