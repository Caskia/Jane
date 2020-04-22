using Jane.Events.Bus;
using Xunit;

namespace Jane.Tests
{
    [Collection(nameof(JaneTestCollection))]
    public abstract class TestBase
    {
        protected IEventBus EventBus;

        protected TestBase()
        {
            EventBus = new EventBus();
        }
    }
}