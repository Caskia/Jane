using Newtonsoft.Json;

namespace Jane.FCM.PushNotifications
{
    public class FCMTopicResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }
    }
}