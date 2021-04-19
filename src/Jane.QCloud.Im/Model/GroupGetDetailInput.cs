using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class GroupGetDetailInput
    {
        [JsonPropertyName("GroupIdList")]
        public List<string> GroupIds { get; set; }
    }
}