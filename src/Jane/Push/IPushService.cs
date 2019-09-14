using System.Threading.Tasks;

namespace Jane.Push
{
    public interface IPushService
    {
        Task<PushMessageOutput> PushAsync(PushMessage message);

        Task<PushMessageOutput> PushAsync(PushMessage message, PushPlatform platform);
    }
}