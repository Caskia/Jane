using Newtonsoft.Json;

namespace Jane.QCloud.Im
{
    public class Message
    {
        [JsonProperty("MsgContent")]
        public MessageContent Content { get; set; }

        [JsonProperty("MsgType")]
        public string Type { get; set; }
    }
}