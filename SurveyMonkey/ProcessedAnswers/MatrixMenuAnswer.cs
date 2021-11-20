using System;
using System.Collections.Generic;
using System.Text;
using SurveyMonkey.Helpers;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MatrixMenuAnswer : IProcessedResponse
    {
        public Dictionary<string, MatrixMenuAnswerRow> Rows { get; set; }
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
                        sb.Append($"{row.Key}{(!String.IsNullOrWhiteSpace(row.Key) ? ":" : String.Empty)}{Environment.NewLine}");
                        if (row.Value.Columns != null)
                        {
                            foreach (var col in row.Value.Columns)
                            {
                                sb.Append($"{col.Key}{(!String.IsNullOrWhiteSpace(col.Key) ? ": " : String.Empty)}{col.Value}{Environment.NewLine}");
                            }
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

    public class MatrixMenuAnswerRow
    {
        public Dictionary<string, string> Columns { get; set; }
    }
}