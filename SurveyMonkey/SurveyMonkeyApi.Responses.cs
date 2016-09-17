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
        public enum ObjectType
        {
            Survey,
            Collector
        }

        public Response GetResponseOverview(int objectId, ObjectType objectType, int responseId)
        {
            return GetResponseRequest(objectId, objectType, responseId, false);
        }

        public Response GetResponseDetail(int objectId, ObjectType objectType, int responseId)
        {
            return GetResponseRequest(objectId, objectType, responseId, true);
        }

        private Response GetResponseRequest(int objectId, ObjectType source, int responseId, bool details)
        {
            var detailString = details ? "/details" : String.Empty;
            string endPoint = String.Format("https://api.surveymonkey.net/v3/{0}s/{1}/responses/{2}", source.ToString().ToLower(), objectId, responseId, detailString);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var responses = result["data"].ToObject<Response>();
            return responses;
        }

        public List<Response> GetResponseOverviewList(int objectId, ObjectType objectType)
        {
            var settings = new GetResponseListSettings();
            return GetResponseListRequest(objectId, objectType, settings, false);
        }

        public List<Response> GetResponseDetailList(int objectId, ObjectType objectType)
        {
            var settings = new GetResponseListSettings();
            return GetResponseListRequest(objectId, objectType, settings, true);
        }

        public List<Response> GetResponseOverviewList(int objectId, ObjectType objectType, GetResponseListSettings settings)
        {
            return GetResponseListRequest(objectId, objectType, settings, false);
        }

        public List<Response> GetResponseDetailList(int objectId, ObjectType objectType, GetResponseListSettings settings)
        {
            return GetResponseListRequest(objectId, objectType, settings, true);
        }

        private List<Response> GetResponseListRequest(int id, ObjectType objectType, GetResponseListSettings settings, bool details)
        {
            var requestData = PropertyCasingHelper.GetPopulatedProperties(settings);
            var bulk = details ? "/bulk" : String.Empty;
            string endPoint = String.Format("https://api.surveymonkey.net/v3/{0}s/{1}/responses{2}", objectType.ToString().ToLower(), id, bulk);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, requestData);
            var responses = result["data"].ToObject<List<Response>>();
            return responses;
        }
    }
}