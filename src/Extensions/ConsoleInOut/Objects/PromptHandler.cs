using System;

namespace Extensions.Core.ConsoleInOut.Objects
{
    public abstract class PromptHandler<TDataType> : IPromptHandler<TDataType>
    {
        public TDataType Handle(string prompt)
        {
            return this.Handle(prompt, false);
        }
        public abstract TDataType Handle(string prompt, bool promptOnNewLine);

        protected void Print(string message, bool onNewLine = false)
        {
            if (message != null)
            {
                if (onNewLine)
                {
                    Console.WriteLine(message);
                }
                else
                {
                    Console.Write(message);
                }
            }
        }

        protected string Prompt(string message, bool onNewLine = false)
        {
            this.Print(message, onNewLine);
            string input = Console.ReadLine();
            return input;
        }
    }
}
