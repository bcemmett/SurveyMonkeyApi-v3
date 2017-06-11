using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.Helpers
{
    internal static class ProcessedAnswerFormatHelper
    {
        internal static string Trim(StringBuilder sb)
        {
            var printable = sb.ToString();
            if (String.IsNullOrWhiteSpace(printable))
            {
                return null;
            }
            return printable.Trim();
        }
    }
}