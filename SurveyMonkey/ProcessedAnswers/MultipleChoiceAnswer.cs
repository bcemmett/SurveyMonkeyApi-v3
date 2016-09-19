using System.Collections.Generic;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MultipleChoiceAnswer : IProcessedResponse
    {
        public List<string> Choices { get; set; }
        public string OtherText { get; set; }
    }
}