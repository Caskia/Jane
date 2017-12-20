using Autofac;
using Jane.Autofac;
using Jane.Dependency;

namespace Jane.Configurations
{
    /// <summary>
    /// configuration class Autofac extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Use Autofac as the object container.
        /// </summary>
        public static Configuration UseAutofac(this Configuration configuration)
        {
            return UseAutofac(configuration, new ContainerBuilder());
        }

        /// <summary>
        /// Use Autofac as the object container.
        /// </summary>
        public static Configuration UseAutofac(this Configuration configuration, ContainerBuilder containerBuilder)
        {
            ObjectContainer.SetContainer(new AutofacObjectContainer(containerBuilder));
            return configuration;
        }
    }
}