using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class User
    {
        public long? Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Language { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
        public bool? EmailVerified { get; set; }
        public Scopes Scopes { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastLogin { get; set; }
        internal string Href { get; set; }
        [JsonIgnore]
        internal object QuestionTypes { get; set; }
        [JsonIgnore]
        internal object SsoConnections { get; set; }
    }
}