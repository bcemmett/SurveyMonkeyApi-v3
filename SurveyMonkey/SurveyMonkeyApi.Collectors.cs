using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi
    {
        //Create collector
        public Collector CreateCollector(long surveyId, CreateCollectorSettings settings)
        {
            string endPoint = $"/surveys/{surveyId}/collectors";
            var verb = Verb.POST;
            var requestData = Helpers.RequestSettingsHelper.GetPopulatedProperties(settings);
            JToken result = MakeApiRequest(endPoint, verb, requestData);
            var collector = result.ToObject<Collector>();
            return collector;
        }

        public async Task<Collector> CreateCollectorAsync(long surveyId, CreateCollectorSettings settings)
        {
            string endPoint = $"/surveys/{surveyId}/collectors";
            var verb = Verb.POST;
            var requestData = Helpers.RequestSettingsHelper.GetPopulatedProperties(settings);
            JToken result = await MakeApiRequestAsync(endPoint, verb, requestData);
            var collector = result.ToObject<Collector>();
            return collector;
        }

        //Collector list
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
            string endPoint = $"/surveys/{surveyId}/collectors";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Collector>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Collector)o);
        }

        public async Task<List<Collector>> GetCollectorListAsync(long surveyId)
        {
            var settings = new GetCollectorListSettings();
            return await GetCollectorListPagerAsync(surveyId, settings);
        }

        public async Task<List<Collector>> GetCollectorListAsync(long surveyId, GetCollectorListSettings settings)
        {
            return await GetCollectorListPagerAsync(surveyId, settings);
        }

        private async Task<List<Collector>> GetCollectorListPagerAsync(long surveyId, IPagingSettings settings)
        {
            string endPoint = $"/surveys/{surveyId}/collectors";
            const int maxResultsPerPage = 1000;
            var results = await PageAsync(settings, endPoint, typeof(List<Collector>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Collector)o);
        }

        //Individual collector
        public Collector GetCollectorDetails(long collectorId)
        {
            string endPoint = $"/collectors/{collectorId}";
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var collector = result.ToObject<Collector>();
            return collector;
        }

        public async Task<Collector> GetCollectorDetailsAsync(long collectorId)
        {
            string endPoint = $"/collectors/{collectorId}";
            JToken result = await MakeApiGetRequestAsync(endPoint, new RequestData());
            var collector = result.ToObject<Collector>();
            return collector;
        }

        //Message list
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
            string endPoint = $"/collectors/{collectorId}/messages";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Message>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Message)o);
        }

        public async Task<List<Message>> GetMessageListAsync(long collectorId)
        {
            var settings = new PagingSettings();
            return await GetMessageListPagerAsync(collectorId, settings);
        }

        public async Task<List<Message>> GetMessageListAsync(long collectorId, PagingSettings settings)
        {
            return await GetMessageListPagerAsync(collectorId, settings);
        }

        private async Task<List<Message>> GetMessageListPagerAsync(long collectorId, IPagingSettings settings)
        {
            string endPoint = $"/collectors/{collectorId}/messages";
            const int maxResultsPerPage = 1000;
            var results = await PageAsync(settings, endPoint, typeof(List<Message>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Message)o);
        }

        //Individual message
        public Message GetMessageDetails(long collectorId, long messageId)
        {
            string endPoint = $"/collectors/{collectorId}/messages/{messageId}";
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var message = result.ToObject<Message>();
            return message;
        }

        public async Task<Message> GetMessageDetailsAsync(long collectorId, long messageId)
        {
            string endPoint = $"/collectors/{collectorId}/messages/{messageId}";
            JToken result = await MakeApiGetRequestAsync(endPoint, new RequestData());
            var message = result.ToObject<Message>();
            return message;
        }

        //Recipient list
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
                $"/collectors/{collectorId}/messages/{messageId}/recipients" :
                $"/collectors/{collectorId}/recipients";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Recipient>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Recipient)o);
        }

        public async Task<List<Recipient>> GetCollectorRecipientListAsync(long collectorId)
        {
            var settings = new GetRecipientListSettings();
            return await GetRecipientListPagerAsync(collectorId, null, settings);
        }

        public async Task<List<Recipient>> GetCollectorRecipientListAsync(long collectorId, GetRecipientListSettings settings)
        {
            return await GetRecipientListPagerAsync(collectorId, null, settings);
        }

        public async Task<List<Recipient>> GetMessageRecipientListAsync(long collectorId, long messageId)
        {
            var settings = new GetRecipientListSettings();
            return await GetRecipientListPagerAsync(collectorId, messageId, settings);
        }

        public async Task<List<Recipient>> GetMessageRecipientListAsync(long collectorId, long messageId, GetRecipientListSettings settings)
        {
            return await GetRecipientListPagerAsync(collectorId, messageId, settings);
        }

        private async Task<List<Recipient>> GetRecipientListPagerAsync(long collectorId, long? messageId, GetRecipientListSettings settings)
        {
            string endPoint = messageId.HasValue ?
                $"/collectors/{collectorId}/messages/{messageId}/recipients" :
                $"/collectors/{collectorId}/recipients";
            const int maxResultsPerPage = 1000;
            var results = await PageAsync(settings, endPoint, typeof(List<Recipient>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Recipient)o);
        }

        //Individual recipient
        public Recipient GetRecipientDetails(long collectorId, long recipientId)
        {
            string endPoint = $"/collectors/{collectorId}/recipients/{recipientId}";
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var recipient = result.ToObject<Recipient>();
            return recipient;
        }

        public async Task<Recipient> GetRecipientDetailsAsync(long collectorId, long recipientId)
        {
            string endPoint = $"/collectors/{collectorId}/recipients/{recipientId}";
            JToken result = await MakeApiGetRequestAsync(endPoint, new RequestData());
            var recipient = result.ToObject<Recipient>();
            return recipient;
        }
    }
}