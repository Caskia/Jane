using Autofac;
using Autofac.Extensions.DependencyInjection;
using Jane.Autofac;
using Jane.Configurations;
using Jane.Timing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Tests
{
    public class JaneTestFixture : IDisposable
    {
        private Assembly[] _bussinessAssemblies;

        public JaneTestFixture()
        {
            if (JaneConfiguration.Instance != null)
            {
                Cleanup();
            }

            InitializeJane();
        }

        public void Dispose()
        {
            Cleanup();
        }

        private void Cleanup()
        {
            Thread.Sleep(1000);
        }

        private void ConfigureContainer(ContainerBuilder builder)
        {
            _bussinessAssemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly()
            };

            JaneConfiguration.Instance
                   .UseAutofac(builder)
                   .RegisterCommonComponents()
                   .RegisterAssemblies(_bussinessAssemblies)
                   .UseLog4Net()
                   .UseClockProvider(ClockProviders.Utc)
                   .RegisterUnhandledExceptionHandler();
        }

        private void ConfigureService(IServiceCollection services)
        {
            services.AddAgoraIm();
        }

        private void InitializeJane()
        {
            var _bussinessAssemblies = new[]
             {
                Assembly.GetExecutingAssembly()
            };

            JaneConfiguration.Create();

            var services = new ServiceCollection();
            ConfigureService(services);

            var serviceProviderFactory = new AutofacServiceProviderFactory();
            var containerBuilder = serviceProviderFactory.CreateBuilder(services);

            ConfigureContainer(containerBuilder);

            var serviceProvider = serviceProviderFactory.CreateServiceProvider(containerBuilder);
            serviceProvider.PopulateJaneDIContainer();

            JaneConfiguration.Instance.RegisterBackgroundJob(_bussinessAssemblies);
        }
    }
}