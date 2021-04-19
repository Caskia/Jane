using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class AccountGetProfileInput
    {
        [JsonPropertyName("To_Account")]
        public List<string> Identifiers { get; set; }

        public List<string> TagList { get; set; }
    }
}