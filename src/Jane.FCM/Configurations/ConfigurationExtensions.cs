using Jane.FCM.PushNotifications;
using Jane.PushNotifications;

namespace Jane.Configurations
{
    /// <summary>
    /// configuration class masstransit rabbitmq extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// use masstransit rabbitmq as the messagebus
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Configuration UseFCM(this Configuration configuration)
        {
            configuration.SetDefault<IFCMConfiguration, FCMConfiguration>(new FCMConfiguration
            (
               configuration.Root["FCM:ApiKey"],
               configuration.Root["FCM:ApiUrl"]
            ));
            configuration.SetDefault<IPushNotificationService, FCMPushNotificationService>();

            return configuration;
        }
    }
}