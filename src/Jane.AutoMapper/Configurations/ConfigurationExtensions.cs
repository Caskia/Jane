using AutoMapper;
using Jane.AutoMapper;
using Jane.Dependency;
using System.Reflection;

namespace Jane.Configurations
{
    /// <summary>
    /// configuration class auto mapper extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>Use auto mapper as the object mapper.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseAutoMapper(
            this Configuration configuration,
            AutoMapperConfiguration autoMapperConfiguraion,
            params Assembly[] assemblies
            )
        {
            configuration.UseTypeFinder(assemblies);
            configuration.SetDefault<IAutoMapperConfiguration, AutoMapperConfiguration>(autoMapperConfiguraion);
            configuration.SetDefault<IAutoMapperRegister, AutoMapperRegister>();
            ObjectContainer.Resolve<IAutoMapperRegister>().CreateMappings(mapper =>
            {
                configuration.SetDefault<IMapper, IMapper>(mapper);
            });
            return configuration;
        }
    }
}