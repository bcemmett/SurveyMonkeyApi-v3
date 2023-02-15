using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.Helpers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi
    {
        private enum ObjectType
        {
            Survey,
            Collector
        }

        //Individual response
        public Response GetSurveyResponseOverview(long surveyId, long responseId)
        {
            return GetResponseRequest(surveyId, ObjectType.Survey, responseId, false);
        }

        public Response GetSurveyResponseDetails(long surveyId, long responseId)
        {
            return GetResponseRequest(surveyId, ObjectType.Survey, responseId, true);
        }

        public Response GetCollectorResponseOverview(long collectorId, long responseId)
        {
            return GetResponseRequest(collectorId, ObjectType.Collector, responseId, false);
        }

        public Response GetCollectorResponseDetails(long collectorId, long responseId)
        {
            return GetResponseRequest(collectorId, ObjectType.Collector, responseId, true);
        }

        private Response GetResponseRequest(long objectId, ObjectType source, long responseId, bool details)
        {
            var detailString = details ? "/details" : String.Empty;
            string endPoint = $"/{source.ToString().ToLower()}s/{objectId}/responses/{responseId}{detailString}";
            var requestData = details ? RequestSettingsHelper.GetPopulatedProperties(new GetResponseListSettings{Simple = true}) : new RequestData();
            JToken result = MakeApiGetRequest(endPoint, requestData);
            var responses = result.ToObject<Response>();
            return responses;
        }

        public async Task<Response> GetSurveyResponseOverviewAsync(long surveyId, long responseId)
        {
            return await GetResponseRequestAsync(surveyId, ObjectType.Survey, responseId, false);
        }

        public async Task<Response> GetSurveyResponseDetailsAsync(long surveyId, long responseId)
        {
            return await GetResponseRequestAsync(surveyId, ObjectType.Survey, responseId, true);
        }

        public async Task<Response> GetCollectorResponseOverviewAsync(long collectorId, long responseId)
        {
            return await GetResponseRequestAsync(collectorId, ObjectType.Collector, responseId, false);
        }

        public async Task<Response> GetCollectorResponseDetailsAsync(long collectorId, long responseId)
        {
            return await GetResponseRequestAsync(collectorId, ObjectType.Collector, responseId, true);
        }

        private async Task<Response> GetResponseRequestAsync(long objectId, ObjectType source, long responseId, bool details)
        {
            var detailString = details ? "/details" : String.Empty;
            string endPoint = $"/{source.ToString().ToLower()}s/{objectId}/responses/{responseId}{detailString}";
            var requestData = details ? RequestSettingsHelper.GetPopulatedProperties(new GetResponseListSettings { Simple = true }) : new RequestData();
            JToken result = await MakeApiGetRequestAsync(endPoint, requestData);
            var responses = result.ToObject<Response>();
            return responses;
        }

        //Responses list
        public List<Response> GetSurveyResponseOverviewList(long surveyId)
        {
            var settings = new GetResponseListSettings();
            return GetResponseListPager(surveyId, ObjectType.Survey, settings, false);
        }

        public List<Response> GetSurveyResponseDetailsList(long surveyId)
        {
            var settings = new GetResponseListSettings();
            return GetResponseListPager(surveyId, ObjectType.Survey, settings, true);
        }

        public List<Response> GetSurveyResponseOverviewList(long surveyId, GetResponseListSettings settings)
        {
            return GetResponseListPager(surveyId, ObjectType.Survey, settings, false);
        }

        public List<Response> GetSurveyResponseDetailsList(long surveyId, GetResponseListSettings settings)
        {
            return GetResponseListPager(surveyId, ObjectType.Survey, settings, true);
        }

        public List<Response> GetCollectorResponseOverviewList(long collectorId)
        {
            var settings = new GetResponseListSettings();
            return GetResponseListPager(collectorId, ObjectType.Collector, settings, false);
        }

        public List<Response> GetCollectorResponseDetailsList(long collectorId)
        {
            var settings = new GetResponseListSettings();
            return GetResponseListPager(collectorId, ObjectType.Collector, settings, true);
        }

        public List<Response> GetCollectorResponseOverviewList(long collectorId, GetResponseListSettings settings)
        {
            return GetResponseListPager(collectorId, ObjectType.Collector, settings, false);
        }

        public List<Response> GetCollectorResponseDetailsList(long collectorId, GetResponseListSettings settings)
        {
            return GetResponseListPager(collectorId, ObjectType.Collector, settings, true);
        }

        private List<Response> GetResponseListPager(long id, ObjectType objectType, GetResponseListSettings settings, bool details)
        {
            var bulk = details ? "/bulk" : String.Empty;
            string endPoint = $"/{objectType.ToString().ToLower()}s/{id}/responses{bulk}";
            int maxResultsPerPage = details ? 100 : 1000;
            settings.Simple = details ? true : null;
            var results = Page(settings, endPoint, typeof(List<Response>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Response)o);
        }

        public async Task<List<Response>> GetSurveyResponseOverviewListAsync(long surveyId)
        {
            var settings = new GetResponseListSettings();
            return await GetResponseListPagerAsync(surveyId, ObjectType.Survey, settings, false);
        }

        public async Task<List<Response>> GetSurveyResponseDetailsListAsync(long surveyId)
        {
            var settings = new GetResponseListSettings();
            return await GetResponseListPagerAsync(surveyId, ObjectType.Survey, settings, true);
        }

        public async Task<List<Response>> GetSurveyResponseOverviewListAsync(long surveyId, GetResponseListSettings settings)
        {
            return await GetResponseListPagerAsync(surveyId, ObjectType.Survey, settings, false);
        }

        public async Task<List<Response>> GetSurveyResponseDetailsListAsync(long surveyId, GetResponseListSettings settings)
        {
            return await GetResponseListPagerAsync(surveyId, ObjectType.Survey, settings, true);
        }

        public async Task<List<Response>> GetCollectorResponseOverviewListAsync(long collectorId)
        {
            var settings = new GetResponseListSettings();
            return await GetResponseListPagerAsync(collectorId, ObjectType.Collector, settings, false);
        }

        public async Task<List<Response>> GetCollectorResponseDetailsListAsync(long collectorId)
        {
            var settings = new GetResponseListSettings();
            return await GetResponseListPagerAsync(collectorId, ObjectType.Collector, settings, true);
        }

        public async Task<List<Response>> GetCollectorResponseOverviewListAsync(long collectorId, GetResponseListSettings settings)
        {
            return await GetResponseListPagerAsync(collectorId, ObjectType.Collector, settings, false);
        }

        public async Task<List<Response>> GetCollectorResponseDetailsListAsync(long collectorId, GetResponseListSettings settings)
        {
            return await GetResponseListPagerAsync(collectorId, ObjectType.Collector, settings, true);
        }

        private async Task<List<Response>> GetResponseListPagerAsync(long id, ObjectType objectType, GetResponseListSettings settings, bool details)
        {
            var bulk = details ? "/bulk" : String.Empty;
            string endPoint = $"/{objectType.ToString().ToLower()}s/{id}/responses{bulk}";
            int maxResultsPerPage = details ? 100 : 1000;
            settings.Simple = details ? true : null;
            var results = await PageAsync(settings, endPoint, typeof(List<Response>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Response)o);
        }
    }
}