using System.Collections.Generic;
using Extensions.Core.Conversion;
using Extensions.Core.TextFunctions;
using Xunit;
using Xunit.Extensions;


namespace Extensions.Tests
{
    public class InvalidStringTests : TestsBase
    {
        private IEnumerable<string> ValidStrings = new List<string>() 
        { 
            "ValidAnswer1", 
            "ValidAnswer2"
        };

        [Theory, InlineData("Yay")]
        public void Unspecified_Strings_Give_Errors(string input)
        {
            //Arrange
            ConvertedValue<string> convertedValue;

            //Act
            input.ValidateAndConvert<string>(out convertedValue, choices:ValidStrings, choicesAreRequired: true);

            //Assert
            AssertFalse(convertedValue.HasValue);
            AssertNotEqual(convertedValue.Value, input);
        }

        [Theory, InlineData("Haha", "HaYa")]
        public void Strings_Not_Matching_Pattern_Give_Errors(string input, string pattern)
        {
            //Arrange
            ConvertedValue<string> convertedValue;

            //Act
            input.ValidateAndConvert<string>(out convertedValue, pattern: pattern);

            //Assert
            AssertFalse(convertedValue.HasValue);
            AssertNotEqual(convertedValue.Value, input);
        }

        [Theory, InlineData(null), InlineData(""), InlineData(" ")]
        public void Invalid_Strings_Give_Errors(string input)
        {
            //Arrange
            ConvertedValue<string> convertedValue;

            //Act
            input.ValidateAndConvert<string>(out convertedValue);

            //Assert
            AssertFalse(convertedValue.HasValue);
            AssertIsNull(convertedValue.Value);
        }
    }
}
