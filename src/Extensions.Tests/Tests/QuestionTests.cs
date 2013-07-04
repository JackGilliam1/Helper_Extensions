using System.Collections.Generic;
using Extensions.Core.Collections;
using Extensions.Core.Generics;
using Extensions.Core.Types;
using Extensions.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Tests
{
    [TestClass]
    public class QuestionTests
    {
        [TestMethod]
        public void IsLastIn_Returns_Correct_Results()
        {
            object testObj1 = new object();
            object testObj2 = new object();
            var list = new List<object>() 
            {
                testObj1,
                testObj2
            };

            //Act
            bool result1 = CollectionExtension.HasLastOf(list, testObj2);
            bool result2 = CollectionExtension.HasLastOf(list, testObj1);

            //Assert
            Assert.IsTrue(result1);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void Existing_Method_Found_For_Types()
        {
            //Arrange
            TestObject<string> testObj = new TestObject<string>();
            bool result = false;

            //Act
            result = TypeExtensions.HasMethod(testObj.GetType(), "Add");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Existing_Method_Found_For_Objects()
        {
            //Arrange
            TestObject<string> testObj = new TestObject<string>();
            bool result = false;

            //Act
            result = TypeExtensions.HasMethod(testObj, "Add");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Collections_Are_Determined_To_Contain_Items()
        {
            //Arrange
            List<int> testObjects = new List<int>()
            {
                4,
                8
            };
            bool result = false;

            //Act
            result = testObjects.HasItems();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Collections_Are_Determined_To_Contain_No_Items()
        {
            //Arrange
            List<int> testObjects = new List<int>();

            bool result = false;

            //Act
            result = testObjects.HasItems();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsGreaterThan_Correct_For_Strings()
        {
            //Arrange
            string first = "c";
            string second = "a";

            //Act
            bool isGreater = GenericExtensions.IsLargerThan(first, second);

            //Assert
            Assert.IsTrue(isGreater);
        }

        [TestMethod]
        public void IsGreaterThan_Correct_For_Primitive_Number_Types()
        {
            //Arrange
            int firstInteger = 3;
            int secondInteger = 1;
            double firstDouble = 3.1;
            double secondDouble = 3.0;
            float firstFloat = 3.1f;
            float secondFloat = 3.0f;

            //Act
            bool firstIntegerIsGreater = GenericExtensions.IsLargerThan(firstInteger, secondInteger);
            bool firstDoubleIsGreater = GenericExtensions.IsLargerThan(firstDouble, secondDouble);
            bool firstFloatIsGreater = GenericExtensions.IsLargerThan(firstFloat, secondFloat);

            //Assert
            Assert.IsTrue(firstIntegerIsGreater);
            Assert.IsTrue(firstDoubleIsGreater);
            Assert.IsTrue(firstFloatIsGreater);
        }


        [TestMethod]
        public void IsLessThan_Correct_For_Strings()
        {
            //Arrange
            string first = "a";
            string second = "c";

            //Act
            bool isLesser = GenericExtensions.IsSmallerThan(first, second);

            //Assert
            Assert.IsTrue(isLesser);
        }

        [TestMethod]
        public void IsLessThan_Correct_For_Primitive_Number_Types()
        {
            //Arrange
            int firstInteger = 1;
            int secondInteger = 3;
            double firstDouble = 3.0;
            double secondDouble = 3.1;
            float firstFloat = 3.0f;
            float secondFloat = 3.1f;

            //Act
            bool firstIntegerIsLesser = GenericExtensions.IsSmallerThan(firstInteger, secondInteger);
            bool firstDoubleIsLesser = GenericExtensions.IsSmallerThan(firstDouble, secondDouble);
            bool firstFloatIsLesser = GenericExtensions.IsSmallerThan(firstFloat, secondFloat);

            //Assert
            Assert.IsTrue(firstIntegerIsLesser);
            Assert.IsTrue(firstDoubleIsLesser);
            Assert.IsTrue(firstFloatIsLesser);
        }


        [TestMethod]
        public void Two_Collections_Equal()
        {
            //Arrange
            List<List<int>> listOne = new List<List<int>>()
            {
                new List<int>() {4, 6},
                new List<int>() {6, 9}
            };
            List<List<int>> listTwo = new List<List<int>>()
            {
                new List<int>() {4, 6},
                new List<int>() {6, 9}
            };

            //Act
            bool areEqual = GenericExtensions.SameAs(listOne, listTwo);

            //Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Two_Collections_Not_Equal()
        {
            //Arrange
            List<List<int>> listOne = new List<List<int>>()
            {
                new List<int>() {4, 7},
                new List<int>() {6, 9}
            };
            List<List<int>> listTwo = new List<List<int>>()
            {
                new List<int>() {4, 6},
                new List<int>() {6, 9}
            };

            //Act
            bool areEqual = GenericExtensions.SameAs(listOne, listTwo);

            //Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Two_Integers_Equal()
        {
            //Arrange
            int first = 1;
            int second = 1;

            //Act
            bool areEqual = GenericExtensions.SameAs(first, second);

            //Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Two_Integers_Not_Equal()
        {
            //Arrange
            int first = 1;
            int second = 3;

            //Act
            bool areEqual = GenericExtensions.SameAs(first, second);

            //Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Two_Doubles_Equal()
        {
            //Arrange
            double first = 1.0;
            double second = 1.0;

            //Act
            bool areEqual = GenericExtensions.SameAs(first, second);

            //Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Two_Doubles_Not_Equal()
        {
            //Arrange
            double first = 1;
            double second = 3;

            //Act
            bool areEqual = GenericExtensions.SameAs(first, second);

            //Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Two_Floats_Equal()
        {
            //Arrange
            float first = 1.0f;
            float second = 1.0f;

            //Act
            bool areEqual = GenericExtensions.SameAs(first, second);

            //Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Two_Floats_Not_Equal()
        {
            //Arrange
            float first = 1f;
            float second = 3f;

            //Act
            bool areEqual = GenericExtensions.SameAs(first, second);

            //Assert
            Assert.IsFalse(areEqual);
        }
    }
}
