using Jane.Dependency;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Jane.Tests.Environment
{
    public class DataGenerator_Tests : TestBase
    {
        private readonly IDataGenerator _dataGenerator;
        private readonly IIdGenerator _idGenerator;
        private readonly IIncrementDataGenerator _incrementDataGenerator;

        public DataGenerator_Tests()
        {
            _dataGenerator = ObjectContainer.Resolve<IDataGenerator>();
            _idGenerator = ObjectContainer.Resolve<IIdGenerator>();
            _incrementDataGenerator = ObjectContainer.Resolve<IIncrementDataGenerator>();
        }

        [Fact(DisplayName = "Should_Create_Guid")]
        public void Should_Create_Guid()
        {
            //Act
            var guid = _idGenerator.Guid();

            //Assert
            guid.ToString().ShouldNotBeNull();
        }

        [Fact(DisplayName = "Should_Create_Increment")]
        public async Task Should_Create_Increment()
        {
            //Arrange
            var key = "test";

            //Act
            var t1 = await _incrementDataGenerator.IncrementAsync(key);
            var t2 = await _incrementDataGenerator.IncrementAsync(key);
            var t3 = await _incrementDataGenerator.IncrementAsync(key);
            var t4 = await _incrementDataGenerator.IncrementAsync(key);

            //Assert
            t4.ShouldBe(4);
        }

        [Fact(DisplayName = "Should_Create_Random_String")]
        public void Should_Create_Random_String()
        {
            _ = _dataGenerator.GetRandomString(10, true, false, false);
        }

        [Fact(DisplayName = "Should_Number_To_S36")]
        public void Should_Number_To_S36()
        {
            //Arrange
            var number = _idGenerator.NextId();

            //Act
            var s36 = _dataGenerator.NumberToS36(number);

            //Assert
            var convertedNumber = _dataGenerator.S36ToNumber(s36);
            number.ShouldBe(convertedNumber);
        }

        [Fact(DisplayName = "Should_Number_To_S64")]
        public void Should_Number_To_S64()
        {
            //Arrange
            var number = _idGenerator.NextId();

            //Act
            var s64 = _dataGenerator.NumberToS64(number);

            //Assert
            var convertedNumber = _dataGenerator.S64ToNumber(s64);
            number.ShouldBe(convertedNumber);
        }
    }
}