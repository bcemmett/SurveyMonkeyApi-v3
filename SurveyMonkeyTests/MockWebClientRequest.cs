using System.Collections.Specialized;
using System.Net;

namespace SurveyMonkeyTests
{
    class MockWebClientRequest
    {
        public WebHeaderCollection Headers { get; set; }
        public NameValueCollection QueryString { get; set; }
        public string Url { get; set; }
        public string Verb { get; set; }
        public string Body { get; set; }
    }
}