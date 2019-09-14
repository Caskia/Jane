using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.QCloud.Sms
{
    public class QCloudSmsMultiResult
    {
        [JsonProperty("detail")]
        public List<QCloudSmsMultiResultDetail> Detail { get; set; }

        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }

        [JsonProperty("ext")]
        public string Ext { get; set; }

        [JsonProperty("result")]
        public int Result { get; set; }
    }
}