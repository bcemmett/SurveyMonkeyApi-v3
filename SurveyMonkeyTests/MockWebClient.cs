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

        private void RecordRequest()
        {
            Requests.Add(new MockWebClientRequest
            {
                Headers = Headers,
                QueryString = QueryString
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

        public string DownloadString(string url)
        {
            RecordRequest();
            return GetNextData();
        }

        public string UploadString(string url, string verb, string body)
        {
            RecordRequest();
            return GetNextData();
        }

        #endregion
    }
}