using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class CallbackConversationMessage : QCloudCallbackData
    {
        [JsonProperty("MsgBody")]
        public List<Message> Contents { get; set; }

        [JsonProperty("From_Account")]
        public string From { get; set; }

        [JsonProperty("MsgSeq")]
        public long? Sequence { get; set; }

        [JsonProperty("MsgTime")]
        public double? Timestamp { get; set; }

        [JsonProperty("To_Account")]
        public string To { get; set; }
    }
}