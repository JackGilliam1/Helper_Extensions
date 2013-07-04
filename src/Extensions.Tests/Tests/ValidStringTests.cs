using System.Collections.Generic;
using Extensions.Core.TextFunctions;
using Xunit;

namespace Extensions.Tests
{
    public class ValidStringTests : TestsBase
    {
        private IEnumerable<string> ValidStrings = new List<string>() 
        { 
            "ValidAnswer1", 
            "ValidAnswer2"
        };

        [Fact]
        public void Strings_Specified_Are_Returned()
        {
            //Arrange
            string input = "ValidAnswer1";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<string>(input, ValidStrings);

            //Assert
            AssertEqual(input, output);
        }

        [Fact]
        public void Strings_Matching_Regex_Are_Returned()
        {
            //Arrange
            string input = "Hahah";
            string pattern = "H[a]ha(\\w)";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<string>(input, pattern: pattern);

            //Assert
            AssertEqual(input, output);
        }
    }
}
