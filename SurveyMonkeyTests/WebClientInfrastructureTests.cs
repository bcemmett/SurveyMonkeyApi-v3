using System.Text;
using NUnit.Framework;
using SurveyMonkey;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System;
using SurveyMonkey.Containers;
using System.Collections.Generic;

namespace SurveyMonkeyTests
{
    [TestFixtureSource(typeof(AsyncTestFixtureSource))]
    public class WebClientInfrastructureTests
    {
        private readonly bool _useAsync;
        private const string CGetSurveyListResponse = "{\"per_page\":50,\"total\":2,\"data\":[{\"href\":\"url1\",\"id\":\"55249163\",\"title\":\"title1\"},{\"href\":\"url2\",\"id\":\"84672934\",\"title\":\"title2\"}],\"page\":17}";

        public WebClientInfrastructureTests(bool useAsync)
        {
            _useAsync = useAsync;
        }

        [Test]
        public void AuthenticationIsPassedToApi()
        {
            var client = new MockWebClient();
            client.Responses.Add(CGetSurveyListResponse);

            var api = new SurveyMonkeyApi("TestOAuthToken", client);
            MakeARequest(api);
            
            Assert.AreEqual("application/json", client.Requests.First().Headers["Content-Type"]);
            Assert.AreEqual("bearer TestOAuthToken", client.Requests.First().Headers["Authorization"]);
            Assert.AreEqual(Encoding.UTF8, client.Requests.First().Encoding);
            Assert.AreEqual("https://api.surveymonkey.com/v3/surveys", client.Requests.First().Url);
        }

        [Test]
        public void RateLimitIsRespected()
        {
            var toleranceMilliseconds = 80;
            var defaultRateLimitMilliseconds = 500;

            var stopwatch = new Stopwatch();
            var client = new MockWebClient();

            client.Responses.Add(CGetSurveyListResponse);
            client.Responses.Add(CGetSurveyListResponse);
            client.Responses.Add(CGetSurveyListResponse);
            client.Responses.Add(CGetSurveyListResponse);

            var api = new SurveyMonkeyApi("token", client, 500);

            //Should be no rate limit first time
            stopwatch.Start();
            MakeARequest(api);
            Assert.LessOrEqual(stopwatch.ElapsedMilliseconds, toleranceMilliseconds);

            //Second request should be rate limited
            stopwatch.Restart();
            MakeARequest(api);
            Assert.GreaterOrEqual(stopwatch.ElapsedMilliseconds, defaultRateLimitMilliseconds - toleranceMilliseconds);
            Assert.LessOrEqual(stopwatch.ElapsedMilliseconds, defaultRateLimitMilliseconds + toleranceMilliseconds);
            
            //Check subsequent requests are rate limited too
            stopwatch.Restart();
            MakeARequest(api);
            Assert.GreaterOrEqual(stopwatch.ElapsedMilliseconds, defaultRateLimitMilliseconds - toleranceMilliseconds);
            Assert.LessOrEqual(stopwatch.ElapsedMilliseconds, defaultRateLimitMilliseconds + toleranceMilliseconds);

            //If some time has elapsed, there should be no delay
            Thread.Sleep(1000);
            stopwatch.Restart();
            MakeARequest(api);
            Assert.LessOrEqual(stopwatch.ElapsedMilliseconds, toleranceMilliseconds);
        }

        [Test]
        public void RetrySequenceIsRespected()
        {
            var toleranceMilliseconds = 80;

            var client = new MockWebClient();
            client.Responses.Add(CGetSurveyListResponse);
            client.Responses.Add(CGetSurveyListResponse);
            client.Responses.Add(CGetSurveyListResponse);
            client.Exceptions.Add(new WebException("1"));
            client.Exceptions.Add(new WebException("2"));
            client.Exceptions.Add(new WebException("3"));
            var api = new SurveyMonkeyApi("token", client, new [] { 1, 2 });

            var exception = Assert.Throws<WebException>(delegate { MakeARequest(api); });
            Assert.AreEqual("3", exception.Message);
            Assert.AreEqual(3, client.Requests.Count);

            Assert.GreaterOrEqual(toleranceMilliseconds, client.Requests.First().TimeSinceInitialisation);

            Assert.LessOrEqual(1000 - toleranceMilliseconds, client.Requests.Skip(1).First().TimeSinceInitialisation);
            Assert.GreaterOrEqual(1000 + toleranceMilliseconds, client.Requests.Skip(1).First().TimeSinceInitialisation);

            Assert.LessOrEqual(3000 - toleranceMilliseconds, client.Requests.Skip(2).First().TimeSinceInitialisation);
            Assert.GreaterOrEqual(3000 + toleranceMilliseconds, client.Requests.Skip(2).First().TimeSinceInitialisation);
        }

        [Test]
        public void TlsErrorIsNotRetried()
        {
            string expectedMessage = "SSL/TLS error. SurveyMonkey requires TLS 1.2, as of 13 June 2018. "
                    + "Configure this globally with \"ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;\" anywhere before using this library. "
                    + "See https://github.com/bcemmett/SurveyMonkeyApi-v3/issues/66 for details.";

            var client = new MockWebClient();
            client.Exceptions.Add(new WebException("1", null, WebExceptionStatus.SecureChannelFailure, null));
            var api = new SurveyMonkeyApi("token", client, new[] { 1, 2 });
            var e = Assert.Throws<WebException>(delegate { MakeARequest(api); });
            Assert.AreEqual(expectedMessage, e.Message);
        }

        [Test]
        public void FailuresAreNotRetriedBeyondTheRetryLimit()
        {
            var client = new MockWebClient();
            client.Exceptions.Add(new WebException("bla1"));
            client.Exceptions.Add(new WebException("bla2"));
            client.Exceptions.Add(new WebException("bla3"));
            client.Exceptions.Add(new WebException("bla4"));
            var api = new SurveyMonkeyApi("token", client, new[] { 1, 2 });
            var e = Assert.Throws<WebException>(delegate { MakeARequest(api); });
            Assert.AreEqual(3, client.Requests.Count);
            Assert.AreEqual("bla3", e.Message);
        }

        [Test]
        public void ServiceUnavailableErrorIsRetried()
        {
            var errorResponse = new MockHttpWebResponse(String.Empty, HttpStatusCode.ServiceUnavailable);
            var client = new MockWebClient();
            client.Exceptions.Add(new WebException("Fail", null, WebExceptionStatus.MessageLengthLimitExceeded, errorResponse));
            client.Responses.Add(String.Empty);
            client.Responses.Add(CGetSurveyListResponse);
            var api = new SurveyMonkeyApi("token", client, new[] { 1, 2, 3 });
            var result = MakeARequest(api);
            Assert.AreEqual(2, client.Requests.Count);
            Assert.AreEqual("title2", result.Last().Title);
        }

        [Test]
        public void SurveyMonkeyErrorResponsesAreNotRetriedAndDisplayASpecialError()
        {
            string errorResponse = "{\"error\":{\"docs\":\"a\",\"message\":\"b\",\"id\":\"c\",\"name\":\"d\",\"http_status_code\":7}}";
            string expectedMessage = $"Http status: 7, error code c. d: b. See a for more information.";

            var response = new MockHttpWebResponse(errorResponse, HttpStatusCode.NonAuthoritativeInformation);
            var client = new MockWebClient();

            client.Exceptions.Add(new WebException("OriginalFail", null, WebExceptionStatus.MessageLengthLimitExceeded, response));
            var api = new SurveyMonkeyApi("token", client, new[] { 1, 2, 3 });
            var e = Assert.Throws<WebException>(delegate { MakeARequest(api); });
            Assert.AreEqual(1, client.Requests.Count);
            Assert.AreEqual(expectedMessage, e.Message);
            Assert.AreEqual("OriginalFail", e.InnerException.Message);
        }

        [Test]
        public void ErrorsOtherThanServiceUnavailableAreWhichDoNotComeFromSurveyMonkeyAreNotRetried()
        {
            string errorResponse = "A badly formed message";

            var response = new MockHttpWebResponse(errorResponse, HttpStatusCode.NonAuthoritativeInformation);
            var client = new MockWebClient();

            client.Exceptions.Add(new WebException("OriginalFail", null, WebExceptionStatus.MessageLengthLimitExceeded, response));
            var api = new SurveyMonkeyApi("token", client, new[] { 1, 2, 3 });
            var e = Assert.Throws<WebException>(delegate { MakeARequest(api); });
            Assert.AreEqual(1, client.Requests.Count);
            Assert.AreEqual("OriginalFail", e.Message);
            Assert.IsNull(e.InnerException);
        }

        private List<Survey> MakeARequest(SurveyMonkeyApi api)
        {
            return _useAsync
                ? api.GetSurveyListAsync().GetAwaiter().GetResult()
                : api.GetSurveyList();
        }
    }
}