namespace Jane.Agora.Rtc
{
    public interface IAgoraImService
    {
        string GenerateToken(uint userId, string channelName);
    }
}