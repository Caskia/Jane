using Refit;
using System.Threading.Tasks;

namespace Jane.Netease.Im
{
    public interface INeteaseImApi
    {
        [Post("/user/create.action")]
        Task<NeteaseResponse> CreateUserAsync([Body(serializationMethod: BodySerializationMethod.UrlEncoded)] CreateUserInput input);
    }
}