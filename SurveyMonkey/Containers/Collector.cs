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
            Weblink,            // Get Web Link - Share a web link via email, on your website, or post to social media. You can also schedule a recurring web link.
            Email,              // Send by Email - Create custom email invitations and track who responds. Send follow up reminders to those who haven’t responded.
            Audience,           // Buy Responses - Get real-time feedback from our panel of global respondents. See results in minutes.
            EmbeddedSurvey,     // Embed on Website - Embed your survey on your website or a link to your survey in a popup window.
            Popup,              // Embed on Website - Embed your survey on your website or a link to your survey in a popup window.
            PopupInvitation,    // Embed on Website - Embed your survey on your website or a link to your survey in a popup window.
            Facebook,           // Post on Social Media - Post your survey on Facebook, LinkedIn, or Twitter.
            FacebookMessenger,  // Share in Messenger - Let others to take your survey directly in Facebook Messenger.
            MobileSdk,          // MOBILE SDK - Integrate SurveyMonkey surveys and responses directly into your mobile app to collect user feedback about their in-app experience.
            OfflineKiosk        // Kiosk Survey - Turn your device into a survey station and collect responses anywhere you go.
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

        //Embedded Web Collector
        public string BorderColor { get; set; }
        public string Headline { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public PrimaryButton PrimaryButton { get; set; }
        public SecondaryButton SecondaryButton { get; set; }
        public string Message { get; set; }
        public bool? IsBrandingEnabled { get; set; }
        public int? SampleRate { get; set; }

        //Mobile SDK
        public string Hash { get; set; }
        
    }
}