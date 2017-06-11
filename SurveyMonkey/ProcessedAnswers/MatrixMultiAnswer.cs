using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MatrixMultiAnswer : IProcessedResponse
    {
        public List<MatrixMultiAnswerRow> Rows { get; set; }
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
                            sb.Append($"{row.RowName}:{Environment.NewLine}");
                            if (row.Choices != null)
                            {
                                foreach (var choice in row.Choices)
                                {
                                    sb.Append(choice);
                                    sb.Append(Environment.NewLine);
                                }
                                sb.Append(Environment.NewLine);
                            }
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

    public class MatrixMultiAnswerRow
    {
        public string RowName { get; set; }
        public List<string> Choices { get; set; }
    }
}