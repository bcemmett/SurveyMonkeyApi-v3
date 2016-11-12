using Newtonsoft.Json;
using System.Collections.Generic;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class QuestionDisplayOptionsCustomOptions
    {
        public List<string> OptionSet { get; set; }
        public int StartingPosition { get; set; }
        public int StepSize { get; set; }
    }
}