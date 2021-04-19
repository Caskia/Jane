using System.Text.Json.Serialization;

namespace Jane.QCloud.Im
{
    public class GroupMember
    {
        [JsonPropertyName("Member_Account")]
        public string Member { get; set; }

        public string Role { get; set; }
    }
}