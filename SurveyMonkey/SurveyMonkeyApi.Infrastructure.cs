﻿using System;
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
        private string _apiKey;
        private string _oAuthToken;
        private IWebClient _webClient;
        private DateTime _lastRequestTime = DateTime.MinValue;
        private readonly int _rateLimitDelay = 500;
        private readonly int[] _retrySequence = { 5, 30, 300, 900 };

        public SurveyMonkeyApi(string oAuthToken)
        : this(string.Empty, oAuthToken, null, null, null)
        {

        }

        public SurveyMonkeyApi(string oAuthToken, int rateLimitDelay)
            : this(string.Empty, oAuthToken, rateLimitDelay, null, null)
        {
        }

        public SurveyMonkeyApi(string oAuthToken, int[] retrySequence)
            : this(string.Empty, oAuthToken, null, retrySequence, null)
        {

        }

        public SurveyMonkeyApi(string apiKey, string oAuthToken)
        : this(apiKey, oAuthToken, null, null, null)
        {
            
        }

        public SurveyMonkeyApi(string apiKey, string oAuthToken, int rateLimitDelay)
            : this(apiKey, oAuthToken, rateLimitDelay, null, null)
        {            
        }

        public SurveyMonkeyApi(string apiKey, string oAuthToken, int[] retrySequence)
            : this(apiKey, oAuthToken, null, retrySequence, null)
        {
            
        }

        public SurveyMonkeyApi(string apiKey, string oAuthToken, int? rateLimitDelay, int[] retrySequence)
        : this(apiKey, oAuthToken, rateLimitDelay, retrySequence, null)
        {
            
        }

        private SurveyMonkeyApi(string apiKey, string oAuthToken, int? rateLimitDelay, int[] retrySequence, IWebClient webClient)
        {
            if (webClient == null)
                _webClient = new LiveWebClient();
            else
                _webClient = webClient;

            if (rateLimitDelay.HasValue)
                _rateLimitDelay = (int)rateLimitDelay;

            if (_retrySequence != null)
                _retrySequence = retrySequence;

            SetupWebClient(apiKey, oAuthToken);
        }

        internal SurveyMonkeyApi(string apiKey, string oAuthToken, IWebClient webClient)
            : this(apiKey, oAuthToken, 0, null, webClient)
        {
        }

        internal SurveyMonkeyApi(string apiKey, string oAuthToken, IWebClient webClient, int rateLimitDelay)
        : this(apiKey, oAuthToken, rateLimitDelay, null, webClient)
        {
        }        

        private void SetupWebClient(string apiKey, string oAuthToken)
        {
            _apiKey = apiKey;
            _oAuthToken = oAuthToken;
            _webClient.Encoding = Encoding.UTF8;
        }

        private JToken MakeApiRequest(string endpoint, Verb verb, RequestData data)
        {
            RateLimit();
            ResetWebClient();

            var url = "https://api.surveymonkey.net/v3" + endpoint;
            _webClient.Headers.Add("Content-Type", "application/json");
            _webClient.Headers.Add("Authorization", "bearer " + _oAuthToken);
            if (!string.IsNullOrEmpty(_apiKey))
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
                catch (WebException ex)
                {
                    if (attempt < _retrySequence.Length && (ex.Response == null || ((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.ServiceUnavailable))
                    {
                        Thread.Sleep(_retrySequence[attempt] * 1000);
                    }
                    else
                    {
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
            return (IEnumerable<IPageableContainer>)results;
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