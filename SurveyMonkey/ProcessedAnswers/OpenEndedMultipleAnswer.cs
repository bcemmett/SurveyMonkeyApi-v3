using System;
using System.Collections.Generic;
using System.Text;

namespace SurveyMonkey.ProcessedAnswers
{
    public class OpenEndedMultipleAnswer : IProcessedResponse
    {
        public List<OpenEndedMultipleAnswerRow> Rows { get; set; }

        public string Printable
        {
            get
            {
                if (Rows == null)
                {
                    return null;
                }
                var sb = new StringBuilder();
                foreach (var row in Rows)
                {
                    sb.Append($"{row.RowName}: {row.Text}{Environment.NewLine}");
                }
                return sb.ToString().TrimEnd();
            }
        }
    }

    public class OpenEndedMultipleAnswerRow
    {
        public string Text { get; set; }
        public string RowName { get; set; }
    }
}