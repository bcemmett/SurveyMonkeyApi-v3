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

        [Test]
        public void GetSurveyDetailsIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""response_count"":0,""page_count"":2,""buttons_text"":{""done_button"":""Done"",""prev_button"":""Prev"",""exit_button"":""Exit"",""next_button"":""Next""},""custom_variables"":{},""id"":""84718275"",""question_count"":2,""category"":""non-profit"",""preview"":""http:\/\/www.surveymonkey.com\/r\/Preview\/?sm=p2hLUoVMWDiMHvAyMM54c0dgHj5dMXyps4vu30ofkoE_3D"",""language"":""en"",""date_modified"":""2016-09-13T21:27:00"",""title"":""Two page survey"",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/AdWVZnUbwxtN65VGmI7A6PwvGUomM4FqZh394SA9Q9s_3D"",""pages"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84718275\/pages\/253894293"",""description"":""<div>Page 1<\/div>"",""questions"":[{""sorting"":null,""family"":""open_ended"",""subtype"":""single"",""required"":null,""visible"":true,""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84718275\/pages\/253894293\/questions\/1013662081"",""headings"":[{""heading"":""Why is <strong>question<\/strong> 1?""}],""position"":1,""validation"":null,""id"":""1013662081"",""forced_ranking"":false}],""title"":""First page"",""position"":1,""id"":""253894293"",""question_count"":1},{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84718275\/pages\/253894451"",""description"":""<div>The second page<\/div>"",""questions"":[{""sorting"":null,""family"":""single_choice"",""subtype"":""menu"",""required"":null,""answers"":{""choices"":[{""visible"":true,""text"":""A"",""position"":1,""id"":""10568741072""},{""visible"":true,""text"":""B"",""position"":2,""id"":""10568741073""},{""visible"":true,""text"":""CCC"",""position"":3,""id"":""10568741074""}]},""visible"":true,""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84718275\/pages\/253894451\/questions\/1013662957"",""headings"":[{""heading"":""Choose question two""}],""position"":1,""validation"":null,""id"":""1013662957"",""forced_ranking"":false}],""title"":""Page <span style=\""text-decoration: underline;\"">two<\/span>!"",""position"":2,""id"":""253894451"",""question_count"":1}],""summary_url"":""http:\/\/www.surveymonkey.com\/summary\/AdWVZnUbwxtN65VGmI7A6PwvGUomM4FqZh394SA9Q9s_3D"",""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84718275"",""date_created"":""2016-09-13T21:25:00"",""collect_url"":""http:\/\/www.surveymonkey.com\/collect\/list?sm=AdWVZnUbwxtN65VGmI7A6PwvGUomM4FqZh394SA9Q9s_3D"",""edit_url"":""http:\/\/www.surveymonkey.com\/create\/?sm=AdWVZnUbwxtN65VGmI7A6PwvGUomM4FqZh394SA9Q9s_3D""}
            ");
            var api = new SurveyMonkeyApi("key", "token", client);
            var results = api.GetSurveyOverview(84718275);
            Assert.AreEqual(0, results.ResponseCount);
            Assert.AreEqual(2, results.PageCount);
            Assert.AreEqual("Done", results.ButtonsText.DoneButton);
            Assert.AreEqual("Prev", results.ButtonsText.PrevButton);
            Assert.AreEqual("Exit", results.ButtonsText.ExitButton);
            Assert.AreEqual("Next", results.ButtonsText.NextButton);
            Assert.AreEqual(84718275, results.Id);
            Assert.AreEqual(2, results.QuestionCount);
            Assert.AreEqual("non-profit", results.Category);
            Assert.AreEqual(@"http://www.surveymonkey.com/r/Preview/?sm=p2hLUoVMWDiMHvAyMM54c0dgHj5dMXyps4vu30ofkoE_3D", results.Preview);
            Assert.AreEqual("en", results.Language);
            Assert.AreEqual(new DateTime(2016, 9, 13, 21, 25, 0), results.DateCreated);
            Assert.AreEqual("Two page survey", results.Title);
            Assert.AreEqual(@"http://www.surveymonkey.com/analyze/AdWVZnUbwxtN65VGmI7A6PwvGUomM4FqZh394SA9Q9s_3D", results.AnalyzeUrl);
            Assert.AreEqual(@"http://www.surveymonkey.com/summary/AdWVZnUbwxtN65VGmI7A6PwvGUomM4FqZh394SA9Q9s_3D", results.SummaryUrl);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/surveys/84718275", results.Href);
            Assert.AreEqual(@"http://www.surveymonkey.com/collect/list?sm=AdWVZnUbwxtN65VGmI7A6PwvGUomM4FqZh394SA9Q9s_3D", results.CollectUrl);
            Assert.AreEqual(@"http://www.surveymonkey.com/create/?sm=AdWVZnUbwxtN65VGmI7A6PwvGUomM4FqZh394SA9Q9s_3D", results.EditUrl);
            Assert.AreEqual(new DateTime(2016, 9, 13, 21, 27, 0), results.DateModified);

            Assert.AreEqual(253894293, results.Pages.First().Id);
            Assert.AreEqual("<div>Page 1</div>", results.Pages.First().Description);
            Assert.AreEqual("https://api.surveymonkey.net/v3/surveys/84718275/pages/253894293", results.Pages.First().Href);
            Assert.AreEqual(1, results.Pages.First().Position);
            Assert.AreEqual(1, results.Pages.First().QuestionCount);
            Assert.AreEqual("First page", results.Pages.First().Title);

            Assert.AreEqual(QuestionFamily.OpenEnded, results.Pages.First().Questions.First().Family);
            Assert.AreEqual(QuestionSubtype.Single, results.Pages.First().Questions.First().Subtype);
            Assert.AreEqual(false, results.Pages.First().Questions.First().ForcedRanking);
            Assert.AreEqual("Why is <strong>question</strong> 1?", results.Pages.First().Questions.First().Headings.First().Heading);
            Assert.AreEqual("https://api.surveymonkey.net/v3/surveys/84718275/pages/253894293/questions/1013662081", results.Pages.First().Questions.First().Href);
            Assert.AreEqual(1013662081, results.Pages.First().Questions.First().Id);
            Assert.AreEqual(1, results.Pages.First().Questions.First().Position);
            Assert.AreEqual(null, results.Pages.First().Questions.First().Required);
            Assert.AreEqual(null, results.Pages.First().Questions.First().Sorting);
            Assert.AreEqual(null, results.Pages.First().Questions.First().Validation);
            Assert.AreEqual(true, results.Pages.First().Questions.First().Visible);
        }
    }
}