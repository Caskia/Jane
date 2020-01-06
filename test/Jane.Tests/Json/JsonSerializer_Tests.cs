using Jane.Json.Microsoft;
using Jane.Json.Newtonsoft;
using Shouldly;
using System;
using Xunit;

namespace Jane.Tests.Json
{
    public class JsonSerializer_Tests : TestBase
    {
        public enum Gender
        {
            Male = 1,
            Female
        }

        [Fact]
        public void Should_Microsoft_Json_Serialize()
        {
            var serializer = new MicrosoftJsonSerializer();

            var person = new Person { Name = "Caskia", Age = 23, Birthday = DateTime.Now.AddYears(-30), TotalAmount = 12213312213L, Gender = Gender.Male, IP = "192.168.31.14", MyCIA = "我的CIA" };
            var json = serializer.Serialize(person);
            var obj = serializer.Deserialize<Person>(json);

            obj.Name.ShouldBe(person.Name);
        }

        [Fact]
        public void Should_Newtonsoft_Json_Serialize()
        {
            var serializer = new NewtonsoftJsonSerializer();

            var person = new Person { Name = "Caskia", Age = 23, Birthday = DateTime.Now.AddYears(-30), TotalAmount = 12213312213L, Gender = Gender.Male, IP = "192.168.31.14", MyCIA = "我的CIA" };
            var json = serializer.Serialize(person);
            var obj = serializer.Deserialize<Person>(json);

            obj.Name.ShouldBe(person.Name);
        }

        public class Person
        {
            public int Age { get; set; }

            public DateTime? Birthday { get; set; }

            public Gender Gender { get; set; }

            public string IP { get; set; }

            public string MyCIA { get; set; }

            public string Name { get; set; }

            public long? TotalAmount { get; set; }
        }
    }
}