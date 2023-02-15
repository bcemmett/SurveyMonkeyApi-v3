using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.Helpers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi : IDisposable, ISurveyMonkeyApi
    {
        private readonly string _accessToken;
        private IWebClient _webClient;
        private DateTime _lastRequestTime = DateTime.MinValue;
        private readonly int _rateLimitDelay = 500;
        private readonly int[] _retrySequence = { 5, 30, 300, 900 };
        private readonly string _baseAccessUrl = "https://api.surveymonkey.com";
        private int _requestsMade;

        /// <summary>
        /// The total number of api requests, including retries, made in the lifetime of this object.
        /// </summary>
        public int ApiRequestsMade => _requestsMade;

        public IWebProxy Proxy
        {
            get { return _webClient.Proxy; }
            set { _webClient.Proxy = value; }
        }

        /// <param name="accessToken">The Access Token, representing either a private app's access token, or the long-lived token granted by an OAuth 2.0 flow.</param>
        public SurveyMonkeyApi(string accessToken)
            : this(accessToken, null, null, null, null)
        {
        }

        /// <param name="accessToken">The Access Token, representing either a private app's access token, or the long-lived token granted by an OAuth 2.0 flow.</param>
        /// <param name="rateLimitDelay">The number of milliseconds to wait between each api request. 500ms by default to accomodate SurveyMonkey's default 120/s limit. Set to a lower number if you have been granted a higher rate limit.</param>
        public SurveyMonkeyApi(string accessToken, int rateLimitDelay)
            : this(accessToken, rateLimitDelay, null, null, null)
        {
        }

        /// <param name="accessToken">The Access Token, representing either a private app's access token, or the long-lived token granted by an OAuth 2.0 flow.</param>
        /// <param name="retrySequence">A sequence of the number of seconds to wait between retries if connectivity issues are encountered. Defaults to 5, 30, 300, then 900 seconds.</param>
        public SurveyMonkeyApi(string accessToken, int[] retrySequence)
            : this(accessToken, null, retrySequence, null, null)
        {
        }

        /// <param name="accessToken">The Access Token, representing either a private app's access token, or the long-lived token granted by an OAuth 2.0 flow.</param>
        /// <param name="rateLimitDelay">The number of milliseconds to wait between each api request. 500ms by default to accomodate SurveyMonkey's default 120/s limit. Set to a lower number if you have been granted a higher rate limit.</param>
        /// <param name="retrySequence">A sequence of the number of seconds to wait between retries if connectivity issues are encountered. Defaults to 5, 30, 300, then 900 seconds.</param>
        public SurveyMonkeyApi(string accessToken, int rateLimitDelay, int[] retrySequence)
            : this(accessToken, rateLimitDelay, retrySequence, null, null)
        {
        }

        /// <param name="accessToken">The Access Token, representing either a private app's access token, or the long-lived token granted by an OAuth 2.0 flow.</param>
        /// <param name="accessUrl">Use to connect to an api access url other than the default https://api.surveymonkey.com.</param>
        public SurveyMonkeyApi(string accessToken, string accessUrl)
            : this(accessToken, null, null, accessUrl, null)
        {
        }

        /// <param name="accessToken">The Access Token, representing either a private app's access token, or the long-lived token granted by an OAuth 2.0 flow.</param>
        /// <param name="rateLimitDelay">The number of milliseconds to wait between each api request. 500ms by default to accomodate SurveyMonkey's default 120/s limit. Set to a lower number if you have been granted a higher rate limit.</param>
        /// <param name="accessUrl">Use to connect to an api access url other than the default https://api.surveymonkey.com.</param>
        public SurveyMonkeyApi(string accessToken, int rateLimitDelay, string accessUrl)
            : this(accessToken, rateLimitDelay, null, accessUrl, null)
        {
        }

        /// <param name="accessToken">The Access Token, representing either a private app's access token, or the long-lived token granted by an OAuth 2.0 flow.</param>
        /// <param name="retrySequence">A sequence of the number of seconds to wait between retries if connectivity issues are encountered. Defaults to 5, 30, 300, then 900 seconds.</param>
        /// <param name="accessUrl">Use to connect to an api access url other than the default https://api.surveymonkey.com.</param>
        public SurveyMonkeyApi(string accessToken, int[] retrySequence, string accessUrl)
            : this(accessToken, null, retrySequence, accessUrl, null)
        {
        }

        /// <param name="accessToken">The Access Token, representing either a private app's access token, or the long-lived token granted by an OAuth 2.0 flow.</param>
        /// <param name="rateLimitDelay">The number of milliseconds to wait between each api request. 500ms by default to accomodate SurveyMonkey's default 120/s limit. Set to a lower number if you have been granted a higher rate limit.</param>
        /// <param name="retrySequence">A sequence of the number of seconds to wait between retries if connectivity issues are encountered. Defaults to 5, 30, 300, then 900 seconds.</param>
        /// <param name="accessUrl">Use to connect to an api access url other than the default https://api.surveymonkey.com.</param>
        public SurveyMonkeyApi(string accessToken, int rateLimitDelay, int[] retrySequence, string accessUrl)
            : this(accessToken, rateLimitDelay, retrySequence, accessUrl, null)
        {
        }

        internal SurveyMonkeyApi(string accessToken, IWebClient webClient)
            : this(accessToken, 0, null, null, webClient)
        {
        }

        internal SurveyMonkeyApi(string accessToken, IWebClient webClient, int rateLimitDelay)
            : this(accessToken, rateLimitDelay, null, null, webClient)
        {
        }

        internal SurveyMonkeyApi(string accessToken, IWebClient webClient, int[] retrySequence)
            : this(accessToken, null, retrySequence, null, webClient)
        {
        }

        private SurveyMonkeyApi(string accessToken, int? rateLimitDelay, int[] retrySequence, string accessUrl, IWebClient webClient)
        {
            _webClient = webClient ?? new LiveWebClient();
            _webClient.Encoding = Encoding.UTF8;
            _accessToken = accessToken;

            if (rateLimitDelay.HasValue)
            {
                _rateLimitDelay = rateLimitDelay.Value;
            }

            if (retrySequence != null)
            {
                _retrySequence = retrySequence;
            }

            if (accessUrl != null)
            {
                if (!accessUrl.StartsWith("http"))
                {
                    throw new ArgumentException($"Invalid url {accessUrl} given. Base access url must be a full url, eg \"https://api.eu.surveymonkey.com\"");
                }
                _baseAccessUrl = accessUrl;
            }
        }

        private JToken MakeApiRequest(string endpoint, Verb verb, RequestData data)
        {
            RateLimit();
            PrepareWebClientForRequest(verb, data);

            string url = GetFullUrl(endpoint);
            string result = AttemptApiRequestWithRetry(url, verb, data);

            _lastRequestTime = DateTime.UtcNow;

            return JObject.Parse(result);
        }

        private async Task<JToken> MakeApiRequestAsync(string endpoint, Verb verb, RequestData data)
        {
            await RateLimitAsync();
            PrepareWebClientForRequest(verb, data);

            string url = GetFullUrl(endpoint);
            string result = await AttemptApiRequestWithRetryAsync(url, verb, data);

            _lastRequestTime = DateTime.UtcNow;

            return JObject.Parse(result);
        }

        private void PrepareWebClientForRequest(Verb verb, RequestData data)
        {
            ResetWebClient();
            _webClient.Headers.Add("Content-Type", "application/json");
            _webClient.Headers.Add("Authorization", "bearer " + _accessToken);
            if (verb == Verb.GET)
            {
                foreach (var item in data)
                {
                    _webClient.QueryString.Add(item.Key, item.Value.ToString());
                }
            }
        }

        private string GetFullUrl(string endpoint)
        {
            return $"{_baseAccessUrl}/v3{endpoint}";
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
                    CheckForTlsError(webEx);
                    if (FailedRequestShouldBeRetried(webEx, attempt))
                    {
                        Thread.Sleep(_retrySequence[attempt] * 1000);
                    }
                    else
                    {
                        HandleWebConnectivityErrors(webEx);
                        throw;
                    }
                }
            }
            return String.Empty;
        }

        private async Task<string> AttemptApiRequestWithRetryAsync(string url, Verb verb, RequestData data)
        {
            if (_retrySequence == null || _retrySequence.Length == 0)
            {
                return await AttemptApiRequestAsync(url, verb, data);
            }
            for (int attempt = 0; attempt <= _retrySequence.Length; attempt++)
            {
                try
                {
                    return await AttemptApiRequestAsync(url, verb, data);
                }
                catch (WebException webEx)
                {
                    CheckForTlsError(webEx);
                    if (FailedRequestShouldBeRetried(webEx, attempt))
                    {
                        await Task.Delay(_retrySequence[attempt] * 1000);
                    }
                    else
                    {
                        HandleWebConnectivityErrors(webEx);
                        throw;
                    }
                }
            }
            return String.Empty;
        }

        private void CheckForTlsError(WebException webEx)
        {
            if (webEx.Status == WebExceptionStatus.SecureChannelFailure)
            {
                throw new WebException("SSL/TLS error. SurveyMonkey requires TLS 1.2, as of 13 June 2018. "
                    + "Configure this globally with \"ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;\" anywhere before using this library. "
                    + "See https://github.com/bcemmett/SurveyMonkeyApi-v3/issues/66 for details.", webEx);
            }
        }

        private bool FailedRequestShouldBeRetried(WebException webEx, int attempt)
        {
            if (attempt >= _retrySequence.Length)
            {
                return false;
            }
            if (webEx.Response == null || ((HttpWebResponse)webEx.Response).StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                return true;
            }
            return false;
        }

        private void HandleWebConnectivityErrors(WebException webEx)
        {
            try
            {
                var response = new System.IO.StreamReader(webEx.Response.GetResponseStream()).ReadToEnd();
                var parsedError = JObject.Parse(response);
                var error = parsedError["error"].ToObject<Error>();
                string message = $"Http status: {error.HttpStatusCode}, error code {error.Id}. {error.Name}: {error.Message}. See {error.Docs} for more information.";
                if (error.Id == "1014")
                {
                    message += " Ensure your app has sufficient scopes granted to make this request: https://developer.surveymonkey.net/api/v3/#scopes";
                }
                throw new WebException(message, webEx);
            }
            catch (Exception e)
            {
                if (e is WebException)
                {
                    throw;
                }
            }
        }

        private string AttemptApiRequest(string url, Verb verb, RequestData data)
        {
            _requestsMade++;

            if (verb == Verb.GET)
            {
                return _webClient.DownloadString(url);
            }
            return _webClient.UploadString(url, verb.ToString(), JsonConvert.SerializeObject(data));
        }

        private async Task<string> AttemptApiRequestAsync(string url, Verb verb, RequestData data)
        {
            _requestsMade++;

            if (verb == Verb.GET)
            {
                return await _webClient.DownloadStringTaskAsync(url);
            }
            return await _webClient.UploadStringTaskAsync(url, verb.ToString(), JsonConvert.SerializeObject(data));
        }

        private void RateLimit()
        {
            int remainingTime = GetRemainingRateLimitTime();
            if (remainingTime > 0)
            {
                Thread.Sleep(remainingTime);
            }
            _lastRequestTime = DateTime.UtcNow; //Also setting here as otherwise if an exception is thrown while making the request it wouldn't get set at all
        }

        private async Task RateLimitAsync()
        {
            int remainingTime = GetRemainingRateLimitTime();
            if (remainingTime > 0)
            {
                await Task.Delay(remainingTime);
            }
            _lastRequestTime = DateTime.UtcNow; //Also setting here as otherwise if an exception is thrown while making the request it wouldn't get set at all
        }

        private int GetRemainingRateLimitTime()
        {
            if (_lastRequestTime == DateTime.MinValue)
            {
                return 0;
            }
            TimeSpan timeSpan = DateTime.UtcNow - _lastRequestTime;
            int elapsedTime = (int)timeSpan.TotalMilliseconds;
            int remainingTime = _rateLimitDelay - elapsedTime;
            return remainingTime;
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

        private async Task<IEnumerable<IPageableContainer>> PageAsync(IPagingSettings settings, string url, Type type, int maxResultsPerPage)
        {
            if (settings.Page.HasValue || settings.PerPage.HasValue)
            {
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                return await PageRequestAsync(url, requestData, type);
            }

            var results = new List<IPageableContainer>();
            bool cont = true;
            int page = 1;
            while (cont)
            {
                settings.Page = page;
                settings.PerPage = maxResultsPerPage;
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                var newResults = await PageRequestAsync(url, requestData, type);
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

        private async Task<IEnumerable<IPageableContainer>> PageRequestAsync(string url, RequestData requestData, Type type)
        {
            var verb = Verb.GET;
            JToken result = await MakeApiRequestAsync(url, verb, requestData);
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