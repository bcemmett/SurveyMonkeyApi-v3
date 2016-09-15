using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    public class ResponsePage
    {
        public long? Id { get; set; }
        public List<ResponseQuestion> Questions { get; set; }
    }
}
