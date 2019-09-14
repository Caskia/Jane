using Refit;
using System.Threading.Tasks;

namespace Jane.QCloud.Sms
{
    public interface IQCloudSmsApi
    {
        [Post("/v5/tlssmssvr/sendmultisms2")]
        Task<QCloudSmsMultiResult> SendMultiMessageAsync(string sdkappid, string random, [Body(BodySerializationMethod.Serialized)]QCloudSmsMultiMessage message);

        [Post("/v5/tlssmssvr/sendsms")]
        Task<QCloudSmsSingleResult> SendSingleMessageAsync(string sdkappid, string random, [Body(BodySerializationMethod.Serialized)]QCloudSmsSingleMessage message);
    }
}