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
        public List<Collector> GetCollectorList(long surveyId)
        {
            var settings = new GetCollectorListSettings();
            return GetCollectorListPager(surveyId, settings);
        }

        public List<Collector> GetCollectorList(long surveyId, GetCollectorListSettings settings)
        {
            return GetCollectorListPager(surveyId, settings);
        }

        private List<Collector> GetCollectorListPager(long surveyId, IPageableSettings settings)
        {
            string endPoint = String.Format("/surveys/{0}/collectors", surveyId);
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Collector>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Collector)o);
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

        private List<Message> GetMessageListPager(long collectorId, IPageableSettings settings)
        {
            string endPoint = String.Format("/collectors/{0}/messages", collectorId);
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Message>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Message)o);
        }

        public Message GetMessageDetails(long collectorId, long messageId)
        {
            string endPoint = String.Format("/collectors/{0}/messages/{1}", collectorId, messageId);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var message = result.ToObject<Message>();
            return message;
        }
    }
}