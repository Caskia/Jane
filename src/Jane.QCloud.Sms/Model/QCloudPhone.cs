using Newtonsoft.Json;

namespace Jane.QCloud.Sms
{
    public class QCloudPhone
    {
        [JsonProperty("nationcode")]
        public string NationCode { get; set; }

        [JsonProperty("mobile")]
        public string PhoneNumber { get; set; }
    }
}