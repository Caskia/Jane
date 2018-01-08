using AutoMapper;
using Jane.AutoMapper;
using Jane.Dependency;
using Jane.Reflection;
using System;
using System.Linq;
using System.Reflection;

namespace Jane.Configurations
{
    /// <summary>
    /// configuration class auto mapper extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Create auto mapper mappings
        /// </summary>
        /// <returns></returns>
        public static Configuration CreateAutoMapperMappings(this Configuration configuration)
        {
            ObjectContainer.Resolve<IAutoMapperRegister>().CreateMappings();
            return configuration;
        }

        /// <summary>Use auto mapper as the object mapper.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseAutoMapper(
            this Configuration configuration,
            Assembly[] assemblies,
            Action<IMapperConfigurationExpression> configureMapper = null
            )
        {
            if (assemblies == null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            configuration.UseTypeFinder(assemblies);

            var autoMapperConfiguraion = GetAutoMapperConfigurationsFromAssembly(assemblies, configureMapper);

            configuration.SetDefault<IAutoMapperConfiguration, AutoMapperConfiguration>(autoMapperConfiguraion);
            configuration.SetDefault<IAutoMapperRegister, AutoMapperRegister>();
            return configuration;
        }

        private static AutoMapperConfiguration GetAutoMapperConfigurationsFromAssembly(
            Assembly[] assemblies,
            Action<IMapperConfigurationExpression> configureMapper
            )
        {
            var autoMapperConfiguraion = new AutoMapperConfiguration();
            if (configureMapper != null)
            {
                autoMapperConfiguraion.Configurators.Add(configureMapper);
            }

            var autoMapMapperTypes = assemblies.SelectMany(a => a.GetMappingTypes(typeof(IAutoMapMapper)));
            if (autoMapMapperTypes.Count() > 0)
            {
                autoMapperConfiguraion.Configurators.Add(expression =>
                {
                    foreach (var mapper in autoMapMapperTypes.Select(Activator.CreateInstance).Cast<IAutoMapMapper>())
                    {
                        mapper.Map(expression);
                    }
                });
            }

            return autoMapperConfiguraion;
        }
    }
}