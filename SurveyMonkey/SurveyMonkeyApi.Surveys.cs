using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi
    {
        //Survey list
        public List<Survey> GetSurveyList()
        {
            var settings = new GetSurveyListSettings();
            return GetSurveyListPager(settings);
        }

        public List<Survey> GetSurveyList(GetSurveyListSettings settings)
        {
            return GetSurveyListPager(settings);
        }

        private List<Survey> GetSurveyListPager(IPagingSettings settings)
        {
            string endPoint = "/surveys";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Survey>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Survey)o);
        }

        public async Task<List<Survey>> GetSurveyListAsync()
        {
            var settings = new GetSurveyListSettings();
            return await GetSurveyListPagerAsync(settings);
        }

        public async Task<List<Survey>> GetSurveyListAsync(GetSurveyListSettings settings)
        {
            return await GetSurveyListPagerAsync(settings);
        }

        private async Task<List<Survey>> GetSurveyListPagerAsync(IPagingSettings settings)
        {
            string endPoint = "/surveys";
            const int maxResultsPerPage = 1000;
            var results = await PageAsync(settings, endPoint, typeof(List<Survey>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Survey)o);
        }

        //Individual survey
        public Survey GetSurveyOverview(long surveyId)
        {
            var endpoint = $"/surveys/{surveyId}";
            JToken result = MakeApiGetRequest(endpoint, new RequestData());
            var survey = result.ToObject<Survey>();
            return survey;
        }

        public async Task<Survey> GetSurveyOverviewAsync(long surveyId)
        {
            var endpoint = $"/surveys/{surveyId}";
            JToken result = await MakeApiGetRequestAsync(endpoint, new RequestData());
            var survey = result.ToObject<Survey>();
            return survey;
        }

        public Survey GetSurveyDetails(long surveyId)
        {
            string endPoint = $"/surveys/{surveyId}/details";
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var survey = result.ToObject<Survey>();
            return survey;
        }

        public async Task<Survey> GetSurveyDetailsAsync(long surveyId)
        {
            string endPoint = $"/surveys/{surveyId}/details";
            JToken result = await MakeApiGetRequestAsync(endPoint, new RequestData());
            var survey = result.ToObject<Survey>();
            return survey;
        }

        //Survey category list
        public List<SurveyCategory> GetSurveyCategoryList()
        {
            var settings = new GetSurveyCategoryListSettings();
            return GetSurveyCategoryListPager(settings);
        }

        public List<SurveyCategory> GetSurveyCategoryList(GetSurveyCategoryListSettings settings)
        {
            return GetSurveyCategoryListPager(settings);
        }

        private List<SurveyCategory> GetSurveyCategoryListPager(IPagingSettings settings)
        {
            string endPoint = "/survey_categories";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<SurveyCategory>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (SurveyCategory)o);
        }

        public async Task<List<SurveyCategory>> GetSurveyCategoryListAsync()
        {
            var settings = new GetSurveyCategoryListSettings();
            return await GetSurveyCategoryListPagerAsync(settings);
        }

        public async Task<List<SurveyCategory>> GetSurveyCategoryListAsync(GetSurveyCategoryListSettings settings)
        {
            return await GetSurveyCategoryListPagerAsync(settings);
        }

        private async Task<List<SurveyCategory>> GetSurveyCategoryListPagerAsync(IPagingSettings settings)
        {
            string endPoint = "/survey_categories";
            const int maxResultsPerPage = 1000;
            var results = await PageAsync(settings, endPoint, typeof(List<SurveyCategory>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (SurveyCategory)o);
        }

        //Survey template list
        public List<SurveyTemplate> GetSurveyTemplateList()
        {
            var settings = new GetSurveyTemplateListSettings();
            return GetSurveyTemplateListPager(settings);
        }

        public List<SurveyTemplate> GetSurveyTemplateList(GetSurveyTemplateListSettings settings)
        {
            return GetSurveyTemplateListPager(settings);
        }

        private List<SurveyTemplate> GetSurveyTemplateListPager(IPagingSettings settings)
        {
            string endPoint = "/survey_templates";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<SurveyTemplate>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (SurveyTemplate)o);
        }

        public async Task<List<SurveyTemplate>> GetSurveyTemplateListAsync()
        {
            var settings = new GetSurveyTemplateListSettings();
            return await GetSurveyTemplateListPagerAsync(settings);
        }

        public async Task<List<SurveyTemplate>> GetSurveyTemplateListAsync(GetSurveyTemplateListSettings settings)
        {
            return await GetSurveyTemplateListPagerAsync(settings);
        }

        private async Task<List<SurveyTemplate>> GetSurveyTemplateListPagerAsync(IPagingSettings settings)
        {
            string endPoint = "/survey_templates";
            const int maxResultsPerPage = 1000;
            var results = await PageAsync(settings, endPoint, typeof(List<SurveyTemplate>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (SurveyTemplate)o);
        }

        //Survey page list
        public List<Page> GetPageList(long surveyId)
        {
            var settings = new PagingSettings();
            return GetPageListPager(surveyId, settings);
        }

        public List<Page> GetPageList(long surveyId, PagingSettings settings)
        {
            return GetPageListPager(surveyId, settings);
        }

        private List<Page> GetPageListPager(long surveyId, IPagingSettings settings)
        {
            string endPoint = $"/surveys/{surveyId}/pages";
            const int maxResultsPerPage = 100;
            var results = Page(settings, endPoint, typeof(List<Page>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Page)o);
        }

        public async Task<List<Page>> GetPageListAsync(long surveyId)
        {
            var settings = new PagingSettings();
            return await GetPageListPagerAsync(surveyId, settings);
        }

        public async Task<List<Page>> GetPageListAsync(long surveyId, PagingSettings settings)
        {
            return await GetPageListPagerAsync(surveyId, settings);
        }

        private async Task<List<Page>> GetPageListPagerAsync(long surveyId, IPagingSettings settings)
        {
            string endPoint = $"/surveys/{surveyId}/pages";
            const int maxResultsPerPage = 100;
            var results = await PageAsync(settings, endPoint, typeof(List<Page>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Page)o);
        }

        //Individual page
        public Page GetPageDetails(long surveyId, long pageId)
        {
            string endPoint = $"/surveys/{surveyId}/pages/{pageId}";
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var page = result.ToObject<Page>();
            return page;
        }
        
        public async Task<Page> GetPageDetailsAsync(long surveyId, long pageId)
        {
            string endPoint = $"/surveys/{surveyId}/pages/{pageId}";
            JToken result = await MakeApiGetRequestAsync(endPoint, new RequestData());
            var page = result.ToObject<Page>();
            return page;
        }

        //Survey question lists
        public List<Question> GetQuestionList(long surveyId, long pageId)
        {
            var settings = new PagingSettings();
            return GetQuestionListPager(surveyId, pageId, settings);
        }

        public List<Question> GetQuestionList(long surveyId, long pageId, PagingSettings settings)
        {
            return GetQuestionListPager(surveyId, pageId, settings);
        }

        private List<Question> GetQuestionListPager(long surveyId, long pageId, IPagingSettings settings)
        {
            string endPoint = $"/surveys/{surveyId}/pages/{pageId}/questions";
            const int maxResultsPerPage = 100;
            var results = Page(settings, endPoint, typeof(List<Question>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Question)o);
        }

        public async Task<List<Question>> GetQuestionListAsync(long surveyId, long pageId)
        {
            var settings = new PagingSettings();
            return await GetQuestionListPagerAsync(surveyId, pageId, settings);
        }

        public async Task<List<Question>> GetQuestionListAsync(long surveyId, long pageId, PagingSettings settings)
        {
            return await GetQuestionListPagerAsync(surveyId, pageId, settings);
        }

        private async Task<List<Question>> GetQuestionListPagerAsync(long surveyId, long pageId, IPagingSettings settings)
        {
            string endPoint = $"/surveys/{surveyId}/pages/{pageId}/questions";
            const int maxResultsPerPage = 100;
            var results = await PageAsync(settings, endPoint, typeof(List<Question>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Question)o);
        }

        //Individual question
        public Question GetQuestionDetails(long surveyId, long pageId, long questionId)
        {
            string endPoint = $"/surveys/{surveyId}/pages/{pageId}/questions/{questionId}";
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var question = result.ToObject<Question>();
            return question;
        }

        public async Task<Question> GetQuestionDetailsAsync(long surveyId, long pageId, long questionId)
        {
            string endPoint = $"/surveys/{surveyId}/pages/{pageId}/questions/{questionId}";
            JToken result = await MakeApiGetRequestAsync(endPoint, new RequestData());
            var question = result.ToObject<Question>();
            return question;
        }
    }
}