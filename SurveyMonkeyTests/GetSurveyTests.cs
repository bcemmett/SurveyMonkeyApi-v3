using System;
using System.Linq;
using NUnit.Framework;
using SurveyMonkey;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class GetSurveyTests
    {
        [Test]
        public void GetSurveyListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":50,""total"":2,""data"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/55249163"",""id"":""55249163"",""title"":""All Question Types Test""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934"",""id"":""84672934"",""title"":""Two Question Survey""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/?page=1&per_page=50""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetSurveys();
            Assert.AreEqual(1, client.Requests.Count);
            Assert.AreEqual(55249163, results.First().Id);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/surveys/55249163", results.First().Href);
            Assert.AreEqual("Two Question Survey", results.Last().Title);
        }


        [Test]
        public void GetSurveyOverviewIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""response_count"":3,""page_count"":1,""buttons_text"":{""done_button"":""Done"",""prev_button"":""Prev"",""exit_button"":""Exit"",""next_button"":""Next""},""custom_variables"":{},""id"":""84672934"",""question_count"":2,""category"":""community"",""preview"":""http:\/\/www.surveymonkey.com\/r\/Preview\/?sm=5D7q7s9Ip1l2yW2UWuInqzCAAX34ti6xYe_2F8M_2FE1CvU_3D"",""language"":""en"",""date_modified"":""2016-09-13T07:30:00"",""title"":""Two Question Survey"",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D"",""summary_url"":""http:\/\/www.surveymonkey.com\/summary\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D"",""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934"",""date_created"":""2016-09-13T07:25:00"",""collect_url"":""http:\/\/www.surveymonkey.com\/collect\/list?sm=9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D"",""edit_url"":""http:\/\/www.surveymonkey.com\/create\/?sm=9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D""}
            ");
            var api = new SurveyMonkeyApi("key", "token", client);
            var results = api.GetSurveyOverview(84672934);
            Assert.AreEqual(3, results.ResponseCount);
            Assert.AreEqual(1, results.PageCount);
            Assert.AreEqual("Done", results.ButtonsText.DoneButton);
            Assert.AreEqual("Prev", results.ButtonsText.PrevButton);
            Assert.AreEqual("Exit", results.ButtonsText.ExitButton);
            Assert.AreEqual("Next", results.ButtonsText.NextButton);
            Assert.AreEqual(84672934, results.Id);
            Assert.AreEqual(2, results.QuestionCount);
            Assert.AreEqual("community", results.Category);
            Assert.AreEqual("Next", results.ButtonsText.NextButton);
            Assert.AreEqual(@"http://www.surveymonkey.com/r/Preview/?sm=5D7q7s9Ip1l2yW2UWuInqzCAAX34ti6xYe_2F8M_2FE1CvU_3D", results.Preview);
            Assert.AreEqual("en", results.Language);
            Assert.AreEqual(new DateTime(2016, 9, 13, 7, 25, 0), results.DateCreated);
            Assert.AreEqual("Two Question Survey", results.Title);
            Assert.AreEqual(@"http://www.surveymonkey.com/analyze/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D", results.AnalyzeUrl);
            Assert.AreEqual(@"http://www.surveymonkey.com/summary/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D", results.SummaryUrl);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/surveys/84672934", results.Href);
            Assert.AreEqual(@"http://www.surveymonkey.com/collect/list?sm=9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D", results.CollectUrl);
            Assert.AreEqual(@"http://www.surveymonkey.com/create/?sm=9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D", results.EditUrl);
            Assert.AreEqual(new DateTime(2016, 9, 13, 07, 30, 0), results.DateModified);
        }
    }
}