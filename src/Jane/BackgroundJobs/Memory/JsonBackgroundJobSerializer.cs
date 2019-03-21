using Jane.Json;
using System;

namespace Jane.BackgroundJobs
{
    public class JsonBackgroundJobSerializer : IBackgroundJobSerializer
    {
        private readonly IJsonSerializer _jsonSerializer;

        public JsonBackgroundJobSerializer(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public object Deserialize(string value, Type type)
        {
            return _jsonSerializer.Deserialize(type, value);
        }

        public T Deserialize<T>(string value)
        {
            return _jsonSerializer.Deserialize<T>(value);
        }

        public string Serialize(object obj)
        {
            return _jsonSerializer.Serialize(obj);
        }
    }
}