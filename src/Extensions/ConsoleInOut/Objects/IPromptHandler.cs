namespace Extensions.Core.ConsoleInOut.Objects
{
    public interface IPromptHandler<TDataType>
    {
        TDataType Handle(string prompt);
        TDataType Handle(string prompt, bool promptOnNewLine);
    }
}
