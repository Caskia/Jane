using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class GroupDeleteMemberInput
    {
        public string GroupId { get; set; }

        [JsonPropertyName("MemberToDel_Account")]
        public List<string> MemeberList { get; set; } = new List<string>();

        public string Reason { get; set; }

        public int Silence { get; set; } = 1;
    }
}