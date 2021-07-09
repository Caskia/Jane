using AgoraIO.Media;
using Jane.Configurations;
using Microsoft.Extensions.Options;

namespace Jane.Agora.Rtc
{
    public class AgoraImService : IAgoraImService
    {
        private readonly AgoraImOptions _options;

        public AgoraImService(IOptions<AgoraImOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public string GenerateToken(uint userId, string channelName)
        {
            return new AccessToken(_options.AppId, _options.AppSecret, channelName, userId.ToString()).build();
        }
    }
}