using Autofac;
using Autofac.Extensions.DependencyInjection;
using Jane.Configurations;
using Jane.Timing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Jane.Autofac;
using JaneConfiguration = Jane.Configurations.Configuration;
using System.Reflection;

namespace JaneGenericHostExample
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var _bussinessAssemblies = new[]
                    {
                        Assembly.GetExecutingAssembly()
                    };

            var host = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices(services =>
                {
                    JaneConfiguration.Create();

                    services.AddHttpClient();

                    services.AddHostedService<HostedService>();
                })
                .ConfigureContainer<ContainerBuilder>((context, builder) =>
                {
                    JaneConfiguration.Instance
                        .UseAutofac(builder)
                        .RegisterCommonComponents()
                        .RegisterAssemblies(_bussinessAssemblies)
                        .UseLog4Net()
                        .UseClockProvider(ClockProviders.Utc)
                        .RegisterUnhandledExceptionHandler();
                })
                .Build();

            host.Services.PopulateJaneDIContainer();

            using (host)
            {
                await host.StartAsync();

                await host.WaitForShutdownAsync();
            }
        }
    }
}