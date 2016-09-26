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
        public List<Survey> GetSurveyList()
        {
            var settings = new GetSurveyListSettings();
            return GetSurveyListPager(settings);
        }

        public List<Survey> GetSurveyList(GetSurveyListSettings settings)
        {
            return GetSurveyListPager(settings);
        }

        private List<Survey> GetSurveyListPager(IPageableSettings settings)
        {
            string endPoint = "/surveys";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Survey>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Survey)o);
        }

        public Survey GetSurveyOverview(long id)
        {
            var verb = Verb.GET;
            var endpoint = String.Format("/surveys/{0}", id);
            JToken result = MakeApiRequest(endpoint, verb, new RequestData());
            var survey = result.ToObject<Survey>();
            return survey;
        }

        public Survey GetSurveyDetails(long id)
        {
            string endPoint = "/surveys/{0}/details";
            var verb = Verb.GET;
            var fullEndpoint = String.Format(endPoint, id);
            JToken result = MakeApiRequest(fullEndpoint, verb, new RequestData());
            var survey = result.ToObject<Survey>();
            return survey;
        }

        public List<SurveyCategory> GetSurveyCategories()
        {
            var settings = new GetSurveyCategoriesSettings();
            return GetSurveyCategoriesPager(settings);
        }

        public List<SurveyCategory> GetSurveyCategories(GetSurveyCategoriesSettings settings)
        {
            return GetSurveyCategoriesPager(settings);
        }

        private List<SurveyCategory> GetSurveyCategoriesPager(IPageableSettings settings)
        {
            string endPoint = "/survey_categories";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<SurveyCategory>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (SurveyCategory)o);
        }

        public List<SurveyTemplate> GetSurveyTemplates()
        {
            var settings = new GetSurveyTemplatesSettings();
            return GetSurveyTemplatesPager(settings);
        }

        public List<SurveyTemplate> GetSurveyTemplates(GetSurveyTemplatesSettings settings)
        {
            return GetSurveyTemplatesPager(settings);
        }

        private List<SurveyTemplate> GetSurveyTemplatesPager(IPageableSettings settings)
        {
            string endPoint = "/survey_templates";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<SurveyTemplate>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (SurveyTemplate)o);
        }

        public List<Page> GetPageList(long surveyId)
        {
            var settings = new PagingSettings();
            return GetPageListPager(surveyId, settings);
        }

        public List<Page> GetPageList(long surveyId, PagingSettings settings)
        {
            return GetPageListPager(surveyId, settings);
        }

        private List<Page> GetPageListPager(long surveyId, IPageableSettings settings)
        {
            string endPoint = String.Format("/surveys/{0}/pages", surveyId);
            const int maxResultsPerPage = 100;
            var results = Page(settings, endPoint, typeof(List<Page>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Page)o);
        }

        public Page GetPageDetails(long surveyId, long pageId)
        {
            string endPoint = String.Format("/surveys/{0}/pages/{1}", surveyId, pageId);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var page = result.ToObject<Page>();
            return page;
        }
    }
}