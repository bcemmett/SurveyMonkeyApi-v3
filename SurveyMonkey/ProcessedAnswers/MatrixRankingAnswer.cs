using System;
using System.Collections.Generic;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MatrixRankingAnswer : IProcessedResponse
    {
        public Dictionary<int, string> Ranking { get; set; }
        public List<string> NotApplicable { get; set; }
    }
}