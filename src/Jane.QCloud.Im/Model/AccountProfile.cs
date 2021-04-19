using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class AccountProfile
    {
        [JsonPropertyName("To_Account")]
        public string Identifier { get; set; }

        public List<ProfileItem> ProfileItem { get; set; }
    }
}