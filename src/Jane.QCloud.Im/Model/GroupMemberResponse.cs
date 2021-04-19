using System.Text.Json.Serialization;

namespace Jane.QCloud.Im
{
    public class GroupMemberResponse
    {
        [JsonPropertyName("Member_Account")]
        public string Member { get; set; }

        public int Result { get; set; }
    }
}