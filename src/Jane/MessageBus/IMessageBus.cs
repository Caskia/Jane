using System.Threading.Tasks;

namespace Jane.MessageBus
{
    public interface IMessageBus
    {
        Task PublishAsync<T>(T message) where T : class;

        Task PublishAsync<T>(object message) where T : class;
    }
}