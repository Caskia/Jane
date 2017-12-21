using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Jane.Json.Converters
{
    /// <summary>
    /// Converts string to and from long value
    /// </summary>
    public class LongConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(long) || objectType == typeof(long?))
            {
                return true;
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jt = JValue.ReadFrom(reader);
            var str = jt.Value<string>();
            long result;
            if (long.TryParse(str, out result))
            {
                return result;
            }
            else
            {
                return null;
            }

            //return jt.Value<long>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                serializer.Serialize(writer, value.ToString());
            }
        }
    }
}