using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.Core.Collections;
using Extensions.Core.Generics;
using Extensions.Core.Types;
using Extensions.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Tests
{
    [TestClass]
    public class FunctionTests
    {
        private TestObject<string> testObject1 = new TestObject<string>()
        {
            Id = 1,
            Name = "Object1",
            RandomValue = 22,
            SomeString = "String2",
        };

        [TestMethod]
        public void Properties_Are_Found()
        {
            //Arrange

            //Act
            object output = TypeExtensions.Prop(testObject1, "RandomValue");

            //Assert
            Assert.IsNotNull(output);
        }

        [TestMethod]
        public void Methods_Are_Found()
        {
            //Act
            object output = TypeExtensions.Meth(testObject1, "Add");

            //Assert
            Assert.IsNotNull(output);
        }

        [TestMethod]
        public void Correct_Property_Values_Return_From_Objects()
        {
            //Arrange
            int intInput = 3;
            double doubleInput = 22.3;
            string stringInput = "String2";

            var testObject1 = new TestObject<string>()
            {
                Id = 1,
                Name = "Object1",
                RandomValue = doubleInput,
                SomeString = stringInput,
                AdditionNumber = intInput
            };

            //Act
            var doubleResult = TypeExtensions.Val(testObject1, "RandomValue");
            var stringResult = TypeExtensions.Val(testObject1, "SomeString");
            var intResult = TypeExtensions.Val(testObject1, "AdditionNumber");

            //Assert
            Assert.IsNotNull(stringResult);
            Assert.IsNotNull(doubleResult);
            Assert.AreEqual(stringInput.ToString(), stringResult.ToString());
            Assert.AreEqual(doubleInput.ToString(), doubleResult.ToString());
            Assert.AreEqual(intInput.ToString(), intResult.ToString());
        }

        [TestMethod]
        public void Correct_Items_Result_From_GetBy()
        {
            //Arrange
            #region TestObjects
            double doubleInputValue = 22;
            var testObject1 = new TestObject<string>()
            {
                Id = 1,
                Name = "Object1",
                RandomValue = doubleInputValue,
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
                RandomValue = doubleInputValue,
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
            #endregion
            TestStorage<string> storage = new TestStorage<string>()
            {
                TestObjects = new List<TestObject<string>>()
                {
                    testObject1,
                    testObject2,
                    testObject3
                }.AsQueryable()
            };

            //Act
            var results = CollectionExtension.GetBy(storage.TestObjects, "RandomValue", doubleInputValue);

            //Assert
            Assert.AreEqual(2, results.Count());
            foreach (object res in results)
            {
                var result = res as TestObject<string>;
                Assert.AreEqual(doubleInputValue, result.RandomValue);
            }
        }

        [TestMethod]
        public void Between_Two_Objects_NonNull_Object_Results()
        {
            //Arrange
            object testObject1 = new object();
            object testObject2 = null;

            //Act
            var result = GenericExtensions.NonNullOf(testObject1, testObject2);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testObject1, result);
        }

        [TestMethod]
        public void Between_Two_NonNull_Objects_First_Object_Results()
        {
            //Arrange
            object testObject1 = new object();
            object testObject2 = new object();

            //Act
            var result = GenericExtensions.NonNullOf(testObject1, testObject2);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testObject1, result);
        }

        [TestMethod]
        public void Between_Two_Null_Objects_Null_Object_Results()
        {
            //Arrange
            object testObject1 =  null;
            object testObject2 = null;

            //Act
            object result = GenericExtensions.NonNullOf(testObject1, testObject2);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Sequences_Turn_Into_Messages()
        {
            //Arrange
            int dataOne = 1;
            int dataTwo = 2;
            int dataThree = 3;
            var numbers = new List<int>()
            {
                dataOne,
                dataTwo,
                dataThree
            };

            //Act
            List<string> results = numbers.ToMessages().ToList();

            //Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count == 3);
            Assert.AreEqual(dataOne.ToString(), results[0].ToString());
            Assert.AreEqual(dataTwo.ToString(), results[1].ToString());
            Assert.AreEqual(dataThree.ToString(), results[2].ToString());
        }
    }
}
