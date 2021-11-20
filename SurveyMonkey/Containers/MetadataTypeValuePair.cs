namespace SurveyMonkey.Containers
{
    [Newtonsoft.Json.JsonConverter(typeof(TolerantJsonConverter))]
    internal class MetadataTypeValuePair
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}