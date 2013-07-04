using Extensions.Core.Conversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Extensions.Tests.Collections
{
    public class ConversionExtensionTests<TDataType>
    {
        public bool IsExpectedValue(ConvertedValue<TDataType> actualValue, TDataType expectedValue)
        {
            bool isExpectedValue;
            if (actualValue.WasConverted)
            {
                if (actualValue.HasValue)
                {
                    isExpectedValue = actualValue.Value.Equals(expectedValue);
                }
                else
                {
                    isExpectedValue = false;
                }
            }
            else
            {
                isExpectedValue = false;
            }
            return isExpectedValue;
        }
        public bool IsExpectedValue(ConvertedValue<TDataType> actualValue)
        {
            return IsExpectedValue(actualValue, default(TDataType));
        }

        public ConvertedValue<TDataType> PerformConversion(string value)
        {
            return value.ConvertTo<TDataType>();
        }
    }

    [TestClass]
    public class StringToIntegerConversion : ConversionExtensionTests<int>
    {
        [TestMethod]
        public void Valid()
        {
            const string value = "1";
            var expectedValue = 1;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue, expectedValue).Should("The string was valid, but was rejected").Be.True();
        }

        [TestMethod]
        public void EmptyString()
        {
            const string value = "";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string empty, but was accepted").Be.False();
        }

        [TestMethod]
        public void SpacesString()
        {
            const string value = " ";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string spaces, but was accepted").Be.False();
        }

        [TestMethod]
        public void NullString()
        {
            string value = null;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string was null, but was accepted").Be.False();
        }

        [TestMethod]
        public void ContainsLetters()
        {
            const string value = "13abc";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string contained letters, but was accepted").Be.False();
        }

        [TestMethod]
        public void ContainsDecimals()
        {
            const string value = "1.5";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue);
        }
    }

    [TestClass]
    public class StringToFloatConversion : ConversionExtensionTests<float>
    {
        [TestMethod]
        public void ContainsDecimal()
        {
            const string value = "1.5";
            var expectedValue = 1.5f;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue, expectedValue).Should("The string was valid, but was rejected").Be.True();
        }

        [TestMethod]
        public void MissingDecimal()
        {
            const string value = "1";
            var expectedValue = 1f;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue, expectedValue).Should("The string was valid, but was rejected").Be.True();
        }

        [TestMethod]
        public void EmptyString()
        {
            const string value = "";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string empty, but was accepted").Be.False();
        }

        [TestMethod]
        public void SpacesString()
        {
            const string value = " ";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string spaces, but was accepted").Be.False();
        }

        [TestMethod]
        public void NullString()
        {
            string value = null;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string was null, but was accepted").Be.False();
        }

        [TestMethod]
        public void ContainsLetters()
        {
            const string value = "13abc";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string contained letters, but was accepted").Be.False();
        }
    }

    [TestClass]
    public class StringToDoubleConversion : ConversionExtensionTests<double>
    {
        [TestMethod]
        public void ContainsDecimal()
        {
            const string value = "1.5";
            var expectedValue = 1.5;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue, expectedValue).Should("The string was valid, but was rejected").Be.True();
        }

        [TestMethod]
        public void MissingDecimal()
        {
            const string value = "1";
            var expectedValue = 1;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue, expectedValue).Should("The string was valid, but was rejected").Be.True();
        }

        [TestMethod]
        public void EmptyString()
        {
            const string value = "";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string empty, but was accepted").Be.False();
        }

        [TestMethod]
        public void SpacesString()
        {
            const string value = " ";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string spaces, but was accepted").Be.False();
        }

        [TestMethod]
        public void NullString()
        {
            string value = null;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string was null, but was accepted").Be.False();
        }

        [TestMethod]
        public void ContainsLetters()
        {
            const string value = "13abc";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string contained letters, but was accepted").Be.False();
        }
    }

    [TestClass]
    public class StringToDecimalConversion : ConversionExtensionTests<decimal>
    {
        [TestMethod]
        public void ContainsDecimal()
        {
            const string value = "1.5";
            var expectedValue = 1.5m;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue, expectedValue).Should("The string was valid, but was rejected").Be.True();
        }

        [TestMethod]
        public void MissingDecimal()
        {
            const string value = "1";
            var expectedValue = 1m;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue, expectedValue).Should("The string was valid, but was rejected").Be.True();
        }

        [TestMethod]
        public void EmptyString()
        {
            const string value = "";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string empty, but was accepted").Be.False();
        }

        [TestMethod]
        public void SpacesString()
        {
            const string value = " ";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string spaces, but was accepted").Be.False();
        }

        [TestMethod]
        public void NullString()
        {
            string value = null;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string was null, but was accepted").Be.False();
        }

        [TestMethod]
        public void ContainsLetters()
        {
            const string value = "13abc";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string contained letters, but was accepted").Be.False();
        }
    }

    [TestClass]
    public class StringToBooleanConversion : ConversionExtensionTests<bool>
    {
        [TestMethod]
        public void TrueString()
        {
            const string value = "True";
            var expectedValue = true;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue, expectedValue).Should("The string was a valid True, but was rejected").Be.True();
        }

        [TestMethod]
        public void FalseString()
        {
            const string value = "False";
            var expectedValue = false;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue, expectedValue).Should("The string was a valid False, but was rejected").Be.True();
        }

        [TestMethod]
        public void EmptyString()
        {
            const string value = "";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string empty, but was accepted").Be.False();
        }

        [TestMethod]
        public void SpacesString()
        {
            const string value = " ";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string spaces, but was accepted").Be.False();
        }

        [TestMethod]
        public void NullString()
        {
            string value = null;
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string was null, but was accepted").Be.False();
        }

        [TestMethod]
        public void InvalidBoolean()
        {
            const string value = "fdsafds";
            var convertedValue = PerformConversion(value);
            IsExpectedValue(convertedValue).Should("The string was an invalid boolean, but was accepted").Be.False();
        }
    }
}
