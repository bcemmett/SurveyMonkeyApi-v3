using System.Collections.Generic;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MatrixMenuAnswer : IProcessedResponse
    {
        public Dictionary<string, MatrixMenuAnswerRow> Rows { get; set; }
        public string OtherText { get; set; }
    }

    public class MatrixMenuAnswerRow
    {
        public Dictionary<string, string> Columns { get; set; }
    }
}