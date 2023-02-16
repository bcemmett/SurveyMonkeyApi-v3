using NUnit.Framework;
using SurveyMonkey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class InterfaceCompletenessTests
    {
        [Test]
        public void AllPublicMethodsOnSurveyMonkeyApiAreOnInterface()
        {
            var allowedExceptions = new List<string>
            {
            };

            var concreteMethods = GetMethods(typeof(SurveyMonkeyApi));
            var interfaceMethods = GetMethods(typeof(ISurveyMonkeyApi));
            var missing = concreteMethods.Where(c =>
                !c.IsSpecialName
                && !interfaceMethods.Any(i => i.Name == c.Name)
                && !allowedExceptions.Contains(c.Name)
                && !c.GetCustomAttributes<ObsoleteAttribute>().Any()
                );

            Assert.IsNotEmpty(concreteMethods);
            Assert.IsNotEmpty(interfaceMethods);
            Assert.IsEmpty(missing, "Missing:" + Environment.NewLine + String.Join(Environment.NewLine, missing.Select(m => m.Name)));
        }

        [Test]
        public void AllPublicPropertiesOnSurveyMonkeyApiAreOnInterface()
        {
            var allowedExceptions = new List<string>
            {
                typeof(SurveyMonkeyApi).GetProperty(nameof(SurveyMonkeyApi.Proxy)).Name
            };

            var concreteProperties = GetProperties(typeof(SurveyMonkeyApi));
            var interfaceProperties = GetProperties(typeof(ISurveyMonkeyApi));
            var missing = concreteProperties.Where(c =>
                !interfaceProperties.Any(i => i.Name == c.Name)
                && !allowedExceptions.Contains(c.Name)
            );

            Assert.IsNotEmpty(concreteProperties);
            Assert.IsNotEmpty(interfaceProperties);
            Assert.IsEmpty(missing, "Missing:" + Environment.NewLine + String.Join(Environment.NewLine, missing.Select(m => m.Name)));
        }

        private IEnumerable<MethodInfo> GetMethods(Type type)
        {
            BindingFlags flags =    BindingFlags.Public |
                                    BindingFlags.Static |
                                    BindingFlags.Instance |
                                    BindingFlags.DeclaredOnly;

            return type.GetMethods(flags);
        }

        private IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            BindingFlags flags =    BindingFlags.Public |
                                    BindingFlags.Static |
                                    BindingFlags.Instance |
                                    BindingFlags.DeclaredOnly;

            return type.GetProperties(flags);
        }
    }
}