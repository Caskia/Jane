using Jane.Configurations;
using Jane.Timing;
using System.Reflection;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Tests
{
    public abstract class TestBase
    {
        public TestBase()
        {
            InitializeJane();
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