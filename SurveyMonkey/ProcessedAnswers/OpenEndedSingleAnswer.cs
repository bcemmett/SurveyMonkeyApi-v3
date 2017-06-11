using System.Text;
using SurveyMonkey.Helpers;

namespace SurveyMonkey.ProcessedAnswers
{
    public class OpenEndedSingleAnswer : IProcessedResponse
    {
        public string Text { get; set; }
        public string Printable
        {
            get
            {
                var sb = new StringBuilder();
                if (Text != null)
                {
                    sb.Append(Text);
                }
                return ProcessedAnswerFormatHelper.Trim(sb);
            }
        }
    }
}