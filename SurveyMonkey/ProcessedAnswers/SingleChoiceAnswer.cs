namespace SurveyMonkey.ProcessedAnswers
{
    public class SingleChoiceAnswer : IProcessedResponse
    {
        public string Choice { get; set; }
        public string OtherText { get; set; }
    }
}