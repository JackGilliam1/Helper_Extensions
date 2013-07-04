using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Extensions.Core.Collections;
using Extensions.Core.TextFunctions;

namespace Extensions.Core.ConsoleInOut
{
    /*
     * Author: Jack Gilliam
     * Date: 10/11/2011
     * Revised: 3/26/2012
     */
    /// <summary>
    /// Provides methods to prompt for input using the Console
    /// </summary>
    public static class ConsoleExtensions
    {
        private const string DEFAULT_NEW_LINE = "%r%n";
        private const string DEFAULT_NUMBERED_LIST_FORMAT = "{0}) ";

        private enum ErrorType
        {
            NumberFormat,
            OutOfRange,
            EmptyInput,
            PatternMismatch
        }

        private static Dictionary<ErrorType, string> _errorDictionary;
        private static Dictionary<ErrorType, string> ErrorDictionary
        {
            get
            {
                if (_errorDictionary == null)
                {
                    _errorDictionary = new Dictionary<ErrorType, string>();
                    _errorDictionary.Add(ErrorType.NumberFormat, "The wasn't one of the choices!");
                    _errorDictionary.Add(ErrorType.OutOfRange, "That wasn't in the range of choices!");
                    _errorDictionary.Add(ErrorType.EmptyInput, "You have to enter something!");
                    _errorDictionary.Add(ErrorType.PatternMismatch, "That doesn't match the pattern!");
                }
                return _errorDictionary;
            }
        }
        private const ConsoleColor ERROR_COLOR = ConsoleColor.Red;

        private static StringBuilder _builder;
        private static StringBuilder Builder
        {
            get
            {
                if (_builder == null)
                {
                    _builder = new StringBuilder();
                }
                else
                {
                    _builder.Clear();
                }
                return _builder;
            }
        }
        /// <summary>
        /// Prints the specified <paramref name="prompt"/>, then prompts the user for input that matches the specified <paramref name="pattern"/>
        /// </summary>
        /// <typeparam name="TDataType">The type of data expected to be returned</typeparam>
        /// <param name="prompt">The text to print to the console</param>
        /// <param name="pattern">The pattern to match the input to</param>
        /// <param name="removeWhiteSpaces">If true, all whitespaces will be removed before matching the input to the specified <paramref name="pattern"/></param>
        /// <param name="onNewLine">If true, the prompt will be printed on a new line</param>
        /// <returns>A value that matches the specified <paramref name="pattern"/></returns>
        public static TDataType PromptFor<TDataType>(string prompt, string pattern, bool removeWhiteSpaces = false, bool onNewLine = true)
        {
            string input = null;
            do
            {
                Write(prompt, onNewLine);
                input = ReadLine();
                if (removeWhiteSpaces)
                {
                    input = input.RemoveWhiteSpace(true);
                }
                if (String.IsNullOrWhiteSpace(input))
                {
                    Write(ErrorType.EmptyInput);
                    input = null;
                    continue;
                }
                else if (!Regex.IsMatch(input, pattern))
                {
                    Write(ErrorType.PatternMismatch);
                    input = null;
                    continue;
                }
            } while (input == null);
            TDataType value = input.ConvertTo<TDataType>();
            return value;
        }
        /// <summary>
        /// Prints the specified <paramref name="prompt"/> and the specified <paramref name="choices"/>, then prompts for input matching it to one of the choices
        /// </summary>
        /// <typeparam name="TDataType">The type of data expected to be returned</typeparam>
        /// <param name="prompt">The text to print to the console</param>
        /// <param name="choices">The choices to choose from</param>
        /// <param name="displayNumbered">If true, the choices will be printed in an ordered list, else in an unordered list</param>
        /// <param name="onNewLine">If true, the prompt will be printed on a new line</param>
        /// <returns>The choice chosen</returns>
        public static TDataType PromptFor<TDataType>(string prompt, IEnumerable<TDataType> choices, bool displayVertically = true, bool displayNumbered = false, bool onNewLine = true)
        {
            string input = null;
            int minValue = 1;
            int maxValue = choices.Count();
            int chosenIndex = minValue - 1;
            TDataType chosenValue = default(TDataType);
            do
            {
                Write(prompt, onNewLine);
                Write(choices, displayVertically, displayNumbered);
                input = ReadLine();
                if (!String.IsNullOrWhiteSpace(input))
                {
                    if (displayNumbered)
                    {
                        if (!Int32.TryParse(input, out chosenIndex))
                        {
                            Write(ErrorType.NumberFormat);
                            input = null;
                            continue;
                        }
                        else if (!chosenIndex.IsWithin(minValue, maxValue))
                        {
                            Write(ErrorType.OutOfRange);
                            input = null;
                            continue;
                        }
                        chosenValue = choices.Get(chosenIndex);
                    }
                    else
                    {
                        chosenValue = input.ConvertTo<TDataType>();
                    }
                }
                else
                {
                    Write(ErrorType.EmptyInput);
                    input = null;
                    continue;
                }
            } while (chosenValue == null || (chosenValue != null && !choices.Contains(chosenValue)));
            return chosenValue;
        }
        /// <summary>
        /// Prints the specified <paramref name="prompt"/>, then prompts for input within the specified range: [<paramref name="min"/>, <paramref name="max"/>]
        /// </summary>
        /// <typeparam name="TDataType">The type of data expected to be returned</typeparam>
        /// <param name="prompt">The text to print to the console</param>
        /// <param name="min">The minimum value that can be chosen</param>
        /// <param name="max">The maximum value that can be chosen</param>
        /// <param name="onNewLine">If true, the prompt will be printed on a new line</param>
        /// <returns>A value within the specified range: [<paramref name="min"/>, <paramref name="max"/>]</returns>
        public static TDataType PromptFor<TDataType>(string prompt, TDataType min, TDataType max, bool onNewLine = true) where TDataType : IComparable
        {
            TDataType value = default(TDataType);
            string input = null;
            do
            {
                Write(prompt, onNewLine);
                input = ReadLine();
                if ((value = input.ConvertTo<TDataType>()).IsNull())
                {
                    Write(ErrorType.NumberFormat);
                    continue;
                }
                else if (!value.IsWithin<TDataType>(min, max))
                {
                    Write(ErrorType.OutOfRange);
                    continue;
                }
            } while (value.IsNull());
            return value;
        }

        /// <summary>
        /// Prints the specified <paramref name="text"/> to the Console
        /// </summary>
        /// <param name="text">The text to print</param>
        /// <param name="newLine">If true, the text will print on a new line</param>
        public static void Write(string text, bool newLine = true)
        {
            if (newLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
        }
        /// <summary>
        /// Prints the specified <paramref name="text"/> with the specified <paramref name="foreColor"/> to the Console
        /// </summary>
        /// <param name="text">The text to print</param>
        /// <param name="foreColor">The font color of the text</param>
        public static void Write(string text, ConsoleColor foreColor, bool newLine = true)
        {
            Console.ForegroundColor = foreColor;
            Write(text, newLine);
            Console.ResetColor();
        }
        /// <summary>
        /// Prints the specified <paramref name="text"/> with the specified <paramref name="foreColor"/> and <paramref name="backColor"/> to the Console
        /// </summary>
        /// <param name="text">The text to print</param>
        /// <param name="foreColor">The Foreground color of the Console</param>
        /// <param name="backColor">The Background color of the Console</param>
        public static void Write(string text, ConsoleColor foreColor, ConsoleColor backColor, bool newLine = true)
        {
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            Write(text, newLine);
            Console.ResetColor();
        }
        /// <summary>
        /// Prints the specified <paramref name="list"/> to the Console
        /// </summary>
        /// <param name="list">A collection of objects to print</param>
        /// <param name="displayVertically">If true, the specified <paramref name="list"/> will be printed vertically, else horizontally</param>
        /// <param name="displayNumbered">If true, the specified <paramref name="list"/> will be printed as an ordered list, else it will be printed as an unordered list</param>
        public static void Write(IEnumerable list, bool displayVertically = true, bool displayNumbered = false)
        {
            if (displayNumbered)
            {
                list = list.ToNumbered(DEFAULT_NUMBERED_LIST_FORMAT);
            }
            foreach (string item in list)
            {
                Write(item, displayVertically);
            }
        }
        /// <summary>
        /// Prints the specified <paramref name="list"/> to the Console
        /// </summary>
        /// <param name="list">A collection of objects to print</param>
        /// <param name="foreColor">When printing to the Console, this will be the font color of the text</param>
        /// <param name="displayVertically">If true, the specified <paramref name="list"/> will be printed vertically, else horizontally</param>
        /// <param name="displayNumbered">If true, the specified <paramref name="list"/> will be printed as an ordered list, else it will be printed as an unordered list</param>
        public static void Write(IEnumerable list, ConsoleColor foreColor, bool displayVertically = true, bool displayNumbered = false)
        {
            IEnumerable<string> numberedList = list.ToNumbered(DEFAULT_NUMBERED_LIST_FORMAT);
            foreach (string item in numberedList)
            {
                Write(item, foreColor, displayVertically);
            }
        }
        /// <summary>
        /// Prints the specified <paramref name="list"/> to the Console
        /// </summary>
        /// <param name="list">A collection of objects to print</param>
        /// <param name="foreColor">When printing to the Console, this will be the font color of the text</param>
        /// <param name="backColor">When printing to the Console, this will be the background color</param>
        /// <param name="displayVertically">If true, the specified <paramref name="list"/> will be printed vertically, else horizontally</param>
        /// <param name="displayNumbered">If true, the specified <paramref name="list"/> will be printed as an ordered list, else it will be printed as an unordered list</param>
        public static void Write(IEnumerable list, ConsoleColor foreColor, ConsoleColor backColor, bool displayVertically = true, bool displayNumbered = false)
        {
            IEnumerable<string> numberedList = list.ToNumbered(DEFAULT_NUMBERED_LIST_FORMAT);
            foreach (string item in numberedList)
            {
                Write(item, foreColor, backColor, displayVertically);
            }
        }
        /// <summary>
        /// Prints the error message associated with the specified <paramref name="errorType"/> to the Console
        /// </summary>
        /// <param name="errorType">The association to the error message being printed</param>
        private static void Write(ErrorType errorType)
        {
            Write(ErrorDictionary[errorType], ERROR_COLOR);
        }

        //TODO: Move this method (ToNumbered) somewhere else as it doesn't belong in the ConsoleExtensions
        /// <summary>
        /// Converts the specified <paramref name="list"/> to a numbered Collection of Strings using the specified <paramref name="numberedFormat"/>
        /// </summary>
        /// <param name="list">A collection of objects</param>
        /// <param name="numberedFormat">The format to use when numbering</param>
        /// <returns>A collection of numbered string</returns>
        //// <param name="appendNewLines">If true, each String will have a new line appended to the end</param>
        public static IEnumerable<string> ToNumbered(this IEnumerable list, String numberedFormat)//, bool appendNewLines = true)
        {
            LinkedList<string> numbered = new LinkedList<string>();
            StringBuilder builder = Builder;
            int currentIndex = 1;
            foreach (var item in list)
            {
                builder.AppendFormat(numberedFormat, currentIndex);
                builder.Append(item);
                //if (appendNewLines)
                //{
                //    builder.AppendLine();
                //}
                numbered.AddLast(builder.ToString());
                builder.Clear();
                currentIndex += 1;
            }
            return numbered;
        }

        /// <summary>
        /// Reads the Console for input
        /// </summary>
        /// <returns>The input from the Console</returns>
        public static string ReadLine()
        {
            string input = Console.ReadLine();
            return input;
        }
        /// <summary>
        /// Reads the Console for input
        /// </summary>
        /// <param name="foreColor">When input is being entered, this will be the font color of the text</param>
        /// <returns>The input from the Console</returns>
        public static string ReadLine(ConsoleColor foreColor)
        {
            Console.ForegroundColor = foreColor;
            string input = ReadLine();
            Console.ResetColor();
            return input;
        }
        /// <summary>
        /// Reads the Console for input
        /// </summary>
        /// <param name="foreColor">When input is being entered, this is the font color of the text</param>
        /// <param name="backColor">When input is being entered, this is the background color behind the text</param>
        /// <returns>The input from the Console</returns>
        public static string ReadLine(ConsoleColor foreColor, ConsoleColor backColor)
        {
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            string input = ReadLine();
            Console.ResetColor();
            return input;
        }

        /// <summary>
        /// Indicates whether the specified <paramref name="value"/> contains the default value of the specified <paramref name="DataType"/>
        /// </summary>
        /// <typeparam name="DataType">The type of item being checked</typeparam>
        /// <param name="value">The value to check</param>
        /// <returns>True if the value is the default value of the specified <paramref name="DataType"/> or if the value is null</returns>
        public static bool IsDefault<DataType>(this DataType value)
        {
            var defaultValue = default(DataType);
            return !value.IsNull() && value.Equals(defaultValue);
        }
        /// <summary>
        /// Indicates whether the specified <paramref name="value"/> contains the value null
        /// </summary>
        /// <typeparam name="DataType">The type of item being checked</typeparam>
        /// <param name="value">The value being checked</param>
        /// <returns>True if the value is null</returns>
        public static bool IsNull<DataType>(this DataType value)
        {
            return value == null;
        }

        /// <summary>
        /// Indicates whether the specified <paramref name="value"/> is within the range: [<paramref name="min"/>, <paramref name="max"/>]
        /// </summary>
        /// <typeparam name="DataType">The type of the value being compared</typeparam>
        /// <param name="value">The value being verified</param>
        /// <param name="min">The smallest possible value that the specified <paramref name="value"/> can be</param>
        /// <param name="max">The largest possible value that the specified <paramref name="value"/> can be</param>
        /// <returns>True if the value is at or below the specified <paramref name="max"/> and at or above the specified <paramref name="min"/></returns>
        public static bool IsWithin<DataType>(this DataType value, DataType min, DataType max) where DataType : IComparable
        {
            int minComparison = value.CompareTo(min);
            int maxComparison = value.CompareTo(max);
            return minComparison >= 0 && maxComparison <= 0;
        }
        
        /////// <summary>
        /////// Indicates whether the specified <paramref name="value"/> is within the range: [<paramref name="min"/>, <paramref name="max"/>]
        /////// </summary>
        /////// <param name="value">The value being verified</param>
        /////// <param name="min">The smallest possible value that the specified <paramref name="value"/> can be</param>
        /////// <param name="max">The largest possible value that the specified <paramref name="value"/> can be</param>
        /////// <returns>True if the value is at or below the specified <paramref name="max"/> and at or above the specified <paramref name="min"/></returns>
        ////private static bool IsWithin(this int value, int min, int max)
        ////{
        ////    return value >= min && value <= max;
        ////}
        /////// <summary>
        /////// Indicates whether the specified <paramref name="value"/> is within the range: [<paramref name="min"/>, <paramref name="max"/>]
        /////// </summary>
        /////// <param name="value">The value being verified</param>
        /////// <param name="min">The smallest possible value that the specified <paramref name="value"/> can be</param>
        /////// <param name="max">The largest possible value that the specified <paramref name="value"/> can be</param>
        /////// <returns>True if the value is at or below the specified <paramref name="max"/> and at or above the specified <paramref name="min"/></returns>
        ////private static bool IsWithin(this float value, float min, float max)
        ////{
        ////    return value >= min && value <= max;
        ////}
        /////// <summary>
        /////// Indicates whether the specified <paramref name="value"/> is within the range: [<paramref name="min"/>, <paramref name="max"/>]
        /////// </summary>
        /////// <param name="value">The value being verified</param>
        /////// <param name="min">The smallest possible value that the specified <paramref name="value"/> can be</param>
        /////// <param name="max">The largest possible value that the specified <paramref name="value"/> can be</param>
        /////// <returns>True if the value is at or below the specified <paramref name="max"/> and at or above the specified <paramref name="min"/></returns>
        ////private static bool IsWithin(this double value, double min, double max)
        ////{
        ////    return value >= min && value <= max;
        ////}



        ////TODO: Go through the code below and remove things that are unneeded


        /////// <summary>
        /////// Prompts for input from the console after displaying <paramref name="prompt"/> then returns input converted to <paramref name="DataType"/>
        /////// </summary>
        /////// <typeparam name="DataType">The type of the returning object</typeparam>
        /////// <param name="prompt">The message to display in the console</param>
        /////// <param name="choices">Answers the user can choose from 
        /////// (These display as a vertical list of items, set this or <paramref name="limitedChoices"/> not both)</param>
        /////// <param name="regex">A pattern to match input against (null, for no regex)</param>
        /////// <param name="min">Minimum accepted value, for number types (null, for minimum of <see cref="Double.MinValue"/>)</param>
        /////// <param name="max">Maximum accepted value, for number types (null, for maximum of <see cref="Double.MaxValue"/>)</param>
        /////// <param name="choicesNumbered">When <paramref name="choices"/> is set, the options available will
        /////// be numbered when they are displayed in the console</param>
        /////// <param name="removeWhiteSpace">If true, the input from the console will have whitespaces removed</param>
        /////// <param name="choicesAreRequired">If true, console input must result as an option from <paramref name="choices"/>.
        /////// If false, console input is not required to be an option from <paramref name="choices"/>.<paramref name="choices"/></param>
        /////// <returns>Console input converted to the type of <paramref name="DataType"/></returns>
        ////public static DataType PromptWith<DataType>(this string prompt, string regex = null, IEnumerable<DataType> choices = null,
        ////    double min = Double.MinValue, double max = Double.MaxValue, bool choicesNumbered = false, bool removeWhiteSpace = false, bool choicesAreRequired = true)
        ////{
        ////    object userInput = null;
        ////    bool inputIsValid = false;
        ////    while (!inputIsValid)
        ////    {
        ////        var input = prompt.Ask(choices, displayNumbered: choicesNumbered, choicesRequired: choicesAreRequired);
        ////        try
        ////        {
        ////            userInput = ValidationExtensions.ValidateAndConvert<DataType>(input, choices, regex, min, max, removeSpaces: removeWhiteSpace);
        ////            if (!userInput.ToString().Equals(input.ToString()))
        ////            {
        ////                throw new Exception(userInput.ToString());
        ////            }
        ////            else
        ////            {
        ////                inputIsValid = userInput != null;
        ////            }
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            Console.WriteLine(e.Message);
        ////        }
        ////    }
        ////    return (DataType)userInput;
        ////}

        /////// <summary>
        /////// Prints <paramref name="source"/> to the console
        /////// </summary>
        /////// <typeparam name="DataType">The type of elements in <paramref name="source"/></typeparam>
        /////// <param name="source">The sequence to print</param>
        /////// <param name="printVertically">If true <paramref name="source"/> prints vertically, if false <paramref name="source"/> prints horizontally</param>
        /////// <param name="separatedBy">Text to seperate items in <paramref name="source"/> by</param>
        /////// <param name="newLine">If true <paramref name="source"/> begins printing on next line</param>
        ////public static void Print<T>(this IEnumerable<T> source, bool printVertically, string separatedBy = "", bool newLine = true, bool numbered = false)
        ////{
        ////    List<T> data = source.ToList();
        ////    string listNumber = "";
        ////    string toPrint = "";
        ////    if (printVertically)
        ////    {
        ////        for (int i = 0; i < data.Count(); i++)
        ////        {
        ////            if (numbered)
        ////            {
        ////                listNumber = (i + 1) + ") ";
        ////            }
        ////            toPrint = listNumber + data[i].ToString();
        ////            if (!newLine && i < 1)
        ////            {
        ////                toPrint = "\n" + toPrint;
        ////                continue;
        ////            }
        ////            Console.WriteLine(toPrint);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        for (int i = 0; i < data.Count(); i++)
        ////        {
        ////            if (numbered)
        ////            {
        ////                listNumber = (i + 1) + ") ";
        ////            }
        ////            toPrint = listNumber + data[i].ToString() + separatedBy;
        ////            if (newLine && i < 1)
        ////            {
        ////                toPrint = "\n" + toPrint;
        ////                continue;
        ////            }
        ////            if (CollectionExtensions.HasLastOf(data, data[i]))
        ////            {
        ////                toPrint = listNumber + data[i].ToString();
        ////            }
        ////            Console.Write(toPrint);
        ////        }
        ////    }
        ////}

        /////// <summary>
        /////// Prompts for input with <paramref name="prompt"/>
        /////// </summary>
        /////// <param name="prompt">Question being asked</param>
        /////// <param name="choices">Choices that can be chosen</param>
        /////// <returns>Console input</returns>
        ////public static string Ask<DataType>(this string prompt, IEnumerable<DataType> choices = null, bool displayVertically = true, bool displayNumbered = false, bool choicesRequired = false)
        ////{
        ////    prompt.Print(true);
        ////    if (choices != null)
        ////    {
        ////        choices.Print(displayVertically, numbered: displayNumbered);
        ////    }
        ////    return Console.ReadLine();
        ////}

        ////private static Dictionary<String, String> _errorMessages = new Dictionary<String, String>()
        ////{
        ////    {"number", "That is not a number"},
        ////    {"input", "\nThat is not what I asked for!\n"},
        ////};
        ////private static Dictionary<String, String> _formats = new Dictionary<String, String>()
        ////{
        ////    {"number", "\\d{"},
        ////    {"range", "{0},{1}"},
        ////};
        //private static String NUMBER_FORMAT = "\\d{";//_formats["number"];
        //private static String RANGE_FORMAT = "{0},{1}";//_formats["range"];

        //private const int DEFAULT_NUM_OF_DIGITS = 1;
        //private static String DEFAULT_INPUT_ERROR = _errorMessages["input"];
        //private static String DEFAULT_NUMBER_INPUT_ERROR = _errorMessages["number"];

        //private const ConsoleColor DEFAULT_ERROR_COLOR = ConsoleColor.Red;
        //private const ConsoleColor DEFAULT_FOREGROUND_COLOR = ConsoleColor.Gray;
        //private const ConsoleColor DEFAULT_BACKGROUND_COLOR = ConsoleColor.Black;

        //public static ConsoleColor ForeColor
        //{
        //    get
        //    {
        //        return Console.ForegroundColor;
        //    }
        //    set
        //    {
        //        Console.ForegroundColor = value;
        //    }
        //}
        //public static ConsoleColor BackColor
        //{
        //    get
        //    {
        //        return Console.BackgroundColor;
        //    }
        //    set
        //    {
        //        Console.BackgroundColor = value;
        //    }
        //}

        //public static String PromptForInput(String message, String pattern, String inputErrorMessage)
        //{
        //    String input = null;
        //    //String inputError = GetDefaultIfNull(inputErrorMessage, DEFAULT_INPUT_ERROR);
        //    do
        //    {
        //        if (input != null)
        //        {
        //            //PrintError(inputError);
        //        }
        //        Write(message);
        //        input = ReadLine();
        //    } while (!Matches(input, pattern));
        //    return input;
        //}

        //public static int PromptForInteger(String prompt, String inputError, int numOfDigits = DEFAULT_NUM_OF_DIGITS)
        //{
        //    int number = 0;
        //    if (numOfDigits >= 1)
        //    {
        //        //String numInputError = GetDefaultIfNull(prompt, DEFAULT_NUMBER_INPUT_ERROR);
        //        String range = String.Format(RANGE_FORMAT, DEFAULT_NUM_OF_DIGITS, numOfDigits);
        //        String numberFormat = NUMBER_FORMAT + range + "}";
        //        String input = PromptForInput(prompt, numberFormat, DEFAULT_NUMBER_INPUT_ERROR);
        //        number = Int32.Parse(input);
        //    }
        //    return number;
        //}

        //public static void PrintError(String errorMessage, ConsoleColor errorColor = default(ConsoleColor))
        //{
        //    ConsoleColor errColor = errorColor;
        //    if (errorColor.Equals(default(ConsoleColor)))
        //    {
        //        errColor = DEFAULT_ERROR_COLOR;
        //    }
        //    Console.ForegroundColor = errColor;
        //    Write(errorMessage);
        //    Console.ResetColor();
        //}

        //public static void Clear()
        //{
        //    Console.Clear();
        //}

        //public static void ResetColor()
        //{
        //    Console.ResetColor();
        //}
        //private static T GetDefaultIfNull<T>(T value, T defaultValue)
        //{
        //    T resultValue = value;
        //    if (GenericExtensions.IsEmpty<T>(resultValue))
        //    {
        //        resultValue = defaultValue;
        //    }
        //    return resultValue;
        //}

        //private static Boolean Matches(String text, String pattern)
        //{
        //    Boolean matches = false;
        //    if (text != null)
        //        matches = TextExtensions.CompletelyMatches(text, pattern);
        //    return matches;
        //}
    }
}