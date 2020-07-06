using Jane.AWS.S3;
using Jane.Dependency;
using Jane.Scheduling;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Jane.Tests.Scheduling
{
    public class Schedule_Tests : TestBase
    {
        private readonly IScheduleService _scheduleService;

        public Schedule_Tests()
        {
            _scheduleService = ObjectContainer.Resolve<IScheduleService>();
        }

        [Fact(DisplayName = "Should_Run_Schedule")]
        public async Task Should_Run_Schedule()
        {
            var test = new Test();
            await test.DoAsync();

            //Arrange
            var count = 0;
            var taskName = "TestCount";

            //Act
            _scheduleService.StartTask(taskName, () => { count++; }, 1000, 800);
            await Task.Delay(2000);
            _scheduleService.StopTask(taskName);

            //Assert
            count.ShouldBe(2);
        }

        [Fact(DisplayName = "Should_Run_Worker")]
        public async Task Should_Run_Worker()
        {
            //Arrange
            var count = 0;
            var worker = new Worker("TestCount", () => { count++; });

            //Act
            worker.Start();
            await Task.Delay(1);
            worker.Stop();

            //Assert
            count.ShouldBeGreaterThan(0);
        }
    }
}