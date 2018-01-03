using AutoMapper;
using Jane.Logging;
using Jane.Reflection;
using System;
using System.Reflection;

namespace Jane.AutoMapper
{
    public class AutoMapperRegister : IAutoMapperRegister
    {
        private static readonly object SyncObj = new object();
        private static volatile bool _createdMappingsBefore;
        private readonly IAutoMapperConfiguration _autoMapperConfiguration;
        private readonly ILogger _logger;
        private readonly ITypeFinder _typeFinder;

        public AutoMapperRegister(
            IAutoMapperConfiguration autoMapperConfiguration,
            ILogger logger,
            ITypeFinder typeFinder
            )
        {
            _autoMapperConfiguration = autoMapperConfiguration;
            _logger = logger;
            _typeFinder = typeFinder;
        }

        public void CreateMappings(Action<IMapper> registerMapper)
        {
            lock (SyncObj)
            {
                Action<IMapperConfigurationExpression> configurer = configuration =>
                {
                    FindAndAutoMapTypes(configuration);
                    foreach (var configurator in _autoMapperConfiguration.Configurators)
                    {
                        configurator(configuration);
                    }
                };

                if (_autoMapperConfiguration.UseStaticMapper)
                {
                    //We should prevent duplicate mapping in an application, since Mapper is static.
                    if (!_createdMappingsBefore)
                    {
                        Mapper.Initialize(configurer);
                        _createdMappingsBefore = true;
                    }
                    registerMapper(Mapper.Instance);
                }
                else
                {
                    var config = new MapperConfiguration(configurer);
                    registerMapper(config.CreateMapper());
                }
            }
        }

        private void FindAndAutoMapTypes(IMapperConfigurationExpression configuration)
        {
            var types = _typeFinder.Find(type =>
                {
                    var typeInfo = type.GetTypeInfo();
                    return typeInfo.IsDefined(typeof(AutoMapAttribute)) ||
                           typeInfo.IsDefined(typeof(AutoMapFromAttribute)) ||
                           typeInfo.IsDefined(typeof(AutoMapToAttribute));
                }
            );

            _logger.DebugFormat("Found {0} classes define auto mapping attributes", types.Length);

            foreach (var type in types)
            {
                _logger.Debug(type.FullName);
                configuration.CreateAutoAttributeMaps(type);
            }
        }
    }
}