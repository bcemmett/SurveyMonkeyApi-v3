namespace SurveyMonkey.RequestSettings
{
    public class GetRecipientListSettings : IPagingSettings
    {
        public int? Page { get; set; }
        public int? PerPage { get; set; }
        internal string Include => "survey_response_status,mail_status,custom_fields,remove_link,extra_fields,survey_link";
    }
}