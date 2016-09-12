using System.Collections.Specialized;
using System.Net;
using System.Text;
using SurveyMonkey;

namespace SurveyMonkeyTests
{
    class MockWebClient : IWebClient
    {
        public WebHeaderCollection Headers { get; set; }
        public NameValueCollection QueryString { get; set; }
        public Encoding Encoding { get; set; }

        public string DownloadString(string url)
        {
            throw new System.NotImplementedException();
        }

        public string UploadString(string url, string verb, string body)
        {
            throw new System.NotImplementedException();
        }
    }
}