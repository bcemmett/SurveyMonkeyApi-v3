using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SurveyMonkey.Helpers;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MatrixRatingAnswer : IProcessedResponse
    {
        public List<MatrixRatingAnswerRow> Rows { get; set; }
        public string OtherText { get; set; }

        public string PrettyPrint
        {
            get
            {
                var sb = new StringBuilder();
                if (Rows != null)
                {
                    foreach (var row in Rows)
                    {
                        if (row != null)
                        {
                            sb.Append($"{row.RowName}{(!String.IsNullOrWhiteSpace(row.RowName) ? ": " : String.Empty)}{row.Choice}");
                            if (!String.IsNullOrWhiteSpace(row.OtherText))
                            {
                                sb.Append($" (Other: {row.OtherText})");
                            }
                            sb.Append(Environment.NewLine);
                        }
                    }
                }
                if (!String.IsNullOrWhiteSpace(OtherText))
                {
                    sb.Append("Other: ");
                    sb.Append(OtherText);
                }
                return ProcessedAnswerFormatHelper.Trim(sb);
            }
        }
    }

    public class MatrixRatingAnswerRow
    {
        public string RowName { get; set; }
        public string Choice { get; set; }
        public string OtherText { get; set; }
    }
}