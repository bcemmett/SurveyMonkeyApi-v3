using System;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Group : IPageableContainer
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        internal string Href { get; set; }
        public int? MemberCount { get; set; }
        public int? MaxInvites { get; set; }
        public DateTime? DateCreated { get; set; }
        public string OwnerEmail { get; set; }
    }
}