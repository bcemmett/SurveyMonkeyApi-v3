using System;
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
                Time1 = new DateTime(2016, 5, 1),
                String1 = "A string",
                Int1 = 1234,
                Long1 = 3216549874654984,
                EnumCamel1 = GetSurveyListSettings.SortByOption.NumResponses,
                EnumCaps1 = GetSurveyListSettings.SortOrderOption.DESC
            };
            var result = RequestSettingsHelper.GetPopulatedProperties(input);
            Assert.AreEqual(new DateTime(2016, 5, 1), result["time_1"]);
            Assert.AreEqual("A string", result["string_1"]);
            Assert.AreEqual(1234, result["int_1"]);
            Assert.AreEqual("num_responses", result["enum_camel_1"]);
            Assert.AreEqual("DESC", result["enum_caps_1"]);
            Assert.AreEqual(6, result.Count);
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
    }
}