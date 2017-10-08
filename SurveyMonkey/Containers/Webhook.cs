using System.Collections.Generic;
using Newtonsoft.Json;
using SurveyMonkey.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Webhook : IPageableContainer
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public WebhookEventType? EventType { get; set; }
        public WebhookObjectType? ObjectType { get; set; }
        public List<long> ObjectIds { get; set; }
        public string SubscriptionUrl { get; set; }
        internal string Href { get; set; }
    }
}