﻿using Jane.Json.Microsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Jane.Runtime.Caching
{
    /// <summary>
    /// A class to hold the Type information and Serialized payload for data stored in the cache.
    /// </summary>
    public class JaneCacheData
    {
        private static readonly IReadOnlyList<string> SystemAssemblyNames = new List<string> { "mscorlib", "System.Private.CoreLib" };

        public JaneCacheData(
            string type, string payload)
        {
            Type = type;
            Payload = payload;
        }

        protected JaneCacheData()
        {
        }

        public static JsonSerializerOptions JsonSerializerOptions
        {
            get
            {
                var options = new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                options.Converters.Add(new JaneDateTimeConverter());

                return options;
            }
        }

        public string Payload { get; set; }

        public string Type { get; set; }

        public static JaneCacheData Deserialize(string serializedCacheData) => JsonSerializer.Deserialize<JaneCacheData>(serializedCacheData, JsonSerializerOptions);

        public static JaneCacheData Serialize(object obj)
        {
            return new JaneCacheData(
                SerializeType(obj.GetType()).ToString(),
                JsonSerializer.Serialize(obj, JsonSerializerOptions));
        }

        private static StringBuilder SerializeType(Type type, bool withAssemblyName = true, StringBuilder typeNameBuilder = null)
        {
            typeNameBuilder = typeNameBuilder ?? new StringBuilder();

            if (type.DeclaringType != null)
            {
                SerializeType(type.DeclaringType, false, typeNameBuilder).Append('+');
            }
            else if (type.Namespace != null)
            {
                typeNameBuilder.Append(type.Namespace).Append('.');
            }

            typeNameBuilder.Append(type.Name);

            if (type.GenericTypeArguments.Length > 0)
            {
                SerializeTypes(type.GenericTypeArguments, '[', ']', typeNameBuilder);
            }

            if (!withAssemblyName)
            {
                return typeNameBuilder;
            }

            var assemblyName = type.GetTypeInfo().Assembly.GetName().Name;

            if (!SystemAssemblyNames.Contains(assemblyName))
            {
                typeNameBuilder.Append(", ").Append(assemblyName);
            }

            return typeNameBuilder;
        }

        private static StringBuilder SerializeTypes(Type[] types, char beginTypeDelimiter = '"', char endTypeDelimiter = '"', StringBuilder typeNamesBuilder = null)
        {
            if (types == null)
            {
                return null;
            }

            if (typeNamesBuilder == null)
            {
                typeNamesBuilder = new StringBuilder();
            }

            typeNamesBuilder.Append('[');

            for (int i = 0; i < types.Length; i++)
            {
                typeNamesBuilder.Append(beginTypeDelimiter);
                SerializeType(types[i], true, typeNamesBuilder);
                typeNamesBuilder.Append(endTypeDelimiter);

                if (i != types.Length - 1)
                {
                    typeNamesBuilder.Append(',');
                }
            }

            return typeNamesBuilder.Append(']');
        }
    }
}