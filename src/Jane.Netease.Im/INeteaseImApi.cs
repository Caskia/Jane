using Refit;
using System.Threading.Tasks;

namespace Jane.Netease.Im
{
    public interface INeteaseImApi
    {
        [Post("/user/create.action")]
        Task<NeteaseResponse> CreateUserAsync([Header("AppKey")]string appKey, [Header("Nonce")]string nonce, [Header("CurTime")]string curTime, [Header("CheckSum")]string checkSum, [Body(serializationMethod: BodySerializationMethod.UrlEncoded)]CreateUserInput input);
    }
}