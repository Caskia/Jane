using Hangfire;
using Hangfire.Server;
using System;
using System.Collections.Generic;

namespace Jane.Configurations
{
    public class HangfireConfiguration : IHangfireConfiguration
    {
        private Func<IServiceProvider, BackgroundJobServer> _backgroundJobServerFactory;

        public HangfireConfiguration()
        {
            _backgroundJobServerFactory = CreateJobServer;
        }

        public IEnumerable<IBackgroundProcess> AdditionalProcesses { get; set; }

        public Func<IServiceProvider, BackgroundJobServer> BackgroundJobServerFactory
        {
            get => _backgroundJobServerFactory;
            set => _backgroundJobServerFactory = value;
        }

        public BackgroundJobServerOptions ServerOptions { get; set; }

        public JobStorage Storage { get; set; }

        private BackgroundJobServer CreateJobServer(IServiceProvider serviceProvider)
        {
            return new BackgroundJobServer(
                ServerOptions,
                Storage,
                AdditionalProcesses
            );
        }
    }
}