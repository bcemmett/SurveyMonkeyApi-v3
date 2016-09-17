using System;
using System.Reflection;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey.Helpers
{
    internal class RequestSettingsHelper
    {
        internal static RequestData GetPopulatedProperties(object obj)
        {
            var output = new RequestData();
            foreach (PropertyInfo property in obj.GetType().GetProperties())
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