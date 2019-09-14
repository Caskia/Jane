using Newtonsoft.Json;

namespace Jane.QCloud.Sms
{
    public class QCloudSmsMultiResultDetail
    {
        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }

        [JsonProperty("fee")]
        public int Fee { get; set; }

        [JsonProperty("nationalCode")]
        public string NationalCode { get; set; }

        [JsonProperty("mobile")]
        public string PhoneNumber { get; set; }

        [JsonProperty("result")]
        public int Result { get; set; }

        [JsonProperty("sid")]
        public string SId { get; set; }
    }
}