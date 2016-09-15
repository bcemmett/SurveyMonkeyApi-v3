using System;
using System.Linq;
using NUnit.Framework;
using SurveyMonkey;
using SurveyMonkey.Enums;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class GetResponseTests
    {
        [Test]
        public void GetSurveyResponseDetailsIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":50,""total"":3,""data"":[{""total_time"":8,""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420283"",""custom_variables"":{},""ip_address"":""81.187.77.182"",""id"":""4968420283"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:09+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420283"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315476""}]},{""id"":""1013185659"",""answers"":[{""text"":""22222""}]}]}],""page_path"":[],""recipient_id"":""123456789"",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:01+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=db1E_2B5FvGitK17_2F_2F8_2Blnhcl_2BCTwKHT5dPY9EBCDJmi8tUeGDo34qJJ5CuL7ceRS7"",""metadata"":{}},{""total_time"":7,""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420510"",""custom_variables"":{},""ip_address"":""81.187.77.182"",""id"":""4968420510"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:21+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420510"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315476""}]},{""id"":""1013185659"",""answers"":[{""text"":""2!""}]}]}],""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:14+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=Lx7StOGuxNDuVUAr8BPyjg2ViuVtA8zOHdXIxBAegrKWNdxw4B0iMvxwLegwwvY3"",""metadata"":{}},{""total_time"":15,""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420845"",""custom_variables"":{},""ip_address"":""81.187.77.182"",""id"":""4968420845"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:40+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420845"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315477""}]},{""id"":""1013185659"",""answers"":[{""text"":""The second!""}]}]}],""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:24+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=bc2BsaGJJWvkzcJSuPORfnpY5wGZdCs_2F0hBWIXcOYE1FZDU3KKxUI_2BgiFpNVzGc4"",""metadata"":{}}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/bulk?page=1&per_page=50""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetSurveyResponseDetails(84672934);
            Assert.AreEqual(1, client.Requests.Count);

            Assert.AreEqual(4968420283, results.First().Id);
            Assert.AreEqual(new DateTime(2016, 9, 13, 7, 29, 9, DateTimeKind.Utc).ToLocalTime(), results.First().DateModified);
            Assert.AreEqual(new DateTime(2016, 9, 13, 7, 29, 01, DateTimeKind.Utc).ToLocalTime(), results.First().DateCreated);
            Assert.AreEqual(8, results.First().TotalTime);
            Assert.IsEmpty(results.First().CustomVariables);
            Assert.AreEqual("81.187.77.182", results.First().IpAddress);
            Assert.AreEqual(ResponseStatus.Completed, results.First().ResponseStatus);
            Assert.AreEqual(String.Empty, results.First().CustomValue);
            Assert.IsEmpty(results.First().LogicPath);
            Assert.IsEmpty(results.First().PagePath);
            Assert.IsEmpty(results.First().Metadata);
            Assert.AreEqual(123456789, results.First().RecipientId);
            Assert.AreEqual(84672934, results.First().SurveyId);
            Assert.AreEqual(91395530, results.First().CollectorId);
            Assert.AreEqual(CollectionMode.Default, results.First().CollectionMode);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/surveys/84672934/responses/4968420283", results.First().Href);
            Assert.AreEqual(@"http://www.surveymonkey.com/r/?sm=db1E_2B5FvGitK17_2F_2F8_2Blnhcl_2BCTwKHT5dPY9EBCDJmi8tUeGDo34qJJ5CuL7ceRS7", results.First().EditUrl);
            Assert.AreEqual(@"http://www.surveymonkey.com/analyze/browse/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420283", results.First().AnalyzeUrl);

            Assert.AreEqual(253784818, results.First().Pages.First().Id);
            Assert.AreEqual(1013185278, results.First().Pages.First().Questions.First().Id);
            Assert.AreEqual(10565315476, results.First().Pages.First().Questions.First().Answers.First().ChoiceId);
            Assert.AreEqual(1013185659, results.First().Pages.First().Questions.Last().Id);
            Assert.AreEqual("22222", results.First().Pages.First().Questions.Last().Answers.First().Text);          
        }

        [Test]
        public void GetSurveyResponseOverviewIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":50,""total"":3,""data"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420283"",""id"":""4968420283""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420510"",""id"":""4968420510""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420845"",""id"":""4968420845""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses?page=1&per_page=50""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetSurveyResponseDetails(84672934);
            Assert.AreEqual(1, client.Requests.Count);

            Assert.AreEqual(4968420283, results.First().Id);
            Assert.IsNull(results.First().DateModified);
            Assert.IsNull(results.First().DateCreated);
            Assert.IsNull(results.First().TotalTime);
            Assert.IsNull(results.First().CustomVariables);
            Assert.IsNull(results.First().IpAddress);
            Assert.IsNull(results.First().ResponseStatus);
            Assert.IsNull(results.First().CustomValue);
            Assert.IsNull(results.First().LogicPath);
            Assert.IsNull(results.First().PagePath);
            Assert.IsNull(results.First().Metadata);
            Assert.IsNull(results.First().RecipientId);
            Assert.IsNull(results.First().SurveyId);
            Assert.IsNull(results.First().CollectorId);
            Assert.IsNull(results.First().CollectionMode);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/surveys/84672934/responses/4968420283", results.First().Href);
            Assert.IsNull(results.First().EditUrl);
            Assert.IsNull(results.First().AnalyzeUrl);
            Assert.IsNull(results.First().Pages);
        }

        [Test]
        public void GetCollectorResponseDetailsIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":50,""total"":3,""data"":[{""total_time"":8,""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420283"",""custom_variables"":{},""ip_address"":""81.187.77.182"",""id"":""4968420283"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:09+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420283"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315476""}]},{""id"":""1013185659"",""answers"":[{""text"":""22222""}]}]}],""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:01+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=db1E_2B5FvGitK17_2F_2F8_2Blnhcl_2BCTwKHT5dPY9EBCDJmi8tUeGDo34qJJ5CuL7ceRS7"",""metadata"":{}},{""total_time"":7,""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420510"",""custom_variables"":{},""ip_address"":""81.187.77.182"",""id"":""4968420510"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:21+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420510"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315476""}]},{""id"":""1013185659"",""answers"":[{""text"":""2!""}]}]}],""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:14+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=Lx7StOGuxNDuVUAr8BPyjg2ViuVtA8zOHdXIxBAegrKWNdxw4B0iMvxwLegwwvY3"",""metadata"":{}},{""total_time"":15,""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420845"",""custom_variables"":{},""ip_address"":""81.187.77.182"",""id"":""4968420845"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:40+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420845"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315477""}]},{""id"":""1013185659"",""answers"":[{""text"":""The second!""}]}]}],""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:24+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=bc2BsaGJJWvkzcJSuPORfnpY5wGZdCs_2F0hBWIXcOYE1FZDU3KKxUI_2BgiFpNVzGc4"",""metadata"":{}}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/bulk?page=1&per_page=50""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetCollectorResponseDetails(91395530);
            Assert.AreEqual(1, client.Requests.Count);

            Assert.AreEqual(4968420283, results.First().Id);
            Assert.AreEqual(new DateTime(2016, 9, 13, 7, 29, 9, DateTimeKind.Utc).ToLocalTime(), results.First().DateModified);
            Assert.AreEqual(new DateTime(2016, 9, 13, 7, 29, 01, DateTimeKind.Utc).ToLocalTime(), results.First().DateCreated);
            Assert.AreEqual(8, results.First().TotalTime);
            Assert.IsEmpty(results.First().CustomVariables);
            Assert.AreEqual("81.187.77.182", results.First().IpAddress);
            Assert.AreEqual(ResponseStatus.Completed, results.First().ResponseStatus);
            Assert.AreEqual(String.Empty, results.First().CustomValue);
            Assert.IsEmpty(results.First().LogicPath);
            Assert.IsEmpty(results.First().PagePath);
            Assert.IsEmpty(results.First().Metadata);
            Assert.IsNull(results.First().RecipientId);
            Assert.AreEqual(84672934, results.First().SurveyId);
            Assert.AreEqual(91395530, results.First().CollectorId);
            Assert.AreEqual(CollectionMode.Default, results.First().CollectionMode);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/collectors/91395530/responses/4968420283", results.First().Href);
            Assert.AreEqual(@"http://www.surveymonkey.com/r/?sm=db1E_2B5FvGitK17_2F_2F8_2Blnhcl_2BCTwKHT5dPY9EBCDJmi8tUeGDo34qJJ5CuL7ceRS7", results.First().EditUrl);
            Assert.AreEqual(@"http://www.surveymonkey.com/analyze/browse/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420283", results.First().AnalyzeUrl);

            Assert.AreEqual(253784818, results.First().Pages.First().Id);
            Assert.AreEqual(1013185278, results.First().Pages.First().Questions.First().Id);
            Assert.AreEqual(10565315476, results.First().Pages.First().Questions.First().Answers.First().ChoiceId);
            Assert.AreEqual(1013185659, results.First().Pages.First().Questions.Last().Id);
            Assert.AreEqual("22222", results.First().Pages.First().Questions.Last().Answers.First().Text);
        }

        [Test]
        public void GetCollectorResponseOverviewIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":50,""total"":3,""data"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420283"",""id"":""4968420283""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420510"",""id"":""4968420510""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420845"",""id"":""4968420845""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses?page=1&per_page=50""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetCollectorResponseDetails(91395530);
            Assert.AreEqual(1, client.Requests.Count);

            Assert.AreEqual(4968420283, results.First().Id);
            Assert.IsNull(results.First().DateModified);
            Assert.IsNull(results.First().DateCreated);
            Assert.IsNull(results.First().TotalTime);
            Assert.IsNull(results.First().CustomVariables);
            Assert.IsNull(results.First().IpAddress);
            Assert.IsNull(results.First().ResponseStatus);
            Assert.IsNull(results.First().CustomValue);
            Assert.IsNull(results.First().LogicPath);
            Assert.IsNull(results.First().PagePath);
            Assert.IsNull(results.First().Metadata);
            Assert.IsNull(results.First().RecipientId);
            Assert.IsNull(results.First().SurveyId);
            Assert.IsNull(results.First().CollectorId);
            Assert.IsNull(results.First().CollectionMode);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/collectors/91395530/responses/4968420283", results.First().Href);
            Assert.IsNull(results.First().EditUrl);
            Assert.IsNull(results.First().AnalyzeUrl);
            Assert.IsNull(results.First().Pages);
        }
    }
}