using System;

namespace Extensions.Core.ConsoleInOut.Objects
{
    public class NumberPromptHandler<TDataType> : PromptHandler<TDataType> where TDataType : IFormattable, IEquatable<TDataType>
    {
        public double MinimumValue
        {
            get;
            set;
        }
        private double MaximumValue
        {
            get;
            set;
        }

        public NumberPromptHandler(double minimumValue, double maximumValue)
        {
            this.MinimumValue = minimumValue;
            this.MaximumValue = maximumValue;
        }

        public override TDataType Handle(string prompt, bool promptOnNewLine)
        {
            TDataType inputValue = default(TDataType);
            string result;
            double value = Double.MinValue;
            do
            {
                result = base.Prompt(prompt, promptOnNewLine);
            } while (result == null || (IsNumber(result, out value)
                && IsInRange(value, this.MinimumValue, this.MaximumValue)));
            inputValue = (TDataType)(object)value;
            return inputValue;
        }

        public bool IsInRange(double value, double minimumValue, double maximumValue)
        {
            return value >= minimumValue && value <= maximumValue
            && !Double.IsNaN(value) && !Double.IsInfinity(value);
        }

        public bool IsNumber(string value, out double resultingValue)
        {
            double numberValue = Double.MinValue;
            bool isNumber = !String.IsNullOrWhiteSpace(value)
                && Double.TryParse(value, out numberValue) 
                && !Double.IsNaN(numberValue) && !Double.IsInfinity(numberValue);
            resultingValue = numberValue;
            return isNumber;
        }
    }
}
