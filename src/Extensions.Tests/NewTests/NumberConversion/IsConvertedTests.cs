using Extensions.Core.Conversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Extensions.Tests.NumberConversion
{
    [TestClass]
    public class IsConvertedTests
    {
        [TestMethod]
        public void IsConvertedIntegerString()
        {
            INumberConverter numberConverter = new StringIntegerConverter();
            const string input = "123";
            const int expectedOutput = 123;

            int actualOutput = (int)numberConverter.Convert(input);

            Assert.AreEqual(expectedOutput, actualOutput, "The input was incorrectly converted");
        }

        [TestMethod]
        public void IsNotConvertedIntegerString()
        {
            INumberConverter numberConverter = new StringIntegerConverter();
            const string input = "12f";

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (FormatException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }
        }

        [TestMethod]
        public void IsConvertedFloatString()
        {
            INumberConverter numberConverter = new StringFloatConverter();
            const string input = "123.8";
            const float expectedOutput = 123.8f;

            float actualOutput = (float)numberConverter.Convert(input);

            Assert.AreEqual(expectedOutput, actualOutput, "The input was incorrectly converted");
        }

        [TestMethod]
        public void IsNotConvertedFloatString()
        {
            INumberConverter numberConverter = new StringFloatConverter();
            const string input = "123.f";

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (FormatException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }
        }

        [TestMethod]
        public void IsConvertedDoubleString()
        {
            INumberConverter numberConverter = new StringDoubleConverter();
            const string input = "123.8";
            const double expectedOutput = 123.8;

            double actualOutput = (double)numberConverter.Convert(input);

            Assert.AreEqual(expectedOutput, actualOutput, "The input was incorrectly converted");
        }

        [TestMethod]
        public void IsNotConvertedDoubleString()
        {
            INumberConverter numberConverter = new StringDoubleConverter();
            const string input = "123.f";

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (FormatException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }
        }

        [TestMethod]
        public void IsConvertedInteger()
        {
            INumberConverter numberConverter = new StringIntegerConverter();
            const int input = 123;
            const int expectedOutput = 123;

            int actualOutput = (int)numberConverter.Convert(input);

            Assert.AreEqual(expectedOutput, actualOutput, "The input was incorrectly converted");
        }

        [TestMethod]
        public void IsConvertedFloat()
        {
            INumberConverter numberConverter = new StringFloatConverter();
            const float input = 123.8f;
            const float expectedOutput = 123.8f;

            float actualOutput = (float)numberConverter.Convert(input);

            Assert.AreEqual(expectedOutput, actualOutput, "The input was incorrectly converted");
        }

        [TestMethod]
        public void IsConvertedDouble()
        {
            INumberConverter numberConverter = new StringDoubleConverter();
            const double input = 123.8;
            const double expectedOutput = 123.8;

            double actualOutput = (double)numberConverter.Convert(input);

            Assert.AreEqual(expectedOutput, actualOutput, "The input was incorrectly converted");
        }

        [TestMethod]
        public void IsNotConvertedEmptyString()
        {
            const string input = "";

            INumberConverter numberConverter = new StringIntegerConverter();

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (FormatException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }

            numberConverter = new StringFloatConverter();

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (FormatException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }

            numberConverter = new StringDoubleConverter();

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (FormatException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }
        }

        [TestMethod]
        public void IsNotConvertedNull()
        {
            const string input = null;

            INumberConverter numberConverter = new StringIntegerConverter();

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (ArgumentNullException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }

            numberConverter = new StringFloatConverter();

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (ArgumentNullException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }

            numberConverter = new StringDoubleConverter();

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (ArgumentNullException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }
        }

        [TestMethod]
        public void IsNotConvertedObject()
        {
            object input = new object();

            INumberConverter numberConverter = new StringIntegerConverter();

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (ArgumentException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }

            numberConverter = new StringFloatConverter();

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (ArgumentException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }

            numberConverter = new StringDoubleConverter();

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (ArgumentException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }
        }

        [TestMethod]
        public void IsConvertedIntegerObject()
        {
            INumberConverter numberConverter = new StringIntegerConverter();
            object input = 123;
            const int expectedOutput = 123;

            int actualOutput = (int)numberConverter.Convert(input);

            Assert.AreEqual(expectedOutput, actualOutput, "The input was incorrectly converted");
        }

        [TestMethod]
        public void IsConvertedFloatObject()
        {
            INumberConverter numberConverter = new StringFloatConverter();
            object input = 123.8f;
            const float expectedOutput = 123.8f;

            float actualOutput = (float)numberConverter.Convert(input);

            Assert.AreEqual(expectedOutput, actualOutput, "The input was incorrectly converted");
        }

        [TestMethod]
        public void IsConvertedDoubleObject()
        {
            INumberConverter numberConverter = new StringDoubleConverter();
            object input = 123.8;
            const double expectedOutput = 123.8;

            double actualOutput = (double)numberConverter.Convert(input);

            Assert.AreEqual(expectedOutput, actualOutput, "The input was incorrectly converted");
        }

        [TestMethod]
        public void IsConvertedIntegerStringObject()
        {
            INumberConverter numberConverter = new StringIntegerConverter();
            object input = "123";
            const int expectedOutput = 123;

            int actualOutput = (int)numberConverter.Convert(input);

            Assert.AreEqual(expectedOutput, actualOutput, "The input was incorrectly converted");
        }

        [TestMethod]
        public void IsConvertedFloatStringObject()
        {
            INumberConverter numberConverter = new StringFloatConverter();
            object input = "123.8";
            const float expectedOutput = 123.8f;

            float actualOutput = (float)numberConverter.Convert(input);

            Assert.AreEqual(expectedOutput, actualOutput, "The input was incorrectly converted");
        }

        [TestMethod]
        public void IsConvertedDoubleStringObject()
        {
            INumberConverter numberConverter = new StringDoubleConverter();
            object input = "123.8";
            const double expectedOutput = 123.8;

            double actualOutput = (double)numberConverter.Convert(input);

            Assert.AreEqual(expectedOutput, actualOutput, "The input was incorrectly converted");
        }

        [TestMethod]
        public void IsNotConvertedIntegerStringObject()
        {
            INumberConverter numberConverter = new StringIntegerConverter();
            object input = "12f";

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (FormatException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }
        }

        [TestMethod]
        public void IsNotConvertedFloatStringObject()
        {
            INumberConverter numberConverter = new StringFloatConverter();
            object input = "123.f";

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (FormatException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }
        }

        [TestMethod]
        public void IsNotConvertedDoubleStringObject()
        {
            INumberConverter numberConverter = new StringDoubleConverter();
            object input = "123.f";

            try
            {
                numberConverter.Convert(input);
                Assert.Fail("The input was incorrectly accepted");
            }
            catch (FormatException ae)
            {
                Trace.WriteLine(ae.Message);
                //This catch is the expected outcome
            }
        }
    }
}
