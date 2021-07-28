using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Jane.Json.Microsoft
{
    public static class JsonConfig
    {
        private static JsonSerializerOptions _serializerOptions;

        public static JsonSerializerOptions DefaultJsonSerializerOptions
        {
            get
            {
                if (_serializerOptions == null)
                {
                   _serializerOptions = CreateDefaultJsonSerializerOptions();
                }

                return _serializerOptions;
            }
        }

        public static JsonSerializerOptions CreateDefaultJsonSerializerOptions()
        {
            var encoderSettings = new TextEncoderSettings();
            encoderSettings.AllowRanges(UnicodeRanges.All);

            var serializerOptions = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(encoderSettings),
                PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy(),
            };

            serializerOptions.Converters.Add(new JaneDateTimeConverter());
            serializerOptions.Converters.Add(new StringLongConverter());
            serializerOptions.Converters.Add(new JsonStringEnumConverter());

            return serializerOptions;
        }
    }
}