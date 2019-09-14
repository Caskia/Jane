using Newtonsoft.Json;

namespace Jane.QCloud.Sms
{
    public class QCloudSmsSingleResult
    {
        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }

        [JsonProperty("ext")]
        public string Ext { get; set; }

        [JsonProperty("fee")]
        public int Fee { get; set; }

        [JsonProperty("result")]
        public int Result { get; set; }

        [JsonProperty("sid")]
        public string SId { get; set; }
    }
}