using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class RelationshipBlacklistAddResponse : QCloudResponse
    {
        [JsonPropertyName("Fail_Account")]
        public List<string> FailAccount { get; set; } = new List<string>();

        [JsonPropertyName("Invalid_Account")]
        public List<string> InvalidAccount { get; set; } = new List<string>();

        public List<RelationshipBlacklistResponse> ResultItem { get; set; } = new List<RelationshipBlacklistResponse>();
    }
}