using Newtonsoft.Json;

namespace Jane.QCloud.Im
{
    public class RelationshipBlacklistResponse
    {
        public int ResultCode { get; set; }

        public string ResultInfo { get; set; }

        [JsonProperty("To_Account")]
        public string To { get; set; }
    }
}