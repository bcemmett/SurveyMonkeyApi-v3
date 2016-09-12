using System.Linq;
using NUnit.Framework;
using SurveyMonkey;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class GetSurveyTests
    {
        [Test]
        public void GetSurveyListReturnsSomething()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":50,""total"":900,""data"":[{""href"":""https:\/\/ api.surveymonkey.net\/ v3\/ surveys\/ 123456"",""id"":""123456"",""title"":""First title""},{""href"":""https:\/\/ api.surveymonkey.net\/ v3\/ surveys\/ 789"",""id"":""789"",""title"":""Second title""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/?page=1&per_page=50"",""last"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/?page=19&per_page=50"",""next"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/?page=2&per_page=50""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetSurveys();
            Assert.AreEqual(1, client.Requests.Count);
            Assert.AreEqual(123456, results.First().Id);
            Assert.AreEqual("Second title", results.Last().Title);
        }
    }
}