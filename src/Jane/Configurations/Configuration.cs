using Jane.Aspects;
using Jane.Authorization;
using Jane.BackgroundJobs;
using Jane.BackgroundWorkers;
using Jane.Dependency;
using Jane.Events.Bus;
using Jane.Events.Bus.Factories;
using Jane.Events.Bus.Handlers;
using Jane.Extensions;
using Jane.Json;
using Jane.Json.Microsoft;
using Jane.Limits;
using Jane.Logging;
using Jane.MessageBus;
using Jane.PushNotifications;
using Jane.Reflection;
using Jane.Runtime;
using Jane.Runtime.Caching;
using Jane.Runtime.Caching.Configuration;
using Jane.Runtime.Caching.Memory;
using Jane.Runtime.Guids;
using Jane.Runtime.Remoting;
using Jane.Runtime.Session;
using Jane.Runtime.Validation.Interception;
using Jane.Scheduling;
using Jane.Threading;
using Jane.Threading.Timers;
using Jane.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            if (Instance == null)
            {
                Instance = new Configuration();
            }
            return Instance;
        }

        public Configuration BuildContainer(IServiceCollection services = null)
        {
            ObjectContainer.Populate(services);
            ObjectContainer.Build();
            return this;
        }

        public Configuration RegisterAssemblies(params Assembly[] assemblies)
        {
            RegisterInterceptors();

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

        public Configuration RegisterBackgroundJob(params Assembly[] assemblies)
        {
            var options = ObjectContainer.Resolve<BackgroundJobOptions>();
            foreach (var assembly in assemblies)
            {
                //Register Backgroundjob
                foreach (var type in assembly.GetTypes().Where(t => ReflectionHelper.IsAssignableToGenericType(t, typeof(IBackgroundJob<>))))
                {
                    options.AddJob(type);
                }
            }

            var backgroundJobManager = ObjectContainer.Resolve<IBackgroundWorkerManager>();
            backgroundJobManager.Add(ObjectContainer.Resolve<IBackgroundJobWorker>());
            backgroundJobManager.Start();

            return this;
        }

        public Configuration RegisterCommonComponents()
        {
            SetDefault<ILoggerFactory, EmptyLoggerFactory>();
            SetDefault<IJsonSerializer, MicrosoftJsonSerializer>();
            SetDefault<SequentialGuidGeneratorOptions, SequentialGuidGeneratorOptions>();
            SetDefault<SequentialGuidGenerator, SequentialGuidGenerator>();
            SetDefault<IIdGenerator, IdGenerator>();
            SetDefault<IDataGenerator, DataGenerator>();
            SetDefault<IIncrementDataGenerator, CacheIncrementDataGenerator>();
            SetDefault<IMachineManager, MachineManager>();
            SetDefault<IMessageBus, NullMessageBus>();
            SetDefault<IEventBus, EventBus>();
            SetDefault<IPushNotificationService, NullPushNotificationService>();
            SetDefault<IAmbientDataContext, AsyncLocalAmbientDataContext>();
            SetDefaultType(typeof(IAmbientScopeProvider<>), typeof(DataContextAmbientScopeProvider<>), null, DependencyLifeStyle.Transient);
            SetDefault<IPrincipalAccessor, DefaultPrincipalAccessor>();
            SetDefault<IJaneSession, ClaimsJaneSession>();
            SetDefault<JaneTimer, JaneTimer>(null, DependencyLifeStyle.Transient);
            SetDefault<ICachingConfiguration, CachingConfiguration>();
            SetDefault<JaneMemoryCache, JaneMemoryCache>(null, DependencyLifeStyle.Transient);
            SetDefault<ICacheManager, JaneMemoryCacheManager>();
            SetDefault<JaneMemoryCacheManager, JaneMemoryCacheManager>();
            SetDefault<IRateLimiter, MemoryRateLimiter>();
            SetDefault<IValidationConfiguration, ValidationConfiguration>();
            SetDefault<MethodInvocationValidator, MethodInvocationValidator>(null, DependencyLifeStyle.Transient);
            SetDefault<IAuthorizationConfiguration, AuthorizationConfiguration>();
            SetDefault<IAuthorizationHelper, AuthorizationHelper>(null, DependencyLifeStyle.Transient);
            SetDefault<IScheduleService, ScheduleService>();
            SetDefault<IBackgroundWorkerManager, BackgroundWorkerManager>();
            SetDefault<BackgroundJobOptions, BackgroundJobOptions>();
            SetDefault<BackgroundJobWorkerOptions, BackgroundJobWorkerOptions>();
            SetDefault<IBackgroundJobWorker, BackgroundJobWorker>();
            SetDefault<IBackgroundJobExecuter, BackgroundJobExecuter>(null, DependencyLifeStyle.Transient);
            SetDefault<IBackgroundJobSerializer, JsonBackgroundJobSerializer>(null, DependencyLifeStyle.Transient);
            SetDefault<IBackgroundJobStore, InMemoryBackgroundJobStore>();
            SetDefault<IBackgroundJobManager, DefaultBackgroundJobManager>();
            return this;
        }

        public Configuration RegisterEventHandler(params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                //Register Event Bus
                foreach (var type in assembly.GetTypes().Where(TypeUtils.IsEventHandler))
                {
                    RegisterEventHandlerType(type);
                }
            }

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

        public Configuration SetDefaultType(Type serviceType, Type implementationType, string serviceName = null, DependencyLifeStyle life = DependencyLifeStyle.Singleton)
        {
            ObjectContainer.RegisterType(serviceType, implementationType, serviceName, life);
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
            var basePath = Directory.GetCurrentDirectory();

            //default setting
            var builder = new ConfigurationBuilder()
                 .SetBasePath(basePath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var environmentName = JaneEnvironment.GetEnvironment();
            if (!environmentName.IsNullOrWhiteSpace())
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
            }

            //external setting for docker
            var configDirName = "docker-config";
            var dirPath = $"{basePath}{Path.DirectorySeparatorChar}{configDirName}";
            if (Directory.Exists(dirPath))
            {
                var skipDirectory = dirPath.Length;
                if (!dirPath.EndsWith("" + Path.DirectorySeparatorChar)) skipDirectory++;
                var fileNames = Directory.EnumerateFiles(dirPath, "*.json", SearchOption.AllDirectories)
                    .Select(f => f.Substring(skipDirectory));
                foreach (var fileName in fileNames)
                {
                    builder = builder.AddJsonFile($"{configDirName}{Path.DirectorySeparatorChar}{fileName}", optional: true, reloadOnChange: true);
                }
            }

            //enviroment variables
            builder.AddEnvironmentVariables();
            Root = builder.Build();
        }

        private void RegisterComponentType(Type type)
        {
            var life = ParseComponentLife(type);
            ComponentRegistrar.RegisterType(type, null, life);
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

                ComponentRegistrar.RegisterType(interfaceType, type, null, life);
            }
        }

        private void RegisterEventHandlerType(Type type)
        {
            var interfaces = type.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
                {
                    continue;
                }

                var genericArgs = @interface.GetGenericArguments();
                if (genericArgs.Length == 1)
                {
                    ObjectContainer.Resolve<IEventBus>().Register(genericArgs[0], new IocHandlerFactory(ObjectContainer.Current, type));
                }
            }
        }

        private void RegisterInterceptors()
        {
            ValidationInterceptorRegistrar.Initialize(ObjectContainer.Current);
        }

        #endregion Private Methods
    }
}