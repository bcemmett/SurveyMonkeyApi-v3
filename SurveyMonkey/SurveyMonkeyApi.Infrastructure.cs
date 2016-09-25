using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.Helpers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi : IDisposable
    {
        private string _apiKey;
        private string _oAuthToken;
        private IWebClient _webClient;
        private DateTime _lastRequestTime = DateTime.MinValue;
        private int _rateLimitDelay;

        public SurveyMonkeyApi(string apiKey, string oAuthToken)
        {
            _webClient = new LiveWebClient();
            SetupWebClient(apiKey, oAuthToken, 500);
        }

        public SurveyMonkeyApi(string apiKey, string oAuthToken, int rateLimitDelay)
        {
            _webClient = new LiveWebClient();
            SetupWebClient(apiKey, oAuthToken, rateLimitDelay);
        }

        internal SurveyMonkeyApi(string apiKey, string oAuthToken, IWebClient webClient)
        {
            _webClient = webClient;
            SetupWebClient(apiKey, oAuthToken, 0);
        }

        internal SurveyMonkeyApi(string apiKey, string oAuthToken, IWebClient webClient, int rateLimitDelay)
        {
            _webClient = webClient;
            SetupWebClient(apiKey, oAuthToken, rateLimitDelay);
        }

        private void SetupWebClient(string apiKey, string oAuthToken, int rateLimitDelay)
        {
            _rateLimitDelay = rateLimitDelay;
            _apiKey = apiKey;
            _oAuthToken = oAuthToken;
            _webClient.Encoding = Encoding.UTF8;
        }

        private JToken MakeApiRequest(string endpoint, Verb verb, RequestData data)
        {
            RateLimit();
            ResetWebClient();
            string result;

            var url = "https://api.surveymonkey.net/v3" + endpoint;
            _webClient.Headers.Add("Content-Type", "application/json");
            _webClient.Headers.Add("Authorization", "bearer " + _oAuthToken);
            _webClient.QueryString.Add("api_key", _apiKey);
            if (verb == Verb.GET)
            {
                foreach (var item in data)
                {
                    _webClient.QueryString.Add(item.Key, item.Value.ToString());
                }
                result = _webClient.DownloadString(url);
            }
            else
            {
                var settings = JsonConvert.SerializeObject(data);
                result = _webClient.UploadString(url, verb.ToString(), settings);
            }
                
            _lastRequestTime = DateTime.UtcNow;

            var parsed = JObject.Parse(result);
            return parsed;
        }

        private void RateLimit()
        {
            TimeSpan timeSpan = DateTime.UtcNow - _lastRequestTime;
            int elapsedTime = (int)timeSpan.TotalMilliseconds;
            int remainingTime = _rateLimitDelay - elapsedTime;
            if ((_lastRequestTime != DateTime.MinValue) && (remainingTime > 0))
            {
                Thread.Sleep(remainingTime);
            }
            _lastRequestTime = DateTime.UtcNow; //Also setting here as otherwise if an exception is thrown while making the request it wouldn't get set at all
        }

        private IEnumerable<IPageable> Page(IPageableSettings settings, string url, Type type, int maxResultsPerPage)
        {
            //Get the specific page & quantity
            if (settings.Page.HasValue || settings.PerPage.HasValue)
            {
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                return PageRequest(url, requestData, type);
            }
            
            var results = new List<IPageable>();
            bool cont = true;
            int page = 1;
            while (cont)
            {
                settings.Page = page;
                settings.PerPage = maxResultsPerPage;
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                var newResults = PageRequest(url, requestData, type);
                if (newResults.Any())
                {
                    results.AddRange(newResults);
                }
                if (newResults.Count() < maxResultsPerPage)
                {
                    cont = false;
                }
                page++;
            }
            return results;
        }

        private IEnumerable<IPageable> PageRequest(string url, RequestData requestData, Type type)
        {
            var verb = Verb.GET;
            JToken result = MakeApiRequest(url, verb, requestData);
            var results = result["data"].ToObject(type);
            return (IEnumerable<IPageable>) results;
        }

        private void ResetWebClient()
        {
            _webClient.Headers.Clear();
            _webClient.QueryString.Clear();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_webClient != null)
            {
                _webClient.Dispose();
                _webClient = null;
            }
        }
    }
}