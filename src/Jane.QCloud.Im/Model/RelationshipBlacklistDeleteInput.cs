using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class RelationshipBlacklistDeleteInput
    {
        [JsonProperty("From_Account")]
        public string From { get; set; }

        [JsonProperty("To_Account")]
        public List<string> To { get; set; } = new List<string>();
    }
}