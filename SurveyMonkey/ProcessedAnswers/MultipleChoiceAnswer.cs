using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MultipleChoiceAnswer : IProcessedResponse
    {
        public List<string> Choices { get; set; }
        public string OtherText { get; set; }

        public string Printable
        {
            get
            {
                if (OtherText == null && (Choices == null || !Choices.Any()))
                {
                    return null;
                }
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

                return sb.ToString().TrimEnd();
            }
        }
    }
}