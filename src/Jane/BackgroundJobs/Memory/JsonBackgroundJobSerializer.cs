using Jane.Json;
using Newtonsoft.Json;
using System;

namespace Jane.BackgroundJobs
{
    public class JsonBackgroundJobSerializer : IBackgroundJobSerializer
    {
        public JsonBackgroundJobSerializer()
        {
        }

        public object Deserialize(string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type, JsonConfig.DefaultJsonSerializerSettings);
        }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, JsonConfig.DefaultJsonSerializerSettings);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonConfig.DefaultJsonSerializerSettings);
        }
    }
}