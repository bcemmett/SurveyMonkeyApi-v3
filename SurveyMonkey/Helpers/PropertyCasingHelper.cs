using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey.Helpers
{
    internal class PropertyCasingHelper
    {
        internal static string CamelCaseToUnderscore(string input)
        {
            if (input == input.ToUpper())
            {
                return input;
            }

            var chars = input.ToCharArray();
            var output = new List<char>();

            bool previousWasNumeric = false;
            for(int i = 0; i < chars.Length; i++)
            {
                if (i > 0 && (Char.IsUpper(chars[i]) || Char.IsNumber(chars[i]) && !previousWasNumeric))
                {
                    output.Add('_');
                }
                if (Char.IsNumber(chars[i]))
                {
                    output.Add(chars[i]);
                    previousWasNumeric = true;
                }
                else
                {
                    output.Add(Char.ToLower(chars[i]));
                    previousWasNumeric = false;
                }
            }
            return new string(output.ToArray());
        }

        internal static RequestData GetPopulatedProperties(object obj)
        {
            var output = new RequestData();
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                if (property.GetValue(obj) != null)
                {
                    Type underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
                    if (underlyingType.IsEnum)
                    {
                        output.Add(CamelCaseToUnderscore(property.Name), CamelCaseToUnderscore(property.GetValue(obj).ToString()));
                    }
                    else
                    {
                        output.Add(CamelCaseToUnderscore(property.Name), property.GetValue(obj));
                    }
                    
                }
            }
            return output;
        }
    }
}