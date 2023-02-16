using System;
using System.Linq;
using NUnit.Framework;
using SurveyMonkey;
using SurveyMonkey.Containers;
using SurveyMonkey.Enums;

namespace SurveyMonkeyTests
{
    [TestFixtureSource(typeof(AsyncTestFixtureSource))]
    public class GetCollectorTests
    {
        private readonly bool _useAsync;

        public GetCollectorTests(bool useAsync)
        {
            _useAsync = useAsync;
        }

        [Test]
        public void GetCollectorListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":1000,""total"":3,""data"":[{""status"":""new"",""name"":""Email Invitation 1"",""date_modified"":""2016-09-17T21:36:00+00:00"",""response_count"":0,""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91664733"",""date_created"":""2016-09-17T21:36:00+00:00"",""type"":""email"",""id"":""91664733""},{""status"":""closed"",""name"":""Web Link 1"",""date_modified"":""2014-08-26T12:50:00+00:00"",""url"":""https:\/\/www.surveymonkey.com\/r\/9QBY7BQ"",""response_count"":1,""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/57184593"",""date_created"":""2014-08-26T11:14:00+00:00"",""type"":""weblink"",""id"":""57184593""},{""status"":""closed"",""name"":""Web Link 2"",""date_modified"":""2014-08-26T12:50:00+00:00"",""url"":""https:\/\/www.surveymonkey.com\/r\/9TWKVDN"",""response_count"":98,""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/57185230"",""date_created"":""2014-08-26T11:42:00+00:00"",""type"":""weblink"",""id"":""57185230""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/55249163\/collectors?page=1&per_page=1000""}}
            ");
            var api = new SurveyMonkeyApi("TestOAuthToken", client);
            var results = _useAsync
                ? api.GetCollectorListAsync(55249163).GetAwaiter().GetResult()
                : api.GetCollectorList(55249163);
            
            Assert.AreEqual(1, client.Requests.Count);
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual(91664733, results.First().Id);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/collectors/91664733", results.First().Href);
            Assert.AreEqual("Web Link 2", results.Last().Name);
            Assert.AreEqual(Collector.StatusType.New, results.First().Status);
            Assert.AreEqual(0, results.First().ResponseCount);
            Assert.IsNull(results.First().AllowMultipleResponses);
        }

        [Test]
        public void GetCollectorDetailWeblinkIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""status"":""closed"",""redirect_url"":""https:\/\/www.surveymonkey.com"",""thank_you_message"":""Thank you for completing our survey!"",""response_count"":1,""closed_page_message"":""closed"",""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/57184593"",""close_date"":null,""display_survey_results"":false,""allow_multiple_responses"":true,""anonymous_type"":""not_anonymous"",""id"":""57184593"",""password_enabled"":false,""name"":""Web Link 1"",""date_modified"":""2014-08-26T12:50:00+00:00"",""url"":null,""edit_response_type"":""until_complete"",""sender_email"":null,""date_created"":""2014-08-26T11:14:00+00:00"",""disqualification_message"":""Thank you for completing our survey!"",""type"":""weblink"",""response_limit"":3,""redirect_type"":""url""}
            ");
            var api = new SurveyMonkeyApi("TestOAuthToken", client);
            var result = _useAsync
                ? api.GetCollectorDetailsAsync(57184593).GetAwaiter().GetResult()
                : api.GetCollectorDetails(57184593);

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
            Assert.AreEqual(3, result.ResponseLimit);
            Assert.AreEqual(Collector.RedirectOption.Url, result.RedirectType);
            Assert.IsNull(result.Url);
        }

        [Test]
        public void GetMessageListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":1000,""total"":1,""data"":[{""status"":""sent"",""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/85470742\/messages\/29296390"",""type"":""invite"",""id"":""29296390""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/85470742\/messages?page=1&per_page=1000""}}
            ");
            var api = new SurveyMonkeyApi("TestOAuthToken", client);
            var result = _useAsync
                ? api.GetMessageListAsync(85470742).GetAwaiter().GetResult()
                : api.GetMessageList(85470742);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("https://api.surveymonkey.net/v3/collectors/85470742/messages/29296390", result.First().Href);
            Assert.AreEqual(29296390, result.First().Id);
            Assert.AreEqual(MessageStatus.Sent, result.First().Status);
            Assert.AreEqual(MessageType.Invite, result.First().Type);
        }

        [Test]
        public void GetMessageDetailsIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""status"":""sent"",""body"":""Hi,\r\nHere's your survey: [SurveyLink]\r\nUnsubscribe: [OptOutLink]\r\n[FooterLink]"",""recipient_status"":null,""is_branding_enabled"":true,""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/85470742\/messages\/29296390"",""is_scheduled"":true,""scheduled_date"":""2016-05-10T16:28:06+00:00"",""date_created"":""2016-05-10T16:23:04+00:00"",""type"":""invite"",""id"":""29296390"",""subject"":""MySubjectLine""}
            ");
            var api = new SurveyMonkeyApi("TestOAuthToken", client);
            var result = _useAsync
                ? api.GetMessageDetailsAsync(85470742, 29296390).GetAwaiter().GetResult()
                : api.GetMessageDetails(85470742, 29296390);

            Assert.AreEqual("Hi,\r\nHere\'s your survey: [SurveyLink]\r\nUnsubscribe: [OptOutLink]\r\n[FooterLink]", result.Body);
            Assert.AreEqual(new DateTime(2016, 5, 10, 16, 23, 4, DateTimeKind.Utc), result.DateCreated);
            Assert.AreEqual("https://api.surveymonkey.net/v3/collectors/85470742/messages/29296390", result.Href);
            Assert.AreEqual(29296390, result.Id);
            Assert.IsTrue(result.IsBrandingEnabled);
            Assert.IsTrue(result.IsScheduled);
            Assert.AreEqual(new DateTime(2016, 5, 10, 16, 28, 6, DateTimeKind.Utc), result.ScheduledDate);
            Assert.AreEqual(MessageStatus.Sent, result.Status);
            Assert.AreEqual(MessageType.Invite, result.Type);
        }

        [Test]
        public void GetMessageRecipientListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":1000,""total"":2,""data"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/85470742\/recipients\/2407626836"",""id"":""2407626836"",""email"":""test+12@gmail.com""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/85470742\/recipients\/2407626837"",""id"":""2407626837"",""email"":""test+13@gmail.com""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/85470742\/messages\/29296390\/recipients?page=1&per_page=1000""}}
            ");
            var api = new SurveyMonkeyApi("TestOAuthToken", client);
            var result = _useAsync
                ? api.GetMessageRecipientListAsync(85470742, 29296390).GetAwaiter().GetResult()
                : api.GetMessageRecipientList(85470742, 29296390);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("https://api.surveymonkey.net/v3/collectors/85470742/recipients/2407626836", result.First().Href);
            Assert.AreEqual(2407626836, result.First().Id);
            Assert.AreEqual("test+13@gmail.com", result.Last().Email);
            Assert.IsNull(result.First().MailStatus);
        }

        [Test]
        public void GetCollectorRecipientListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":1000,""total"":2,""data"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/94732812\/recipients\/2751878525"",""id"":""2751878525"",""email"":""test1@test123456789.com""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/94732812\/recipients\/2751878526"",""id"":""2751878526"",""email"":""test2@test123456789.com""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/94732812\/recipients?page=1&per_page=1000""}}
            ");
            var api = new SurveyMonkeyApi("TestOAuthToken", client);
            var result = _useAsync
                ? api.GetCollectorRecipientListAsync(94732812).GetAwaiter().GetResult()
                : api.GetCollectorRecipientList(94732812);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2751878525, result.First().Id);
            Assert.AreEqual("test2@test123456789.com", result.Last().Email);
            Assert.IsNull(result.First().MailStatus);
        }

        [Test]
        public void GetRecipientDetailsIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""survey_response_status"":""not_responded"",""mail_status"":""sent"",""id"":""2407626836"",""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/85470742\/recipients\/2407626836"",""remove_link"":""https:\/\/www.surveymonkey.com\/optout.aspx?sm=blablabla"",""survey_id"":""53774320"",""email"":""test+12@gmail.com"",""first_name"":""Firstname"",""last_name"":""lastName"",""survey_link"":""https:\/\/www.surveymonkey.com\/r\/?sm=blabla""}
            ");
            var api = new SurveyMonkeyApi("TestOAuthToken", client);
            var result = _useAsync
                ? api.GetRecipientDetailsAsync(85470742, 2407626836).GetAwaiter().GetResult()
                : api.GetRecipientDetails(85470742, 2407626836);

            Assert.AreEqual("test+12@gmail.com", result.Email);
            Assert.AreEqual("https://api.surveymonkey.net/v3/collectors/85470742/recipients/2407626836", result.Href);
            Assert.AreEqual(2407626836, result.Id);
            Assert.AreEqual(MessageStatus.Sent, result.MailStatus);
            Assert.AreEqual("https://www.surveymonkey.com/optout.aspx?sm=blablabla", result.RemoveLink);
            Assert.AreEqual(53774320, result.SurveyId);
            Assert.AreEqual("https://www.surveymonkey.com/r/?sm=blabla", result.SurveyLink);
            Assert.AreEqual(RecipientSurveyResponseStatus.NotResponded, result.SurveyResponseStatus);
            Assert.AreEqual("Firstname", result.FirstName);
            Assert.AreEqual("lastName", result.LastName);
        }
    }
}