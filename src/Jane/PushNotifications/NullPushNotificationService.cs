using Jane.Logging;
using System.Threading.Tasks;

namespace Jane.PushNotifications
{
    public class NullPushNotificationService : IPushNotificationService
    {
        private readonly ILogger _logger;

        public NullPushNotificationService(
               ILogger logger
               )
        {
            _logger = logger;
        }

        public Task<bool> SendToDeviceAsync(string deviceToken, string title, string body, string clickAction, dynamic data)
        {
            _logger.Warn("USING NullPushNotificationService!");
            _logger.Debug("SendToDeviceAsync:");
            _logger.Debug($"DeviceToken: {deviceToken}");
            _logger.Debug($"Title: {title}");
            _logger.Debug($"Body: {body}");
            _logger.Debug($"ClickAction: {clickAction}");
            _logger.Debug($"Data: {data}");
            return Task.FromResult(false);
        }

        public Task<bool> SendToTopicAsync(string topic, string title, string body, string clickAction, dynamic data)
        {
            _logger.Warn("USING NullPushNotificationService!");
            _logger.Debug("SendToTopicAsync:");
            _logger.Debug($"Topic: {topic}");
            _logger.Debug($"Title: {title}");
            _logger.Debug($"Body: {body}");
            _logger.Debug($"ClickAction: {clickAction}");
            _logger.Debug($"Data: {data}");
            return Task.FromResult(false);
        }
    }
}