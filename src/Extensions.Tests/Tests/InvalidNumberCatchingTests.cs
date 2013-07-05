using Extensions.Core.Conversion;
using Extensions.Core.TextFunctions;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

namespace Extensions.Tests
{
    public class InvalidNumberTests : TestsBase
    {
        private const double DEFAULT_MIN = 1;
        private const double DEFAULT_MAX = 3;

        public IEnumerable<int> ValidIntegers = new List<int>()
        {
            1,
            2,
            3
        };

        public IEnumerable<double> ValidDoubles = new List<double>()
        {
            1.1,
            2.2,
            3.3
        };

        public IEnumerable<float> ValidFloats = new List<float>()
        {
            1.1f,
            2.2f,
            3.3f
        };

        [Theory, InlineData("5454a45hsdas24234"), InlineData("-1"), InlineData("4")]
        public void Invalid_Integers_Give_Errors(string input)
        {
            //Arrange
            ConvertedValue<int> convertedValue;

            //Act
            input.ValidateAndConvert<int>(out convertedValue, min: DEFAULT_MIN, max: DEFAULT_MAX);

            //Assert
            AssertFalse(convertedValue.HasValue);
            AssertNotEqual(convertedValue.Value, input);
        }
        [Theory, InlineData("1", "123")]
        public void Integers_Not_Matching_Pattern_Give_Errors(string input, string pattern)
        {
            //Arrange
            ConvertedValue<int> convertedValue;

            //Act
            input.ValidateAndConvert<int>(out convertedValue, pattern: pattern);

            //Assert
            AssertFalse(convertedValue.HasValue);
            AssertNotEqual(convertedValue.Value, input);
        }
        [Theory, InlineData("4")]
        public void Unspecified_Integers_Give_Errors(string input)
        {
            //Arrange
            ConvertedValue<int> convertedValue;

            //Act
            input.ValidateAndConvert<int>(out convertedValue, choices: ValidIntegers, choicesAreRequired: true);

            //Assert
            AssertFalse(convertedValue.HasValue);
            AssertNotEqual(convertedValue.Value, input);
        }


        [Theory, InlineData("5454a45hsdas24234"), InlineData("0.9"), InlineData("3.1")]
        public void Invalid_Doubles_Give_Errors(string input)
        {
            //Arrange
            ConvertedValue<double> convertedValue;

            //Act
            object output = input.ValidateAndConvert<double>(out convertedValue, min: DEFAULT_MIN, max: DEFAULT_MAX);

            //Assert
            AssertFalse(convertedValue.HasValue);
            AssertNotEqual(convertedValue.Value, input);
        }
        [Theory, InlineData("1.333", "1.232")]
        public void Doubles_Not_Matching_Pattern_Give_Errors(string input, string pattern)
        {
            //Arrange
            ConvertedValue<double> convertedValue;

            //Act
            object output = input.ValidateAndConvert<double>(out convertedValue, pattern: pattern);

            //Assert
            AssertFalse(convertedValue.HasValue);
            AssertNotEqual(convertedValue.Value, input);
        }
        [Theory, InlineData("1.2")]
        public void Unspecified_Doubles_Give_Errors(string input)
        {
            //Arrange
            ConvertedValue<double> convertedValue;

            //Act
            object output = input.ValidateAndConvert<double>(out convertedValue, choices: ValidDoubles, choicesAreRequired: true);

            //Assert
            AssertFalse(convertedValue.HasValue);
            AssertNotEqual(convertedValue.Value, input);
        }


        [Theory, InlineData("5454a45hsdas24234"), InlineData("0.9"), InlineData("3.1")]
        public void Invalid_Floats_Give_Errors(string input)
        {
            //Arrange
            ConvertedValue<float> convertedValue;

            //Act
            object output = input.ValidateAndConvert<float>(out convertedValue, min: DEFAULT_MIN, max: DEFAULT_MAX);

            //Assert
            AssertFalse(convertedValue.HasValue);
            AssertNotEqual(convertedValue.Value, input);
        }
        [Theory, InlineData("1.333", "1.232")]
        public void Floats_Not_Matching_Pattern_Give_Errors(string input, string pattern)
        {
            //Arrange
            ConvertedValue<float> convertedValue;

            //Act
            object output = input.ValidateAndConvert<float>(out convertedValue, pattern: pattern);

            //Assert
            AssertFalse(convertedValue.HasValue);
            AssertNotEqual(convertedValue.Value, input);
        }
        [Theory, InlineData("1.2")]
        public void Unspecified_Floats_Give_Errors(string input)
        {
            //Arrange
            ConvertedValue<float> convertedValue;

            //Act
            object output = input.ValidateAndConvert<float>(out convertedValue, choices: ValidFloats, choicesAreRequired: true);

            //Assert
            AssertFalse(convertedValue.HasValue);
            AssertNotEqual(convertedValue.Value, input);
        }
    }
}