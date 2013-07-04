using System.Collections.Generic;

namespace Extensions.Core.ConsoleInOut.Objects
{
    public class EnumerableStringPromptHandler : EnumerablePromptHandler<string>
    {
        public EnumerableStringPromptHandler(IEnumerable<string> choices, bool printVertically, bool printNumbered) : base(choices, printVertically, printNumbered)
        {}

        public override string Handle(string prompt, bool promptOnNewLine)
        {
            return base.HandleEnumerable(prompt, promptOnNewLine);
        }
    }
}
