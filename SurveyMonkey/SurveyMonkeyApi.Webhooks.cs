using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi
    {
        public List<Webhook> GetWebhookList()
        {
            var settings = new PagingSettings();
            return GetWebhookListPager(settings);
        }

        public List<Webhook> GetWebhookList(PagingSettings settings)
        {
            return GetWebhookListPager(settings);
        }

        private List<Webhook> GetWebhookListPager(PagingSettings settings)
        {
            string endPoint = "/webhooks";
            const int maxResultsPerPage = 100;
            var results = Page(settings, endPoint, typeof(List<Webhook>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Webhook)o);
        }

        public Webhook GetWebhookDetails(long webhookId)
        {
            string endPoint = $"/webhooks/{webhookId}";
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var webhook = result.ToObject<Webhook>();
            return webhook;
        }

        public Webhook CreateWebhook(Webhook webhook)
        {
            if (webhook.Id.HasValue)
            {
                throw new ArgumentException("An id can't be supplied when creating a webhook.");
            }
            string endPoint = "/webhooks";
            var verb = Verb.POST;
            var requestData = Helpers.RequestSettingsHelper.GetPopulatedProperties(webhook);
            JToken result = MakeApiRequest(endPoint, verb, requestData);
            var createdWebhook = result.ToObject<Webhook>();
            return createdWebhook;
        }
    }
}