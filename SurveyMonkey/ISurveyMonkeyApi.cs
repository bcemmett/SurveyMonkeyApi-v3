using System.Collections.Generic;
using SurveyMonkey.Containers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public interface ISurveyMonkeyApi
    {
        List<Survey> GetSurveyList();
        List<Survey> GetSurveyList(GetSurveyListSettings settings);
        Survey GetSurveyOverview(long surveyId);
        Survey GetSurveyDetails(long surveyId);
        List<Response> GetSurveyResponseOverviewList(long surveyId);
        List<Response> GetSurveyResponseDetailsList(long surveyId);
        List<Response> GetSurveyResponseOverviewList(long surveyId, GetResponseListSettings settings);
        List<Response> GetSurveyResponseDetailsList(long surveyId, GetResponseListSettings settings);
        List<Response> GetCollectorResponseOverviewList(long collectorId);
        List<Response> GetCollectorResponseDetailsList(long collectorId);
        List<Response> GetCollectorResponseOverviewList(long collectorId, GetResponseListSettings settings);
        List<Response> GetCollectorResponseDetailsList(long collectorId, GetResponseListSettings settings);
        Response GetSurveyResponseOverview(long surveyId, long responseId);
        Response GetSurveyResponseDetails(long surveyId, long responseId);
        Response GetCollectorResponseOverview(long collectorId, long responseId);
        Response GetCollectorResponseDetails(long collectorId, long responseId);
        List<Survey> PopulateSurveyResponseInformationBulk(List<long> surveyIds);
        Survey PopulateSurveyResponseInformation(long surveyId);
        List<Collector> GetCollectorList(long surveyId);
        List<Collector> GetCollectorList(long surveyId, GetCollectorListSettings settings);
        Collector GetCollectorDetails(long collectorId);
        List<Message> GetMessageList(long collectorId);
        List<Message> GetMessageList(long collectorId, PagingSettings settings);
        Message GetMessageDetails(long collectorId, long messageId);
        List<Recipient> GetRecipientList(long collectorId, long messageId);
        List<Recipient> GetRecipientList(long collectorId, long messageId, PagingSettings settings);
        Recipient GetRecipientDetails(long collectorId, long recipientId);
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
        User GetUserDetails();
        List<Group> GetGroupList();
        List<Group> GetGroupList(PagingSettings settings);
        Group GetGroupDetails(long groupId);
        List<Member> GetMemberList(long groupId);
        List<Member> GetMemberList(long groupId, PagingSettings settings);
        Member GetMemberDetails(long groupId, long memberId);
        void Dispose();
    }
}