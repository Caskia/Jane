using Jane.Configurations;
using Jane.Timing;
using System.Reflection;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Tests.BackgroundJobs
{
    public abstract class BackgroundJobsTestBase
    {
        protected BackgroundJobsTestBase()
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
                   .BuildContainer()
                   .RegisterBackgroundJob(_bussinessAssemblies);
        }
    }
}