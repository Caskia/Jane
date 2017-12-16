using Jane.Dependency;
using Jane.Extensions;
using Jane.Infrastructure;
using Jane.Logging;
using Jane.MessageBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Jane.Configurations
{
    public class Configuration
    {
        private Configuration()
        {
            BuildConfiguration();
        }

        /// <summary>Provides the singleton access instance.
        /// </summary>
        public static Configuration Instance { get; private set; }

        public IConfigurationRoot Root { get; set; }

        public static Configuration Create()
        {
            Instance = new Configuration();
            return Instance;
        }

        public Configuration BuildContainer()
        {
            ObjectContainer.Build();
            return this;
        }

        public Configuration RegisterAssemblies(params Assembly[] assemblies)
        {
            var registeredTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                //Singleton
                foreach (var type in assembly.GetTypes().Where(TypeUtils.IsSingleton))
                {
                    RegisterComponentType(type);
                    registeredTypes.Add(type);
                }

                //Transient
                foreach (var type in assembly.GetTypes().Where(TypeUtils.IsTransient))
                {
                    if (!registeredTypes.Contains(type))
                    {
                        RegisterComponentType(type);
                    }
                }
            }
            return this;
        }

        public Configuration RegisterCommonComponents()
        {
            SetDefault<ILoggerFactory, EmptyLoggerFactory>();
            SetDefault<IIdGenerator, IdGenerator>();
            SetDefault<IMachineManager, MachineManager>();
            SetDefault<IMessageBus, NullMessageBus>();
            return this;
        }

        public Configuration RegisterUnhandledExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var logger = ObjectContainer.Resolve<ILoggerFactory>().Create(GetType().FullName);
                logger.ErrorFormat("Unhandled exception: {0}", e.ExceptionObject);
            };
            return this;
        }

        public Configuration SetDefault<TService, TImplementer>(string serviceName = null, DependencyLifeStyle life = DependencyLifeStyle.Singleton)
                                    where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.Register<TService, TImplementer>(serviceName, life);
            return this;
        }

        public Configuration SetDefault<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.RegisterInstance<TService, TImplementer>(instance, serviceName);
            return this;
        }

        #region Private Methods

        private static DependencyLifeStyle ParseComponentLife(Type type)
        {
            if (TypeUtils.IsSingleton(type))
            {
                return DependencyLifeStyle.Singleton;
            }
            else if (TypeUtils.IsTransient(type))
            {
                return DependencyLifeStyle.Transient;
            }
            else
            {
                throw new Exception($"Can not find dependency type[{nameof(type)}]");
            }
        }

        private void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var environmentName = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!environmentName.IsNullOrWhiteSpace())
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName.ToLower()}.json", optional: true, reloadOnChange: true);
            }

            Root = builder.Build();
        }

        private void RegisterComponentType(Type type)
        {
            var life = ParseComponentLife(type);
            ObjectContainer.RegisterType(type, null, life);
            foreach (var interfaceType in type.GetInterfaces())
            {
                if (interfaceType == typeof(ISingletonDependency))
                {
                    continue;
                }

                if (interfaceType == typeof(ITransientDependency))
                {
                    continue;
                }

                ObjectContainer.RegisterType(interfaceType, type, null, life);
            }
        }

        #endregion Private Methods
    }
}