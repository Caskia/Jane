using Newtonsoft.Json;
using System.Collections.Generic;
using TencentCloud.Common;

namespace Jane.QCloud.CKafka.Models
{
    public class CreateTopicRequest : AbstractModel
    {
        [JsonProperty("enableWhiteList")]
        public int EnableWhiteList { get; set; }

        [JsonProperty("instanceId")]
        public string InstanceId { get; set; }

        [JsonProperty("ipWhiteList")]
        public string IpWhiteList { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("partitionNum")]
        public int PartitionNum { get; set; }

        [JsonProperty("replicaNum")]
        public int ReplicaNum { get; set; }

        [JsonProperty("topicName")]
        public string TopicName { get; set; }

        internal override void ToMap(Dictionary<string, string> map, string prefix)
        {
            this.SetParamSimpleRaw(map, prefix + "instanceId", this.InstanceId);
            this.SetParamSimpleRaw(map, prefix + "topicName", this.TopicName);
            this.SetParamSimpleRaw(map, prefix + "partitionNum", this.PartitionNum);
            this.SetParamSimpleRaw(map, prefix + "replicaNum", this.ReplicaNum);
            this.SetParamSimpleRaw(map, prefix + "note", this.Note);
            this.SetParamSimpleRaw(map, prefix + "enableWhiteList", this.EnableWhiteList);
            this.SetParamSimpleRaw(map, prefix + "ipWhiteList.", this.IpWhiteList);
        }
    }
}