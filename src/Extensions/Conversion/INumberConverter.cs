namespace Extensions.Core.Conversion
{
    public interface INumberConverter
    {
        object Convert(object input);

        bool IsNumber(object input);
    }
}
