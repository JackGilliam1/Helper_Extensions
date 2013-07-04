using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.Core.Collections;
using Extensions.Core.Generics;
using Extensions.Core.Types;
using Extensions.Tests.TestObjects;
using Xunit;


namespace Extensions.Tests
{
    public class FunctionTests : TestsBase
    {
        private TestObject<string> testObject1 = new TestObject<string>()
        {
            Id = 1,
            Name = "Object1",
            RandomValue = 22,
            SomeString = "String2",
        };

        [Fact]
        public void Properties_Are_Found()
        {
            //Arrange

            //Act
            object output = TypeExtensions.Prop(testObject1, "RandomValue");

            //Assert
            AssertIsNotNull(output);
        }

        [Fact]
        public void Methods_Are_Found()
        {
            //Act
            object output = TypeExtensions.Meth(testObject1, "Add");

            //Assert
            AssertIsNotNull(output);
        }

        [Fact]
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
            AssertIsNotNull(stringResult);
            AssertIsNotNull(doubleResult);
            AssertEqual(stringInput.ToString(), stringResult.ToString());
            AssertEqual(doubleInput.ToString(), doubleResult.ToString());
            AssertEqual(intInput.ToString(), intResult.ToString());
        }

        [Fact]
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
            AssertEqual(2, results.Count());
            foreach (object res in results)
            {
                var result = res as TestObject<string>;
                AssertEqual(doubleInputValue, result.RandomValue);
            }
        }

        [Fact]
        public void Between_Two_Objects_NonNull_Object_Results()
        {
            //Arrange
            object testObject1 = new object();
            object testObject2 = null;

            //Act
            var result = GenericExtensions.NonNullOf(testObject1, testObject2);

            //Assert
            AssertIsNotNull(result);
            AssertEqual(testObject1, result);
        }

        [Fact]
        public void Between_Two_NonNull_Objects_First_Object_Results()
        {
            //Arrange
            object testObject1 = new object();
            object testObject2 = new object();

            //Act
            var result = GenericExtensions.NonNullOf(testObject1, testObject2);

            //Assert
            AssertIsNotNull(result);
            AssertEqual(testObject1, result);
        }

        [Fact]
        public void Between_Two_Null_Objects_Null_Object_Results()
        {
            //Arrange
            object testObject1 =  null;
            object testObject2 = null;

            //Act
            object result = GenericExtensions.NonNullOf(testObject1, testObject2);

            //Assert
            AssertIsNull(result);
        }

        [Fact]
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
            AssertIsNotNull(results);
            AssertTrue(results.Count == 3);
            AssertEqual(dataOne.ToString(), results[0].ToString());
            AssertEqual(dataTwo.ToString(), results[1].ToString());
            AssertEqual(dataThree.ToString(), results[2].ToString());
        }
    }
}
