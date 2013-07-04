using Xunit.Extensions;

namespace Extensions.Tests.ValueConversion
{
    public class StringToBooleanConversion : ConversionExtensionTests<bool>
    {
        [Theory, InlineData("True", true), InlineData("False", false), InlineData("TrUE", true), InlineData("FaLSe", false)]
        public void ValidBooleanString(string value, bool expectedValue)
        {
            var convertedValue = PerformConversion(value);
            AssertEqual(convertedValue.Value, expectedValue, "The string was a valie boolean string, but was rejected");
        }

        [Theory, InlineData(null), InlineData(""), InlineData(" "), InlineData("fdsafds")]
        public void InvalidBooleanString(string value)
        {
            var convertedValue = PerformConversion(value);
            AssertFalse(convertedValue.HasValue, "The boolean string was invalid, but was accepted");
        }
    }
}