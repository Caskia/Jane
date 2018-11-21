using Hangfire;
using Hangfire.Server;
using Jane.Dependency;
using System.Collections.Generic;

namespace Jane.Configurations
{
    public interface IHangfireConfiguration : ISingletonDependency
    {
        IEnumerable<IBackgroundProcess> AdditionalProcesses { get; set; }

        BackgroundJobServerOptions ServerOptions { get; set; }

        JobStorage Storage { get; set; }
    }
}