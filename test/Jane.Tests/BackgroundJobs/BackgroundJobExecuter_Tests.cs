using Jane.BackgroundJobs;
using Jane.Dependency;
using Shouldly;
using Xunit;

namespace Jane.Tests.BackgroundJobs
{
    public class BackgroundJobExecuter_Tests : BackgroundJobsTestBase
    {
        private readonly IBackgroundJobExecuter _backgroundJobExecuter;

        public BackgroundJobExecuter_Tests()
        {
            _backgroundJobExecuter = ObjectContainer.Resolve<IBackgroundJobExecuter>();
        }

        [Fact]
        public void Should_Execute_Tasks()
        {
            //Arrange

            var jobObject = ObjectContainer.Resolve<MyJob>();
            jobObject.ExecutedValues.ShouldBeEmpty();

            //Act

            _backgroundJobExecuter.Execute(
                new JobExecutionContext(
                    typeof(MyJob),
                    new MyJobArgs("42")
                )
            );

            //Assert

            jobObject.ExecutedValues.ShouldContain("42");
        }
    }
}