using Xunit.Extensions;

namespace Extensions.Tests.ValueConversion
{
    public class StringToDoubleConversion : ConversionExtensionTests<double>
    {
        [Theory, InlineData("1.5", 1.5), InlineData("1", 1)]
        public void ValidDecimalString(string value, double expectedValue)
        {
            var convertedValue = PerformConversion(value);
            AssertEqual(convertedValue.Value, expectedValue, "The double string was valid, but it was rejected");
        }

        [Theory, InlineData(""), InlineData(" "), InlineData("13abc"), InlineData("13.1.1"), InlineData(null)]
        public void InvalidDecimalString(string value)
        {
            var convertedValue = PerformConversion(value);
            AssertFalse(convertedValue.HasValue, "The double string was invalid, but it was accepted");
        }
    }
}