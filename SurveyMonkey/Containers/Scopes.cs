using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class Scopes
    {
        public List<string> Available { get; set; }
        public List<string> Granted { get; set; }
    }
}
