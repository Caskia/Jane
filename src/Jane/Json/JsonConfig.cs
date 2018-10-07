using Jane.Json.Converters;
using Jane.Json.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Jane.Json
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
                    _serializer.ContractResolver = new CustomPropertyNamesContractResolver();
                    _serializer.Converters.Add(new JaneDateTimeConverter());
                    _serializer.Converters.Add(new LongConverter());
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
                    _serializerSettings.ContractResolver = new CustomPropertyNamesContractResolver();
                    _serializerSettings.Converters.Add(new JaneDateTimeConverter());
                    _serializerSettings.Converters.Add(new LongConverter());
                    _serializerSettings.Converters.Add(new StringEnumConverter());
                }

                return _serializerSettings;
            }
        }
    }
}