using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SurveyMonkey.Helpers;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MultipleChoiceAnswer : IProcessedResponse
    {
        public List<string> Choices { get; set; }
        public string OtherText { get; set; }

        public string PrettyPrint
        {
            get
            {
                var sb = new StringBuilder();
                if (Choices != null)
                {
                    foreach (var choice in Choices)
                    {
                        sb.Append(choice);
                        sb.Append(Environment.NewLine);
                    }
                }
                if (OtherText != null)
                {
                    sb.Append($"Other: {OtherText}");
                }

                return ProcessedAnswerFormatHelper.Trim(sb);
            }
        }
    }
}