using Autofac;
using Autofac.Extensions.DependencyInjection;
using ENode.Configurations;
using Jane.Configurations;
using Jane.Extensions;
using Jane.Timing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace JaneENodeGenericHostBuilderExample
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
                    ENodeManager.Configuration = JaneConfiguration.Create()
                                                        .UseAutofac(builder)
                                                        .RegisterCommonComponents()
                                                        .UseLog4Net()
                                                        .UseClockProvider(ClockProviders.Utc)
                                                        .RegisterUnhandledExceptionHandler()
                                                        .CreateECommon()
                                                        .CreateENode(new ConfigurationSetting())
                                                        .RegisterENodeComponents()
                                                        .RegisterBusinessComponents();

                    builder.RegisterType<HostedService>().As<IHostedService>();

                    ENodeManager.ContainerBuilder = builder;
                })
                .Build();

            var servicesProvider = host.Services as AutofacServiceProvider;
            var container = servicesProvider.GetFieldValue<ILifetimeScope>("_lifetimeScope");

            //var ecommonAutofacContainer = new ECommon.Autofac.AutofacObjectContainer(ENodeManager.ContainerBuilder);
            //var propertyInfo = ecommonAutofacContainer.GetType().GetProperty("_container");
            //propertyInfo.SetValue(ecommonAutofacContainer, container);
            //ECommon.Components.ObjectContainer.SetContainer(ecommonAutofacContainer);

            //var janeAutofacContainer = new Jane.Autofac.AutofacObjectContainer(ENodeManager.ContainerBuilder);
            //janeAutofacContainer.SetContainer(container);
            //Jane.Dependency.ObjectContainer.SetContainer(janeAutofacContainer);

            await host.StartAsync();
        }
    }
}