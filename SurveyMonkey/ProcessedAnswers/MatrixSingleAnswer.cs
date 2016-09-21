using System.Collections.Generic;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MatrixSingleAnswer : IProcessedResponse
    {
        public List<MatrixSingleAnswerRow> Rows { get; set; }
        public string OtherText { get; set; }
    }

    public class MatrixSingleAnswerRow
    {
        public string RowName { get; set; }
        public string Choice { get; set; }
    }
}