using Newtonsoft.Json;
using System.Collections.Generic;
using TencentCloud.Common;

namespace Jane.QCloud.CKafka.Models
{
    public class SetTopicAttributesRequest : AbstractModel
    {
        [JsonProperty("enableWhiteList")]
        public int EnableWhiteList { get; set; }

        [JsonProperty("instanceId")]
        public string InstanceId { get; set; }

        [JsonProperty("maxMessageBytes")]
        public int MaxMessageBytes { get; set; }

        [JsonProperty("minInsyncReplicas")]
        public int MinInsyncReplicas { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("retentionMs")]
        public int RetentionMs { get; set; }

        [JsonProperty("segmentMs")]
        public int SegmentMs { get; set; }

        [JsonProperty("topicName")]
        public string TopicName { get; set; }

        [JsonProperty("uncleanLeaderElectionEnable")]
        public int UncleanLeaderElectionEnable { get; set; }

        internal override void ToMap(Dictionary<string, string> map, string prefix)
        {
            this.SetParamSimpleRaw(map, prefix + "instanceId", this.InstanceId);
            this.SetParamSimpleRaw(map, prefix + "topicName", this.TopicName);
            this.SetParamSimpleRaw(map, prefix + "note", this.Note);
            this.SetParamSimpleRaw(map, prefix + "enableWhiteList", this.EnableWhiteList);
            this.SetParamSimpleRaw(map, prefix + "minInsyncReplicas", this.MinInsyncReplicas);
            this.SetParamSimpleRaw(map, prefix + "uncleanLeaderElectionEnable", this.UncleanLeaderElectionEnable);
            this.SetParamSimpleRaw(map, prefix + "retentionMs", this.RetentionMs);
            this.SetParamSimpleRaw(map, prefix + "segmentMs", this.SegmentMs);
            this.SetParamSimpleRaw(map, prefix + "maxMessageBytes", this.MaxMessageBytes);
        }
    }
}