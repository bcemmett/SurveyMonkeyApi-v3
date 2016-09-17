using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SurveyMonkey;

namespace SurveyMonkeyTests
{
    [TestFixture]
    class JsonDateTimeTests
    {
        [Test]
        public void ExplitZeroOffsetInSummerTime()
        {
            string input = @"{""Timestamp"":""2016-07-05T12:10:20+00:00""}";
            DeserialiseAndTest(input, new DateTime(2016, 7, 5, 12, 10, 20, DateTimeKind.Utc));
        }

        [Test]
        public void ExplitZeroOffsetInWinterTime()
        {
            string input = @"{""Timestamp"":""2016-01-05T12:10:20+00:00""}";
            DeserialiseAndTest(input, new DateTime(2016, 1, 5, 12, 10, 20, DateTimeKind.Utc));
        }

        [Test]
        public void NoOffsetInSummerTime()
        {
            string input = @"{""Timestamp"":""2016-07-05T12:10:20""}";
            DeserialiseAndTest(input, new DateTime(2016, 7, 5, 12, 10, 20, DateTimeKind.Utc));
        }

        [Test]
        public void NoOffsetInWinterTime()
        {
            string input = @"{""Timestamp"":""2016-01-05T12:10:20""}";
            DeserialiseAndTest(input, new DateTime(2016, 1, 5, 12, 10, 20, DateTimeKind.Utc));
        }

        [Test]
        public void ExplicitOffsetInSummerTime()
        {
            string input = @"{""Timestamp"":""2016-07-05T15:10:20+03:00""}";
            DeserialiseAndTest(input, new DateTime(2016, 7, 5, 12, 10, 20, DateTimeKind.Utc));
        }

        [Test]
        public void ExplicitOffsetInWinterTime()
        {
            string input = @"{""Timestamp"":""2016-01-05T15:10:20+03:00""}";
            DeserialiseAndTest(input, new DateTime(2016, 1, 5, 12, 10, 20, DateTimeKind.Utc));
        }

        private void DeserialiseAndTest(string input, DateTime desiredResult)
        {
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDateTimeTestsContainer>();
            Assert.AreEqual(DateTimeKind.Utc, output.Timestamp.Value.Kind);
            Assert.AreEqual(desiredResult, output.Timestamp);
        }
    }

    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    class JsonDateTimeTestsContainer
    {
        public DateTime? Timestamp { get; set; }
    }
}