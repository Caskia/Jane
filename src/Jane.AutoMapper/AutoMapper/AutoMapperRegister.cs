using AutoMapper;
using Jane.Configurations;
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
        private readonly ITypeFinder _typeFinder;
        private readonly ILogger Logger;

        public AutoMapperRegister(
            IAutoMapperConfiguration autoMapperConfiguration,
            ILoggerFactory loggerFactory,
            ITypeFinder typeFinder
            )
        {
            _autoMapperConfiguration = autoMapperConfiguration;
            Logger = loggerFactory.Create(typeof(AutoMapperRegister));
            _typeFinder = typeFinder;
        }

        public void CreateMappings()
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

                IMapper mapper;
                if (_autoMapperConfiguration.UseStaticMapper)
                {
                    //We should prevent duplicate mapping in an application, since Mapper is static.
                    if (!_createdMappingsBefore)
                    {
                        Mapper.Initialize(configurer);
                        _createdMappingsBefore = true;
                    }

                    mapper = Mapper.Instance;
                }
                else
                {
                    var config = new MapperConfiguration(configurer);
                    mapper = config.CreateMapper();
                }

                Configuration.Instance.SetDefault<IMapper, IMapper>();
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

            Logger.DebugFormat("Found {0} classes define auto mapping attributes", types.Length);

            foreach (var type in types)
            {
                Logger.Debug(type.FullName);
                configuration.CreateAutoAttributeMaps(type);
            }
        }
    }
}