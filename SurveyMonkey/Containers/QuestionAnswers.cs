using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class QuestionAnswers
    {
        public List<Choice> Choices { get; set; }
        public List<Row> Rows { get; set; }
        public List<Column> Cols { get; set; }
    }
}