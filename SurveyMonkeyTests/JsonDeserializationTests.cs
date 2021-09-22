using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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

        [Test]
        public void DateTimeWithMilisecondsInWinterTime()
        {
            string input = @"{""Timestamp"":""2016-01-05T12:10:20.397000+00:00""}";
            DeserializeAndTestDateTime(input, new DateTime(2016, 1, 5, 12, 10, 20, 397, DateTimeKind.Utc));
        }

        [Test]
        public void DateTimeWithMilisecondsInSummerTime()
        {
            string input = @"{""Timestamp"":""2016-01-05T12:10:20.397000+00:00""}";
            DeserializeAndTestDateTime(input, new DateTime(2016, 1, 5, 12, 10, 20, 397, DateTimeKind.Utc));
        }

        [Test]
        public void NullDateTimeIsLeftNull()
        {
            string input = @"{""Timestamp"":null}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.Timestamp);
        }

        [Test]
        public void AbsentDateTimeIsLeftNull()
        {
            string input = @"{}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.Timestamp);
        }

        [Test]
        public void BadlyFormatedDateTimeThrows()
        {
            string input = @"{""Timestamp"":""Asdf""}";
            var parsed = JObject.Parse(input);
            Assert.Throws<JsonReaderException>(delegate { parsed.ToObject<JsonDeserializationTestsContainer>(); });
        }

        private void DeserializeAndTestDateTime(string input, DateTime desiredResult)
        {
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(DateTimeKind.Utc, output.Timestamp.Value.Kind);
            Assert.AreEqual(desiredResult, output.Timestamp);
        }

        #endregion

        #region StringDeserialization

        [Test]
        public void StringsAreDeserialized()
        {
            string input = @"{""AString"":""hEllo""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual("hEllo", output.AString);
        }

        [Test]
        public void NumericStringsAreLeftAsStrings()
        {
            string input = @"{""AString"":""12345""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual("12345", output.AString);
        }

        [Test]
        public void NullStringsAreLeftNull()
        {
            string input = @"{""AString"":null}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.AString);
        }

        [Test]
        public void AbsentStringsAreLeftNull()
        {
            string input = @"{}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.AString);
        }

        [Test]
        public void StringContainingNullAreDeserialized()
        {
            string input = @"{""AString"":""null""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual("null", output.AString);
        }

        #endregion

        #region IntDeserialization

        [Test]
        public void IntsAreDeserialized()
        {
            string input = @"{""AnInt"":12345}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(12345, output.AnInt);
        }

        [Test]
        public void NullIntsAreLeftNull()
        {
            string input = @"{""AnInt"":null}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.AnInt);
        }

        [Test]
        public void StringyIntsAreLeftNull()
        {
            string input = @"{""AnInt"":""Asf3s""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.AnInt);
        }

        [Test]
        public void StringyNumericIntsAreDeserialized()
        {
            string input = @"{""AnInt"":""12345""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(12345, output.AnInt);
        }

        [Test]
        public void OverflowingIntsThrow()
        {
            string input = @"{""AnInt"":2134515487945}";
            var parsed = JObject.Parse(input);
            Assert.Throws<JsonReaderException>(delegate { parsed.ToObject<JsonDeserializationTestsContainer>(); });
        }

        [Test]
        public void OverflowingStringyIntsThrow()
        {
            string input = @"{""AnInt"":""2134515487945""}";
            var parsed = JObject.Parse(input);
            Assert.Throws<JsonReaderException>(delegate { parsed.ToObject<JsonDeserializationTestsContainer>(); });
        }

        [Test]
        public void AbsentIntsAreLeftNull()
        {
            string input = @"{}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.AnInt);
        }

        #endregion

        #region LongDeserialization

        [Test]
        public void LongsAreDeserialized()
        {
            string input = @"{""ALong"":12345}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(12345, output.ALong);
        }

        [Test]
        public void NullLongsAreLeftNull()
        {
            string input = @"{""ALong"":null}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.ALong);
        }

        [Test]
        public void StringyLongsAreLeftNull()
        {
            string input = @"{""ALong"":""Asf3s""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.ALong);
        }

        [Test]
        public void StringyNumericLongsAreDeserialized()
        {
            string input = @"{""ALong"":""12345""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(12345, output.ALong);
        }

        [Test]
        public void LargeLongsAreDeserialized()
        {
            string input = @"{""ALong"":2134515487945}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(2134515487945, output.ALong);
        }

        [Test]
        public void LargeStringyLongsAreDeserialized()
        {
            string input = @"{""ALong"":""2134515487945""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(2134515487945, output.ALong);
        }

        [Test]
        public void AbsentLongsAreLeftNull()
        {
            string input = @"{}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.ALong);
        }

        #endregion

        #region EnumDeserialization

        [Test]
        public void EnumsAreDeserialized()
        {
            string input = @"{""AnEnum"":""ItemOne""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(JsonDeserializationTestsContainer.EnumType.ItemOne, output.AnEnum);
        }

        [Test]
        public void DifferentlyCasedEnumsAreDeserialized()
        {
            string input = @"{""AnEnum"":""itemone""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(JsonDeserializationTestsContainer.EnumType.ItemOne, output.AnEnum);
        }

        [Test]
        public void EnumsWithUnderscoresAreDeserialized()
        {
            string input = @"{""AnEnum"":""iTem_oNE""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(JsonDeserializationTestsContainer.EnumType.ItemOne, output.AnEnum);
        }

        [Test]
        public void NumericEnumsAreDeserialized()
        {
            string input = @"{""AnEnum"":2}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(JsonDeserializationTestsContainer.EnumType.Item3, output.AnEnum);
        }

        [Test]
        public void StringyNumericEnumsAreLeftNull()
        {
            string input = @"{""AnEnum"":""2""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.AnEnum);
        }

        [Test]
        public void EnumsWithNonMatchingTextAreLeftNull()
        {
            string input = @"{""AnEnum"":""FourthItem""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.AnEnum);
        }

        [Test]
        public void NullEnumsAreLeftNull()
        {
            string input = @"{""AnEnum"":null}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.AnEnum);
        }

        [Test]
        public void AbsentEnumsAreLeftNull()
        {
            string input = @"{}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.AnEnum);
        }

        #endregion

        #region BoolDeserialization

        [Test]
        public void BoolsAreDeserialized()
        {
            string input = @"{""ABool"":true}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(true, output.ABool);
        }

        [Test]
        public void StringyBoolsAreDeserialized()
        {
            string input = @"{""ABool"":""true""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(true, output.ABool);
        }

        [Test]
        public void NumericOneBoolsAreDeserialized()
        {
            string input = @"{""ABool"":1}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(true, output.ABool);
        }

        [Test]
        public void NumericZeroBoolsAreDeserialized()
        {
            string input = @"{""ABool"":0}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(false, output.ABool);
        }

        [Test]
        public void NumericLargeBoolsAreDeserialized()
        {
            string input = @"{""ABool"":5236}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual(true, output.ABool);
        }

        [Test]
        public void MalformedBoolsThrow()
        {
            string input = @"{""ABool"":""asdf""}";
            var parsed = JObject.Parse(input);
            Assert.Throws<JsonReaderException>(delegate { parsed.ToObject<JsonDeserializationTestsContainer>();});
        }

        [Test]
        public void AbsentBoolsAreLeftNull()
        {
            string input = @"{}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.IsNull(output.ABool);
        }

        #endregion

        #region PropertyNameMatching

        [Test]
        public void ExactPropertyNamesMatch()
        {
            string input = @"{""AString"":""hEllo""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual("hEllo", output.AString);
        }

        [Test]
        public void SnakeCasePropertyNamesMatch()
        {
            string input = @"{""a_string"":""hEllo""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual("hEllo", output.AString);
        }

        [Test]
        public void LowerCasePropertyNamesMatch()
        {
            string input = @"{""astring"":""hEllo""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual("hEllo", output.AString);
        }

        [Test]
        public void RandomCasePropertyNamesMatch()
        {
            string input = @"{""aStRinG"":""hEllo""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual("hEllo", output.AString);
        }

        [Test]
        public void SurpurflousUnderscorePropertyNamesMatch()
        {
            string input = @"{""astri_ng"":""hEllo""}";
            var parsed = JObject.Parse(input);
            var output = parsed.ToObject<JsonDeserializationTestsContainer>();
            Assert.AreEqual("hEllo", output.AString);
        }

        [Test]
        public void UnknownPropertiesAreIgnored()
        {
            string input = @"{""a_string"":""hi"",""some_random_object"":""hi"",""an_int"":5}";
            var parsed = JObject.Parse(input);
            var result = new JsonDeserializationTestsContainer();
            Assert.DoesNotThrow(delegate { result = parsed.ToObject<JsonDeserializationTestsContainer>(); });
            Assert.AreEqual("hi", result.AString);
            Assert.AreEqual(5, result.AnInt);
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
        public void AllCustomObjectsUseTheTolerantJsonDeserializer()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(t => t.GetTypes())
               .Where(t => (t.IsClass && t.Namespace == "SurveyMonkey.Containers") || (t.IsEnum && (t.Namespace == "SurveyMonkey.Enums" || t.Namespace == "SurveyMonkey.Containers")));

            foreach (var type in types)
            {
                var compilerGeneratedAttribute = (CompilerGeneratedAttribute)Attribute.GetCustomAttributes(type, typeof(CompilerGeneratedAttribute)).FirstOrDefault();
                if (compilerGeneratedAttribute == null)
                {
                    var failMessage = String.Format("{0} needs the [JsonConverter(typeof(CaseTolerantJsonConverter))] attribute", type);
                    var element = (JsonConverterAttribute)Attribute.GetCustomAttributes(type, typeof(JsonConverterAttribute)).FirstOrDefault();
                    Assert.IsNotNull(element, failMessage);
                    Assert.AreEqual(typeof(TolerantJsonConverter), element.ConverterType, failMessage);
                }
            }
        }

        #endregion
    }

    [JsonConverter(typeof(WarningSupressingTolerantJsonConverter))]
    public class JsonDeserializationTestsContainer
    {
        [JsonConverter(typeof(WarningSupressingTolerantJsonConverter))]
        public enum EnumType
        {
            ItemOne,
            ITEMTWO,
            Item3
        }

        public DateTime? Timestamp { get; set; }
        public string AString { get; set; }
        public long? ALong { get; set; }
        public int? AnInt { get; set; }
        public EnumType? AnEnum { get; set; }
        public bool? ABool { get; set; }
    }

    internal class WarningSupressingTolerantJsonConverter : TolerantJsonConverter
    {
    }
}