using Jane.BackgroundJobs;
using Jane.Hangfire;
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
        public static Configuration UseHangFire(this Configuration configuration)
        {
            var assemblies = new[]
            {
                Assembly.GetExecutingAssembly()
            };
            configuration.RegisterAssemblies(assemblies);

            configuration.SetDefault<IBackgroundJobManager, HangfireBackgroundJobManager>();

            return configuration;
        }
    }
}