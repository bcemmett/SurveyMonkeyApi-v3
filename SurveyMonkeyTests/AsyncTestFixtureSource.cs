using System.Collections;

namespace SurveyMonkeyTests
{
    internal class AsyncTestFixtureSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return false;
            yield return true;
        }
    }
}