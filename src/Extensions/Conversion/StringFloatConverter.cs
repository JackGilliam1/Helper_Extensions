using System;

namespace Extensions.Core.Conversion
{
    public class StringFloatConverter : INumberConverter
    {
        public bool IsNumber(object input)
        {
            bool isNumber = input != null && input.GetType().Equals(typeof(Single));
            string stringInput;
            if (input != null && (stringInput = input as String) != null)
            {
                float value;
                isNumber = !String.IsNullOrWhiteSpace(stringInput) && Single.TryParse(stringInput, out value);
            }
            return isNumber;
        }

        public object Convert(object input)
        {
            float output;
            if (input != null && input.GetType().Equals(typeof(Single)))
            {
                output = (float)input;
            }
            else
            {
                output = Single.Parse(input as String);
            }
            return output;
        }
    }
}
