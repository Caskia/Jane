using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class RelationshipBlacklistDeleteResponse : QCloudResponse
    {
        [JsonProperty("Fail_Account")]
        public List<string> FailAccount { get; set; } = new List<string>();

        [JsonProperty("Invalid_Account")]
        public List<string> InvalidAccount { get; set; } = new List<string>();

        public List<RelationshipBlacklistResponse> ResultItem { get; set; } = new List<RelationshipBlacklistResponse>();
    }
}