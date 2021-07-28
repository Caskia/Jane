using System;
using System.Text.Json;

namespace Jane.Json.Microsoft
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Returns deserialized string using default options<see cref="JsonConfig.DefaultJsonSerializerOptions"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string value)
        {
            return value.FromJsonString<T>(JsonConfig.DefaultJsonSerializerOptions);
        }

        /// <summary>
        /// Returns deserialized string using custom <see cref="JsonSerializerOptions"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string value, JsonSerializerOptions options)
        {
            return value != null
                ? JsonSerializer.Deserialize<T>(value, options)
                : default;
        }

        /// <summary>
        /// Returns deserialized string using explicit <see cref="Type"/> and default options <see cref="JsonConfig.DefaultJsonSerializerOptions"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object FromJsonString(this string value, Type type)
        {
            return value.FromJsonString(type, JsonConfig.DefaultJsonSerializerOptions);
        }

        /// <summary>
        /// Returns deserialized string using explicit <see cref="Type"/> and custom <see cref="JsonSerializerOptions"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static object FromJsonString(this string value, Type type, JsonSerializerOptions options)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return value != null
                ? JsonSerializer.Deserialize(value, type, options)
                : null;
        }

        /// <summary>
        /// Converts given object to JSON string using default options <see cref="JsonConfig.DefaultJsonSerializerOptions"/>.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            return obj != null
                ? JsonSerializer.Serialize(obj, JsonConfig.DefaultJsonSerializerOptions)
                : default;
        }

        /// <summary>
        /// Converts given object to JSON string using custom <see cref="JsonSerializerOptions"/>.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj, JsonSerializerOptions options)
        {
            return obj != null
                ? JsonSerializer.Serialize(obj, options)
                : default;
        }
    }
}