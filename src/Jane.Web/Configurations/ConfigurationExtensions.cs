using System.Reflection;

namespace Jane.Configurations
{
    /// <summary>
    /// configuration class Web extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        public static Configuration UseWeb(this Configuration configuration)
        {
            var assemblies = new[]
           {
                Assembly.Load("Jane.Web")
            };
            configuration.RegisterAssemblies(assemblies);

            return configuration;
        }
    }
}