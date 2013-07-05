using System.Collections.Generic;
using Extensions.Core.Conversion;
using Extensions.Core.TextFunctions;
using Xunit;
using Xunit.Extensions;

namespace Extensions.Tests
{
    public class ValidNumberTests : TestsBase
    {
        private const double DEFAULT_MIN = 1;
        private const double DEFAULT_MAX = 3;

        private IEnumerable<int> ValidIntegers = new List<int>()
        {
            1,
            2,
            3
        };

        private IEnumerable<double> ValidDoubles = new List<double>()
        {
            1.1,
            2.2,
            3.3
        };

        private IEnumerable<float> ValidFloats = new List<float>()
        {
            1.1f,
            2.2f,
            3.3f
        };

        [Theory, InlineData("1", null, 1), InlineData("2", "2", 2)]
        public void Valid_Integers_Accepted(string input, string pattern, int expected)
        {
            //Arrange
            ConvertedValue<int> convertedValue;

            //Act
            input.ValidateAndConvert<int>(out convertedValue, choices:ValidIntegers, min:DEFAULT_MIN, max:DEFAULT_MAX, pattern:pattern);

            //Assert
            AssertTrue(convertedValue.HasValue);
            AssertEqual(convertedValue.Value, expected);
        }


        [Theory, InlineData("1.1", null, 1.1), InlineData("2.0", null, 2.0), InlineData("1.232", "1\\.232", 1.232)]
        public void Valid_Doubles_Accepted(string input, string pattern, double expected)
        {
            //Arrange
            ConvertedValue<double> convertedValue;

            //Act
            input.ValidateAndConvert<double>(out convertedValue, choices: ValidDoubles, min: DEFAULT_MIN, max: DEFAULT_MAX, pattern: pattern);

            //Assert
            AssertTrue(convertedValue.HasValue);
            AssertEqual(convertedValue.Value, expected);
        }
        

        [Theory, InlineData("1.1", null, 1.1), InlineData("2.0", null, 2.0), InlineData("1.232", "1\\.232", 1.232)]
        public void Valid_Floats_Accepted(string input, string pattern, double expected)
        {
            //Arrange
            float expectedFloat = (float)expected;
            ConvertedValue<float> convertedValue;

            //Act
            input.ValidateAndConvert<float>(out convertedValue, choices: ValidFloats, min: DEFAULT_MIN, max: DEFAULT_MAX, pattern: pattern);

            //Assert
            AssertTrue(convertedValue.HasValue);
            AssertEqual(convertedValue.Value, expectedFloat);
        }
    }
}
