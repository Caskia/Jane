using Refit;
using System.Threading.Tasks;

namespace Jane.UMeng.Push
{
    public interface IUMengPushApi
    {
        [Post("/api/send")]
        Task<UMengPushMessageResult> SendAsync<TPayload, TPolicy>(string sign, [Body(BodySerializationMethod.Serialized)]UMengPushMessage<TPayload, TPolicy> message)
            where TPayload : UMengPayload
            where TPolicy : UMengPolicy;
    }
}