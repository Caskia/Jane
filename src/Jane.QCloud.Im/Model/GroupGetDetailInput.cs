using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class GroupGetDetailInput
    {
        [JsonProperty("GroupIdList")]
        public List<string> GroupIds { get; set; }
    }
}