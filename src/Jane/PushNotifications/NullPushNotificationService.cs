using Jane.Logging;
using System.Threading.Tasks;

namespace Jane.PushNotifications
{
    public class NullPushNotificationService : IPushNotificationService
    {
        private readonly ILogger Logger;

        public NullPushNotificationService(
               ILoggerFactory loggerFactory
               )
        {
            Logger = loggerFactory.Create(typeof(NullPushNotificationService));
        }

        public Task<bool> SendToDeviceAsync(string deviceToken, string title, string body, string clickAction, dynamic data)
        {
            Logger.Warn("USING NullPushNotificationService!");
            Logger.Debug("SendToDeviceAsync:");
            Logger.Debug($"DeviceToken: {deviceToken}");
            Logger.Debug($"Title: {title}");
            Logger.Debug($"Body: {body}");
            Logger.Debug($"ClickAction: {clickAction}");
            Logger.Debug($"Data: {data}");
            return Task.FromResult(false);
        }

        public Task<bool> SendToTopicAsync(string topic, string title, string body, string clickAction, dynamic data)
        {
            Logger.Warn("USING NullPushNotificationService!");
            Logger.Debug("SendToTopicAsync:");
            Logger.Debug($"Topic: {topic}");
            Logger.Debug($"Title: {title}");
            Logger.Debug($"Body: {body}");
            Logger.Debug($"ClickAction: {clickAction}");
            Logger.Debug($"Data: {data}");
            return Task.FromResult(false);
        }
    }
}