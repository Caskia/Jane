using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;

namespace Jane.Json.Newtonsoft
{
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string jsonString, NamingStrategyType namingStrategyType = NamingStrategyType.SnakeCase)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, CreateSerializerSettings(namingStrategyType));
        }

        public object Deserialize(Type type, string jsonString, NamingStrategyType namingStrategyType = NamingStrategyType.SnakeCase)
        {
            return JsonConvert.DeserializeObject(jsonString, type, CreateSerializerSettings(namingStrategyType));
        }

        public string Serialize(object obj, NamingStrategyType namingStrategyType = NamingStrategyType.SnakeCase, bool indented = false)
        {
            return JsonConvert.SerializeObject(obj, CreateSerializerSettings(namingStrategyType, indented));
        }

        protected virtual JsonSerializerSettings CreateSerializerSettings(NamingStrategyType namingStrategyType = NamingStrategyType.SnakeCase, bool indented = false)
        {
            var settings = new JsonSerializerSettings();

            var contractResolver = new DefaultContractResolver();
            switch (namingStrategyType)
            {
                case NamingStrategyType.SnakeCase:
                    contractResolver.NamingStrategy = new SnakeCaseNamingStrategy();
                    break;

                case NamingStrategyType.CamelCase:
                    contractResolver.NamingStrategy = new CamelCaseNamingStrategy();
                    break;

                default:
                    contractResolver.NamingStrategy = new DefaultNamingStrategy();
                    break;
            }

            settings.ContractResolver = contractResolver;
            settings.Converters.Add(new JaneDateTimeConverter());
            settings.Converters.Add(new StringLongConverter());
            settings.Converters.Add(new StringEnumConverter());

            if (indented)
            {
                settings.Formatting = Formatting.Indented;
            }

            return settings;
        }
    }
}