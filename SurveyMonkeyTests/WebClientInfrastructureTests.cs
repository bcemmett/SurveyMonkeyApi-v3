using System.Text;
using NUnit.Framework;
using SurveyMonkey;
using System.Diagnostics;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class WebClientInfrastructureTests
    {
        [Test]
        public void AuthenticationIsPassedToApi()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":50,""total"":2,""data"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/55249163"",""id"":""55249163"",""title"":""All Question Types Test""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934"",""id"":""84672934"",""title"":""Two Question Survey""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/?page=1&per_page=50""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetSurveys();
            
            Assert.AreEqual("application/json", client.Headers["Content-Type"]);
            Assert.AreEqual("bearer TestOAuthToken", client.Headers["Authorization"]);
            Assert.AreEqual("TestApiKey", client.QueryString["api_key"]);
            Assert.AreEqual(Encoding.UTF8, client.Encoding);
        }

        [Test]
        public void RateLimitIsRespected()
        {
            var toleranceMilliseconds = 50;
            var defaultRateLimitMilliseconds = 500;

            var stopwatch = new Stopwatch();
            var client = new MockWebClient();
            var repeatedResponse = @"
                {""per_page"":50,""total"":2,""data"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/55249163"",""id"":""55249163"",""title"":""All Question Types Test""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934"",""id"":""84672934"",""title"":""Two Question Survey""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/?page=1&per_page=50""}}
            ";
            client.Responses.Add(repeatedResponse);
            client.Responses.Add(repeatedResponse);
            client.Responses.Add(repeatedResponse);

            var api = new SurveyMonkeyApi("key", "token", client);

            //Should be no rate limit first time
            stopwatch.Start();
            api.GetSurveys();
            Assert.LessOrEqual(stopwatch.ElapsedMilliseconds, toleranceMilliseconds);

            //Second request should be rate limited
            stopwatch.Restart();
            api.GetSurveys();
            Assert.GreaterOrEqual(stopwatch.ElapsedMilliseconds, defaultRateLimitMilliseconds - toleranceMilliseconds);
            Assert.LessOrEqual(stopwatch.ElapsedMilliseconds, defaultRateLimitMilliseconds + toleranceMilliseconds);
            
            //Check subsequent requests are rate limited too
            stopwatch.Restart();
            api.GetSurveys();
            Assert.GreaterOrEqual(stopwatch.ElapsedMilliseconds, defaultRateLimitMilliseconds - toleranceMilliseconds);
            Assert.LessOrEqual(stopwatch.ElapsedMilliseconds, defaultRateLimitMilliseconds + toleranceMilliseconds);
        }
    }
}