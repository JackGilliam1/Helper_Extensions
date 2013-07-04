using Extensions.Core.ConsoleInOut;
using Xunit;

namespace Extensions.Tests
{
    public class ConsoleIoExtTests : IExtTest
    {
        [Fact]
        public void IsWithinRangeInteger()
        {
            //Arrange
            const int input = TestInteger;
            const double inputRangeMin = TestRangeMin;
            const double inputRangeMax = TestRangeMax;

            //Act
            bool withinRange = ConsoleExtensions.IsBetween(input, inputRangeMin, inputRangeMax);

            //Assert
            AssertTrue(withinRange);
        }

        [Fact]
        public void IsWithinRangeDouble()
        {
            //Arrange
            const double input = TestDouble;
            const double inputRangeMin = TestRangeMin;
            const double inputRangeMax = TestRangeMax;

            //Act
            bool withinRange = input.IsBetween(inputRangeMin, inputRangeMax);

            //Assert
            AssertTrue(withinRange);
        }

        [Fact]
        public void IsWithinRangeFloat()
        {
            //Arrange
            const float input = TestFloat;
            const double inputRangeMin = TestRangeMin;
            const double inputRangeMax = TestRangeMax;

            //Act
            bool withinRange = ConsoleExtensions.IsBetween(input, inputRangeMin, inputRangeMax);

            //Assert
            AssertTrue(withinRange);
        }
    }
}
