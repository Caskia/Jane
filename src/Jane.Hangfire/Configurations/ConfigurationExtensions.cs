using System.Reflection;

namespace Jane.Configurations
{
    /// <summary>
    /// configuration class hangefire extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// use hangefire as background job service
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Configuration UseHangeFire(this Configuration configuration)
        {
            var assemblies = new[]
            {
                Assembly.GetExecutingAssembly()
            };
            configuration.RegisterAssemblies(assemblies);

            return configuration;
        }
    }
}