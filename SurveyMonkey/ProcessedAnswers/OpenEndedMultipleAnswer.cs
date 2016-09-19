using System.Collections.Generic;

namespace SurveyMonkey.ProcessedAnswers
{
    public class OpenEndedMultipleAnswer : IProcessedResponse
    {
        public List<OpenEndedMultipleAnswerRow> Rows { get; set; }
    }

    public class OpenEndedMultipleAnswerRow
    {
        public string Text { get; set; }
        public string RowName { get; set; }
    }
}