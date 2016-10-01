namespace SurveyMonkey.RequestSettings
{
    internal interface IPagingSettings
    {
        int? Page { get; set; }
        int? PerPage { get; set; }
    }
}