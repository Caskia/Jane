using System.Threading.Tasks;

namespace Jane.PushNotifications
{
    public interface IPushNotificationService
    {
        Task<bool> SendToDeviceAsync(string deviceToken, string title, string body, string clickAction, dynamic data);

        Task<bool> SendToTopicAsync(string topic, string title, string body, string clickAction, dynamic data);
    }
}