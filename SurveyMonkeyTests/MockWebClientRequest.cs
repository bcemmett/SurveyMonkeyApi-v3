using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace SurveyMonkeyTests
{
    class MockWebClientRequest
    {
        public WebHeaderCollection Headers { get; set; }
        public NameValueCollection QueryString { get; set; }
        public string Url { get; set; }
        public string Verb { get; set; }
        public string Body { get; set; }
        public Encoding Encoding { get; set; }
        public long TimeSinceInitialisation { get; set; }
    }
}