using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class CallbackGroupMessage : QCloudCallbackData
    {
        [JsonProperty("MsgBody")]
        public List<Message> Contents { get; set; }

        [JsonProperty("From_Account")]
        public string From { get; set; }

        [JsonProperty("GroupId")]
        public string GroupId { get; set; }

        [JsonProperty("MsgSeq")]
        public long? Sequence { get; set; }

        [JsonProperty("MsgTime")]
        public double? Timestamp { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}