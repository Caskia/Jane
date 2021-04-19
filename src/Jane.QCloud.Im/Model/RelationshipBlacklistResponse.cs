using System.Text.Json.Serialization;

namespace Jane.QCloud.Im
{
    public class RelationshipBlacklistResponse
    {
        public int ResultCode { get; set; }

        public string ResultInfo { get; set; }

        [JsonPropertyName("To_Account")]
        public string To { get; set; }
    }
}