using Newtonsoft.Json;
using System.Collections.Generic;
using TencentCloud.Common;

namespace Jane.QCloud.CKafka.Models
{
    public class DeleteTopicRequest : AbstractModel
    {
        [JsonProperty("instanceId")]
        public string InstanceId { get; set; }

        [JsonProperty("topicName")]
        public string TopicName { get; set; }

        internal override void ToMap(Dictionary<string, string> map, string prefix)
        {
            this.SetParamSimpleRaw(map, prefix + "instanceId", this.InstanceId);
            this.SetParamSimpleRaw(map, prefix + "topicName", this.TopicName);
        }
    }
}