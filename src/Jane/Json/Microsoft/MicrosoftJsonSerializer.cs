using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

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

        protected virtual JsonSerializerOptions CreateSerializerOptions(NamingStrategyType namingStrategyType = NamingStrategyType.SnakeCase, bool indented = false)
        {
            var encoderSettings = new TextEncoderSettings();
            encoderSettings.AllowRanges(UnicodeRanges.All);

            var options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(encoderSettings)
            };

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

            options.Converters.Add(new JaneDateTimeConverter());
            options.Converters.Add(new StringLongConverter());
            options.Converters.Add(new JsonStringEnumConverter());

            if (indented)
            {
                options.WriteIndented = true;
            }

            return options;
        }
    }
}