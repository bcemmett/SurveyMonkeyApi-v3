using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.Helpers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi
    {
        public List<Collector> GetCollectorList(long surveyId)
        {
            var settings = new GetCollectorListSettings();
            return GetCollectorListPager(surveyId, settings);
        }

        public List<Collector> GetCollectorList(long surveyId, GetCollectorListSettings settings)
        {
            return GetCollectorListPager(surveyId, settings);
        }

        private List<Collector> GetCollectorListPager(long surveyId, GetCollectorListSettings settings)
        {
            //Get the specific page & quantity
            if (settings.Page.HasValue || settings.PerPage.HasValue)
            {
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                return GetCollectorListRequest(surveyId, requestData);
            }

            //Auto-page
            const int maxResultsPerPage = 1000;
            var results = new List<Collector>();
            bool cont = true;
            int page = 1;
            while (cont)
            {
                settings.Page = page;
                settings.PerPage = maxResultsPerPage;
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                var newResults = GetCollectorListRequest(surveyId, requestData);
                if (newResults.Count > 0)
                {
                    results.AddRange(newResults);
                }
                if (newResults.Count < maxResultsPerPage)
                {
                    cont = false;
                }
                page++;
            }
            return results;
        }

        private List<Collector> GetCollectorListRequest(long surveyId, RequestData requestData)
        {
            string endPoint = String.Format("/surveys/{0}/collectors", surveyId);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, requestData);
            var collectors = result["data"].ToObject<List<Collector>>();
            return collectors;
        }

        public Collector GetCollectorDetails(long collectorId)
        {
            string endPoint = String.Format("/collectors/{0}", collectorId);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var collector = result.ToObject<Collector>();
            return collector;
        }

        public List<Message> GetMessageList(long collectorId)
        {
            var settings = new PagingSettings();
            return GetMessageListPager(collectorId, settings);
        }

        public List<Message> GetMessageList(long collectorId, PagingSettings settings)
        {
            return GetMessageListPager(collectorId, settings);
        }

        private List<Message> GetMessageListPager(long collectorId, PagingSettings settings)
        {
            //Get the specific page & quantity
            if (settings.Page.HasValue || settings.PerPage.HasValue)
            {
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                return GetMessageListRequest(collectorId, requestData);
            }

            //Auto-page
            const int maxResultsPerPage = 1000;
            var results = new List<Message>();
            bool cont = true;
            int page = 1;
            while (cont)
            {
                settings.Page = page;
                settings.PerPage = maxResultsPerPage;
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                var newResults = GetMessageListRequest(collectorId, requestData);
                if (newResults.Count > 0)
                {
                    results.AddRange(newResults);
                }
                if (newResults.Count < maxResultsPerPage)
                {
                    cont = false;
                }
                page++;
            }
            return results;
        }

        private List<Message> GetMessageListRequest(long collectorId, RequestData requestData)
        {
            string endPoint = String.Format("/collectors/{0}/messages", collectorId);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, requestData);
            var messages = result["data"].ToObject<List<Message>>();
            return messages;
        }
    }
}