using System;
using System.Linq;
using NUnit.Framework;
using SurveyMonkey;
using SurveyMonkey.Containers;

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
            Assert.IsNull(results.First().AllowMultipleResponses);
        }

        [Test]
        public void GetCollectorDetailWeblinkIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""status"":""closed"",""redirect_url"":""https:\/\/www.surveymonkey.com"",""thank_you_message"":""Thank you for completing our survey!"",""response_count"":1,""closed_page_message"":""closed"",""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/57184593"",""close_date"":null,""display_survey_results"":false,""allow_multiple_responses"":true,""anonymous_type"":""not_anonymous"",""id"":""57184593"",""password_enabled"":false,""name"":""Web Link 1"",""date_modified"":""2014-08-26T12:50:00+00:00"",""url"":null,""edit_response_type"":""until_complete"",""sender_email"":null,""date_created"":""2014-08-26T11:14:00+00:00"",""disqualification_message"":""Thank you for completing our survey!"",""type"":""weblink""}
            ");
            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var result = api.GetCollectorDetails(57184593);
            Assert.IsTrue(result.AllowMultipleResponses);
            Assert.AreEqual(Collector.AnonymousOption.NotAnonymous, result.AnonymousType);
            Assert.IsNull(result.CloseDate);
            Assert.AreEqual("closed", result.ClosedPageMessage);
            Assert.AreEqual(new DateTime(2014, 8, 26, 11, 14, 0, DateTimeKind.Utc), result.DateCreated);
            Assert.AreEqual(new DateTime(2014, 8, 26, 12, 50, 0, DateTimeKind.Utc), result.DateModified);
            Assert.IsFalse(result.DisplaySurveyResults);
            Assert.AreEqual("Thank you for completing our survey!", result.DisqualificationMessage);
            Assert.AreEqual(Collector.EditResponseOption.UntilComplete, result.EditResponseType);
            Assert.AreEqual("https://api.surveymonkey.net/v3/collectors/57184593", result.Href);
            Assert.AreEqual(57184593, result.Id);
            Assert.AreEqual("Web Link 1", result.Name);
            Assert.IsFalse(result.PasswordEnabled);
            Assert.AreEqual("https://www.surveymonkey.com", result.RedirectUrl);
            Assert.AreEqual(1, result.ResponseCount);
            Assert.IsNull(result.SenderEmail);
            Assert.AreEqual(Collector.StatusType.Closed, result.Status);
            Assert.AreEqual("Thank you for completing our survey!", result.ThankYouMessage);
            Assert.AreEqual(Collector.CollectorType.Weblink, result.Type);
            Assert.IsNull(result.Url);
        }
    }
}