using System.Threading.Tasks;

namespace Jane.MessageBus
{
    public class NullMessageBus : IMessageBus
    {
        public Task PublishAsync<T>(T message) where T : class
        {
            return Task.FromResult(0);
        }

        public Task PublishAsync<T>(object message) where T : class
        {
            return Task.FromResult(0);
        }
    }
}