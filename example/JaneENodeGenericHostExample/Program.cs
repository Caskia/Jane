using Autofac;
using Autofac.Extensions.DependencyInjection;
using ENode.Configurations;
using Jane.Configurations;
using Jane.Autofac;
using Jane.ENode;
using Jane.Timing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Threading.Tasks;
using JaneConfiguration = Jane.Configurations.Configuration;
using ENodeConfiguration = ENode.Configurations.ENodeConfiguration;

namespace JaneENodeGenericHostExample
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var bussinessAssemblies = new[]
                    {
                        Assembly.GetExecutingAssembly()
                    };

            var eNodeConfiguration = default(ENodeConfiguration);

            var host = new HostBuilder()
                .UseAutofac()
                .ConfigureServices(services =>
                {
                    services.AddHttpClient();

                    services.AddHostedService<HostedService>();
                })
                .ConfigureContainer<ContainerBuilder>((context, builder) =>
                {
                    var janeConfiguration = JaneConfiguration.Create()
                        .UseAutofac(builder)
                        .RegisterCommonComponents()
                        .RegisterAssemblies(bussinessAssemblies)
                        .UseLog4Net()
                        .UseClockProvider(ClockProviders.Utc)
                        .RegisterUnhandledExceptionHandler();

                    eNodeConfiguration = janeConfiguration
                         .CreateECommon(builder)
                         .CreateENode(new ConfigurationSetting())
                         .RegisterENodeComponents()
                         .RegisterBusinessComponents();
                })
                .Build();

            host.Services.PopulateJaneDIContainer();
            host.Services.PopulateENodeDIContainer();

            eNodeConfiguration
                .InitializeBusinessAssemblies(bussinessAssemblies);

            using (host)
            {
                await host.StartAsync();

                await host.WaitForShutdownAsync();
            }
        }
    }
}