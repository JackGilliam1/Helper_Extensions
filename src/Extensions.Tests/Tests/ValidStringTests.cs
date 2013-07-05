using System.Collections.Generic;
using Extensions.Core.Conversion;
using Extensions.Core.TextFunctions;
using Xunit;
using Xunit.Extensions;

namespace Extensions.Tests
{
    public class ValidStringTests : TestsBase
    {
        private IEnumerable<string> ValidStrings = new List<string>() 
        { 
            "ValidAnswer1", 
            "ValidAnswer2"
        };

        [Theory, InlineData("ValidAnswer1", null), InlineData("Hahah", "H[a]ha(\\w)")]
        public void Valid_Strings_Accepted(string input, string pattern)
        {
            //Arrange
            ConvertedValue<string> convertedValue;

            //Act
            object output = input.ValidateAndConvert(out convertedValue, choices:ValidStrings, pattern:pattern);

            //Assert
            AssertTrue(convertedValue.HasValue);
            AssertEqual(convertedValue.Value, input);
        }
    }
}
