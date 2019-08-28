using ENode.Commanding;
using Jane.Logging;
using JaneENodeGenericHostExample.Services;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JaneENodeGenericHostExample
{
    internal class HostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ICommandService _commandService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        private readonly ITestService _testService;

        // ReSharper disable once UnusedMember.Global
        public HostedService(
            IHostApplicationLifetime appLifetime,
            ICommandService commandService,
            IHttpClientFactory httpClientFactory,
            ILoggerFactory loggerFactory,
            ITestService testService
            )
        {
            _appLifetime = appLifetime;
            _commandService = commandService;
            _httpClientFactory = httpClientFactory;
            _logger = loggerFactory.Create(typeof(HostedService).Name);
            _testService = testService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            _logger.Info($"Starting hosted service, random[{_testService.GetRandomString()}]");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Info("Stopping hosted service");
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.Info("OnStarted has been called.");

            // Perform post-startup activities here
        }

        private void OnStopped()
        {
            _logger.Info("OnStopped has been called.");

            // Perform post-stopped activities here
        }

        private void OnStopping()
        {
            _logger.Info("OnStopping has been called.");

            // Perform on-stopping activities here
        }
    }
}