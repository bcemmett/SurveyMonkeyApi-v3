using System;
using System.Text;
using SurveyMonkey.Helpers;

namespace SurveyMonkey.ProcessedAnswers
{
    public class SingleChoiceAnswer : IProcessedResponse
    {
        public string Choice { get; set; }
        public string OtherText { get; set; }

        public string PrettyPrint
        {
            get
            {
                var sb = new StringBuilder();
                if (Choice != null)
                {
                    sb.Append(Choice);
                    sb.Append(Environment.NewLine);
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