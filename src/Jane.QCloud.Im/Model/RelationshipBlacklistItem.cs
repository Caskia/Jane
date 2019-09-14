using Newtonsoft.Json;

namespace Jane.QCloud.Im
{
    public class RelationshipBlacklistItem
    {
        public int AddBlackTimeStamp { get; set; }

        [JsonProperty("To_Account")]
        public string To { get; set; }
    }
}