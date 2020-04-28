using Refit;
using System.Threading.Tasks;

namespace Jane.RongCloud.Im
{
    public interface IRongCloudImApi
    {
        [Post("/user/getToken.json")]
        Task<GetUserTokenResponse> GetUserTokenAsync([Body(serializationMethod: BodySerializationMethod.UrlEncoded)] GetUserTokenInput input);

        [Post("/user/refresh.json")]
        Task<RongCloudResponse> RefreshUserAsync([Body(serializationMethod: BodySerializationMethod.UrlEncoded)] RefreshUserInput input);
    }
}