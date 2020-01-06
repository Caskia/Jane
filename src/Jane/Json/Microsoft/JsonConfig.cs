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
                    var encoderSettings = new TextEncoderSettings();
                    encoderSettings.AllowRanges(UnicodeRanges.All);

                    _serializerOptions = new JsonSerializerOptions()
                    {
                        Encoder = JavaScriptEncoder.Create(encoderSettings),
                        PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy(),
                    };

                    _serializerOptions.Converters.Add(new JaneDateTimeConverter());
                    _serializerOptions.Converters.Add(new StringLongConverter());
                    _serializerOptions.Converters.Add(new JsonStringEnumConverter());
                }

                return _serializerOptions;
            }
        }
    }
}