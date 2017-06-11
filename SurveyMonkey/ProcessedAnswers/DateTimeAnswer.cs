using System;
using System.Collections.Generic;
using System.Text;

namespace SurveyMonkey.ProcessedAnswers
{
    public class DateTimeAnswer : IProcessedResponse
    {
        public List<DateTimeAnswerRow> Rows { get; set; }

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
                    sb.Append($"{row.RowName}: {row.TimeStamp:u}{Environment.NewLine}");
                }
                return sb.ToString().TrimEnd();
            }
        }
    }

    public class DateTimeAnswerRow
    {
        public DateTime TimeStamp { get; set; }
        public string RowName { get; set; }
    }
}