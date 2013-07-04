using Extensions.Core.ConsoleInOut;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Tests
{
    //[TestClass]
    public class ConsoleIOExtTests : IExtTest
    {
        [TestMethod]
        public void IsWithinRangeInteger()
        {
            //Arrange
            const int input = TestInteger;
            const double inputRangeMin = TestRangeMin;
            const double inputRangeMax = TestRangeMax;

            //Act
            bool withinRange = ConsoleExtensions.IsWithin(input, inputRangeMin, inputRangeMax);

            //Assert
            Assert.IsTrue(withinRange);
        }

        [TestMethod]
        public void IsWithinRangeDouble()
        {
            //Arrange
            const double input = TestDouble;
            const double inputRangeMin = TestRangeMin;
            const double inputRangeMax = TestRangeMax;

            //Act
            bool withinRange = ConsoleExtensions.IsWithin(input, inputRangeMin, inputRangeMax);

            //Assert
            Assert.IsTrue(withinRange);
        }

        [TestMethod]
        public void IsWithinRangeFloat()
        {
            //Arrange
            const float input = TestFloat;
            const double inputRangeMin = TestRangeMin;
            const double inputRangeMax = TestRangeMax;

            //Act
            bool withinRange = ConsoleExtensions.IsWithin(input, inputRangeMin, inputRangeMax);

            //Assert
            Assert.IsTrue(withinRange);
        }
    }
}
