using System.Collections.Generic;
using Newtonsoft.Json;
using SurveyMonkey.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Recipient : IPageableContainer
    {
        public long? Id { get; set; }
        public long? SurveyId { get; set; }
        internal string Href { get; set; }
        public string Email { get; set; }
        public RecipientSurveyResponseStatus? SurveyResponseStatus { get; set; }
        public MessageStatus? MailStatus { get; set; }
        public Dictionary<string, string> CustomFields { get; set; }
        public string SurveyLink { get; set; }
        public string RemoveLink { get; set; }
        public Dictionary<string, string> ExtraFields { get; set; }
    }
}