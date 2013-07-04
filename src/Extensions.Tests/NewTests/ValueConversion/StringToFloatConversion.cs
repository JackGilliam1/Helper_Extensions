using Xunit.Extensions;

namespace Extensions.Tests.ValueConversion
{
    public class StringToFloatConversion : ConversionExtensionTests<float>
    {
        [Theory, InlineData("1.5", 1.5), InlineData("1", 1)]
        public void ValidDecimalString(string value, double expectedValue)
        {
            var expectedFloatValue = (float)expectedValue;
            var convertedValue = PerformConversion(value);
            AssertEqual(convertedValue.Value, expectedFloatValue, "The float string was valid, but it was rejected");
        }

        [Theory, InlineData(""), InlineData(" "), InlineData("13abc"), InlineData("13.1.1"), InlineData(null)]
        public void InvalidDecimalString(string value)
        {
            var convertedValue = PerformConversion(value);
            AssertFalse(convertedValue.HasValue, "The float string was invalid, but it was accepted");
        }
    }
}