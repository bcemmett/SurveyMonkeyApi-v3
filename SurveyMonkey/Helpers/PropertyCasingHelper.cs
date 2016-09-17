using System;
using System.Collections.Generic;

namespace SurveyMonkey.Helpers
{
    internal class PropertyCasingHelper
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
    }
}