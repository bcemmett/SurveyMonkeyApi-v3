using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    internal class ResponseMetadata
    {
        internal Dictionary<string, MetadataTypeValuePair> Contact { get; set; }

        internal string GetValueByKeyOrNull(string key)
        {
            if (Contact == null)
            {
                return null;
            }
            if (!Contact.ContainsKey(key))
            {
                return null;
            }
            string value = Contact[key].Value;
            if (String.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            return value;
        }
    }
}