using Jane.MessageBus;
using MassTransit;
using System.Threading.Tasks;

namespace Jane.Masstransit.RabbitMq.MessageBus
{
    public class MasstransitMessageBus : IMessageBus
    {
        private readonly IBusControl _bus;

        public MasstransitMessageBus(IBusControl bus)
        {
            _bus = bus;
        }

        public Task PublishAsync<T>(T message) where T : class
        {
            return _bus.Publish<T>(message);
        }

        public Task PublishAsync<T>(object message) where T : class
        {
            return _bus.Publish<T>(message);
        }
    }
}