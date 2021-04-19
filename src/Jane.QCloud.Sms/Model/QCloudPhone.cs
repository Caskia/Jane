using System.Text.Json.Serialization;

namespace Jane.QCloud.Sms
{
    public class QCloudPhone
    {
        [JsonPropertyName("nationcode")]
        public string NationCode { get; set; }

        [JsonPropertyName("mobile")]
        public string PhoneNumber { get; set; }
    }
}