using Extensions.Core.Conversion;

namespace Extensions.Tests.ValueConversion
{
    public class ConversionExtensionTests<TDataType> : TestsBase
    {
        public ConvertedValue<TDataType> PerformConversion(string value)
        {
            return value.ConvertTo<TDataType>();
        }
    }
}
