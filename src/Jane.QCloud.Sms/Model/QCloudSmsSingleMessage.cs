using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.QCloud.Sms
{
    public class QCloudSmsSingleMessage
    {
        [JsonProperty("ext")]
        public string Ext { get; set; } = string.Empty;

        [JsonProperty("extend")]
        public string Extend { get; set; } = string.Empty;

        [JsonProperty("params")]
        public List<string> Params { get; set; }

        [JsonProperty("sig")]
        public string Sig { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }

        [JsonProperty("tel")]
        public QCloudPhone Telphone { get; set; }

        [JsonProperty("tpl_id")]
        public int TemplateId { get; set; }

        [JsonProperty("time")]
        public long Timestamp { get; set; }
    }
}