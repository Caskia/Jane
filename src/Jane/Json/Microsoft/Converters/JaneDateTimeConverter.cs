using Jane.Timing;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jane.Json.Microsoft
{
    public class JaneDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateTime = reader.GetDateTime();

            return Clock.Normalize(dateTime);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(Clock.Normalize(value));
        }
    }
}