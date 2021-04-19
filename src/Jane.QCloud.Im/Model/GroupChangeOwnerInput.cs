using System.Text.Json.Serialization;

namespace Jane.QCloud.Im
{
    public class GroupChangeOwnerInput
    {
        public string GroupId { get; set; }

        [JsonPropertyName("NewOwner_Account")]
        public string Owner { get; set; }
    }
}