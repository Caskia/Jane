using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class CallbackGroupMessage : QCloudCallbackData
    {
        [JsonPropertyName("MsgBody")]
        public List<Message> Contents { get; set; }

        [JsonPropertyName("From_Account")]
        public string From { get; set; }

        [JsonPropertyName("GroupId")]
        public string GroupId { get; set; }

        [JsonPropertyName("MsgSeq")]
        public long? Sequence { get; set; }

        [JsonPropertyName("MsgTime")]
        public double? Timestamp { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }
    }
}