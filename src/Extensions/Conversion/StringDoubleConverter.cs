using System;

namespace Extensions.Core.Conversion
{
    public class StringDoubleConverter : INumberConverter
    {
        public bool IsNumber(object input)
        {
            bool isNumber = input != null && input.GetType().Equals(typeof(Double));
            string stringInput;
            if (input != null && (stringInput = input as String) != null)
            {
                double value;
                isNumber = !String.IsNullOrWhiteSpace(stringInput) && Double.TryParse(stringInput, out value);
            }
            return isNumber;
        }

        public object Convert(object input)
        {
            double output;
            if (input != null && input.GetType().Equals(typeof(Double)))
            {
                output = (double)input;
            }
            else
            {
                output = Double.Parse(input as String);
            }
            return output;
        }
    }
}
