namespace SurveyMonkey.RequestSettings
{
    public class GetSurveyCategoriesSettings : PagingSettings
    {
        public string Language { get; set; } //TODO ideally would use an enum if they'll share a list
    }
}