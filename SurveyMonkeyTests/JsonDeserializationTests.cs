using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SurveyMonkey;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class JsonDeserializationTests
    {
        #region DateTimeDeserialization

        [Test]
        public void DateTimeExplitZeroOffsetInSummerTime()
        {
            string input = @"{""Timestamp"":""2016-07-05T12:10:20+00:00""}";
            DeserializeAndTestDateTime(input, new DateTime(2016, 7, 5, 12, 10, 20, DateTimeKind.Utc));
        }

        [Test]
        public void DateTimeExplitZeroOffsetInWinterTime()
        {
            string input = @"{""Timestamp"":""2016-01-05T12:10:20+00:00""}";
            DeserializeAndTestDateTime(input, new DateTime(2016, 1, 5, 12, 10, 20, DateTimeKind.Utc));
        }

        [Test]
        public void DateTimeNoOffsetInSummerTime()
        {
            string input = @"{""Timestamp"":""2016-07-05T12:10:20""}";
            DeserializeAndTestDateTime(input, new DateTime(2016, 7, 5, 12, 10, 20, DateTimeKind.Utc));
        }

        [Test]
        public void DateTimeNoOffsetInWinterTime()
        {
            string input = @"{""Timestamp"":""2016-01-05T12:10:20""}";
            DeserializeAndTestDateTime(input, new DateTime(2016, 1, 5, 12, 10, 20, DateTimeKind.Utc));
        }

        [Test]
        public void DateTimeExplicitOffsetInSummerTime()
        {
            string input = @"{""Timestamp"":""2016-07-05T15:10:20+03:00""}";
            DeserializeAndTestDateTime(input, new DateTime(2016, 7, 5, 12, 10, 20, DateTimeKind.Utc));
        }

        [Test]
        public void DateTimeExplicitOffsetInWinterTime()
        {
            string input = @"{""Timestamp"":""2016-01-05T15:10:20+03:00""}";
            DeserializeAndTestDateTime(input, new DateTime(2016, 1, 5, 12, 10, 20, DateTimeKind.Utc));
        }

        private void DeserializeAndTestDateTime(string input, DateTime desiredResult)
        {
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDateTimeTestsContainer>();
            Assert.AreEqual(DateTimeKind.Utc, output.Timestamp.Value.Kind);
            Assert.AreEqual(desiredResult, output.Timestamp);
        }

        #endregion

        #region ObjectsHaveBeenWrittenToUseCorrectTypesAndConverters

        [Test]
        public void AllValueTypesAreMadeNullable()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && t.Namespace == "SurveyMonkey.Containers");

            foreach (var type in types)
            {
                PropertyInfo[] properties = type.GetProperties();
                foreach (var property in properties)
                {
                    Assert.IsTrue((Nullable.GetUnderlyingType(property.PropertyType) != null || !property.PropertyType.IsValueType),
                        String.Format("Type: {0}, Property: {1}", type, property));
                }
            }
        }

        [Test]
        public void AllContainerUseTheLaxJsonPropertyDeserialiser()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(t => t.GetTypes())
               .Where(t => t.IsClass && t.Namespace == "SurveyMonkey.Containers");

            foreach (var type in types)
            {
                var failMessage = String.Format("{0} needs the [JsonConverter(typeof(LaxPropertyNameJsonConverter))] attribute", type);
                var element = (JsonConverterAttribute)Attribute.GetCustomAttributes(type, typeof(JsonConverterAttribute)).FirstOrDefault();
                Assert.IsNotNull(element, failMessage);
                Assert.AreEqual(typeof(LaxPropertyNameJsonConverter),element.ConverterType, failMessage);
            }
        }

        [Test]
        public void AllEnumsUseTheLaxEnumDeserialiser()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(t => t.GetTypes())
               .Where(t => t.IsEnum && (t.Namespace == "SurveyMonkey.Enums" || t.Namespace == "SurveyMonkey.Containers"));

            foreach (var type in types)
            {
                var failMessage = String.Format("{0} needs the [JsonConverter(typeof(LaxEnumJsonConverter))] attribute", type);
                var element = (JsonConverterAttribute)Attribute.GetCustomAttributes(type, typeof(JsonConverterAttribute)).FirstOrDefault();
                Assert.IsNotNull(element, failMessage);
                Assert.AreEqual(typeof(LaxEnumJsonConverter), element.ConverterType, failMessage);
            }
        }

#endregion
    }

    [JsonConverter(typeof(LaxPropertyNameJsonConverter))]
    class JsonDateTimeTestsContainer
    {
        public DateTime? Timestamp { get; set; }
    }
}