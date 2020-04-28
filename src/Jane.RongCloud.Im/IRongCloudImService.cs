using System.Threading.Tasks;

namespace Jane.RongCloud.Im
{
    public interface IRongCloudImService
    {
        Task<string> GetUserTokenAsync(GetUserTokenInput input);
    }
}