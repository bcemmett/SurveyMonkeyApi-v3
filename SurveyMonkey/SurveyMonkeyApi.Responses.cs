using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi
    {
        public List<Response> GetResponseOverviews(int id)
        {
            string endPoint = String.Format("https://api.surveymonkey.net/v3/surveys/{0}/responses", id);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var responses = result["data"].ToObject<List<Response>>();
            return responses;
        }

        public List<Response> GetResponseDetails(int id)
        {
            string endPoint = String.Format("https://api.surveymonkey.net/v3/surveys/{0}/responses/bulk", id);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var responses = result["data"].ToObject<List<Response>>();
            return responses;
        }
    }
}