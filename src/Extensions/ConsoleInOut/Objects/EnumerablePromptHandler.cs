using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.Core.ConsoleInOut.Objects
{
    public abstract class EnumerablePromptHandler<TDataType> : PromptHandler<TDataType>
    {
        public IEnumerable<TDataType> Choices
        {
            get;
            set;
        }

        public bool ChoicesPrintedVertically
        {
            get;
            set;
        }
        public bool ChoicesNumbered
        {
            get;
            set;
        }
        
        public EnumerablePromptHandler(IEnumerable<TDataType> choices, bool choicesPrintedVertically, bool choicesNumbered)
        {
            this.Choices = choices;
            this.ChoicesPrintedVertically = choicesPrintedVertically;
            this.ChoicesNumbered = choicesNumbered;
        }

        private IEnumerable<TDataType> previousChoices;
        private IEnumerable<String> previousStringChoices;

        public bool ChoiceExists(TDataType choice)
        {
            return Choices.Contains<TDataType>(choice);
        }

        public virtual string HandleEnumerable(string prompt, bool promptOnNewLine)
        {
            PrintEnumerable(Choices, ChoicesPrintedVertically, ChoicesNumbered);
            if (ChoicesNumbered)
            {
                double minimumValue = 1;
                double maximumValue = Choices.Count();
                IPromptHandler<int> promptHandler = new NumberPromptHandler<int>(minimumValue, maximumValue);
                return Choices.ElementAt(promptHandler.Handle(prompt, promptOnNewLine)).ToString();
            }
            else
            {
                IPromptHandler<string> promptHandler = new StringPromptHandler();
                string result = null;
                do {
                    result = base.Prompt(prompt, promptOnNewLine);
                } while(result == null || (!String.IsNullOrWhiteSpace(result)));
                return result;
            }
        }

        protected void PrintEnumerable(IEnumerable<TDataType> choices, bool printVertically, bool printNumbered)
        {
            if (previousChoices == null || choices.Equals(previousChoices))
            {
                previousChoices = choices;
                previousStringChoices = toPrintableList(choices, printNumbered);
            }
            if (printVertically)
            {
                foreach (string choice in previousStringChoices)
                {
                    Console.WriteLine(choice);
                }
            }
            else
            {
                foreach (string choice in previousStringChoices)
                {
                    Console.Write(choice);
                }
            }
        }

        private IEnumerable<string> toPrintableList(IEnumerable<TDataType> printableData, bool numbered)
        {
            List<string> stringData = new List<string>();
            if (numbered)
            {
                const String format = "{0}){1}";
                ICollection<TDataType> collectionData = (ICollection<TDataType>)printableData;
                for (int i = 1; i <= collectionData.Count; i += 1)
                {
                    stringData.Add(String.Format(format, i, collectionData.ElementAt<TDataType>(i - 1)));
                }
            }
            else
            {
                foreach (TDataType data in printableData)
                {
                    stringData.Add(data.ToString());
                }
            }
            return stringData;
        }
    }
}
