using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SurveyMonkey.Helpers;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MatrixRankingAnswer : IProcessedResponse
    {
        public Dictionary<int, string> Ranking { get; set; }
        public List<string> NotApplicable { get; set; }
        public string NotApplicableText { get; set; }

        public string PrettyPrint
        {
            get
            {
                var sb = new StringBuilder();
                if (Ranking != null)
                {
                    var keys = Ranking.Keys.ToList();
                    keys.Sort();
                    foreach (var key in keys)
                    {
                        sb.Append($"{key}: {Ranking[key]}{Environment.NewLine}");
                    }
                }
                if (NotApplicable != null && NotApplicable.Any())
                {
                    sb.Append(NotApplicableText);
                    sb.Append(":");
                    sb.Append(Environment.NewLine);
                    foreach (var item in NotApplicable)
                    {
                        sb.Append(item);
                        sb.Append(Environment.NewLine);
                    }
                }
                return ProcessedAnswerFormatHelper.Trim(sb);
            }
        }
    }
}