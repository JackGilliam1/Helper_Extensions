using System.Collections.Generic;
using Extensions.Core.TextFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Tests
{

    [TestClass]
    public class ValidStringTests
    {
        private IEnumerable<string> ValidStrings = new List<string>() 
        { 
            "ValidAnswer1", 
            "ValidAnswer2"
        };

        [TestMethod]
        public void Strings_Specified_Are_Returned()
        {
            //Arrange
            string input = "ValidAnswer1";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<string>(input, ValidStrings);

            //Assert
            Assert.AreEqual(input.ToString(), output.ToString());
        }

        [TestMethod]
        public void Strings_Matching_Regex_Are_Returned()
        {
            //Arrange
            string input = "Hahah";
            string pattern = "H[a]ha(\\w)";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<string>(input, pattern: pattern);

            //Assert
            Assert.AreEqual(input.ToString(), output.ToString());
        }
    }
}
