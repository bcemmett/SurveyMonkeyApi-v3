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
    }
}