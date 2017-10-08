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
            throw new NotImplementedException();
        }
    }
}