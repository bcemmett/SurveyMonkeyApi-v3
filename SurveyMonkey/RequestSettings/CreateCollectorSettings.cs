namespace SurveyMonkey.RequestSettings
{
    public class CreateCollectorSettings
    {
        public enum TypeOption
        {
            Weblink,
            Email
        }

        public TypeOption Type { get; set; }
        public string Name { get; set; }
    }
}