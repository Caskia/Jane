using System.Threading.Tasks;

namespace Jane
{
    public interface IIncrementDataGenerator
    {
        Task<long> IncrementAsync(string key, int value = 1);
    }
}