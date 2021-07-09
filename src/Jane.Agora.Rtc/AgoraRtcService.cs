using AgoraIO.Media;
using Jane.Configurations;
using Microsoft.Extensions.Options;

namespace Jane.Agora.Rtc
{
    public class AgoraRtcService : IAgoraRtcService
    {
        private readonly AgoraRtcOptions _options;

        public AgoraRtcService(IOptions<AgoraRtcOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public string GenerateToken(uint userId, string channelName)
        {
            return new AccessToken(_options.AppId, _options.AppSecret, channelName, userId.ToString()).build();
        }
    }
}