using Newtonsoft.Json;

namespace Jane.QCloud.Im
{
    public class GroupChangeOwnerInput
    {
        public string GroupId { get; set; }

        [JsonProperty("NewOwner_Account")]
        public string Owner { get; set; }
    }
}