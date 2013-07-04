using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.Core.Searching;
using Extensions.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Tests
{

    [TestClass]
    public class SearchExtTests
    {
        [TestMethod]
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
            Assert.IsTrue(outputs.Contains(testObject1));
            Assert.IsTrue(outputs.Contains(testObject2));
            Assert.IsFalse(outputs.Contains(testObject3));
            Assert.IsTrue(outputs2.Contains(testObject1));
            Assert.IsTrue(outputs2.Contains(testObject3));
            Assert.IsFalse(outputs2.Contains(testObject2));
        }

        [TestMethod]
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
            Assert.IsTrue(outputs.Contains(testObject1));
            Assert.IsTrue(outputs.Contains(testObject2));
            Assert.IsFalse(outputs.Contains(testObject3));
            Assert.IsTrue(outputs2.Contains(testObject1));
            Assert.IsTrue(outputs2.Contains(testObject3));
            Assert.IsFalse(outputs2.Contains(testObject2));
        }
    }
}
