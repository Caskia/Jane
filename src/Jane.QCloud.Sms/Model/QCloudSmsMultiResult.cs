using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Jane.QCloud.Sms
{
    public class QCloudSmsMultiResult
    {
        [JsonPropertyName("detail")]
        public List<QCloudSmsMultiResultDetail> Detail { get; set; }

        [JsonPropertyName("errmsg")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("ext")]
        public string Ext { get; set; }

        [JsonPropertyName("result")]
        public int Result { get; set; }
    }
}