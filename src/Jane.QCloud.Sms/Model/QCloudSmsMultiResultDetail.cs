using System.Text.Json.Serialization;

namespace Jane.QCloud.Sms
{
    public class QCloudSmsMultiResultDetail
    {
        [JsonPropertyName("errmsg")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("fee")]
        public int Fee { get; set; }

        [JsonPropertyName("nationalCode")]
        public string NationalCode { get; set; }

        [JsonPropertyName("mobile")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("result")]
        public int Result { get; set; }

        [JsonPropertyName("sid")]
        public string SId { get; set; }
    }
}