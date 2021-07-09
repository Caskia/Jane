namespace Jane.Agora.Rtc
{
    public interface IAgoraRtcService
    {
        string GenerateToken(uint userId, string channelName);
    }
}