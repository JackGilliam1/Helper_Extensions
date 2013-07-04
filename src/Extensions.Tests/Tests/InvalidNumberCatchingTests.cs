using Extensions.Core.TextFunctions;
using System.Collections.Generic;
using Xunit;

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

        [Fact]
        public void Integers_Containing_Letters_Caught()
        {
            //Arrange
            string input = "5454a45hsdas24234";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<int>(input.ToString());

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Integers_Not_Matching_Pattern_Caught()
        {
            //Arrange
            int input = 1;
            string pattern = "123";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<int>(input.ToString(), pattern: pattern);

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Integers_Below_Range_Caught()
        {
            //Arrange
            int input = -1;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<int>(input.ToString(), min: DEFAULT_MIN, max: DEFAULT_MAX);

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Integers_Above_Range_Caught()
        {
            //Arrange
            int input = 4;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<int>(input.ToString(), min: DEFAULT_MIN, max: DEFAULT_MAX);

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Integers_Unspecified_Caught()
        {
            //Arrange
            int input = 4;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<int>(text: input.ToString(), choices: ValidIntegers, choicesAreRequired: true);

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Doubles_Containing_Letters_Caught()
        {
            //Arrange
            string input = "5454a45hsdas24234";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<double>(input.ToString());

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Doubles_Not_Matching_Pattern_Caught()
        {
            //Arrange
            double input = 1.333;
            string pattern = "1.232";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<double>(input.ToString(), pattern: pattern);

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Doubles_Below_Range_Caught()
        {
            //Arrange
            double input = 0.9;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<double>(input.ToString(), min: DEFAULT_MIN, max: DEFAULT_MAX);

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Doubles_Above_Range_Caught()
        {
            //Arrange
            double input = 3.1;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<double>(input.ToString(), min: DEFAULT_MIN, max: DEFAULT_MAX);

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Doubles_Unspecified_Caught()
        {
            //Arrange
            double input = 1.2;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<double>(text: input.ToString(), choices: ValidDoubles, choicesAreRequired: true);

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Floats_Containing_Letters_Caught()
        {
            //Arrange
            string input = "5454a45hsdas24234";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<float>(input.ToString());

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Floats_Not_Matching_Pattern_Caught()
        {
            //Arrange
            float input = 1.333f;
            string pattern = "1.223";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<float>(input.ToString(), pattern: pattern);

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Floats_Below_Range_Caught()
        {
            //Arrange
            float input = 0.9f;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<float>(input.ToString(), min: DEFAULT_MIN, max: DEFAULT_MAX);

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Floats_Above_Range_Caught()
        {
            //Arrange
            float input = 3.1f;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<float>(input.ToString(), min: DEFAULT_MIN, max: DEFAULT_MAX);

            //Assert
            AssertNotEqual(input, output);
        }

        [Fact]
        public void Floats_Unspecified_Caught()
        {
            //Arrange
            float input = 1.2f;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<float>(text: input.ToString(), choices: ValidFloats, choicesAreRequired: true);

            //Assert
            AssertNotEqual(input, output);
        }
    }
}