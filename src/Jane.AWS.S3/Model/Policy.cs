using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Jane.AWS.S3
{
    public class Policy
    {
        //[JsonPropertyName("conditions")]
        //public Dictionary<string, string> Conditions { get; set; } = new Dictionary<string, string>();

        [JsonPropertyName("conditions")]
        public List<object> Conditions { get; set; } = new List<object>();

        [JsonPropertyName("expiration")]
        public string Expiration { get; set; }
    }
}