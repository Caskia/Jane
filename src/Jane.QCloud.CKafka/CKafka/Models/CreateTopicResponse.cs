using Newtonsoft.Json;
using System.Collections.Generic;
using TencentCloud.Common;

namespace Jane.QCloud.CKafka.Models
{
    public class CreateTopicResponse : AbstractModel
    {
        [JsonProperty("topicId")]
        public string TopicId { get; set; }

        internal override void ToMap(Dictionary<string, string> map, string prefix)
        {
            this.SetParamSimple(map, prefix + "topicId", this.TopicId);
        }
    }
}