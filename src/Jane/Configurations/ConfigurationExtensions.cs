using Jane.Timing;

namespace Jane.Configurations
{
    public static class ConfigurationExtensions
    {
        public static Configuration UseClockProvider(this Configuration configuration, IClockProvider clockProvider)
        {
            Clock.Provider = clockProvider;
            return configuration;
        }
    }
}