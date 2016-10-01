using System;
using System.Linq;
using NUnit.Framework;
using SurveyMonkey;
using SurveyMonkey.Enums;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class GetResponseTests
    {
        [Test]
        public void GetSurveyResponseDetailListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":50,""total"":3,""data"":[{""total_time"":8,""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420283"",""custom_variables"":{},""ip_address"":""18.187.48.612"",""id"":""4968420283"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:09+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420283"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315476""}]},{""id"":""1013185659"",""answers"":[{""text"":""22222""}]}]}],""page_path"":[],""recipient_id"":""123456789"",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:01+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=db1E_2B5FvGitK17_2F_2F8_2Blnhcl_2BCTwKHT5dPY9EBCDJmi8tUeGDo34qJJ5CuL7ceRS7"",""metadata"":{}},{""total_time"":7,""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420510"",""custom_variables"":{},""ip_address"":""18.187.48.612"",""id"":""4968420510"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:21+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420510"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315476""}]},{""id"":""1013185659"",""answers"":[{""text"":""2!""}]}]}],""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:14+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=Lx7StOGuxNDuVUAr8BPyjg2ViuVtA8zOHdXIxBAegrKWNdxw4B0iMvxwLegwwvY3"",""metadata"":{}},{""total_time"":15,""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420845"",""custom_variables"":{},""ip_address"":""18.187.48.612"",""id"":""4968420845"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:40+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420845"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315477""}]},{""id"":""1013185659"",""answers"":[{""text"":""The second!""}]}]}],""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:24+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=bc2BsaGJJWvkzcJSuPORfnpY5wGZdCs_2F0hBWIXcOYE1FZDU3KKxUI_2BgiFpNVzGc4"",""metadata"":{}}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/bulk?page=1&per_page=50""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetResponseDetailList(84672934, SurveyMonkeyApi.ObjectType.Survey);
            Assert.AreEqual(1, client.Requests.Count);

            Assert.AreEqual(4968420283, results.First().Id);
            Assert.AreEqual(new DateTime(2016, 9, 13, 7, 29, 9, DateTimeKind.Utc), results.First().DateModified);
            Assert.AreEqual(new DateTime(2016, 9, 13, 7, 29, 1, DateTimeKind.Utc), results.First().DateCreated);
            Assert.AreEqual(8, results.First().TotalTime);
            Assert.IsEmpty(results.First().CustomVariables);
            Assert.AreEqual("18.187.48.612", results.First().IpAddress);
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
        public void GetSurveyResponseOverviewListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":50,""total"":3,""data"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420283"",""id"":""4968420283""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420510"",""id"":""4968420510""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420845"",""id"":""4968420845""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses?page=1&per_page=50""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetResponseOverviewList(84672934, SurveyMonkeyApi.ObjectType.Survey);
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
        public void GetCollectorResponseDetailListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":50,""total"":3,""data"":[{""total_time"":8,""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420283"",""custom_variables"":{},""ip_address"":""18.187.48.612"",""id"":""4968420283"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:09+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420283"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315476""}]},{""id"":""1013185659"",""answers"":[{""text"":""22222""}]}]}],""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:01+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=db1E_2B5FvGitK17_2F_2F8_2Blnhcl_2BCTwKHT5dPY9EBCDJmi8tUeGDo34qJJ5CuL7ceRS7"",""metadata"":{}},{""total_time"":7,""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420510"",""custom_variables"":{},""ip_address"":""18.187.48.612"",""id"":""4968420510"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:21+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420510"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315476""}]},{""id"":""1013185659"",""answers"":[{""text"":""2!""}]}]}],""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:14+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=Lx7StOGuxNDuVUAr8BPyjg2ViuVtA8zOHdXIxBAegrKWNdxw4B0iMvxwLegwwvY3"",""metadata"":{}},{""total_time"":15,""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420845"",""custom_variables"":{},""ip_address"":""18.187.48.612"",""id"":""4968420845"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:40+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420845"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315477""}]},{""id"":""1013185659"",""answers"":[{""text"":""The second!""}]}]}],""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:24+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=bc2BsaGJJWvkzcJSuPORfnpY5wGZdCs_2F0hBWIXcOYE1FZDU3KKxUI_2BgiFpNVzGc4"",""metadata"":{}}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/bulk?page=1&per_page=50""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetResponseDetailList(91395530, SurveyMonkeyApi.ObjectType.Collector);
            Assert.AreEqual(1, client.Requests.Count);

            Assert.AreEqual(4968420283, results.First().Id);
            Assert.AreEqual(new DateTime(2016, 9, 13, 7, 29, 9, DateTimeKind.Utc), results.First().DateModified);
            Assert.AreEqual(new DateTime(2016, 9, 13, 7, 29, 01, DateTimeKind.Utc), results.First().DateCreated);
            Assert.AreEqual(8, results.First().TotalTime);
            Assert.IsEmpty(results.First().CustomVariables);
            Assert.AreEqual("18.187.48.612", results.First().IpAddress);
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
        public void GetCollectorResponseOverviewListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":50,""total"":3,""data"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420283"",""id"":""4968420283""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420510"",""id"":""4968420510""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420845"",""id"":""4968420845""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses?page=1&per_page=50""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetResponseOverviewList(91395530, SurveyMonkeyApi.ObjectType.Collector);
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

        [Test]
        public void GetResponseListPagesAndStopsWhenIncompletePageIsReturned()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":100,""total"":299,""data"":[{""id"":""1""},{""id"":""2""},{""id"":""3""},{""id"":""4""},{""id"":""5""},{""id"":""6""},{""id"":""7""},{""id"":""8""},{""id"":""9""},{""id"":""10""},{""id"":""11""},{""id"":""12""},{""id"":""13""},{""id"":""14""},{""id"":""15""},{""id"":""16""},{""id"":""17""},{""id"":""18""},{""id"":""19""},{""id"":""20""},{""id"":""21""},{""id"":""22""},{""id"":""23""},{""id"":""24""},{""id"":""25""},{""id"":""26""},{""id"":""27""},{""id"":""28""},{""id"":""29""},{""id"":""30""},{""id"":""31""},{""id"":""32""},{""id"":""33""},{""id"":""34""},{""id"":""35""},{""id"":""36""},{""id"":""37""},{""id"":""38""},{""id"":""39""},{""id"":""40""},{""id"":""41""},{""id"":""42""},{""id"":""43""},{""id"":""44""},{""id"":""45""},{""id"":""46""},{""id"":""47""},{""id"":""48""},{""id"":""49""},{""id"":""50""},{""id"":""51""},{""id"":""52""},{""id"":""53""},{""id"":""54""},{""id"":""55""},{""id"":""56""},{""id"":""57""},{""id"":""58""},{""id"":""59""},{""id"":""60""},{""id"":""61""},{""id"":""62""},{""id"":""63""},{""id"":""64""},{""id"":""65""},{""id"":""66""},{""id"":""67""},{""id"":""68""},{""id"":""69""},{""id"":""70""},{""id"":""71""},{""id"":""72""},{""id"":""73""},{""id"":""74""},{""id"":""75""},{""id"":""76""},{""id"":""77""},{""id"":""78""},{""id"":""79""},{""id"":""80""},{""id"":""81""},{""id"":""82""},{""id"":""83""},{""id"":""84""},{""id"":""85""},{""id"":""86""},{""id"":""87""},{""id"":""88""},{""id"":""89""},{""id"":""90""},{""id"":""91""},{""id"":""92""},{""id"":""93""},{""id"":""94""},{""id"":""95""},{""id"":""96""},{""id"":""97""},{""id"":""98""},{""id"":""99""},{""id"":""100""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses?page=1&per_page=100""}}
            ");
            client.Responses.Add(@"
                {""per_page"":100,""total"":299,""data"":[{""id"":""101""},{""id"":""102""},{""id"":""103""},{""id"":""104""},{""id"":""105""},{""id"":""106""},{""id"":""107""},{""id"":""108""},{""id"":""109""},{""id"":""110""},{""id"":""111""},{""id"":""112""},{""id"":""113""},{""id"":""114""},{""id"":""115""},{""id"":""116""},{""id"":""117""},{""id"":""118""},{""id"":""119""},{""id"":""120""},{""id"":""121""},{""id"":""122""},{""id"":""123""},{""id"":""124""},{""id"":""125""},{""id"":""126""},{""id"":""127""},{""id"":""128""},{""id"":""129""},{""id"":""130""},{""id"":""131""},{""id"":""132""},{""id"":""133""},{""id"":""134""},{""id"":""135""},{""id"":""136""},{""id"":""137""},{""id"":""138""},{""id"":""139""},{""id"":""140""},{""id"":""141""},{""id"":""142""},{""id"":""143""},{""id"":""144""},{""id"":""145""},{""id"":""146""},{""id"":""147""},{""id"":""148""},{""id"":""149""},{""id"":""150""},{""id"":""151""},{""id"":""152""},{""id"":""153""},{""id"":""154""},{""id"":""155""},{""id"":""156""},{""id"":""157""},{""id"":""158""},{""id"":""159""},{""id"":""160""},{""id"":""161""},{""id"":""162""},{""id"":""163""},{""id"":""164""},{""id"":""165""},{""id"":""166""},{""id"":""167""},{""id"":""168""},{""id"":""169""},{""id"":""170""},{""id"":""171""},{""id"":""172""},{""id"":""173""},{""id"":""174""},{""id"":""175""},{""id"":""176""},{""id"":""177""},{""id"":""178""},{""id"":""179""},{""id"":""180""},{""id"":""181""},{""id"":""182""},{""id"":""183""},{""id"":""184""},{""id"":""185""},{""id"":""186""},{""id"":""187""},{""id"":""188""},{""id"":""189""},{""id"":""190""},{""id"":""191""},{""id"":""192""},{""id"":""193""},{""id"":""194""},{""id"":""195""},{""id"":""196""},{""id"":""197""},{""id"":""198""},{""id"":""199""},{""id"":""200""}],""page"":2,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/?page=2&per_page=100""}}
            ");
            client.Responses.Add(@"
                {""per_page"":100,""total"":299,""data"":[{""id"":""201""},{""id"":""202""},{""id"":""203""},{""id"":""204""},{""id"":""205""},{""id"":""206""},{""id"":""207""},{""id"":""208""},{""id"":""209""},{""id"":""210""},{""id"":""211""},{""id"":""212""},{""id"":""213""},{""id"":""214""},{""id"":""215""},{""id"":""216""},{""id"":""217""},{""id"":""218""},{""id"":""219""},{""id"":""220""},{""id"":""221""},{""id"":""222""},{""id"":""223""},{""id"":""224""},{""id"":""225""},{""id"":""226""},{""id"":""227""},{""id"":""228""},{""id"":""229""},{""id"":""230""},{""id"":""231""},{""id"":""232""},{""id"":""233""},{""id"":""234""},{""id"":""235""},{""id"":""236""},{""id"":""237""},{""id"":""238""},{""id"":""239""},{""id"":""240""},{""id"":""241""},{""id"":""242""},{""id"":""243""},{""id"":""244""},{""id"":""245""},{""id"":""246""},{""id"":""247""},{""id"":""248""},{""id"":""249""},{""id"":""250""},{""id"":""251""},{""id"":""252""},{""id"":""253""},{""id"":""254""},{""id"":""255""},{""id"":""256""},{""id"":""257""},{""id"":""258""},{""id"":""259""},{""id"":""260""},{""id"":""261""},{""id"":""262""},{""id"":""263""},{""id"":""264""},{""id"":""265""},{""id"":""266""},{""id"":""267""},{""id"":""268""},{""id"":""269""},{""id"":""270""},{""id"":""271""},{""id"":""272""},{""id"":""273""},{""id"":""274""},{""id"":""275""},{""id"":""276""},{""id"":""277""},{""id"":""278""},{""id"":""279""},{""id"":""280""},{""id"":""281""},{""id"":""282""},{""id"":""283""},{""id"":""284""},{""id"":""285""},{""id"":""286""},{""id"":""287""},{""id"":""288""},{""id"":""289""},{""id"":""290""},{""id"":""291""},{""id"":""292""},{""id"":""293""},{""id"":""294""},{""id"":""295""},{""id"":""296""},{""id"":""297""},{""id"":""298""},{""id"":""299""}],""page"":3,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/?page=3&per_page=100""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetResponseDetailList(84672934, SurveyMonkeyApi.ObjectType.Survey);
            Assert.AreEqual(299, results.Count);
            Assert.AreEqual(299, results.Last().Id);
            Assert.AreEqual(1, results.First().Id);
            Assert.AreEqual(3, client.Requests.Count);
        }

        [Test]
        public void GetResponseListPagesAndContinuesWhenCompletePageIsReturned()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":100,""total"":300,""data"":[{""id"":""1""},{""id"":""2""},{""id"":""3""},{""id"":""4""},{""id"":""5""},{""id"":""6""},{""id"":""7""},{""id"":""8""},{""id"":""9""},{""id"":""10""},{""id"":""11""},{""id"":""12""},{""id"":""13""},{""id"":""14""},{""id"":""15""},{""id"":""16""},{""id"":""17""},{""id"":""18""},{""id"":""19""},{""id"":""20""},{""id"":""21""},{""id"":""22""},{""id"":""23""},{""id"":""24""},{""id"":""25""},{""id"":""26""},{""id"":""27""},{""id"":""28""},{""id"":""29""},{""id"":""30""},{""id"":""31""},{""id"":""32""},{""id"":""33""},{""id"":""34""},{""id"":""35""},{""id"":""36""},{""id"":""37""},{""id"":""38""},{""id"":""39""},{""id"":""40""},{""id"":""41""},{""id"":""42""},{""id"":""43""},{""id"":""44""},{""id"":""45""},{""id"":""46""},{""id"":""47""},{""id"":""48""},{""id"":""49""},{""id"":""50""},{""id"":""51""},{""id"":""52""},{""id"":""53""},{""id"":""54""},{""id"":""55""},{""id"":""56""},{""id"":""57""},{""id"":""58""},{""id"":""59""},{""id"":""60""},{""id"":""61""},{""id"":""62""},{""id"":""63""},{""id"":""64""},{""id"":""65""},{""id"":""66""},{""id"":""67""},{""id"":""68""},{""id"":""69""},{""id"":""70""},{""id"":""71""},{""id"":""72""},{""id"":""73""},{""id"":""74""},{""id"":""75""},{""id"":""76""},{""id"":""77""},{""id"":""78""},{""id"":""79""},{""id"":""80""},{""id"":""81""},{""id"":""82""},{""id"":""83""},{""id"":""84""},{""id"":""85""},{""id"":""86""},{""id"":""87""},{""id"":""88""},{""id"":""89""},{""id"":""90""},{""id"":""91""},{""id"":""92""},{""id"":""93""},{""id"":""94""},{""id"":""95""},{""id"":""96""},{""id"":""97""},{""id"":""98""},{""id"":""99""},{""id"":""100""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses?page=1&per_page=100""}}
            ");
            client.Responses.Add(@"
                {""per_page"":100,""total"":300,""data"":[{""id"":""101""},{""id"":""102""},{""id"":""103""},{""id"":""104""},{""id"":""105""},{""id"":""106""},{""id"":""107""},{""id"":""108""},{""id"":""109""},{""id"":""110""},{""id"":""111""},{""id"":""112""},{""id"":""113""},{""id"":""114""},{""id"":""115""},{""id"":""116""},{""id"":""117""},{""id"":""118""},{""id"":""119""},{""id"":""120""},{""id"":""121""},{""id"":""122""},{""id"":""123""},{""id"":""124""},{""id"":""125""},{""id"":""126""},{""id"":""127""},{""id"":""128""},{""id"":""129""},{""id"":""130""},{""id"":""131""},{""id"":""132""},{""id"":""133""},{""id"":""134""},{""id"":""135""},{""id"":""136""},{""id"":""137""},{""id"":""138""},{""id"":""139""},{""id"":""140""},{""id"":""141""},{""id"":""142""},{""id"":""143""},{""id"":""144""},{""id"":""145""},{""id"":""146""},{""id"":""147""},{""id"":""148""},{""id"":""149""},{""id"":""150""},{""id"":""151""},{""id"":""152""},{""id"":""153""},{""id"":""154""},{""id"":""155""},{""id"":""156""},{""id"":""157""},{""id"":""158""},{""id"":""159""},{""id"":""160""},{""id"":""161""},{""id"":""162""},{""id"":""163""},{""id"":""164""},{""id"":""165""},{""id"":""166""},{""id"":""167""},{""id"":""168""},{""id"":""169""},{""id"":""170""},{""id"":""171""},{""id"":""172""},{""id"":""173""},{""id"":""174""},{""id"":""175""},{""id"":""176""},{""id"":""177""},{""id"":""178""},{""id"":""179""},{""id"":""180""},{""id"":""181""},{""id"":""182""},{""id"":""183""},{""id"":""184""},{""id"":""185""},{""id"":""186""},{""id"":""187""},{""id"":""188""},{""id"":""189""},{""id"":""190""},{""id"":""191""},{""id"":""192""},{""id"":""193""},{""id"":""194""},{""id"":""195""},{""id"":""196""},{""id"":""197""},{""id"":""198""},{""id"":""199""},{""id"":""200""}],""page"":2,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/?page=2&per_page=100""}}
            ");
            client.Responses.Add(@"
                {""per_page"":100,""total"":300,""data"":[{""id"":""201""},{""id"":""202""},{""id"":""203""},{""id"":""204""},{""id"":""205""},{""id"":""206""},{""id"":""207""},{""id"":""208""},{""id"":""209""},{""id"":""210""},{""id"":""211""},{""id"":""212""},{""id"":""213""},{""id"":""214""},{""id"":""215""},{""id"":""216""},{""id"":""217""},{""id"":""218""},{""id"":""219""},{""id"":""220""},{""id"":""221""},{""id"":""222""},{""id"":""223""},{""id"":""224""},{""id"":""225""},{""id"":""226""},{""id"":""227""},{""id"":""228""},{""id"":""229""},{""id"":""230""},{""id"":""231""},{""id"":""232""},{""id"":""233""},{""id"":""234""},{""id"":""235""},{""id"":""236""},{""id"":""237""},{""id"":""238""},{""id"":""239""},{""id"":""240""},{""id"":""241""},{""id"":""242""},{""id"":""243""},{""id"":""244""},{""id"":""245""},{""id"":""246""},{""id"":""247""},{""id"":""248""},{""id"":""249""},{""id"":""250""},{""id"":""251""},{""id"":""252""},{""id"":""253""},{""id"":""254""},{""id"":""255""},{""id"":""256""},{""id"":""257""},{""id"":""258""},{""id"":""259""},{""id"":""260""},{""id"":""261""},{""id"":""262""},{""id"":""263""},{""id"":""264""},{""id"":""265""},{""id"":""266""},{""id"":""267""},{""id"":""268""},{""id"":""269""},{""id"":""270""},{""id"":""271""},{""id"":""272""},{""id"":""273""},{""id"":""274""},{""id"":""275""},{""id"":""276""},{""id"":""277""},{""id"":""278""},{""id"":""279""},{""id"":""280""},{""id"":""281""},{""id"":""282""},{""id"":""283""},{""id"":""284""},{""id"":""285""},{""id"":""286""},{""id"":""287""},{""id"":""288""},{""id"":""289""},{""id"":""290""},{""id"":""291""},{""id"":""292""},{""id"":""293""},{""id"":""294""},{""id"":""295""},{""id"":""296""},{""id"":""297""},{""id"":""298""},{""id"":""299""},{""id"":""300""}],""page"":3,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/?page=3&per_page=100""}}
            ");
            client.Responses.Add(@"
                {""per_page"":100,""total"":300,""data"":[],""page"":4,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/?page=4&per_page=100""}}
            ");
            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetResponseOverviewList(91395530, SurveyMonkeyApi.ObjectType.Collector, new GetResponseListSettings {Custom = "asdf"});
            Assert.AreEqual(300, results.Count);
            Assert.AreEqual(300, results.Last().Id);
            Assert.AreEqual(1, results.First().Id);
            Assert.AreEqual(4, client.Requests.Count);
        }

        [Test]
        public void GetResponseListUrlsAreCorrectlyGenerated()
        {
            var standardListResponse = @"{""per_page"":50,""total"":3,""data"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420283"",""id"":""4968420283""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420510"",""id"":""4968420510""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420845"",""id"":""4968420845""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses?page=1&per_page=50""}}";
            var standardIndividualResponse = @"{""total_time"":8,""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420283"",""custom_variables"":{},""ip_address"":""18.187.48.612"",""id"":""4968420283"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:09+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420283"",""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:01+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=db1E_2B5FvGitK17_2F_2F8_2Blnhcl_2BCTwKHT5dPY9EBCDJmi8tUeGDo34qJJ5CuL7ceRS7"",""metadata"":{}}";
            var client = new MockWebClient();
            client.Responses.Add(standardListResponse);
            client.Responses.Add(standardListResponse);
            client.Responses.Add(standardListResponse);
            client.Responses.Add(standardListResponse);
            client.Responses.Add(standardListResponse);
            client.Responses.Add(standardListResponse);
            client.Responses.Add(standardListResponse);
            client.Responses.Add(standardListResponse);
            client.Responses.Add(standardIndividualResponse);
            client.Responses.Add(standardIndividualResponse);
            client.Responses.Add(standardIndividualResponse);
            client.Responses.Add(standardIndividualResponse);
            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);

            api.GetResponseDetailList(1, SurveyMonkeyApi.ObjectType.Survey);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/surveys/1/responses/bulk", client.Requests.Last().Url);

            api.GetResponseDetailList(2, SurveyMonkeyApi.ObjectType.Collector);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/collectors/2/responses/bulk", client.Requests.Last().Url);

            api.GetResponseOverviewList(3, SurveyMonkeyApi.ObjectType.Survey);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/surveys/3/responses", client.Requests.Last().Url);

            api.GetResponseOverviewList(4, SurveyMonkeyApi.ObjectType.Collector);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/collectors/4/responses", client.Requests.Last().Url);

            api.GetResponseDetailList(5, SurveyMonkeyApi.ObjectType.Survey, new GetResponseListSettings());
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/surveys/5/responses/bulk", client.Requests.Last().Url);

            api.GetResponseDetailList(6, SurveyMonkeyApi.ObjectType.Collector, new GetResponseListSettings());
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/collectors/6/responses/bulk", client.Requests.Last().Url);

            api.GetResponseOverviewList(7, SurveyMonkeyApi.ObjectType.Survey, new GetResponseListSettings());
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/surveys/7/responses", client.Requests.Last().Url);

            api.GetResponseOverviewList(8, SurveyMonkeyApi.ObjectType.Collector, new GetResponseListSettings());
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/collectors/8/responses", client.Requests.Last().Url);

            api.GetResponseDetail(9, SurveyMonkeyApi.ObjectType.Survey, 10);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/surveys/9/responses/10/details", client.Requests.Last().Url);

            api.GetResponseDetail(11, SurveyMonkeyApi.ObjectType.Collector, 12);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/collectors/11/responses/12/details", client.Requests.Last().Url);

            api.GetResponseOverview(13, SurveyMonkeyApi.ObjectType.Survey, 14);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/surveys/13/responses/14", client.Requests.Last().Url);

            api.GetResponseOverview(15, SurveyMonkeyApi.ObjectType.Collector, 16);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/collectors/15/responses/16", client.Requests.Last().Url);
        }

        [Test]
        public void GetResponseDetailIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""total_time"":8,""href"":""https:\/\/api.surveymonkey.net\/v3\/surveys\/84672934\/responses\/4968420283"",""custom_variables"":{""custvar_1"":""one"",""custvar_2"":""two""},""ip_address"":""18.187.48.612"",""id"":""4968420283"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:09+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420283"",""pages"":[{""id"":""253784818"",""questions"":[{""id"":""1013185278"",""answers"":[{""choice_id"":""10565315476""}]},{""id"":""1013185659"",""answers"":[{""text"":""22222""}]}]}],""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:01+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=db1E_2B5FvGitK17_2F_2F8_2Blnhcl_2BCTwKHT5dPY9EBCDJmi8tUeGDo34qJJ5CuL7ceRS7"",""metadata"":{}}
            ");
            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var result = api.GetResponseDetail(84672934, SurveyMonkeyApi.ObjectType.Survey, 4968420283);

            Assert.AreEqual(@"http://www.surveymonkey.com/analyze/browse/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420283", result.AnalyzeUrl);
            Assert.AreEqual(CollectionMode.Default, result.CollectionMode);
            Assert.AreEqual(91395530, result.CollectorId);
            Assert.AreEqual(String.Empty, result.CustomValue);
            Assert.AreEqual("two", result.CustomVariables["custvar_2"]);
            Assert.AreEqual(new DateTime(2016, 9, 13, 07, 29, 01, DateTimeKind.Utc), result.DateCreated);
            Assert.AreEqual(new DateTime(2016, 9, 13, 07, 29, 09, DateTimeKind.Utc), result.DateModified);
            Assert.AreEqual(@"http://www.surveymonkey.com/r/?sm=db1E_2B5FvGitK17_2F_2F8_2Blnhcl_2BCTwKHT5dPY9EBCDJmi8tUeGDo34qJJ5CuL7ceRS7", result.EditUrl);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/surveys/84672934/responses/4968420283", result.Href);
            Assert.AreEqual(4968420283, result.Id);
            Assert.AreEqual("18.187.48.612", result.IpAddress);
            Assert.IsEmpty(result.LogicPath);
            Assert.IsEmpty(result.Metadata);
            Assert.IsEmpty(result.PagePath);
            Assert.IsNull(result.RecipientId);
            Assert.AreEqual(ResponseStatus.Completed, result.ResponseStatus);
            Assert.AreEqual(84672934, result.SurveyId);
            Assert.AreEqual(8, result.TotalTime);

            Assert.AreEqual(253784818, result.Pages.First().Id);
            Assert.AreEqual(10565315476, result.Pages.First().Questions.First().Answers.First().ChoiceId);
            Assert.AreEqual("22222", result.Pages.First().Questions.Last().Answers.First().Text);
            Assert.IsNull(result.Pages.First().Questions.Last().Answers.First().ChoiceId);
        }

        [Test]
        public void GetResponseOverviewIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""total_time"":8,""href"":""https:\/\/api.surveymonkey.net\/v3\/collectors\/91395530\/responses\/4968420283"",""custom_variables"":{},""ip_address"":""18.187.48.612"",""id"":""4968420283"",""logic_path"":{},""date_modified"":""2016-09-13T07:29:09+00:00"",""response_status"":""completed"",""custom_value"":"""",""analyze_url"":""http:\/\/www.surveymonkey.com\/analyze\/browse\/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420283"",""page_path"":[],""recipient_id"":"""",""collector_id"":""91395530"",""date_created"":""2016-09-13T07:29:01+00:00"",""survey_id"":""84672934"",""collection_mode"":""default"",""edit_url"":""http:\/\/www.surveymonkey.com\/r\/?sm=db1E_2B5FvGitK17_2F_2F8_2Blnhcl_2BCTwKHT5dPY9EBCDJmi8tUeGDo34qJJ5CuL7ceRS7"",""metadata"":{}}
            ");
            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var result = api.GetResponseDetail(84672934, SurveyMonkeyApi.ObjectType.Collector, 4968420283);

            Assert.AreEqual(@"http://www.surveymonkey.com/analyze/browse/9GyriWHWhcPYK8l_2FdYdcIEvqmtt5hBjuRL79fS2mOFI_3D?respondent_id=4968420283", result.AnalyzeUrl);
            Assert.AreEqual(CollectionMode.Default, result.CollectionMode);
            Assert.AreEqual(91395530, result.CollectorId);
            Assert.AreEqual(String.Empty, result.CustomValue);
            Assert.IsEmpty(result.CustomVariables);
            Assert.AreEqual(new DateTime(2016, 9, 13, 07, 29, 01, DateTimeKind.Utc), result.DateCreated);
            Assert.AreEqual(new DateTime(2016, 9, 13, 07, 29, 09, DateTimeKind.Utc), result.DateModified);
            Assert.AreEqual(@"http://www.surveymonkey.com/r/?sm=db1E_2B5FvGitK17_2F_2F8_2Blnhcl_2BCTwKHT5dPY9EBCDJmi8tUeGDo34qJJ5CuL7ceRS7", result.EditUrl);
            Assert.AreEqual(@"https://api.surveymonkey.net/v3/collectors/91395530/responses/4968420283", result.Href);
            Assert.AreEqual(4968420283, result.Id);
            Assert.AreEqual("18.187.48.612", result.IpAddress);
            Assert.IsEmpty(result.LogicPath);
            Assert.IsEmpty(result.Metadata);
            Assert.IsEmpty(result.PagePath);
            Assert.IsNull(result.RecipientId);
            Assert.AreEqual(ResponseStatus.Completed, result.ResponseStatus);
            Assert.AreEqual(84672934, result.SurveyId);
            Assert.AreEqual(8, result.TotalTime);
            Assert.IsNull(result.Pages);
        }
    }
}