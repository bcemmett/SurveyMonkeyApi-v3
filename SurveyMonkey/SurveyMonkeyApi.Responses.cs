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
        public enum ObjectType
        {
            Survey,
            Collector
        }

        public Response GetResponseOverview(long objectId, ObjectType objectType, long responseId)
        {
            return GetResponseRequest(objectId, objectType, responseId, false);
        }

        public Response GetResponseDetails(long objectId, ObjectType objectType, long responseId)
        {
            return GetResponseRequest(objectId, objectType, responseId, true);
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

        public List<Response> GetResponseOverviewList(long objectId, ObjectType objectType)
        {
            var settings = new GetResponseListSettings();
            return GetResponseListPager(objectId, objectType, settings, false);
        }

        public List<Response> GetResponseDetailList(long objectId, ObjectType objectType)
        {
            var settings = new GetResponseListSettings();
            return GetResponseListPager(objectId, objectType, settings, true);
        }

        public List<Response> GetResponseOverviewList(long objectId, ObjectType objectType, GetResponseListSettings settings)
        {
            return GetResponseListPager(objectId, objectType, settings, false);
        }

        public List<Response> GetResponseDetailList(long objectId, ObjectType objectType, GetResponseListSettings settings)
        {
            return GetResponseListPager(objectId, objectType, settings, true);
        }

        private List<Response> GetResponseListPager(long id, ObjectType objectType, IPagingSettings settings, bool details)
        {
            var bulk = details ? "/bulk" : String.Empty;
            string endPoint = String.Format("/{0}s/{1}/responses{2}", objectType.ToString().ToLower(), id, bulk);
            const int maxResultsPerPage = 100;
            var results = Page(settings, endPoint, typeof(List<Response>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Response)o);
        }
    }
}