using System.Collections.Generic;
using Newtonsoft.Json;
using SurveyMonkey.ProcessedAnswers;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class ResponseQuestion
    {
        public long? Id { get; set; }
        public long? VariableId { get; set; }
        public List<ResponseAnswer> Answers { get; set; }

        public ProcessedAnswer ProcessedAnswer { get; set; }
    }
}