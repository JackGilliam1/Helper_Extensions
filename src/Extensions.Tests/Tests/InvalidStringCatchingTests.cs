using System.Collections.Generic;
using Extensions.Core.TextFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Tests
{

    [TestClass]
    public class InvalidStringTests
    {
        private IEnumerable<string> ValidStrings = new List<string>() 
        { 
            "ValidAnswer1", 
            "ValidAnswer2"
        };

        private const string expected = "error";

        [TestMethod]
        public void Unspecified_String_Input_Caught()
        {
            //Arrange
            string input = "Yay";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<string>(input, choices:ValidStrings, choicesAreRequired: true);

            //Assert
            Assert.IsTrue(output.ToString().ToLower().Contains(expected));
        }

        [TestMethod]
        public void String_Input_Not_Matching_Regex_Caught()
        {
            //Arrange
            string input = "Haha";
            string pattern = "HaYa";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<string>(input, pattern: pattern);

            //Assert
            Assert.IsTrue(output.ToString().ToLower().Contains(expected));
        }

        [TestMethod]
        public void Empty_String_Input_Caught()
        {
            //Arrange
            string input = "";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<string>(input);

            //Assert
            Assert.IsTrue(output.ToString().ToLower().Contains(expected));
        }

        [TestMethod]
        public void Blank_String_Input_Caught()
        {
            //Arrange
            string input = "";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<string>(input);

            //Assert
            Assert.IsTrue(output.ToString().ToLower().Contains(expected));
        }
    }
}
