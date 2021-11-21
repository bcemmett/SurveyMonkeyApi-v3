using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SurveyMonkey.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Response : IPageableContainer
    {
        public long? Id { get; set; }
        public long? SurveyId { get; set; }
        public long? CollectorId { get; set; }
        internal string Href { get; set; }
        public Dictionary<string, string> CustomVariables { get; set; }
        public int? TotalTime { get; set; }
        public string CustomValue { get; set; }
        public string EditUrl { get; set; }
        public string AnalyzeUrl { get; set; }
        public Dictionary<string, object> LogicPath { get; set; } //TODO this structure isn't documented
        public List<object> PagePath { get; set; } //TODO this structure isn't documented
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public ResponseStatus? ResponseStatus { get; set; }
        public CollectionMode? CollectionMode { get; set; }
        public string IpAddress { get; set; }
        public long? RecipientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /*
         * SurveyMonkey's docs say that for an email collector, responses will have a property "email". I've never
         * seen that property exist. There does appear to always be a property email_address, which has always
         * been an empty string when retrieving responses in bulk, and is populated when retrieving an individual response.
         * Finally, they have an undocumented metadata object, with a contact which has key value pair properties for
         * first_name, last_name, custom_value, and email when retriving responses in bulk, and the same when
         * retrieving an individual response but with the email property bring populated. The first_name, last_name,
         * and custom_value properties also exist duplicated as root properties on the response object. Suspect
         * that their intention was to do that with email too, and that they've misnamed the root email property
         * as email_address in a way which prevents the data being populated correctly but still makes it appear as
         * an empty string.
         *
         * Approach will be to internally deserialise each of the 3x places they could theoretically present the email
         * address, and then the public api surface exposes only reading from EmailAddress, a property that looks at each and
         * presents the first non-null non-empty one it finds, if at all. Some tests for this in GetResponseTests, and this
         * can all be removed if SurveyMonkey fix the error & expose the email as documented.
         *
         * Metadata's structure is undocumented, so some danger that if there are other contexts in which it's also used
         * but with a different structure, it could break the deserialiser.
         */
        internal ResponseMetadata Metadata { get; set; } // this structure isn't documented
        [JsonProperty("email")]
        internal string EmailFromDirectReferenceToEmail { get; set; }
        [JsonProperty("email_address")]
        internal string EmailFromDirectReferenceToEmailAddress { get; set; }
        [JsonIgnore]
        public string EmailAddress =>
            !String.IsNullOrWhiteSpace(EmailFromDirectReferenceToEmail) ? EmailFromDirectReferenceToEmail :
                !String.IsNullOrWhiteSpace(EmailFromDirectReferenceToEmailAddress) ? EmailFromDirectReferenceToEmailAddress :
                    Metadata?.GetValueByKeyOrNull("email");

        public List<ResponsePage> Pages { get; set; }
        public QuizResults QuizResults { get; set; }

        public List<ResponseQuestion> Questions
        {
            get { return Pages?.SelectMany(page => page.Questions).ToList(); }
        }
    }
}