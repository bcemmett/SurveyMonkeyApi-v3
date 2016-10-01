using System;
using Newtonsoft.Json;
using SurveyMonkey.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Member : IPageableContainer
    {
        public long? Id { get; set; }
        public string Username { get; set; }
        internal string Href { get; set; }
        public string Email { get; set; }
        public MemberType? Type { get; set; }
        public MemberStatus? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserId { get; set; } //todo unclear if this is guaranteed to be numeric
    }
}