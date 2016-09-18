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
            Assert.AreEqual("single", PropertyCasingHelper.CamelToSnake("Single"));
            Assert.AreEqual("two_words", PropertyCasingHelper.CamelToSnake("TwoWords"));
            Assert.AreEqual("test_many_separate_words_next_to_each_other", PropertyCasingHelper.CamelToSnake("TestManySeparateWordsNextToEachOther"));
            Assert.AreEqual("test_1_number", PropertyCasingHelper.CamelToSnake("Test1Number"));
            Assert.AreEqual("test_451_numbers", PropertyCasingHelper.CamelToSnake("Test451Numbers"));
        }

        [Test]
        public void SnakeCaseIsConvertedToCamelCase()
        {
            Assert.AreEqual("Single", PropertyCasingHelper.SnakeToCamel("single"));
            Assert.AreEqual("TwoWords", PropertyCasingHelper.SnakeToCamel("two_words"));
            Assert.AreEqual("ManySeparateWords", PropertyCasingHelper.SnakeToCamel("many_separate_words"));
            Assert.AreEqual("ANumber3WithWords", PropertyCasingHelper.SnakeToCamel("a_number_3_with_words"));
            Assert.AreEqual("ManyNumeric345Digits", PropertyCasingHelper.SnakeToCamel("many_numeric_345_digits"));
            Assert.AreEqual("WordAndAnother", PropertyCasingHelper.SnakeToCamel("wOrd_and_ANOTHER"));
        }

        [Test]
        public void AllCapitalsAreNotConverted()
        {
            Assert.AreEqual("DESC", PropertyCasingHelper.CamelToSnake("DESC"));
            Assert.AreEqual("de_s_c", PropertyCasingHelper.CamelToSnake("DeSC"));
            Assert.AreEqual("DESC", PropertyCasingHelper.SnakeToCamel("DESC"));
        }
    }
}