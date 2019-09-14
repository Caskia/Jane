using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class AccountGetProfileInput
    {
        [JsonProperty("To_Account")]
        public List<string> Identifiers { get; set; }

        public List<string> TagList { get; set; }
    }
}