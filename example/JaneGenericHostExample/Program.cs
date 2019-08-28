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
            var host = new HostBuilder()
                .UseAutofac()
                .ConfigureServices(services =>
                {
                    services.AddHttpClient();

                    services.AddSingleton<IHostedService, HostedService>();
                })
                .ConfigureContainer<ContainerBuilder>((context, builder) =>
                {
                    var _bussinessAssemblies = new[]
                    {
                        Assembly.GetExecutingAssembly()
                    };

                    JaneConfiguration.Create()
                        .UseAutofac(builder)
                        .RegisterCommonComponents()
                        .RegisterAssemblies(_bussinessAssemblies)
                        .UseLog4Net()
                        .UseClockProvider(ClockProviders.Utc)
                        .RegisterUnhandledExceptionHandler();
                })
                .Build();

            host.Services.PopulateJaneDIContainer();

            await host.StartAsync();
        }
    }
}