namespace SurveyMonkey.RequestSettings
{
    public class PagingSettings : IPagingSettings
    {
        public int? Page { get; set; }
        public int? PerPage { get; set; }
    }
}