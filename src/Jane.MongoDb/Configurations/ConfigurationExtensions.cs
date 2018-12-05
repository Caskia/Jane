using System;
using System.Reflection;

namespace Jane.Configurations
{
    /// <summary>
    /// configuration class mongnodb extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// use mongodb as the repository
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Configuration UseMongoDb(this Configuration configuration, Action action = null)
        {
            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(MongoDbConfiguration))
            };
            configuration.RegisterAssemblies(assemblies);
            configuration.SetDefault<IIncrementDataGenerator, MongoDbIncrementDataGenerator>();

            action?.Invoke();

            return configuration;
        }
    }
}