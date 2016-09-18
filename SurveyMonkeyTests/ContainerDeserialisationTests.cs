using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using NUnit.Framework;
using SurveyMonkey;

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
    }
}