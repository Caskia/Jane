using Autofac;
using Autofac.Extensions.DependencyInjection;
using ENode.Configurations;
using Jane.Configurations;
using Jane.ENode;
using Jane.Timing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Threading.Tasks;
using ENodeConfiguration = ENode.Configurations.ENodeConfiguration;
using JaneConfiguration = Jane.Configurations.Configuration;

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
                    JaneConfiguration.Create();

                    services.AddHttpClient();

                    services.AddHostedService<HostedService>();
                })
                .ConfigureContainer<ContainerBuilder>((context, builder) =>
                {
                    JaneConfiguration.Instance
                        .UseAutofac(builder)
                        .RegisterCommonComponents()
                        .RegisterAssemblies(bussinessAssemblies)
                        .UseLog4Net()
                        .UseClockProvider(ClockProviders.Utc)
                        .RegisterUnhandledExceptionHandler()
                        .CreateECommon(builder)
                        .CreateENode(new ConfigurationSetting())
                        .RegisterENodeComponents()
                        .RegisterBusinessComponents();
                })
                .Build();

            host.Services.PopulateJaneENodeDIContainer();

            ENodeConfiguration.Instance
                .InitializeBusinessAssemblies(bussinessAssemblies);

            using (host)
            {
                await host.StartAsync();

                await host.WaitForShutdownAsync();
            }
        }
    }
}