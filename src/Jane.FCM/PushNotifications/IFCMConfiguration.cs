namespace Jane.FCM.PushNotifications
{
    public interface IFCMConfiguration
    {
        string FCMApiKey { get; set; }
        string FCMApiUrl { get; set; }
    }
}