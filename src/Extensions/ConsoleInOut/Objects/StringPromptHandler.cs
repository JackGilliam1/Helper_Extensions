namespace Extensions.Core.ConsoleInOut.Objects
{
    public class StringPromptHandler : PromptHandler<string>
    {
        public override string Handle(string prompt, bool promptOnNewLine)
        {
            return base.Prompt(prompt, promptOnNewLine);
        }
    }
}
