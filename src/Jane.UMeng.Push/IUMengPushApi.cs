using Refit;
using System.Threading.Tasks;

namespace Jane.UMeng.Push
{
    public interface IUMengPushApi
    {
        [Post("/api/send")]
        Task<UMengPushMessageResult> SendAsync(string sign, [Body(BodySerializationMethod.Serialized)] UMengPushMessage message);
    }
}