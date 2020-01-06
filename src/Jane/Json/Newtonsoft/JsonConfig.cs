using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Jane.Json.Newtonsoft
{
    public static class JsonConfig
    {
        private static JsonSerializer _serializer;

        private static JsonSerializerSettings _serializerSettings;

        public static JsonSerializer DefaultJsonSerializer
        {
            get
            {
                if (_serializer == null)
                {
                    _serializer = new JsonSerializer();
                    var contractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                    _serializer.ContractResolver = contractResolver;
                    _serializer.Converters.Add(new JaneDateTimeConverter());
                    _serializer.Converters.Add(new StringLongConverter());
                    _serializer.Converters.Add(new StringEnumConverter());
                }

                return _serializer;
            }
        }

        public static JsonSerializerSettings DefaultJsonSerializerSettings
        {
            get
            {
                if (_serializerSettings == null)
                {
                    _serializerSettings = new JsonSerializerSettings();
                    var contractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                    _serializerSettings.ContractResolver = contractResolver;
                    _serializerSettings.Converters.Add(new JaneDateTimeConverter());
                    _serializerSettings.Converters.Add(new StringLongConverter());
                    _serializerSettings.Converters.Add(new StringEnumConverter());
                }

                return _serializerSettings;
            }
        }
    }
}