using Extensions.Core.Conversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Tests.NumberConversion
{
    [TestClass]
    public class IsNumberTests
    {
        [TestMethod]
        public void IsNumberIntegerString()
        {
            INumberConverter numberConverter = new StringIntegerConverter();
            const string input = "123";

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsTrue(isNumber, "The input was incorrectly stated as not being a number");
        }

        [TestMethod]
        public void IsNotNumberIntegerString()
        {
            INumberConverter numberConverter = new StringIntegerConverter();
            const string input = "12f";

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");
        }

        [TestMethod]
        public void IsNumberFloatString()
        {
            INumberConverter numberConverter = new StringFloatConverter();
            const string input = "123.8";

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsTrue(isNumber, "The input was incorrectly stated as not being a number");
        }

        [TestMethod]
        public void IsNotNumberFloatString()
        {
            INumberConverter numberConverter = new StringFloatConverter();
            const string input = "123.f";

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");
        }

        [TestMethod]
        public void IsNumberDoubleString()
        {
            INumberConverter numberConverter = new StringDoubleConverter();
            const string input = "123.8";

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsTrue(isNumber, "The input was incorrectly stated as not being a number");
        }

        [TestMethod]
        public void IsNotNumberDoubleString()
        {
            INumberConverter numberConverter = new StringDoubleConverter();
            const string input = "123.f";

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");
        }

        [TestMethod]
        public void IsNotNumberEmptyString()
        {
            const string input = "";

            //Integer
            INumberConverter numberConverter = new StringIntegerConverter();
            bool isNumber = numberConverter.IsNumber(input);
            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");


            //Float
            numberConverter = new StringFloatConverter();
            isNumber = numberConverter.IsNumber(input);
            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");

            //Double
            numberConverter = new StringDoubleConverter();
            isNumber = numberConverter.IsNumber(input);
            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");
        }

        [TestMethod]
        public void IsNotNumberNull()
        {
            const string input = null;

            //Integer
            INumberConverter numberConverter = new StringIntegerConverter();
            bool isNumber = numberConverter.IsNumber(input);
            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");


            //Float
            numberConverter = new StringFloatConverter();
            isNumber = numberConverter.IsNumber(input);
            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");

            //Double
            numberConverter = new StringDoubleConverter();
            isNumber = numberConverter.IsNumber(input);
            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");
        }

        [TestMethod]
        public void IsNotNumberObject()
        {
            object input = new object();

            //Integer
            INumberConverter numberConverter = new StringIntegerConverter();
            bool isNumber = numberConverter.IsNumber(input);
            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");


            //Float
            numberConverter = new StringFloatConverter();
            isNumber = numberConverter.IsNumber(input);
            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");

            //Double
            numberConverter = new StringDoubleConverter();
            isNumber = numberConverter.IsNumber(input);
            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");
        }

        [TestMethod]
        public void IsNumberInteger()
        {
            INumberConverter numberConverter = new StringIntegerConverter();
            const int input = 123;

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsTrue(isNumber, "The input was incorrectly stated as not being a number");
        }

        [TestMethod]
        public void IsNumberFloat()
        {
            INumberConverter numberConverter = new StringFloatConverter();
            const float input = 123.8f;

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsTrue(isNumber, "The input was incorrectly stated as not being a number");
        }

        [TestMethod]
        public void IsNumberDouble()
        {
            INumberConverter numberConverter = new StringDoubleConverter();

            const double input = 123.8;

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsTrue(isNumber, "The input was incorrectly stated as not being a number");
        }

        [TestMethod]
        public void IsNumberIntegerObject()
        {
            INumberConverter numberConverter = new StringIntegerConverter();
            object input = (int)123;

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsTrue(isNumber, "The input was incorrectly stated as not being a number");
        }

        [TestMethod]
        public void IsNumberFloatObject()
        {
            INumberConverter numberConverter = new StringFloatConverter();
            object input = 123.8f;

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsTrue(isNumber, "The input was incorrectly stated as not being a number");
        }

        [TestMethod]
        public void IsNumberDoubleObject()
        {
            INumberConverter numberConverter = new StringDoubleConverter();

            object input = (double)123.8;

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsTrue(isNumber, "The input was incorrectly stated as not being a number");
        }

        [TestMethod]
        public void IsNumberIntegerStringObject()
        {
            INumberConverter numberConverter = new StringIntegerConverter();
            object input = "123";

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsTrue(isNumber, "The input was incorrectly stated as not being a number");
        }

        [TestMethod]
        public void IsNumberFloatStringObject()
        {
            INumberConverter numberConverter = new StringFloatConverter();
            object input = "123.8";

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsTrue(isNumber, "The input was incorrectly stated as not being a number");
        }

        [TestMethod]
        public void IsNumberDoubleStringObject()
        {
            INumberConverter numberConverter = new StringDoubleConverter();

            object input = "123.8";

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsTrue(isNumber, "The input was incorrectly stated as not being a number");
        }

        [TestMethod]
        public void IsNotNumberIntegerStringObject()
        {
            INumberConverter numberConverter = new StringIntegerConverter();
            object input = "12f";

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");
        }

        [TestMethod]
        public void IsNotNumberFloatStringObject()
        {
            INumberConverter numberConverter = new StringFloatConverter();
            object input = "123.f";

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");
        }

        [TestMethod]
        public void IsNotNumberDoubleStringObject()
        {
            INumberConverter numberConverter = new StringDoubleConverter();

            object input = "123.f";

            bool isNumber = numberConverter.IsNumber(input);

            Assert.IsFalse(isNumber, "The input was incorrectly stated as being a number");
        }
    }
}
