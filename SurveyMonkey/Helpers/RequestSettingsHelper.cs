using System;
using System.Collections.Generic;
using System.Reflection;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey.Helpers
{
    internal static class RequestSettingsHelper
    {
        internal static RequestData GetPopulatedProperties(object obj)
        {
            var output = new RequestData();
            foreach (PropertyInfo property in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (property.GetValue(obj) != null)
                {
                    Type underlyingType = property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                        ? Nullable.GetUnderlyingType(property.PropertyType)
                        : property.PropertyType;
                    if (underlyingType.IsEnum)
                    {
                        output.Add(PropertyCasingHelper.CamelToSnake(property.Name), PropertyCasingHelper.CamelToSnake(property.GetValue(obj).ToString()));
                    }
                    else if (underlyingType == typeof(DateTime))
                    {
                        output.Add(PropertyCasingHelper.CamelToSnake(property.Name), ((DateTime)property.GetValue(obj)).ToString("s"));
                    }
                    else if (underlyingType == typeof(List<DateTime>))
                    {
                        output.Add(PropertyCasingHelper.CamelToSnake(property.Name), ((List<DateTime>)property.GetValue(obj)).ConvertAll(x => x.ToString("s")));
                    }
                    //SurveyMonkey uses strings to represent longs (eg for any Ids)
                    else if (underlyingType == typeof(long))
                    {
                        output.Add(PropertyCasingHelper.CamelToSnake(property.Name), ((long)property.GetValue(obj)).ToString());
                    }
                    else if (underlyingType == typeof(List<long>))
                    {
                        output.Add(PropertyCasingHelper.CamelToSnake(property.Name), ((List<long>)property.GetValue(obj)).ConvertAll(x => x.ToString()));
                    }
                    else
                    {
                        output.Add(PropertyCasingHelper.CamelToSnake(property.Name), property.GetValue(obj));
                    }

                }
            }
            return output;
        }
    }
}