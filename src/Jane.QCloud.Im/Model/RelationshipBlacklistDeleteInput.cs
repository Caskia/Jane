using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class RelationshipBlacklistDeleteInput
    {
        [JsonPropertyName("From_Account")]
        public string From { get; set; }

        [JsonPropertyName("To_Account")]
        public List<string> To { get; set; } = new List<string>();
    }
}