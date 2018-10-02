using Jane.Dependency;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jane.Tests.Environment
{
    public class IdGenerator_Tests : TestBase
    {
        private readonly IIdGenerator _idGenerator;

        public IdGenerator_Tests()
        {
            _idGenerator = ObjectContainer.Resolve<IIdGenerator>();
        }

        [Fact(DisplayName = "Should_Create_Guid")]
        public void Should_Create_Guid()
        {
            //Act
            var guid = _idGenerator.Guid();

            //Assert
            guid.ShouldNotBeNull();
        }
    }
}