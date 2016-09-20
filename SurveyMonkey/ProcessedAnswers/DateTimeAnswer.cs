using System;
using System.Collections.Generic;

namespace SurveyMonkey.ProcessedAnswers
{
    public class DateTimeAnswer : IProcessedResponse
    {
        public List<DateTimeAnswerRow> Rows { get; set; }
    }

    public class DateTimeAnswerRow
    {
        public DateTime TimeStamp { get; set; }
        public string RowName { get; set; }
    }
}