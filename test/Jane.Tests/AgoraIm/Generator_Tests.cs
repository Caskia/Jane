using Jane.Agora.Rtc;
using Jane.Dependency;
using Shouldly;
using Xunit;

namespace Jane.Tests.AgoraIm
{
    public class Generator_Tests : TestBase
    {
        private readonly IAgoraRtcService _agoraImService;

        public Generator_Tests()
        {
            _agoraImService = ObjectContainer.Resolve<IAgoraRtcService>();
        }

        [Fact]
        public void Should_Generator_Access_Token()
        {
            //Arrange
            var userId = 12324u;
            var channelName = "room1";

            //Act
            var token = _agoraImService.GenerateToken(userId, channelName);

            //Assert
            token.ShouldNotBeNull();
        }
    }
}