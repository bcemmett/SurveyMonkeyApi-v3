using NUnit.Framework;
using SurveyMonkey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SurveyMonkeyTests
{
    internal class AsyncEquivalenceTests
    {
        [Test]
        public void PublicSynchronousAndAsyncMethodsHaveMatchingPairs()
        {
            var allowedSynchronousOnly = new List<string>
            {
                typeof(SurveyMonkeyApi).GetMethod(nameof(SurveyMonkeyApi.Dispose)).Name,
                typeof(SurveyMonkeyApi).GetMethod(nameof(SurveyMonkeyApi.MatchResponseToSurveyStructure)).Name,
            };

            var allowedAsyncOnly = new List<string>
            {
            };

            BindingFlags flags = BindingFlags.Public |
                                    BindingFlags.Static |
                                    BindingFlags.Instance |
                                    BindingFlags.DeclaredOnly;

            var publicMethods = typeof(SurveyMonkeyApi).GetMethods(flags).Where(m => 
                !m.IsSpecialName
                && !m.GetCustomAttributes<ObsoleteAttribute>().Any()
                );
            var asyncMethodNames = publicMethods
                .Where(m => m.GetCustomAttributes<AsyncStateMachineAttribute>().Any())
                .Select(m => m.Name);
            var synchronousMethodNames = publicMethods
                .Where(m => !m.GetCustomAttributes<AsyncStateMachineAttribute>().Any())
                .Select(m => m.Name);

            var asyncMethodsWithNoMatchingSynchronousEquivalent = asyncMethodNames
                .Where(m =>
                    !allowedAsyncOnly.Contains(m)
                    && !synchronousMethodNames
                        .Select(s => s + "Async")
                        .Contains(m));

            var synchronousMethodsWithNoMatchingAsyncEquivalent = synchronousMethodNames
                .Where(m =>
                    !allowedSynchronousOnly.Contains(m)
                    && !asyncMethodNames
                        .Select(a => a.Substring(0, a.Length - 5))
                        .Contains(m));

            Assert.IsEmpty(asyncMethodsWithNoMatchingSynchronousEquivalent);
            Assert.IsEmpty(synchronousMethodsWithNoMatchingAsyncEquivalent);
        }

        [Test]
        public void AsyncMethodsHaveAsyncInTheName()
        {
            BindingFlags flags = BindingFlags.Public |
                                    BindingFlags.NonPublic |
                                    BindingFlags.Static |
                                    BindingFlags.Instance |
                                    BindingFlags.DeclaredOnly;

            var methods = typeof(SurveyMonkeyApi)
                .GetMethods(flags)
                .Where(m => m.GetCustomAttributes<AsyncStateMachineAttribute>().Any())
                .Select(m => m.Name);

            Assert.IsNotEmpty(methods);
            
            foreach(string method in methods)
            {
                Assert.True(method.EndsWith("Async", StringComparison.Ordinal));
            }
        }
    }
}