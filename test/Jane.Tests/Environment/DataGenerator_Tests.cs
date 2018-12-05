using Jane.Dependency;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jane.Tests.Environment
{
    public class DataGenerator_Tests : TestBase
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IIncrementDataGenerator _incrementDataGenerator;

        public DataGenerator_Tests()
        {
            _idGenerator = ObjectContainer.Resolve<IIdGenerator>();
            _incrementDataGenerator = ObjectContainer.Resolve<IIncrementDataGenerator>();
        }

        [Fact(DisplayName = "Should_Create_Guid")]
        public void Should_Create_Guid()
        {
            //Act
            var guid = _idGenerator.Guid();

            //Assert
            guid.ShouldNotBeNull();
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
    }
}