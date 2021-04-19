using System.Text.Json.Serialization;

namespace Jane.QCloud.Sms
{
    public class QCloudSmsSingleResult
    {
        [JsonPropertyName("errmsg")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("ext")]
        public string Ext { get; set; }

        [JsonPropertyName("fee")]
        public int Fee { get; set; }

        [JsonPropertyName("result")]
        public int Result { get; set; }

        [JsonPropertyName("sid")]
        public string SId { get; set; }
    }
}