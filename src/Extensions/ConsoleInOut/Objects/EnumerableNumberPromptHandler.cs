using System;
using System.Collections.Generic;

namespace Extensions.Core.ConsoleInOut.Objects
{
    public class EnumerableNumberPromptHandler<TDataType> : EnumerablePromptHandler<TDataType> where TDataType : IFormattable, IEquatable<TDataType>
    {
        public EnumerableNumberPromptHandler(IEnumerable<TDataType> choices, bool printVertically, bool printNumbered) : base(choices, printVertically, printNumbered){}

        public override TDataType Handle(string prompt, bool promptOnNewLine)
        {
            TDataType resultData = default(TDataType);
            string resultString = null;
            double value;
            do
            {
                resultString = base.HandleEnumerable(prompt, promptOnNewLine);
            } while (resultString == null || (Double.TryParse(resultString, out value)
                && !Double.IsNaN(value) && !Double.IsInfinity(value)
                && !base.ChoiceExists(resultData = ((TDataType)(object) value))));
            return resultData;
        }
    }
}
