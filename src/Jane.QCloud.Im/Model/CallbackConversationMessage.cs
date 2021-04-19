using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class CallbackConversationMessage : QCloudCallbackData
    {
        [JsonPropertyName("MsgBody")]
        public List<Message> Contents { get; set; }

        [JsonPropertyName("From_Account")]
        public string From { get; set; }

        [JsonPropertyName("MsgSeq")]
        public long? Sequence { get; set; }

        [JsonPropertyName("MsgTime")]
        public double? Timestamp { get; set; }

        [JsonPropertyName("To_Account")]
        public string To { get; set; }
    }
}