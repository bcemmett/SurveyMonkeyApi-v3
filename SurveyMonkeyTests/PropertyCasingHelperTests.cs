using System;
using NUnit.Framework;
using SurveyMonkey.Enums;
using SurveyMonkey.Helpers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class PropertyCasingHelperTests
    {
        [Test]
        public void CamelCaseIsConvertedToSnakeCase()
        {
            Assert.AreEqual("single", PropertyCasingHelper.CamelCaseToUnderscore("Single"));
            Assert.AreEqual("two_words", PropertyCasingHelper.CamelCaseToUnderscore("TwoWords"));
            Assert.AreEqual("test_many_separate_words_next_to_each_other", PropertyCasingHelper.CamelCaseToUnderscore("TestManySeparateWordsNextToEachOther"));
            Assert.AreEqual("test_1_number", PropertyCasingHelper.CamelCaseToUnderscore("Test1Number"));
            Assert.AreEqual("test_451_numbers", PropertyCasingHelper.CamelCaseToUnderscore("Test451Numbers"));
        }

        [Test]
        public void AllCapitalsAreNotConverted()
        {
            Assert.AreEqual("DESC", PropertyCasingHelper.CamelCaseToUnderscore("DESC"));
            Assert.AreEqual("de_s_c", PropertyCasingHelper.CamelCaseToUnderscore("DeSC"));
        }

        [Test]
        public void ObjectPropertiesAreProcessed()
        {
            var input = new GetSurveyListSettings
            {
                EndModifiedAt = new DateTime(2016, 5, 6),
                Page = 5,
                SortBy = GetSurveyListSortBy.DateModified,
                SortOrder = SortOrder.DESC
            };
            var result = PropertyCasingHelper.GetPopulatedProperties(input);
            Assert.AreEqual(new DateTime(2016, 5, 6), result["end_modified_at"]);
            Assert.AreEqual(5, result["page"]);
            Assert.AreEqual("date_modified", result["sort_by"]);
            Assert.AreEqual("DESC", result["sort_order"]);
            Assert.AreEqual(4, result.Count);
        }
    }
}