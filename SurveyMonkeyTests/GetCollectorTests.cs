using System.Linq;
using NUnit.Framework;
using SurveyMonkey;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class GetCollectorTests
    {
        [Test]
        public void GetCollectorListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":1000,""total"":3,""data"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91664733"",""name"":""Email Invitation 1"",""id"":""91664733""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/57184593"",""name"":""Web Link 1"",""id"":""57184593""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/57185230"",""name"":""Web Link 2"",""id"":""57185230""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/55249163\/collectors?page=1&per_page=1000""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetCollectorList(55249163);
            Assert.AreEqual(1, client.Requests.Count);
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual(91664733, results.First().Id);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/collectors/91664733", results.First().Href);
            Assert.AreEqual("Web Link 2", results.Last().Name);
        }
    }
}