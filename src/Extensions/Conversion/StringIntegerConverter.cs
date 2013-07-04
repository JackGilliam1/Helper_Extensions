using System;

namespace Extensions.Core.Conversion
{
    public class StringIntegerConverter : INumberConverter
    {
        public bool IsNumber(object input)
        {
            bool isNumber = input != null && input.GetType().Equals(typeof(Int32));
            string stringInput;
            if (!isNumber && (stringInput = input as string) != null)
            {
                int value = default(int);
                isNumber = !String.IsNullOrWhiteSpace(stringInput) && Int32.TryParse(stringInput, out value);
            }
            return isNumber;
        }

        public object Convert(object input)
        {
            int output;
            if (input != null && input.GetType().Equals(typeof(Int32)))
            {
                output = (int)input;
            }
            else
            {
                output = Int32.Parse(input as String);
            }
            return output;
        }
    }
}
