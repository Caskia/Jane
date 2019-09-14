using Newtonsoft.Json;

namespace Jane.QCloud.Im
{
    public class GroupMemberResponse
    {
        [JsonProperty("Member_Account")]
        public string Member { get; set; }

        public int Result { get; set; }
    }
}