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

        public Survey GetSurveyOverview(long surveyId)
        {
            var verb = Verb.GET;
            var endpoint = String.Format("/surveys/{0}", surveyId);
            JToken result = MakeApiRequest(endpoint, verb, new RequestData());
            var survey = result.ToObject<Survey>();
            return survey;
        }

        public Survey GetSurveyDetails(long surveyId)
        {
            string endPoint = String.Format("/surveys/{0}/details", surveyId);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var survey = result.ToObject<Survey>();
            return survey;
        }

        public List<SurveyCategory> GetSurveyCategories()
        {
            var settings = new GetSurveyCategoryListSettings();
            return GetSurveyCategoryListPager(settings);
        }

        public List<SurveyCategory> GetSurveyCategories(GetSurveyCategoryListSettings settings)
        {
            return GetSurveyCategoryListPager(settings);
        }

        private List<SurveyCategory> GetSurveyCategoryListPager(IPageableSettings settings)
        {
            string endPoint = "/survey_categories";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<SurveyCategory>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (SurveyCategory)o);
        }

        public List<SurveyTemplate> GetSurveyTemplateList()
        {
            var settings = new GetSurveyTemplateListSettings();
            return GetSurveyTemplateListPager(settings);
        }

        public List<SurveyTemplate> GetSurveyTemplateList(GetSurveyTemplateListSettings settings)
        {
            return GetSurveyTemplateListPager(settings);
        }

        private List<SurveyTemplate> GetSurveyTemplateListPager(IPageableSettings settings)
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
        
        public List<Question> GetQuestionList(long surveyId, long pageId)
        {
            var settings = new PagingSettings();
            return GetQuestionListPager(surveyId, pageId, settings);
        }

        public List<Question> GetQuestionList(long surveyId, long pageId, PagingSettings settings)
        {
            return GetQuestionListPager(surveyId, pageId, settings);
        }

        private List<Question> GetQuestionListPager(long surveyId, long pageId, IPageableSettings settings)
        {
            string endPoint = String.Format("/surveys/{0}/pages/{1}/questions", surveyId, pageId);
            const int maxResultsPerPage = 100;
            var results = Page(settings, endPoint, typeof(List<Question>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Question)o);
        }

        public Question GetQuestionDetails(long surveyId, long pageId, long questionId)
        {
            string endPoint = String.Format("/surveys/{0}/pages/{1}/questions/{2}", surveyId, pageId, questionId);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var question = result.ToObject<Question>();
            return question;
        }
    }
}