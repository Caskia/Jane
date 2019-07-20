using Autofac;
using Autofac.Extensions.DependencyInjection;
using Jane.Configurations;
using Jane.Timing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace JaneGenericHostBuilderExample
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHttpClient();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>((context, builder) =>
                {
                    JaneConfiguration.Create()
                    .UseAutofac(builder)
                    .RegisterCommonComponents()
                    .UseLog4Net()
                    .UseClockProvider(ClockProviders.Utc)
                    .RegisterUnhandledExceptionHandler();

                    builder.RegisterType<HostedService>().As<IHostedService>();
                })
                .Build();

            await host.StartAsync();
        }
    }
}