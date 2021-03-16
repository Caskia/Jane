using Jane.PushNotifications;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Jane.FCM.PushNotifications
{
    public class FCMPushNotificationService : IPushNotificationService
    {
        private readonly IFCMConfiguration _configuration;

        public FCMPushNotificationService(IFCMConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendToDeviceAsync(string deviceToken, string title, string body, string clickAction, dynamic data)
        {
            if (string.IsNullOrEmpty(deviceToken))
            {
                throw new ArgumentNullException("deviceToken");
            }

            var client = new RestClient(_configuration.FCMApiUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "key=" + _configuration.FCMApiKey);
            request.AddHeader("Content-Type", "application/json");

            var content = new
            {
                to = deviceToken,
                notification = new
                {
                    title = title,
                    body = body,
                    icon = "myicon",
                    sound = "default",
                    click_action = clickAction
                },
                time_to_live = 60 * 60 * 24,
                data = data
            };
            request.AddJsonBody(content);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(response.Content))
            {
                return false;
            }
            var result = JsonConvert.DeserializeObject<FCMDeviceResponse>(response.Content);
            if (result.Success > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> SendToTopicAsync(string topic, string title, string body, string clickAction, dynamic data)
        {
            if (string.IsNullOrEmpty(topic))
            {
                throw new ArgumentNullException("topic");
            }

            var client = new RestClient(_configuration.FCMApiUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "key=" + _configuration.FCMApiKey);
            request.AddHeader("Content-Type", "application/json");

            var content = new
            {
                to = topic,
                notification = new
                {
                    title = title,
                    body = body,
                    icon = "myicon",
                    sound = "default",
                    click_action = clickAction
                },
                time_to_live = 60 * 60 * 24,
                data = data
            };
            request.AddJsonBody(content);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(response.Content))
            {
                return false;
            }
            var result = JsonConvert.DeserializeObject<FCMTopicResponse>(response.Content);
            if (!string.IsNullOrEmpty(result.MessageId) && string.IsNullOrEmpty(result.Error))
            {
                return true;
            }
            return false;
        }
    }
}