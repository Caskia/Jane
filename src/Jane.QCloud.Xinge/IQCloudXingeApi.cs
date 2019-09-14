using Refit;
using System.Threading.Tasks;

namespace Jane.QCloud.Xinge
{
    public interface IQCloudXingeApi
    {
        [Post("/push/app")]
        Task<XingePushResult> PushAsync([Header("Authorization")] string authorization, [Body(BodySerializationMethod.Serialized)]XingePushMessage message);
    }
}