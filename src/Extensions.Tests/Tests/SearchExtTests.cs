using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.Core.Searching;
using Extensions.Tests.TestObjects;
using Xunit;


namespace Extensions.Tests
{
    public class SearchExtTests : TestsBase
    {
        [Fact]
        public void SearchTest_Correct_Objects_Are_Found_Using_A_String_Property()
        {
            //Arrange
            #region TestObjects
            var testObject1 = new TestObject<string>()
            {
                Id = 1,
                Name = "Object1",
                RandomValue = 22,
                SomeString = "String2",
                Stuff = new List<string>()
                    {
                        "Hahaha",
                        "wwwww",
                        "babababa",
                        "gaaab"
                    }
            };
            var testObject2 = new TestObject<string>()
            {
                Id = 2,
                Name = "Object1",
                RandomValue = 22,
                SomeString = "String1",
                Stuff = new List<string>()
                    {
                        "Hahaha",
                        "Yayaya",
                        "hhhh",
                        "babababa",
                        "gaaab"
                    },
                dateTime = DateTime.Now
            };
            var testObject3 = new TestObject<string>()
            {
                Id = 1,
                Name = "Object2",
                RandomValue = 22.5,
                SomeString = "String2",
                Stuff = new List<string>()
                    {
                        "wwwww",
                    },
                dateTime = DateTime.Now
            };
            TestStorage<string> storage = new TestStorage<string>()
            {
                TestObjects = new List<TestObject<string>>()
                {
                    testObject1,
                    testObject2,
                    testObject3
                }.AsQueryable()
            };
            #endregion
            //Act
            var outputs = storage.TestObjects.ToList().StandardSearch<TestObject<string>>("Name", "Object1");
            var outputs2 = storage.TestObjects.ToList().StandardSearch<TestObject<string>>("SomeString", "String2");

            //Assert
            AssertTrue(outputs.Contains(testObject1));
            AssertTrue(outputs.Contains(testObject2));
            AssertFalse(outputs.Contains(testObject3));
            AssertTrue(outputs2.Contains(testObject1));
            AssertTrue(outputs2.Contains(testObject3));
            AssertFalse(outputs2.Contains(testObject2));
        }

        [Fact]
        public void SearchTest_Correct_Objects_Are_Found_Using_A_Number_Property()
        {
            //Arrange
            #region TestObjects
            var testObject1 = new TestObject<string>()
            {
                Id = 1,
                Name = "Object1",
                RandomValue = 22,
                SomeString = "String2",
                Stuff = new List<string>()
                    {
                        "Hahaha",
                        "wwwww",
                        "babababa",
                        "gaaab"
                    }
            };
            var testObject2 = new TestObject<string>()
            {
                Id = 2,
                Name = "Object1",
                RandomValue = 22,
                SomeString = "String1",
                Stuff = new List<string>()
                    {
                        "Hahaha",
                        "Yayaya",
                        "hhhh",
                        "babababa",
                        "gaaab"
                    },
                dateTime = DateTime.Now
            };
            var testObject3 = new TestObject<string>()
            {
                Id = 1,
                Name = "Object2",
                RandomValue = 22.5,
                SomeString = "String2",
                Stuff = new List<string>()
                    {
                        "wwwww",
                    },
                dateTime = DateTime.Now
            };
            TestStorage<string> storage = new TestStorage<string>()
            {
                TestObjects = new List<TestObject<string>>()
                {
                    testObject1,
                    testObject2,
                    testObject3
                }.AsQueryable()
            };
            #endregion

            //Act
            var outputs = storage.TestObjects.ToList().StandardSearch<TestObject<string>>("RandomValue", 22);
            var outputs2 = storage.TestObjects.ToList().StandardSearch<TestObject<string>>("Id", 1);

            //Assert
            AssertTrue(outputs.Contains(testObject1));
            AssertTrue(outputs.Contains(testObject2));
            AssertFalse(outputs.Contains(testObject3));
            AssertTrue(outputs2.Contains(testObject1));
            AssertTrue(outputs2.Contains(testObject3));
            AssertFalse(outputs2.Contains(testObject2));
        }
    }
}
