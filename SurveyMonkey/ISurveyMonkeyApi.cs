using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyMonkey.Containers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public interface ISurveyMonkeyApi
    {
        //Surveys
        List<Survey> GetSurveyList();
        List<Survey> GetSurveyList(GetSurveyListSettings settings);
        Survey GetSurveyOverview(long surveyId);
        Survey GetSurveyDetails(long surveyId);
        List<SurveyCategory> GetSurveyCategoryList();
        List<SurveyCategory> GetSurveyCategoryList(GetSurveyCategoryListSettings settings);
        List<SurveyTemplate> GetSurveyTemplateList();
        List<SurveyTemplate> GetSurveyTemplateList(GetSurveyTemplateListSettings settings);
        List<Page> GetPageList(long surveyId);
        List<Page> GetPageList(long surveyId, PagingSettings settings);
        Page GetPageDetails(long surveyId, long pageId);
        List<Question> GetQuestionList(long surveyId, long pageId);
        List<Question> GetQuestionList(long surveyId, long pageId, PagingSettings settings);
        Question GetQuestionDetails(long surveyId, long pageId, long questionId);

        Task<List<Survey>> GetSurveyListAsync();
        Task<List<Survey>> GetSurveyListAsync(GetSurveyListSettings settings);
        Task<Survey> GetSurveyOverviewAsync(long surveyId);
        Task<Survey> GetSurveyDetailsAsync(long surveyId);
        Task<List<SurveyCategory>> GetSurveyCategoryListAsync();
        Task<List<SurveyCategory>> GetSurveyCategoryListAsync(GetSurveyCategoryListSettings settings);
        Task<List<SurveyTemplate>> GetSurveyTemplateListAsync();
        Task<List<SurveyTemplate>> GetSurveyTemplateListAsync(GetSurveyTemplateListSettings settings);
        Task<List<Page>> GetPageListAsync(long surveyId);
        Task<List<Page>> GetPageListAsync(long surveyId, PagingSettings settings);
        Task<Page> GetPageDetailsAsync(long surveyId, long pageId);
        Task<List<Question>> GetQuestionListAsync(long surveyId, long pageId);
        Task<List<Question>> GetQuestionListAsync(long surveyId, long pageId, PagingSettings settings);
        Task<Question> GetQuestionDetailsAsync(long surveyId, long pageId, long questionId);

        //Collectors
        Collector CreateCollector(long surveyId, CreateCollectorSettings settings);
        List<Collector> GetCollectorList(long surveyId);
        List<Collector> GetCollectorList(long surveyId, GetCollectorListSettings settings);
        Collector GetCollectorDetails(long collectorId);
        List<Message> GetMessageList(long collectorId);
        List<Message> GetMessageList(long collectorId, PagingSettings settings);
        Message GetMessageDetails(long collectorId, long messageId);
        List<Recipient> GetCollectorRecipientList(long collectorId);
        List<Recipient> GetCollectorRecipientList(long collectorId, GetRecipientListSettings settings);
        List<Recipient> GetMessageRecipientList(long collectorId, long messageId);
        List<Recipient> GetMessageRecipientList(long collectorId, long messageId, GetRecipientListSettings settings);
        Recipient GetRecipientDetails(long collectorId, long recipientId);

        Task<Collector> CreateCollectorAsync(long surveyId, CreateCollectorSettings settings);
        Task<List<Collector>> GetCollectorListAsync(long surveyId);
        Task<List<Collector>> GetCollectorListAsync(long surveyId, GetCollectorListSettings settings);
        Task<Collector> GetCollectorDetailsAsync(long collectorId);
        Task<List<Message>> GetMessageListAsync(long collectorId);
        Task<List<Message>> GetMessageListAsync(long collectorId, PagingSettings settings);
        Task<Message> GetMessageDetailsAsync(long collectorId, long messageId);
        Task<List<Recipient>> GetCollectorRecipientListAsync(long collectorId);
        Task<List<Recipient>> GetCollectorRecipientListAsync(long collectorId, GetRecipientListSettings settings);
        Task<List<Recipient>> GetMessageRecipientListAsync(long collectorId, long messageId);
        Task<List<Recipient>> GetMessageRecipientListAsync(long collectorId, long messageId, GetRecipientListSettings settings);
        Task<Recipient> GetRecipientDetailsAsync(long collectorId, long recipientId);

        //Responses
        Response GetSurveyResponseOverview(long surveyId, long responseId);
        Response GetSurveyResponseDetails(long surveyId, long responseId);
        Response GetCollectorResponseOverview(long collectorId, long responseId);
        Response GetCollectorResponseDetails(long collectorId, long responseId);
        List<Response> GetSurveyResponseOverviewList(long surveyId);
        List<Response> GetSurveyResponseDetailsList(long surveyId);
        List<Response> GetSurveyResponseOverviewList(long surveyId, GetResponseListSettings settings);
        List<Response> GetSurveyResponseDetailsList(long surveyId, GetResponseListSettings settings);
        List<Response> GetCollectorResponseOverviewList(long collectorId);
        List<Response> GetCollectorResponseDetailsList(long collectorId);
        List<Response> GetCollectorResponseOverviewList(long collectorId, GetResponseListSettings settings);
        List<Response> GetCollectorResponseDetailsList(long collectorId, GetResponseListSettings settings);

        Task<Response> GetSurveyResponseOverviewAsync(long surveyId, long responseId);
        Task<Response> GetSurveyResponseDetailsAsync(long surveyId, long responseId);
        Task<Response> GetCollectorResponseOverviewAsync(long collectorId, long responseId);
        Task<Response> GetCollectorResponseDetailsAsync(long collectorId, long responseId);
        Task<List<Response>> GetSurveyResponseOverviewListAsync(long surveyId);
        Task<List<Response>> GetSurveyResponseDetailsListAsync(long surveyId);
        Task<List<Response>> GetSurveyResponseOverviewListAsync(long surveyId, GetResponseListSettings settings);
        Task<List<Response>> GetSurveyResponseDetailsListAsync(long surveyId, GetResponseListSettings settings);
        Task<List<Response>> GetCollectorResponseOverviewListAsync(long collectorId);
        Task<List<Response>> GetCollectorResponseDetailsListAsync(long collectorId);
        Task<List<Response>> GetCollectorResponseOverviewListAsync(long collectorId, GetResponseListSettings settings);
        Task<List<Response>> GetCollectorResponseDetailsListAsync(long collectorId, GetResponseListSettings settings);

        //Data processing
        List<Survey> PopulateSurveyResponseInformationBulk(List<long> surveyIds);
        Survey PopulateSurveyResponseInformation(long surveyId);
        void MatchResponseToSurveyStructure(Survey survey, Response response); //no async equivalent

        Task<List<Survey>> PopulateSurveyResponseInformationBulkAsync(List<long> surveyIds);
        Task<Survey> PopulateSurveyResponseInformationAsync(long surveyId);

        //Users
        User GetUserDetails();
        List<Group> GetGroupList();
        List<Group> GetGroupList(PagingSettings settings);
        Group GetGroupDetails(long groupId);
        List<Member> GetMemberList(long groupId);
        List<Member> GetMemberList(long groupId, PagingSettings settings);
        Member GetMemberDetails(long groupId, long memberId);

        Task<User> GetUserDetailsAsync();
        Task<List<Group>> GetGroupListAsync();
        Task<List<Group>> GetGroupListAsync(PagingSettings settings);
        Task<Group> GetGroupDetailsAsync(long groupId);
        Task<List<Member>> GetMemberListAsync(long groupId);
        Task<List<Member>> GetMemberListAsync(long groupId, PagingSettings settings);
        Task<Member> GetMemberDetailsAsync(long groupId, long memberId);

        //Webhooks
        List<Webhook> GetWebhookList();
        List<Webhook> GetWebhookList(PagingSettings settings);
        Webhook GetWebhookDetails(long webhookId);
        Webhook CreateWebhook(Webhook webhook);
        Webhook ReplaceWebhook(long webhookId, Webhook webhook);
        Webhook ModifyWebhook(long webhookId, Webhook webhook);
        Webhook DeleteWebhook(long webhookId);

        Task<List<Webhook>> GetWebhookListAsync();
        Task<List<Webhook>> GetWebhookListAsync(PagingSettings settings);
        Task<Webhook> GetWebhookDetailsAsync(long webhookId);
        Task<Webhook> CreateWebhookAsync(Webhook webhook);
        Task<Webhook> ReplaceWebhookAsync(long webhookId, Webhook webhook);
        Task<Webhook> ModifyWebhookAsync(long webhookId, Webhook webhook);
        Task<Webhook> DeleteWebhookAsync(long webhookId);

        //Infrastructure
        int ApiRequestsMade { get; }
        void Dispose();
    }
}