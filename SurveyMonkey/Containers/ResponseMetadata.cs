using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    internal class ResponseMetadata
    {
        internal Dictionary<string, MetadataTypeValuePair> Contact { get; set; }
    }
}