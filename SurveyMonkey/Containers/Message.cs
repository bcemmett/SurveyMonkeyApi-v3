using System;
using Newtonsoft.Json;
using SurveyMonkey.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Message
    {
        public long? Id { get; set; }
        public string Href { get; set; }
        public MessageStatus? Status { get; set; }
        public MessageType? Type { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsBrandingEnabled { get; set; }
        public bool? IsScheduled { get; set; }
        public RecipientStatus? RecipientStatus { get; set; }
        public DateTime? ScheduledDate { get; set; }
    }
}