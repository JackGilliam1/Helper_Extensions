using Xunit.Extensions;

namespace Extensions.Tests.ValueConversion
{
    public class StringToIntegerConversion : ConversionExtensionTests<int>
    {
        [Theory, InlineData("1", 1), InlineData("1454", 1454)]
        public void ValidDecimalString(string value, int expectedValue)
        {
            var convertedValue = PerformConversion(value);
            AssertEqual(convertedValue.Value, expectedValue, "The integer string was valid, but it was rejected");
        }

        [Theory, InlineData(""), InlineData(" "), InlineData("13abc"), InlineData("13.1.1"), InlineData("1.5"), InlineData(null)]
        public void InvalidDecimalString(string value)
        {
            var convertedValue = PerformConversion(value);
            AssertFalse(convertedValue.HasValue, "The integer string was invalid, but it was accepted");
        }
    }
}