using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SurveyMonkey
{
    //Using a custom converter to ignore underscores in the json returned by SM
    //http://stackoverflow.com/questions/19792274/alternate-property-name-while-deserializing/19885911#19885911
    internal class LaxPropertyNameJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsClass;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            object instance = objectType.GetConstructor(Type.EmptyTypes).Invoke(null);
            PropertyInfo[] props = objectType.GetProperties();

            JObject jo = JObject.Load(reader);
            foreach (JProperty jp in jo.Properties())
            {
                string name = Regex.Replace(jp.Name, "[^A-Za-z0-9]+", "");

                PropertyInfo prop = props.FirstOrDefault(pi =>
                    pi.CanWrite && string.Equals(pi.Name, name, StringComparison.OrdinalIgnoreCase));

                if (prop != null)
                {
                    /*The v2 api sometimes misbehaves and returns stringy values like "None"
                    when it should be returning numeric values, leading to a FormatException
                    doing the conversion. Detect and an use 0 in these situations*/
                    long n;
                    if ((prop.PropertyType == typeof(int) || prop.PropertyType == typeof(long))
                        && !Int64.TryParse(jp.Value.ToString(), out n))
                    {
                        prop.SetValue(instance, null);
                    }
                    else
                    {
                        prop.SetValue(instance, jp.Value.ToObject(prop.PropertyType, serializer));
                    }
                }
#if DEBUG
                else
                {
                    //During debugging, we want to know if we've omitted any properties that can be deserialised
                    throw new Exception(String.Format(@"Discarded property '{0}'", jp.Name));
                }
#endif
            }

            return instance;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    //http://stackoverflow.com/questions/22752075/how-can-i-ignore-unknown-enum-values-during-json-deserialization/22755077#22755077
    internal class LaxEnumJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            Type type = IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
            return type.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool isNullable = IsNullableType(objectType);
            Type enumType = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            string[] names = Enum.GetNames(enumType);

            if (reader.TokenType == JsonToken.String)
            {
                string enumText = Regex.Replace(reader.Value.ToString(), "[^A-Za-z0-9]+", "");

                if (!string.IsNullOrEmpty(enumText))
                {
                    string match = names
                        .Where(n => string.Equals(n, enumText, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();

                    if (match != null)
                    {
                        return Enum.Parse(enumType, match);
                    }
                }
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                int enumVal = Convert.ToInt32(reader.Value);
                int[] values = (int[])Enum.GetValues(enumType);
                if (values.Contains(enumVal))
                {
                    return Enum.Parse(enumType, enumVal.ToString());
                }
            }

            if (!isNullable)
            {
                string defaultName = names
                    .Where(n => string.Equals(n, "Unknown", StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (defaultName == null)
                {
                    defaultName = names.First();
                }

                return Enum.Parse(enumType, defaultName);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        private bool IsNullableType(Type t)
        {
            return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
