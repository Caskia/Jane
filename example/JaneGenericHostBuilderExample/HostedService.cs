﻿using Jane.Logging;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JaneGenericHostBuilderExample
{
    internal class HostedService : IHostedService
    {
        private readonly IApplicationLifetime _appLifetime;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        // ReSharper disable once UnusedMember.Global
        public HostedService(
            IApplicationLifetime appLifetime,
            IHttpClientFactory httpClientFactory,
            ILoggerFactory loggerFactory
            )
        {
            _appLifetime = appLifetime;
            _httpClientFactory = httpClientFactory;
            _logger = loggerFactory.Create(typeof(HostedService).Name);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            _logger.Info("Starting hosted service");
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