namespace SurveyMonkey.RequestSettings
{
    public class GetSurveyCategoryListSettings : PagingSettings
    {
        public string Language { get; set; } //TODO ideally would use an enum if they'll share a list
    }
}