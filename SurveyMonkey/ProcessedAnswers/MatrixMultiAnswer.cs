using System.Collections.Generic;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MatrixMultiAnswer : IProcessedResponse
    {
        public List<MatrixMultiAnswerRow> Rows { get; set; }
        public string OtherText { get; set; }
    }

    public class MatrixMultiAnswerRow
    {
        public string RowName { get; set; }
        public List<string> Choices { get; set; }
    }
}