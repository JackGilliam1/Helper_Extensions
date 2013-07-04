using Xunit.Extensions;

namespace Extensions.Tests.ValueConversion
{
    public class StringToDecimalConversion : ConversionExtensionTests<decimal>
    {
        [Theory, InlineData("1.5", 1.5), InlineData("1", 1)]
        public void ValidDecimalString(string value, double expectedValue)
        {
            var expectedDecimalValue = (decimal)expectedValue;
            var convertedValue = PerformConversion(value);
            AssertEqual(convertedValue.Value, expectedDecimalValue, "The decimal string was valid, but it was rejected");
        }

        [Theory, InlineData(""), InlineData(" "), InlineData("13abc"), InlineData("13.1.1"), InlineData(null)]
        public void InvalidDecimalString(string value)
        {
            var convertedValue = PerformConversion(value);
            AssertFalse(convertedValue.HasValue, "The decimal string was invalid, but it was accepted");
        }
    }
}