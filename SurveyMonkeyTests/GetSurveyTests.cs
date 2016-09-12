using NUnit.Framework;
using SurveyMonkey;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class GetSurveyTests
    {
        [Test]
        public void GetSurveyListReturnsSomething()
        {
            var api = new SurveyMonkeyApi("asdf", "qwer", new MockWebClient());
        }
    }
}