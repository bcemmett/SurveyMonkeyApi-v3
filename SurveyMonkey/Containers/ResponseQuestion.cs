using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class ResponseQuestion
    {
        public long Id { get; set; }
        public long VariableId { get; set; }
        public List<ResponseAnswer> Answers { get; set; }
    }
}