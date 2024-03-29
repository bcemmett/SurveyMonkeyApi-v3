﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SurveyMonkey.Helpers;

namespace SurveyMonkey.ProcessedAnswers
{
    public class MatrixSingleAnswer : IProcessedResponse
    {
        public List<MatrixSingleAnswerRow> Rows { get; set; }
        public string OtherText { get; set; }

        public string PrettyPrint
        {
            get
            {
                var sb = new StringBuilder();
                if (Rows != null)
                {
                    foreach (var row in Rows)
                    {
                        if (row != null)
                        {
                            sb.Append($"{row.RowName}{(!String.IsNullOrWhiteSpace(row.RowName) ? ": " : String.Empty)}{row.Choice}{Environment.NewLine}");
                        }
                    }
                }
                if (!String.IsNullOrWhiteSpace(OtherText))
                {
                    sb.Append("Other: ");
                    sb.Append(OtherText);
                }
                return ProcessedAnswerFormatHelper.Trim(sb);
            }
        }
    }

    public class MatrixSingleAnswerRow
    {
        public string RowName { get; set; }
        public string Choice { get; set; }
    }
}