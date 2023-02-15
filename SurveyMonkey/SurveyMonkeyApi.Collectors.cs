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
        public Collector CreateCollector(long surveyId, CreateCollectorSettings settings)
        {
            string endPoint = String.Format("/surveys/{0}/collectors", surveyId);
            var verb = Verb.POST;
            var requestData = Helpers.RequestSettingsHelper.GetPopulatedProperties(settings);
            JToken result = MakeApiRequest(endPoint, verb, requestData);
            var collector = result.ToObject<Collector>();
            return collector;
        }

        public List<Collector> GetCollectorList(long surveyId)
        {
            var settings = new GetCollectorListSettings();
            return GetCollectorListPager(surveyId, settings);
        }

        public List<Collector> GetCollectorList(long surveyId, GetCollectorListSettings settings)
        {
            return GetCollectorListPager(surveyId, settings);
        }

        private List<Collector> GetCollectorListPager(long surveyId, IPagingSettings settings)
        {
            string endPoint = String.Format("/surveys/{0}/collectors", surveyId);
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Collector>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Collector)o);
        }

        public Collector GetCollectorDetails(long collectorId)
        {
            string endPoint = String.Format("/collectors/{0}", collectorId);
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
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

        private List<Message> GetMessageListPager(long collectorId, IPagingSettings settings)
        {
            string endPoint = String.Format("/collectors/{0}/messages", collectorId);
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Message>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Message)o);
        }

        public Message GetMessageDetails(long collectorId, long messageId)
        {
            string endPoint = String.Format("/collectors/{0}/messages/{1}", collectorId, messageId);
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var message = result.ToObject<Message>();
            return message;
        }

        [Obsolete("GetRecipientList() is obsolete. Either use GetMessageRecipientList() or GetCollectorRecipientList().", true)]
        public List<Recipient> GetRecipientList(long collectorId, long messageId)
        {
            return GetMessageRecipientList(collectorId, messageId);
        }

        [Obsolete("GetRecipientList() is obsolete. Either use GetMessageRecipientList() or GetCollectorRecipientList().", true)]
        public List<Recipient> GetRecipientList(long collectorId, long messageId, GetRecipientListSettings settings)
        {
            return GetMessageRecipientList(collectorId, messageId, settings);
        }

        public List<Recipient> GetCollectorRecipientList(long collectorId)
        {
            var settings = new GetRecipientListSettings();
            return GetRecipientListPager(collectorId, null, settings);
        }

        public List<Recipient> GetCollectorRecipientList(long collectorId, GetRecipientListSettings settings)
        {
            return GetRecipientListPager(collectorId, null, settings);
        }
        
        public List<Recipient> GetMessageRecipientList(long collectorId, long messageId)
        {
            var settings = new GetRecipientListSettings();
            return GetRecipientListPager(collectorId, messageId, settings);
        }

        public List<Recipient> GetMessageRecipientList(long collectorId, long messageId, GetRecipientListSettings settings)
        {
            return GetRecipientListPager(collectorId, messageId, settings);
        }

        private List<Recipient> GetRecipientListPager(long collectorId, long? messageId, GetRecipientListSettings settings)
        {
            string endPoint = messageId.HasValue ?
                String.Format("/collectors/{0}/messages/{1}/recipients", collectorId, messageId) :
                String.Format("/collectors/{0}/recipients", collectorId);
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Recipient>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Recipient)o);
        }

        public Recipient GetRecipientDetails(long collectorId, long recipientId)
        {
            string endPoint = String.Format("/collectors/{0}/recipients/{1}", collectorId, recipientId);
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var recipient = result.ToObject<Recipient>();
            return recipient;
        }
    }
}