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
        public List<Survey> GetSurveyList()
        {
            var settings = new GetSurveyListSettings();
            return GetSurveyListPager(settings);
        }

        public List<Survey> GetSurveyList(GetSurveyListSettings settings)
        {
            return GetSurveyListPager(settings);
        }

        private List<Survey> GetSurveyListPager(GetSurveyListSettings settings)
        {
            //Get the specific page & quantity
            if (settings.Page.HasValue || settings.PerPage.HasValue)
            {
                var requestData = PropertyCasingHelper.GetPopulatedProperties(settings);
                return GetSurveyListRequest(requestData);
            }

            //Auto-page
            const int maxSurveysPerPage = 1000;
            var surveys = new List<Survey>();
            bool cont = true;
            int page = 1;
            while (cont)
            {
                settings.Page = page;
                settings.PerPage = maxSurveysPerPage;
                var requestData = PropertyCasingHelper.GetPopulatedProperties(settings);
                var newSurveys = GetSurveyListRequest(requestData);
                if (newSurveys.Count > 0)
                {
                    surveys.AddRange(newSurveys);
                }
                if (newSurveys.Count < maxSurveysPerPage)
                {
                    cont = false;
                }
                page++;
            }
            return surveys;
        }

        private List<Survey> GetSurveyListRequest(RequestData requestData)
        {
            string endPoint = "https://api.surveymonkey.net/v3/surveys";
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, requestData);
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

        public Survey GetSurveyDetails(long id)
        {
            string endPoint = "https://api.surveymonkey.net/v3/surveys/{0}/details";
            var verb = Verb.GET;
            var fullEndpoint = String.Format(endPoint, id);
            JToken result = MakeApiRequest(fullEndpoint, verb, new RequestData());
            var survey = result.ToObject<Survey>();
            return survey;
        }
    }
}