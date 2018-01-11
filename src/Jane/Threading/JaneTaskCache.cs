using System.Threading.Tasks;

namespace Jane.Threading
{
    public static class JaneTaskCache
    {
        public static Task CompletedTask { get; } = Task.FromResult(0);
    }
}