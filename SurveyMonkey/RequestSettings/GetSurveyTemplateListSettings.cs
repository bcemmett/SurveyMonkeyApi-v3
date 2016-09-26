namespace SurveyMonkey.RequestSettings
{
    public class GetSurveyTemplateListSettings : PagingSettings
    {
        public string Language { get; set; } //TODO ideally would use an enum if they'll share a list
        public string Category { get; set; }
    }
}