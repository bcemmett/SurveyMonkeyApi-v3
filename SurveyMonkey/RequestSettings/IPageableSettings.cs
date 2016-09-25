namespace SurveyMonkey.RequestSettings
{
    internal interface IPageableSettings
    {
        int? Page { get; set; }
        int? PerPage { get; set; }
    }
}