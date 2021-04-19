using System.Text.Json.Serialization;

namespace Jane.QCloud.Im
{
    public class Message
    {
        [JsonPropertyName("MsgContent")]
        public MessageContent Content { get; set; }

        [JsonPropertyName("MsgType")]
        public string Type { get; set; }
    }
}