using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class ContainerDeserialisationTests
    {
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
    }
}