using Jane.Masstransit.RabbitMq.MessageBus;
using Jane.MessageBus;
using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Reflection;

namespace Jane.Configurations
{
    /// <summary>
    /// configuration class masstransit rabbitmq extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// use masstransit rabbitmq as the messagebus
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Configuration UseMasstransitRabbitMq(this Configuration configuration, Action<IRabbitMqBusFactoryConfigurator> rabbitMqConfigure = null)
        {
            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(MasstransitMessageBus))
            };
            configuration.RegisterAssemblies(assemblies);

            //create bus
            var bus = Bus.Factory.CreateUsingRabbitMq(configure =>
            {
                configure.Host(
                     configuration.Root["RabbitMq:Host"],
                     Convert.ToUInt16(configuration.Root["RabbitMq:Port"]),
                     configuration.Root["RabbitMq:VirtualHost"],
                     h =>
                     {
                         h.Username(configuration.Root["RabbitMq:UserName"]);
                         h.Password(configuration.Root["RabbitMq:Password"]);
                     });

                rabbitMqConfigure?.Invoke(configure);
            });

            configuration.SetDefault<IBusControl, IBusControl>(bus);
            configuration.SetDefault<IMessageBus, MasstransitMessageBus>();

            return configuration;
        }
    }
}