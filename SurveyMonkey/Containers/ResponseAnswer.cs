﻿using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class ResponseAnswer
    {
        public long? ChoiceId { get; set; }
        public long? RowId { get; set; }
        public long? ColId { get; set; }
        public long? OtherId { get; set; }
        public string Text { get; set; }
        public bool? IsCorrect { get; set; }
        public int? Score { get; set; }
        public string DownloadUrl { get; set; }
        public string ContentType { get; set; }
        public string SimpleText { get; set; }
        [JsonIgnore]
        internal object TagData { get; set; }
        public ChoiceMetadata ChoiceMetadata { get; set; }
    }
}