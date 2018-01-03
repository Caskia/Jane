using Newtonsoft.Json;

namespace Jane.FCM.PushNotifications
{
    public class FCMDeviceResponse
    {
        [JsonProperty("canonical_ids")]
        public string CanonicalIds { get; set; }

        [JsonProperty("failure")]
        public int Failure { get; set; }

        [JsonProperty("multicast_id")]
        public string MulticastId { get; set; }

        [JsonProperty("success")]
        public int Success { get; set; }
    }
}