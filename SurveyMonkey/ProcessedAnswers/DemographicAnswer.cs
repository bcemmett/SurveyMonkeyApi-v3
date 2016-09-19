namespace SurveyMonkey.ProcessedAnswers
{
    public class DemographicAnswer : IProcessedResponse
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}