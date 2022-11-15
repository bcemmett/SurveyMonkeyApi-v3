using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;
using SurveyMonkey;
using SurveyMonkey.Containers;
using SurveyMonkey.Enums;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class WebhookTests
    {
        [Test]
        public void GetWebhookListIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""per_page"":100,""total"":2,""data"":[{""href"":""https:\/\/api.surveymonkey.net\/v3\/webhooks\/3618472"",""id"":""3618472"",""name"":""First webhook""},{""href"":""https:\/\/api.surveymonkey.net\/v3\/webhooks\/3481793"",""id"":""3481793"",""name"":""Second webhook""}],""page"":1,""links"":{""self"":""https:\/\/api.surveymonkey.net\/v3\/webhooks?page=1&per_page=100""}}
            ");

            var api = new SurveyMonkeyApi("TestOAuthToken", client);
            var results = api.GetWebhookList();
            Assert.AreEqual(1, client.Requests.Count);
            Assert.AreEqual(3618472, results.First().Id);
            Assert.AreEqual("Second webhook", results.Last().Name);
        }

        [Test]
        public void GetWebhookDetailsIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""href"":""https:\/\/api.surveymonkey.net\/v3\/webhooks\/3285187"",""event_type"":""response_completed"",""subscription_url"":""http:\/\/targetsite.com\/api\/"",""object_type"":""survey"",""object_ids"":[""86842167""],""id"":""3285187"",""name"":""First webhook""}
            ");

            var api = new SurveyMonkeyApi("TestOAuthToken", client);
            
            var result = api.GetWebhookDetails(3285187);
            Assert.AreEqual("First webhook", result.Name);
            Assert.AreEqual(3285187, result.Id);
            Assert.AreEqual("http://targetsite.com/api/", result.SubscriptionUrl);
            Assert.AreEqual(86842167, result.ObjectIds.First());
            Assert.IsNull(result.Authorization);
        }

        [Test]
        public void CreateWebhookHandledProperly()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""href"":""https:\/\/api.surveymonkey.net\/v3\/webhooks\/3289918"",""event_type"":""response_completed"",""subscription_url"":""http:\/\/targetsite.com\/api\/"",""object_type"":""collector"",""object_ids"":[""49143218""],""id"":""3289918"",""name"":""New webhook""}
            ");

            var api = new SurveyMonkeyApi("TestOAuthToken", client);

            var webhook = new Webhook
            {
                EventType = WebhookEventType.ResponseCompleted,
                Name = "New webhook",
                SubscriptionUrl = "http://targetsite.com/api/",
                ObjectType = WebhookObjectType.Collector,
                ObjectIds = new List<long> { 49143218 }
            };

            var result = api.CreateWebhook(webhook);
            Assert.AreEqual("New webhook", result.Name);
            Assert.AreEqual(3289918, result.Id);
            Assert.AreEqual("POST", client.Requests.First().Verb);
            StringAssert.DoesNotContain("\"id\"", client.Requests.First().Body);
        }

        [Test]
        public void ModifyWebhookHandledProperly()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""href"":""https:\/\/api.surveymonkey.net\/v3\/webhooks\/3289918"",""event_type"":""response_completed"",""subscription_url"":""http:\/\/targetsite.com\/api\/"",""object_type"":""survey"",""object_ids"":[""49143218"",""49146481""],""id"":""3289918"",""name"":""First webhook""}
            ");

            var api = new SurveyMonkeyApi("TestOAuthToken", client);

            var webhook = new Webhook
            {
                ObjectIds = new List<long> { 49143218, 49146481 }
            };

            var result = api.ModifyWebhook(3289918, webhook);
            Assert.AreEqual("First webhook", result.Name);
            Assert.AreEqual(3289918, result.Id);
            Assert.AreEqual("PATCH", client.Requests.First().Verb);
            StringAssert.DoesNotContain("\"id\"", client.Requests.First().Body);
        }

        [Test]
        public void ReplaceWebhookHandledProperly()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""href"":""https:\/\/api.surveymonkey.net\/v3\/webhooks\/3289918"",""event_type"":""response_completed"",""subscription_url"":""http:\/\/targetsite.com\/api\/"",""object_type"":""collector"",""object_ids"":[""49143218""],""id"":""3289918"",""name"":""New webhook""}
            ");

            var api = new SurveyMonkeyApi("TestOAuthToken", client);

            var webhook = new Webhook
            {
                EventType = WebhookEventType.ResponseCompleted,
                Name = "New webhook",
                SubscriptionUrl = "http://targetsite.com/api/",
                ObjectType = WebhookObjectType.Collector,
                ObjectIds = new List<long> { 49143218 }
            };

            var result = api.ReplaceWebhook(3289918, webhook);
            Assert.AreEqual("New webhook", result.Name);
            Assert.AreEqual(3289918, result.Id);
            Assert.AreEqual("PUT", client.Requests.First().Verb);
            StringAssert.DoesNotContain("\"id\"", client.Requests.First().Body);
        }

        [Test]
        public void DeleteWebhookHandledProperly()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""href"":""https:\/\/api.surveymonkey.net\/v3\/webhooks\/3289918"",""event_type"":""response_completed"",""subscription_url"":""http:\/\/targetsite.com\/api\/"",""object_type"":""survey"",""object_ids"":[""49143218"",""49146481""],""id"":""3289918"",""name"":""First webhook""}
            ");

            var api = new SurveyMonkeyApi("TestOAuthToken", client);

            var result = api.DeleteWebhook(3289918);
            Assert.AreEqual("First webhook", result.Name);
            Assert.AreEqual(3289918, result.Id);
            Assert.AreEqual("DELETE", client.Requests.First().Verb);
        }
    }
}