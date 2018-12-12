using Jane.Json.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Jane.Json
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Returns deserialized string using default <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string value)
        {
            return value.FromJsonString<T>(new JsonSerializerSettings());
        }

        /// <summary>
        /// Returns deserialized string using custom <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string value, JsonSerializerSettings settings)
        {
            return value != null
                ? JsonConvert.DeserializeObject<T>(value, settings)
                : default(T);
        }

        /// <summary>
        /// Returns deserialized string using explicit <see cref="Type"/> and custom <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static object FromJsonString(this string value, Type type, JsonSerializerSettings settings)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return value != null
                ? JsonConvert.DeserializeObject(value, type, settings)
                : null;
        }

        /// <summary>
        /// Converts given object to JSON string.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false, bool referenceLoopIgnore = false, bool retainProperties = false, bool ignoreProperties = false, string[] properties = null)
        {
            var settings = new JsonSerializerSettings();

            if (camelCase)
            {
                settings.ContractResolver = new JaneCamelCasePropertyNamesContractResolver();
            }
            else
            {
                settings.ContractResolver = new JaneContractResolver();
            }

            if (indented)
            {
                settings.Formatting = Formatting.Indented;
            }

            if (referenceLoopIgnore)
            {
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }

            if (ignoreProperties)
            {
                settings.ContractResolver = new LimitPropertyContractResolver(properties);
            }

            if (retainProperties)
            {
                settings.ContractResolver = new LimitPropertyContractResolver(properties, true);
            }

            return ToJsonString(obj, settings);
        }

        /// <summary>
        /// Converts given object to JSON string using custom <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj, JsonSerializerSettings settings)
        {
            return obj != null
                ? JsonConvert.SerializeObject(obj, settings)
                : default(string);
        }
    }
}