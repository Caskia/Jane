using System.Threading.Tasks;

namespace Jane.Netease.Im
{
    public interface INeteaseImService
    {
        Task CreateUserAsync(CreateUserInput input);
    }
}