using System;
using Extensions.Core.Generics;
using Extensions.Core.TextFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Extensions.Tests
{
    [TestClass]
    public class TextExtTests
    {
        [TestMethod]
        public void Can_Convert_String_To_Method()
        {
            //Arrange
            string methodBody = @"
                    int result = 0;
                    if(listOfNumbers == null) 
                    {
                        result = numberOne * numberTwo;
                    }
                    else
                    {
                        result = listOfNumbers[0];
                    }
                    return result;
                    ";
            string methodParameters = "int numberOne, int numberTwo, System.Collections.Generic.List<int> listOfNumbers";
            string methodReturnType = "int";
            int firstParam = 2;
            int secondParam = 3;
            int result = 0;

            //Act
            try
            {
                MethodInfo method = methodBody.ToFunction(methodReturnType, methodParameters);
                result = Int32.Parse(method.Invoke(null, new object[] { firstParam, secondParam, null }).ToString());
            }
            catch
            {
                bool wentInHere = true;
                Assert.IsFalse(wentInHere);
            }

            //Assert
            Assert.AreNotEqual(0, result);
        }

        [TestMethod]
        public void WhiteSpace_Is_Removed()
        {
            //Arrange
            string input = "   g g   g  g       g g   g   g     g    ";
            string expected = "ggggggggg";

            //Act
            string output = input.RemoveWhiteSpace();

            //Assert
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void Empty_Integer()
        {
            //Arrange
            int input = 0;

            //Act
            bool output = GenericExtensions.IsEmpty(input, true);

            //Assert
            Assert.IsTrue(output);
        }

        [TestMethod]
        public void Empty_Double()
        {
            //Arrange
            double input = 0.0;

            //Act
            bool output = GenericExtensions.IsEmpty(input, true);

            //Assert
            Assert.IsTrue(output);
        }

        [TestMethod]
        public void Blank_String()
        {
            //Arrange
            string input = "";

            //Act
            bool output = GenericExtensions.IsEmpty(input);

            //Assert
            Assert.IsTrue(output);
        }

        [TestMethod]
        public void Null_Object()
        {
            //Arrange
            object input = null;

            //Act
            bool output = GenericExtensions.IsEmpty(input);

            //Assert
            Assert.IsTrue(output);
        }

        [TestMethod]
        public void Non_Null_Object()
        {
            //Arrange
            object input = new Object();

            //Act
            bool output = GenericExtensions.IsEmpty(input);

            //Assert
            Assert.IsFalse(output);
        }

        [TestMethod]
        public void Non_Empty_String()
        {
            //Arrange
            string input = "h";

            //Act
            bool output = GenericExtensions.IsEmpty(input);

            //Assert
            Assert.IsFalse(output);
        }

        [TestMethod]
        public void Non_Empty_Number_String()
        {
            //Arrange
            int input = 2;

            //Act
            bool output = GenericExtensions.IsEmpty(input, true);

            //Assert
            Assert.IsFalse(output);
        }
    }
}
