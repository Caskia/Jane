using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class AccountSetProfileInput
    {
        [JsonProperty("From_Account")]
        public string Identifier { get; set; }

        public List<ProfileItem> ProfileItem { get; set; } = new List<ProfileItem>();
    }
}