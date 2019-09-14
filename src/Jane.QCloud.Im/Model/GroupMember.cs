using Newtonsoft.Json;

namespace Jane.QCloud.Im
{
    public class GroupMember
    {
        [JsonProperty("Member_Account")]
        public string Member { get; set; }

        public string Role { get; set; }
    }
}