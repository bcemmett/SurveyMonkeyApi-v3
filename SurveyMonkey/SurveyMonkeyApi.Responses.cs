using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi
    {
        private enum SurveyOrCollector
        {
            Survey,
            Collector
        }

        public List<Response> GetSurveyResponseOverviews(int id)
        {
            return GetResponsesRequest(id, false, SurveyOrCollector.Survey);
        }

        public List<Response> GetSurveyResponseDetails(int id)
        {
            return GetResponsesRequest(id, true, SurveyOrCollector.Survey);
        }

        public List<Response> GetCollectorResponseOverviews(int id)
        {
            return GetResponsesRequest(id, false, SurveyOrCollector.Collector);
        }

        public List<Response> GetCollectorResponseDetails(int id)
        {
            return GetResponsesRequest(id, true, SurveyOrCollector.Collector);
        }

        private List<Response> GetResponsesRequest(int id, bool details, SurveyOrCollector source)
        {
            var bulk = details ? "/bulk" : String.Empty;
            string endPoint = String.Format("https://api.surveymonkey.net/v3/{0}s/{1}/responses{2}", source.ToString().ToLower(), id, bulk);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var responses = result["data"].ToObject<List<Response>>();
            return responses;
        }
    }
}