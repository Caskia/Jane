using System;
using System.Text.Json;

namespace Jane.Json.Microsoft
{
    public class MicrosoftJsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string jsonString, NamingStrategyType namingStrategyType = NamingStrategyType.SnakeCase)
        {
            return JsonSerializer.Deserialize<T>(jsonString, CreateSerializerOptions(namingStrategyType));
        }

        public object Deserialize(Type type, string jsonString, NamingStrategyType namingStrategyType = NamingStrategyType.SnakeCase)
        {
            return JsonSerializer.Deserialize(jsonString, type, CreateSerializerOptions(namingStrategyType));
        }

        public string Serialize(object obj, NamingStrategyType namingStrategyType = NamingStrategyType.SnakeCase, bool indented = false)
        {
            return JsonSerializer.Serialize(obj, CreateSerializerOptions(namingStrategyType, indented));
        }

        /// <summary>
        /// default options see <see cref="JsonConfig.CreateDefaultJsonSerializerOptions"/>
        /// </summary>
        protected virtual JsonSerializerOptions CreateSerializerOptions(NamingStrategyType namingStrategyType = NamingStrategyType.SnakeCase, bool indented = false)
        {
            var options = JsonConfig.CreateDefaultJsonSerializerOptions();

            switch (namingStrategyType)
            {
                case NamingStrategyType.SnakeCase:
                    options.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy();
                    break;

                case NamingStrategyType.CamelCase:
                    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    break;

                default:
                    break;
            }

            if (indented)
            {
                options.WriteIndented = true;
            }

            return options;
        }
    }
}