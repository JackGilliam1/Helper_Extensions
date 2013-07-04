namespace Extensions.Core.Conversion
{
    public class ConvertedValue<TDataType>
    {
        public TDataType Value { get; private set; }

        public bool WasConverted { get; private set; }

        public bool HasValue { get; private set; }

        public ConvertedValue()
        {
            HasValue = false;
            WasConverted = false;
        }
        
        public ConvertedValue(TDataType value)
        {
            Value = value;
            HasValue = true;
            WasConverted = true;
        }

    }
}
