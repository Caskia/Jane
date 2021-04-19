using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class BatchSendMessageInput
    {
        [JsonPropertyName("MsgBody")]
        public List<Message> Contents { get; set; }

        [JsonPropertyName("From_Account")]
        public string From { get; set; }

        public int MsgRandom { get; set; }

        public int SyncOtherMachine { get; set; } = 1;

        [JsonPropertyName("To_Account")]
        public List<string> To { get; set; }
    }
}