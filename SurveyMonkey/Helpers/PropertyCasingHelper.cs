using System;
using System.Collections.Generic;

namespace SurveyMonkey.Helpers
{
    internal static class PropertyCasingHelper
    {
        internal static string CamelToSnake(string input)
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

        internal static string SnakeToCamel(string input)
        {
            if (input.StartsWith("$"))
            {
                input = input.Substring(1);
            }

            if (input == input.ToUpper())
            {
                return input;
            }

            var chars = input.ToCharArray();
            var output = new List<char>();
            bool previousWasUnderscore = false;
            for(int i=0; i < chars.Length; i++)
            {
                if (chars[i] == '_')
                {
                    previousWasUnderscore = true;
                }
                else
                {
                    if (i == 0 || previousWasUnderscore)
                    {
                        output.Add(Char.ToUpper(chars[i]));
                    }
                    else
                    {
                        output.Add(Char.ToLower(chars[i]));
                    }
                    previousWasUnderscore = false;
                }
            }
            return new string(output.ToArray());
        }
    }
}