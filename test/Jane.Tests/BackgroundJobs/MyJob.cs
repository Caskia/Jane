using Jane.BackgroundJobs;
using Jane.Dependency;
using System.Collections.Generic;

namespace Jane.Tests.BackgroundJobs
{
    public class MyJob : BackgroundJob<MyJobArgs>, ISingletonDependency
    {
        public List<string> ExecutedValues { get; } = new List<string>();

        public override void Execute(MyJobArgs args)
        {
            ExecutedValues.Add(args.Value);
        }
    }
}