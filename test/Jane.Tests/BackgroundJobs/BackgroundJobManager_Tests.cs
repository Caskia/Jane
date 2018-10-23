using System;
using System.Threading.Tasks;
using Jane.BackgroundJobs;
using Jane.Dependency;
using Jane.Threading;
using Shouldly;
using Xunit;

namespace Jane.Tests.BackgroundJobs
{
    public class BackgroundJobManager_Tests : BackgroundJobsTestBase
    {
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IBackgroundJobStore _backgroundJobStore;

        public BackgroundJobManager_Tests()
        {
            _backgroundJobManager = ObjectContainer.Resolve<IBackgroundJobManager>();
            _backgroundJobStore = ObjectContainer.Resolve<IBackgroundJobStore>();
        }

        [Fact]
        public async Task Should_Enqueue_And_Execute_Jobs()
        {
            //Arrange
            var jobObject = ObjectContainer.Resolve<MyJob>();
            jobObject.ExecutedValues.ShouldBeEmpty();
            var arg = new MyJobArgs("42");

            //Act
            await _backgroundJobManager.EnqueueAsync(arg);

            //Assert
            var values = await TimerTaskFactory.StartNew(
                    () => jobObject.ExecutedValues,
                    v => v.Count > 0,
                    TimeSpan.FromMilliseconds(750),
                    TimeSpan.FromSeconds(10)
                );

            values.ShouldContain(arg.Value);
        }

        [Fact]
        public async Task Should_Store_Jobs()
        {
            var jobIdAsString = await _backgroundJobManager.EnqueueAsync(new MyJobArgs("42"));
            jobIdAsString.ShouldNotBe(default(string));
            (await _backgroundJobStore.FindAsync(Guid.Parse(jobIdAsString))).ShouldNotBeNull();
        }
    }
}