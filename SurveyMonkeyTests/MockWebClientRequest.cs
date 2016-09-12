using System.Collections.Specialized;
using System.Net;

namespace SurveyMonkeyTests
{
    class MockWebClientRequest
    {
        public WebHeaderCollection Headers { get; set; }
        public NameValueCollection QueryString { get; set; }
    }
}