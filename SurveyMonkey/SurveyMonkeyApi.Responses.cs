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
        internal enum ObjectType
        {
            Survey,
            Collector
        }

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
            string endPoint = String.Format("/{0}s/{1}/responses/{2}{3}", source.ToString().ToLower(), objectId, responseId, detailString);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var responses = result.ToObject<Response>();
            return responses;
        }

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

        private List<Response> GetResponseListPager(long id, ObjectType objectType, IPagingSettings settings, bool details)
        {
            var bulk = details ? "/bulk" : String.Empty;
            string endPoint = String.Format("/{0}s/{1}/responses{2}", objectType.ToString().ToLower(), id, bulk);
            int maxResultsPerPage = details ? 100 : 1000;
            var results = Page(settings, endPoint, typeof(List<Response>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Response)o);
        }
    }
}