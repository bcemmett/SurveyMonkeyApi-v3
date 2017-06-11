using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MatrixSingleAnswer : IProcessedResponse
    {
        public List<MatrixSingleAnswerRow> Rows { get; set; }
        public string OtherText { get; set; }

        public string Printable
        {
            get
            {
                if ((Rows == null || !Rows.Any()) && OtherText == null)
                {
                    return null;
                }
                var sb = new StringBuilder();
                if (Rows != null && Rows.Any())
                {
                    foreach (var row in Rows)
                    {
                        if (row != null)
                        {
                            sb.Append($"{row.RowName}: {row.Choice}{Environment.NewLine}");
                        }
                    }
                }
                if (OtherText != null)
                {
                    sb.Append("Other: ");
                    sb.Append(OtherText);
                }
                return sb.ToString().TrimEnd();
            }
        }
    }

    public class MatrixSingleAnswerRow
    {
        public string RowName { get; set; }
        public string Choice { get; set; }
    }
}