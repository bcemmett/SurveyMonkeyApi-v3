using NUnit.Framework;
using SurveyMonkey.Helpers;

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
    }
}