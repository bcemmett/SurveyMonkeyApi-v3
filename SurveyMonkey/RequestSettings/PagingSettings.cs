namespace SurveyMonkey.RequestSettings
{
    public class PagingSettings : IPageableSettings
    {
        public int? Page { get; set; }
        public int? PerPage { get; set; }
    }
}