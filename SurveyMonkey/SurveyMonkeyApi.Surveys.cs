using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi
    {
        public List<Survey> GetSurveys()
        {
            string endPoint = "https://api.surveymonkey.net/v3/surveys";
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var surveys = result["data"].ToObject<List<Survey>>();
            return surveys;
        }

        public Survey GetSurveyOverview(long id)
        {
            string endPoint = "https://api.surveymonkey.net/v3/surveys/{0}";
            var verb = Verb.GET;
            var fullEndpoint = String.Format(endPoint, id);
			JToken result = MakeApiRequest(fullEndpoint, verb, new RequestData());
            var survey = result.ToObject<Survey>();
            return survey;
        }
    }
}