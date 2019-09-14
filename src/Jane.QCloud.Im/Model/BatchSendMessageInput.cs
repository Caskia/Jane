using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class BatchSendMessageInput
    {
        [JsonProperty("MsgBody")]
        public List<Message> Contents { get; set; }

        [JsonProperty("From_Account")]
        public string From { get; set; }

        public int MsgRandom { get; set; }

        public int SyncOtherMachine { get; set; } = 1;

        [JsonProperty("To_Account")]
        public List<string> To { get; set; }
    }
}