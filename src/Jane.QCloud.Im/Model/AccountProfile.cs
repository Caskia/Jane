using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class AccountProfile
    {
        [JsonProperty("To_Account")]
        public string Identifier { get; set; }

        public List<ProfileItem> ProfileItem { get; set; }
    }
}