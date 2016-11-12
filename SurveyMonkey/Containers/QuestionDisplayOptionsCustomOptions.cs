using Newtonsoft.Json;
using System.Collections.Generic;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class QuestionDisplayOptionsCustomOptions
    {
        public List<string> OptionSet { get; set; }
        public int StartingPosition { get; set; } //slider questions
        public int StepSize { get; set; } //slider questions
        public string Color { get; set; } //star rating questions
    }
}