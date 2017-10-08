using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SurveyMonkey.Helpers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkeyTests
{
    [TestFixture]
    class RequestSettingsHelperTests
    {
        [Test]
        public void ObjectPropertiesAreProcessed()
        {
            var input = new LotsOfProperties()
            {
                Time1 = new DateTime(2016, 5, 1, 12, 30, 18, DateTimeKind.Utc),
                String1 = "A string",
                Int1 = 1234,
                Long1 = 3216549874654984,
                EnumCamel1 = GetSurveyListSettings.SortByOption.NumResponses,
                EnumCaps1 = GetSurveyListSettings.SortOrderOption.DESC,
                ListTime1 = new List<DateTime> { new DateTime(2015, 4, 2, 10, 19, 12, DateTimeKind.Utc) },
                ListString1 = new List<string> { "asdf", "ghjk", "qwer" },
                ListInt1 = new List<int> { 1, 5, 0, 94 },
                ListLong1 = new List<long> { 42, 918274828787344, 9827, 5219258, 51928375123857 }

            };
            var result = RequestSettingsHelper.GetPopulatedProperties(input);
            Assert.AreEqual("2016-05-01T12:30:18", result["time_1"]);
            Assert.AreEqual("A string", result["string_1"]);
            Assert.AreEqual(1234, result["int_1"]);
            Assert.AreEqual("3216549874654984", result["long_1"]);
            Assert.AreEqual("num_responses", result["enum_camel_1"]);
            Assert.AreEqual("DESC", result["enum_caps_1"]);
            Assert.AreEqual("2015-04-02T10:19:12", ((List<string>)result["list_time_1"]).First());
            Assert.AreEqual("qwer", ((List<string>)result["list_string_1"]).Last());
            Assert.AreEqual(94, ((List<int>)result["list_int_1"]).Last());
            Assert.AreEqual("918274828787344", ((List<string>)result["list_long_1"]).Skip(1).First());
            Assert.AreEqual(10, result.Count);
        }
    }

    internal class LotsOfProperties
    {
        public DateTime? Time1 { get; set; }
        public DateTime? Time2 { get; set; }
        public string String1 { get; set; }
        public string String2 { get; set; }
        public int? Int1 { get; set; }
        public int? Int2 { get; set; }
        public long? Long1 { get; set; }
        public long? Long2 { get; set; }
        public GetSurveyListSettings.SortByOption? EnumCamel1 { get; set; }
        public GetSurveyListSettings.SortByOption? EnumCamel2 { get; set; }
        public GetSurveyListSettings.SortOrderOption? EnumCaps1 { get; set; }
        public GetSurveyListSettings.SortOrderOption? EnumCaps2 { get; set; }
        public List<DateTime> ListTime1 { get; set; }
        public List<DateTime> ListTime2 { get; set; }
        public List<string> ListString1 { get; set; }
        public List<string> ListString2 { get; set; }
        public List<int> ListInt1 { get; set; }
        public List<int> ListInt2 { get; set; }
        public List<long> ListLong1 { get; set; }
        public List<long> ListLong2 { get; set; }
    }
}