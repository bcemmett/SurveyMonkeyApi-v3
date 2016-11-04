using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SurveyMonkey.Enums;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class QuestionDisplayOptions : IPageableContainer
    {
        public string MiddleLabel { get; set; }
        public bool ShowDisplayNumber { get; set; }
        public string DisplaySubtype { get; set; }
        public string RightLabel { get; set; }
        public string DisplayType { get; set; }
        public string LeftLabel { get; set; }

        public QuestionDisplayOptionsCustomOptions CustomOptions { get; set; }

    }
}
