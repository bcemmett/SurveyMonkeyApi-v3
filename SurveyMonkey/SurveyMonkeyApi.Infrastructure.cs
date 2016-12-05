using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.Helpers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi : IDisposable, ISurveyMonkeyApi
    {
        private readonly string _apiKey;
        private readonly string _accessToken;
        private IWebClient _webClient;
        private DateTime _lastRequestTime = DateTime.MinValue;
        private readonly int _rateLimitDelay = 500;
        private readonly int[] _retrySequence = { 5, 30, 300, 900 };

        public IWebProxy Proxy
        {
            get { return _webClient.Proxy; }
            set { _webClient.Proxy = value; }
        }

        public SurveyMonkeyApi(string accessToken)
            : this(null, accessToken, null, null, null)
        {
        }

        public SurveyMonkeyApi(string accessToken, int rateLimitDelay)
            : this(null, accessToken, rateLimitDelay, null, null)
        {
        }

        public SurveyMonkeyApi(string accessToken, int[] retrySequence)
            : this(null, accessToken, null, retrySequence, null)
        {
        }

        public SurveyMonkeyApi(string accessToken, int rateLimitDelay, int[] retrySequence)
            : this(null, accessToken, rateLimitDelay, retrySequence, null)
        {
        }

        public SurveyMonkeyApi(string apiKey, string accessToken)
            : this(apiKey, accessToken, null, null, null)
        {
        }

        public SurveyMonkeyApi(string apiKey, string accessToken, int rateLimitDelay)
            : this(apiKey, accessToken, rateLimitDelay, null, null)
        {
        }

        public SurveyMonkeyApi(string apiKey, string accessToken, int[] retrySequence)
            : this(apiKey, accessToken, null, retrySequence, null)
        {
        }

        public SurveyMonkeyApi(string apiKey, string accessToken, int rateLimitDelay, int[] retrySequence)
            : this(apiKey, accessToken, rateLimitDelay, retrySequence, null)
        {
        }


        internal SurveyMonkeyApi(string apiKey, string accessToken, IWebClient webClient)
            : this(apiKey, accessToken, 0, null, webClient)
        {
        }

        internal SurveyMonkeyApi(string apiKey, string accessToken, IWebClient webClient, int rateLimitDelay)
            : this(apiKey, accessToken, rateLimitDelay, null, webClient)
        {
        }

        internal SurveyMonkeyApi(string apiKey, string accessToken, IWebClient webClient, int[] retrySequence)
            : this(apiKey, accessToken, null, retrySequence, webClient)
        {
        }

        private SurveyMonkeyApi(string apiKey, string accessToken, int? rateLimitDelay, int[] retrySequence, IWebClient webClient)
        {
            _webClient = webClient ?? new LiveWebClient();
            _webClient.Encoding = Encoding.UTF8;
            _apiKey = apiKey;
            _accessToken = accessToken;

            if (rateLimitDelay.HasValue)
            {
                _rateLimitDelay = rateLimitDelay.Value;
            }

            if (retrySequence != null)
            {
                _retrySequence = retrySequence;
            }
        }

        private JToken MakeApiRequest(string endpoint, Verb verb, RequestData data)
        {
            RateLimit();
            ResetWebClient();

            var url = "https://api.surveymonkey.net/v3" + endpoint;
            _webClient.Headers.Add("Content-Type", "application/json");
            _webClient.Headers.Add("Authorization", "bearer " + _accessToken);
            if (!String.IsNullOrEmpty(_apiKey))
            {
                _webClient.QueryString.Add("api_key", _apiKey);
            }
            if (verb == Verb.GET)
            {
                foreach (var item in data)
                {
                    _webClient.QueryString.Add(item.Key, item.Value.ToString());
                }
            }
            string result = AttemptApiRequestWithRetry(url, verb, data);

            _lastRequestTime = DateTime.UtcNow;

            var parsed = JObject.Parse(result);
            return parsed;
        }

        private string AttemptApiRequestWithRetry(string url, Verb verb, RequestData data)
        {
            if (_retrySequence == null || _retrySequence.Length == 0)
            {
                return AttemptApiRequest(url, verb, data);
            }
            for (int attempt = 0; attempt <= _retrySequence.Length; attempt++)
            {
                try
                {
                    return AttemptApiRequest(url, verb, data);
                }
                catch (WebException webEx)
                {
                    if (attempt < _retrySequence.Length && (webEx.Response == null || ((HttpWebResponse)webEx.Response).StatusCode == HttpStatusCode.ServiceUnavailable))
                    {
                        Thread.Sleep(_retrySequence[attempt] * 1000);
                    }
                    else
                    {
                        try
                        {
                            var response = new System.IO.StreamReader(webEx.Response.GetResponseStream()).ReadToEnd();
                            var parsedError = JObject.Parse(response);
                            var error = parsedError["error"].ToObject<Error>();
                            string message = String.Format("Http status: {0}, error code {1}. {2}: {3}. See {4} for more information.", error.HttpStatusCode, error.Id, error.Name, error.Message, error.Docs);
                            if (error.Id == "1014")
                            {
                                message += " Ensure your app has sufficient scopes granted to make this request: https://developer.surveymonkey.net/api/v3/#scopes";
                            }
                            throw new WebException(message, webEx);
                        }
                        catch (Exception e)
                        {
                            if(e is WebException)
                            {
                                throw;
                            }
                            //For anything other than our new WebException, swallow so that the original raw WebException is thrown
                        }
                        throw;
                    }
                }
            }
            return String.Empty;
        }

        private string AttemptApiRequest(string url, Verb verb, RequestData data)
        {
            if (verb == Verb.GET)
            {
                return _webClient.DownloadString(url);
            }
            return _webClient.UploadString(url, verb.ToString(), JsonConvert.SerializeObject(data));
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

        private IEnumerable<IPageableContainer> Page(IPagingSettings settings, string url, Type type, int maxResultsPerPage)
        {
            if (settings.Page.HasValue || settings.PerPage.HasValue)
            {
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                return PageRequest(url, requestData, type);
            }
            
            var results = new List<IPageableContainer>();
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

        private IEnumerable<IPageableContainer> PageRequest(string url, RequestData requestData, Type type)
        {
            var verb = Verb.GET;
            JToken result = MakeApiRequest(url, verb, requestData);
            var results = result["data"].ToObject(type);
            return (IEnumerable<IPageableContainer>) results;
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