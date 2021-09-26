using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Helpers;

namespace SurveyMonkey
{
    internal class TolerantJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            Type type = GetUnderlyingType(objectType);
            return type.IsEnum || type.IsClass;
        }

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            Type type = GetUnderlyingType(objectType);
            if (type.IsEnum)
            {
                if (reader.TokenType == JsonToken.String)
                {
                    string enumText = PropertyCasingHelper.SnakeToCamel(reader.Value.ToString());
                    string[] names = Enum.GetNames(type);
                    string match = names.FirstOrDefault(n => String.Equals(n, enumText, StringComparison.InvariantCultureIgnoreCase));
                    if (match != null)
                    {
                        return Enum.Parse(type, match);
                    }
                }
                else if (reader.TokenType == JsonToken.Integer)
                {
                    int enumVal = Convert.ToInt32(reader.Value);
                    int[] values = (int[])Enum.GetValues(type);
                    if (values.Contains(enumVal))
                    {
                        return Enum.ToObject(type, enumVal);
                    }
                }
                WarnOfMissingDeserializationOpportunity(reader.Value.ToString(), type.FullName);
                return null;
            }

            object instance = objectType.GetConstructor(Type.EmptyTypes).Invoke(null);
            PropertyInfo[] properties = objectType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            IEnumerable<JProperty> jsonProperties = JObject.Load(reader).Properties();
            foreach (JProperty jsonProperty in jsonProperties)
            {
                string name = PropertyCasingHelper.SnakeToCamel(jsonProperty.Name);
                PropertyInfo property = properties.FirstOrDefault(pi => String.Equals(pi.Name, name, StringComparison.OrdinalIgnoreCase));

                if (property != null)
                {
                    if (
                        !Attribute.IsDefined(property, typeof(JsonIgnoreAttribute))
                        && jsonProperty.Value.Type != JTokenType.Null
                        && !IsUnparseableNumeric(property, jsonProperty))
                    {
                        if (property.PropertyType == typeof(DateTime?))
                        {
                            //Want DateTimes to always be treated as UTC
                            var rawDate = (DateTime)jsonProperty.Value.ToObject(typeof(DateTime), serializer);
                            var convertedDate = new DateTime();
                            switch (rawDate.Kind)
                            {
                                case DateTimeKind.Local:
                                    convertedDate = rawDate.ToUniversalTime();
                                    break;
                                case DateTimeKind.Unspecified:
                                    convertedDate = DateTime.SpecifyKind(rawDate, DateTimeKind.Utc);
                                    break;
                                case DateTimeKind.Utc:
                                    convertedDate = rawDate;
                                    break;
                            }
                            property.SetValue(instance, convertedDate);
                        }
                        else
                        {
                            if (property.Name == "Choices" && jsonProperty.Value.Type != JTokenType.Array)
                            {
                                var legacyProperty = properties.FirstOrDefault(pi => String.Equals("LegacyChoices", pi.Name, StringComparison.OrdinalIgnoreCase));
                                legacyProperty.SetValue(instance, jsonProperty.Value.ToObject(legacyProperty.PropertyType, serializer));
                            }
                            else
                            {
                                property.SetValue(instance, jsonProperty.Value.ToObject(property.PropertyType, serializer));
                            }
                        }
                    }
                }
                else
                {
                    WarnOfMissingDeserializationOpportunity(jsonProperty.Name, type.Name);
                }
            }
            return instance;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private bool IsNullableType(Type t)
        {
            return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        private Type GetUnderlyingType(Type type)
        {
            return IsNullableType(type) ? Nullable.GetUnderlyingType(type) : type;
        }

        private bool IsUnparseableNumeric(PropertyInfo classProperty, JProperty jsonProperty)
        {
            //Api very occasionaly supplies strings for numeric values
            long n;
            return (classProperty.PropertyType == typeof(int?) || classProperty.PropertyType == typeof(long?)) && !Int64.TryParse(jsonProperty.Value.ToString(), out n);
        }

        [Conditional("DEBUG")]
        void WarnOfMissingDeserializationOpportunity(string propertyName, string type)
        {
            if(this.GetType() == typeof(TolerantJsonConverter))
            {
                throw new ArgumentException(String.Format("Json property {0} doesn't exist on object {1}", propertyName, type));
            }
        }
    }
}