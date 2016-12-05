using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using SurveyMonkey;

namespace SurveyMonkeyTests
{
    class MockWebClient : IWebClient
    {
        public List<MockWebClientRequest> Requests { get; set; }
        public List<string> Responses { get; set; }
        private int _nextResponseSequence;

        public MockWebClient()
        {
            Headers = new WebHeaderCollection();
            QueryString = new NameValueCollection();
            Requests = new List<MockWebClientRequest>();
            Responses = new List<string>();
        }

        private void RecordRequest(string url, string verb, string body)
        {
            Requests.Add(new MockWebClientRequest
            {
                Encoding = Encoding,
                Headers = Headers,
                QueryString = QueryString,
                Url = url,
                Verb = verb,
                Body = body
            });
        }

        private string GetNextData()
        {
            var response = Responses.Skip(_nextResponseSequence).First();
            _nextResponseSequence++;
            return response;
        }


        #region Interface

        public WebHeaderCollection Headers { get; set; }
        public NameValueCollection QueryString { get; set; }
        public Encoding Encoding { get; set; }
        public IWebProxy Proxy { get; set; }

        public string DownloadString(string url)
        {
            RecordRequest(url, "NOT SUPPLIED", "NOT SUPPLIED");
            return GetNextData();
        }

        public string UploadString(string url, string verb, string body)
        {
            RecordRequest(url, verb, body);
            return GetNextData();
        }

        public void Dispose()
        {
        }

        #endregion
    }
}