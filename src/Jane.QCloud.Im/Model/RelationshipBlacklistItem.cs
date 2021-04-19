using System.Text.Json.Serialization;

namespace Jane.QCloud.Im
{
    public class RelationshipBlacklistItem
    {
        public int AddBlackTimeStamp { get; set; }

        [JsonPropertyName("To_Account")]
        public string To { get; set; }
    }
}