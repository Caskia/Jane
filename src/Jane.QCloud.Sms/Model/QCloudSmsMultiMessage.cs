using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Jane.QCloud.Sms
{
    public class QCloudSmsMultiMessage
    {
        [JsonPropertyName("ext")]
        public string Ext { get; set; } = string.Empty;

        [JsonPropertyName("extend")]
        public string Extend { get; set; } = string.Empty;

        [JsonPropertyName("params")]
        public List<string> Params { get; set; }

        [JsonPropertyName("sig")]
        public string Sig { get; set; }

        [JsonPropertyName("sign")]
        public string Sign { get; set; }

        [JsonPropertyName("tel")]
        public List<QCloudPhone> Telphones { get; set; }

        [JsonPropertyName("tpl_id")]
        public int TemplateId { get; set; }

        [JsonPropertyName("time")]
        public long Timestamp { get; set; }
    }
}