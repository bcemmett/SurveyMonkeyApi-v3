using System;
using System.Collections.Generic;
using System.Text;
using SurveyMonkey.Helpers;

namespace SurveyMonkey.ProcessedAnswers
{
    public class OpenEndedMultipleAnswer : IProcessedResponse
    {
        public List<OpenEndedMultipleAnswerRow> Rows { get; set; }

        public string PrettyPrint
        {
            get
            {
                var sb = new StringBuilder();
                if (Rows != null)
                {
                    foreach (var row in Rows)
                    {
                        sb.Append($"{row.RowName}: {row.Text}{Environment.NewLine}");
                    }
                }

                return ProcessedAnswerFormatHelper.Trim(sb);
            }
        }
    }

    public class OpenEndedMultipleAnswerRow
    {
        public string Text { get; set; }
        public string RowName { get; set; }
    }
}