using Jane.Configurations;
using Jane.Events.Bus;
using Jane.Logging;
using Jane.Timing;
using System.Reflection;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Tests.Events.Bus
{
    public abstract class EventBusTestBase
    {
        protected IEventBus EventBus;

        protected EventBusTestBase()
        {
            InitializeJane();

            EventBus = new EventBus();
        }

        private JaneConfiguration InitializeJane()
        {
            var _bussinessAssemblies = new[]
             {
                Assembly.GetExecutingAssembly()
            };

            return JaneConfiguration.Create()
                   .UseAutofac()
                   .RegisterCommonComponents()
                   .RegisterAssemblies(_bussinessAssemblies)
                   .UseLog4Net()
                   .UseClockProvider(ClockProviders.Utc)
                   .RegisterUnhandledExceptionHandler()
                   .BuildContainer();
        }
    }
}