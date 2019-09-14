using Refit;
using System.Threading.Tasks;

namespace Jane.Mob.Push
{
    public interface IMobPushApi
    {
        [Post("/v2/push")]
        Task<MobPushResult> PushAsync([Header("key")]string key, [Header("sign")]string sign, [Body(BodySerializationMethod.Serialized)]MobPushMessage message);
    }
}