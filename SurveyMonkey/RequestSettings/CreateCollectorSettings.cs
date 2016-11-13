using System;
using SurveyMonkey.Containers;

namespace SurveyMonkey.RequestSettings
{
    public class CreateCollectorSettings
    {
        public Collector.CollectorType Type { get; set; }
        public string Name { get; set; }
        public string ThankYouMessage { get; set; }
        public string DisqualificationMessage { get; set; }
        public DateTime? CloseDate { get; set; }
        public string ClosedPageMessage { get; set; }
        public string RedirectUrl { get; set; }
        public bool? DisplaySurveyResults { get; set; }
        public Collector.EditResponseOption? EditResponseType { get; set; }
        public Collector.AnonymousOption? AnonymousType { get; set; }
        public bool? AllowMultipleResponses { get; set; }
        public string Password { get; set; }
        public string SenderEmail { get; set; }
    }
}