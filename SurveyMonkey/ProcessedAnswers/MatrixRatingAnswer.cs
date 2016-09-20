using System.Collections.Generic;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MatrixRatingAnswer : IProcessedResponse
    {
        public List<MatrixRatingAnswerRow> Rows { get; set; }
        public string OtherText { get; set; }
    }

    public class MatrixRatingAnswerRow
    {
        public string RowName { get; set; }
        public string Choice { get; set; }
        public string OtherText { get; set; }
    }
}