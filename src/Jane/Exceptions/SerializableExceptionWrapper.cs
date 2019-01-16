using Newtonsoft.Json;
using System;

namespace Jane
{
    [Serializable]
    public class SerializableExceptionWrapper : IHasErrorCode
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public static SerializableExceptionWrapper Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<SerializableExceptionWrapper>(json);
        }

        public static string Serialize(SerializableExceptionWrapper wrapper)
        {
            return JsonConvert.SerializeObject(wrapper);
        }
    }
}