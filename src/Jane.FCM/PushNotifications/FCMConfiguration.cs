using Jane.Extensions;
using System;

namespace Jane.FCM.PushNotifications
{
    public class FCMConfiguration : IFCMConfiguration
    {
        public FCMConfiguration(string fcmApiKey, string fcmApiUrl)
        {
            if (fcmApiKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(fcmApiKey));
            }

            if (fcmApiUrl.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(fcmApiUrl));
            }

            FCMApiKey = fcmApiKey;
            FCMApiUrl = fcmApiUrl;
        }

        public string FCMApiKey { get; set; }

        public string FCMApiUrl { get; set; }
    }
}