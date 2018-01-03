using Jane.Reflection;
using Jane.Timing;
using System.Linq;
using System.Reflection;

namespace Jane.Configurations
{
    public static class ConfigurationExtensions
    {
        public static Configuration UseClockProvider(this Configuration configuration, IClockProvider clockProvider)
        {
            Clock.Provider = clockProvider;
            return configuration;
        }

        public static Configuration UseTypeFinder(this Configuration configuration, params Assembly[] assemblies)
        {
            configuration.SetDefault<IAssemblyFinder, AssemblyFinder>(new AssemblyFinder(assemblies.ToList()));
            configuration.SetDefault<ITypeFinder, TypeFinder>();
            return configuration;
        }
    }
}