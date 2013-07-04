using System.Collections.Generic;
using Extensions.Core.TextFunctions;
using Xunit;


namespace Extensions.Tests
{
    public class InvalidStringTests : TestsBase
    {
        private IEnumerable<string> ValidStrings = new List<string>() 
        { 
            "ValidAnswer1", 
            "ValidAnswer2"
        };

        private const string expected = "error";

        [Fact]
        public void Unspecified_String_Input_Caught()
        {
            //Arrange
            string input = "Yay";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<string>(input, choices:ValidStrings, choicesAreRequired: true);

            //Assert
            AssertTrue(output.ToString().ToLower().Contains(expected));
        }

        [Fact]
        public void String_Input_Not_Matching_Regex_Caught()
        {
            //Arrange
            string input = "Haha";
            string pattern = "HaYa";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<string>(input, pattern: pattern);

            //Assert
            AssertTrue(output.ToString().ToLower().Contains(expected));
        }

        [Fact]
        public void Empty_String_Input_Caught()
        {
            //Arrange
            string input = "";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<string>(input);

            //Assert
            AssertTrue(output.ToString().ToLower().Contains(expected));
        }

        [Fact]
        public void Blank_String_Input_Caught()
        {
            //Arrange
            string input = "";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<string>(input);

            //Assert
            AssertTrue(output.ToString().ToLower().Contains(expected));
        }
    }
}
