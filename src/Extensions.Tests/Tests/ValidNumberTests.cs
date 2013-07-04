using System.Collections.Generic;
using Extensions.Core.TextFunctions;
using Xunit;

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

        [Fact]
        public void Integers_Are_Specified()
        {
            //Arrange
            int input = 1;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<int>(input.ToString(), ValidIntegers);

            //Assert
            AssertEqual(input, output);
        }

        [Fact]
        public void Integers_Are_In_Range()
        {
            //Arrange
            int input = 2;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<int>(input.ToString(), min: DEFAULT_MIN, max: DEFAULT_MAX);

            //Assert
            AssertEqual(input, output);
        }

        [Fact]
        public void Integers_Match_Pattern()
        {
            //Arrange
            int input = 123;
            string pattern = "123";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<int>(input.ToString(), pattern: pattern);

            //Assert
            AssertEqual(input, output);
        }

        [Fact]
        public void Doubles_Are_Specified()
        {
            //Arrange
            double input = 1.1;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<double>(input.ToString(), ValidDoubles);

            //Assert
            AssertEqual(input, output);
        }

        [Fact]
        public void Doubles_Are_In_Range()
        {
            //Arrange
            double input = 2.0;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<double>(input.ToString(), min: DEFAULT_MIN, max: DEFAULT_MAX);

            //Assert
            AssertEqual(input, output);
        }

        [Fact]
        public void Doubles_Match_Pattern()
        {
            //Arrange
            double input = 1.232;
            string pattern = "1\\.232";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<double>(input.ToString(), pattern: pattern);

            //Assert
            AssertEqual(input, output);
        }

        [Fact]
        public void Floats_Are_Specified()
        {
            //Arrange
            float input = 1.1f;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<float>(input.ToString(), ValidFloats);

            //Assert
            AssertEqual(input, output);
        }

        [Fact]
        public void Floats_Are_In_Range()
        {
            //Arrange
            float input = 2.0f;

            //Act
            object output = ValidationExtensions.ValidateAndConvert<float>(input.ToString(), min: DEFAULT_MIN, max: DEFAULT_MAX);

            //Assert
            AssertEqual(input, output);
        }

        [Fact]
        public void Floats_Match_Pattern()
        {
            //Arrange
            float input = 1.223f;
            string pattern = "1\\.223";

            //Act
            object output = ValidationExtensions.ValidateAndConvert<float>(input.ToString(), pattern: pattern);

            //Assert
            AssertEqual(input, output);
        }
    }
}
