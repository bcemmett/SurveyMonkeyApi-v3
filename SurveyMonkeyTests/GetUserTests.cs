using System;
using System.Linq;
using NUnit.Framework;
using SurveyMonkey;
using SurveyMonkey.Enums;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class GetUserTests
    {
        [Test]
        public void GetUserMeIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""username"":""test@gmail.com"",""first_name"":null,""last_name"":""McTest"",""account_type"":""gold"",""language"":""en"",""email"":""testemail@gmail.com"",""href"":""https:\/\/api.surveymonkey.net\/v3\/users\/me"",""date_last_login"":""2016-09-26T16:23:40.397000+00:00"",""date_created"":""2006-06-28T12:48:00+00:00"",""id"":""123456789""}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetUserDetails();
            Assert.AreEqual("test@gmail.com", results.Username);
            Assert.IsNull(results.FirstName);
            Assert.AreEqual("McTest", results.LastName);
            Assert.AreEqual("gold", results.AccountType);
            Assert.AreEqual("en", results.Language);
            Assert.AreEqual("testemail@gmail.com", results.Email);
            Assert.AreEqual("https://api.surveymonkey.net/v3/users/me", results.Href);
            Assert.AreEqual(new DateTime(2006, 6, 28, 12, 48, 0, DateTimeKind.Utc), results.DateCreated);
            Assert.AreEqual(new DateTime(2016, 9, 26, 16, 23, 40, 397, DateTimeKind.Utc), results.DateLastLogin);
            Assert.AreEqual(123456789, results.Id);
        }

        [Test]
        public void GetGroupListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":1,""page"":1,""total"":1,""data"":[{""id"":""1234"",""name"":""Test Group"",""href"":""https://api.surveymonkey.net/v3/groups/1234""}],""links"": {""self"":""https://api.surveymonkey.net/v3/groups?page=1&per_page=1""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetGroupList();
            Assert.AreEqual(1234, results.First().Id);
            Assert.AreEqual("Test Group", results.First().Name);
            Assert.AreEqual("https://api.surveymonkey.net/v3/groups/1234", results.First().Href);
        }

        [Test]
        public void GetGroupDetailsIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""id"":""1234"",""name"":""Test Group"",""member_count"":1,""max_invites"":100,""date_created"":""2015-10-06T12:56:55+00:00""}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetGroupDetails(1234);
            Assert.AreEqual(1234, results.Id);
            Assert.AreEqual("Test Group", results.Name);
            Assert.AreEqual(100, results.MaxInvites);
            Assert.AreEqual(new DateTime(2015, 10, 6, 12, 56, 55, DateTimeKind.Utc), results.DateCreated);
            Assert.IsNull(results.OwnerEmail);
        }

        [Test]
        public void GetMemberListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":1,""page"":1,""total"":1,""data"":[{""id"":""1234"",""username"":""test_user"",""href"":""http://api.surveymonkey.com/v3/members/1234""}],""links"":{""self"":""https://api.surveymonkey.net/v3/groups/12345/members?page=1&per_page=1""}}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetMemberList(1234);
            Assert.AreEqual(1234, results.First().Id);
            Assert.AreEqual("test_user", results.First().Username);
            Assert.AreEqual("http://api.surveymonkey.com/v3/members/1234", results.First().Href);
            Assert.IsNull(results.First().Status);
        }

        [Test]
        public void GetMemberDetailsIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""id"":""1234"",""username"":""test_user"",""email"":""test@surveymonkey.com"",""type"":""regular"",""status"":""active"",""user_id"":""1234"",""date_created"":""2015-10-06T12:56:55+00:00""}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetMemberDetails(1234, 1234);
            Assert.AreEqual(1234, results.Id);
            Assert.AreEqual("test_user", results.Username);
            Assert.AreEqual("test@surveymonkey.com", results.Email);
            Assert.AreEqual(MemberStatus.Active, results.Status);
            Assert.AreEqual(MemberType.Regular, results.Type);
            Assert.IsNull(results.Href);
            Assert.AreEqual("1234", results.UserId);
            Assert.AreEqual(new DateTime(2015, 10, 6, 12, 56, 55, DateTimeKind.Utc), results.DateCreated);
        }
    }
}