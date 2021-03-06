﻿using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.Core.Conversion;
using Extensions.Core.Generics;
using Extensions.Core.Numeric;

//using Extensions.Types;

namespace Extensions.Core.TextFunctions
{
    /*
     * Author: Jack Gilliam
     * Date: 10/11/2011
     * Revised: 3/26/2012
     */
    /// <summary>
    /// Provides methods to validate and convert text
    /// </summary>
    public static class ValidationExtensions
    {
        #region Constants
        private const double DEFAULT_MIN_VALUE = Double.MinValue;
        private const double DEFAULT_MAX_VALUE = Double.MaxValue;
        private static Dictionary<string, string> _errors = new Dictionary<string, string>()
        {
            {"empty", "Error: Nothing was put in!"},
            {"notvalid", "Error: That is not valid!"},
            {"notvalidnum", "Error: The input is not a valid number!"},
            {"notgiven", "Error: That is not one of the answers!"},
            {"outofrange", "Error: That value is not in the correct range"},
        };
        //private static Dictionary<string, string> _patterns = new Dictionary<string,string>()
        //{
        //    {"numdec", "(\\-?\\d+(?:\\.\\d+)?)"}
        //};
        #endregion Constants

        public static bool IsValid<TDataType>(
            this string data,
            out ConvertedValue<TDataType> convertedValue, bool removeSpaces = false)
        {
            var isValid = false;
            if(!data.IsEmpty())
            {
                if (removeSpaces)
                {
                    data = data.Replace(" ", "");
                }
                convertedValue = data.ConvertTo<TDataType>();
                if (convertedValue.HasValue)
                {
                    isValid = true;
                }
            }
            else
            {
                convertedValue = new ConvertedValue<TDataType>();
            }
            return isValid;
        }

        public static bool IsValid<TDataType>(
            this string data,
            IEnumerable<TDataType> choices,
            out ConvertedValue<TDataType> convertedValue, bool removeSpaces = false)
        {
            var isValid = false;
            if (!data.IsEmpty())
            {
                if (removeSpaces)
                {
                    data = data.Replace(" ", "");
                }
                convertedValue = data.ConvertTo<TDataType>();
                if (convertedValue.HasValue)
                {
                    if (choices != null && choices.Contains(convertedValue.Value))
                    {
                        isValid = true;
                    }
                }
            }
            else
            {
                convertedValue = new ConvertedValue<TDataType>();
            }
            return isValid;
        }

        public static bool IsValid<TDataType>(
            this string data,
            out ConvertedValue<TDataType> convertedValue, double min = DEFAULT_MIN_VALUE, double max = DEFAULT_MAX_VALUE, string pattern = null)
        {
            var isValid = false;
            convertedValue = new ConvertedValue<TDataType>();
            if (!data.IsEmpty())
            {
                if (pattern == null || data.CompletelyMatches(pattern))
                {
                    convertedValue = data.ConvertTo<TDataType>();
                    if (convertedValue.HasValue)
                    {
                        if (((double)(object)convertedValue.Value).InRange(min, max))
                        {
                            isValid = true;
                        }
                    }
                }
            }
            return isValid;
        }

        public static bool IsValid(this string data, string pattern, out ConvertedValue<string> convertedValue, bool removeSpaces = false)
        {
            var isValid = false;
            convertedValue = new ConvertedValue<string>();
            if (!data.IsEmpty())
            {
                if (removeSpaces)
                {
                    data = data.Replace(" ", "");
                }
                if (data.CompletelyMatches(pattern))
                {
                    convertedValue = new ConvertedValue<string>(data);
                    isValid = true;
                }
            }
            return isValid;
        }

        /// <summary>
        /// Validates <paramref name="text"/> using the parameters sent in. The <paramref name="text"/>, if valid, is converted to type <paramref name="DataType"/>
        /// </summary>
        /// <typeparam name="TDataType">The return type</typeparam>
        /// <param name="text">The string to convert</param>
        /// <param name="convertedValue"></param>
        /// <param name="choices">Answers available, (Required if <paramref name="choicesAreRequired"/>)</param>
        /// <param name="pattern">A pattern to match <paramref name="text"/> against (null, for no regex)</param>
        /// <param name="min">When asking for a numeric type, <paramref name="text"/> must be at or above this value
        /// (Default: <see cref="Double.MinValue"/>)</param>
        /// <param name="max">When asking for a numeric type, <paramref name="text"/> must be at or below this value
        /// (Default: <see cref="Double.MaxValue"/>)</param>
        /// <param name="removeSpaces">If set to true, <paramref name="text"/> will be matched without whitespace</param>
        /// <param name="choicesAreRequired">If true, <paramref name="text"/> must be contained within <paramref name="choices"/>.</param>
        /// <returns>If valid, <paramref name="text"/> converted to the type <paramref name="DataType"/>.
        /// Otherwise an error message.</returns>
        public static string ValidateAndConvert<TDataType>(this string text, out ConvertedValue<TDataType> convertedValue,
            IEnumerable<TDataType> choices = null, string pattern = null,
            double min = DEFAULT_MIN_VALUE, double max = DEFAULT_MAX_VALUE,
            bool removeSpaces = false, bool choicesAreRequired = false)
        {
            string result = null;
            convertedValue = new ConvertedValue<TDataType>();
            if (!text.IsEmpty())
            {
                if ((result = MinMaxAreValid(min, max)) == null)
                {
                    if ((result = MatchPattern<TDataType>(text, pattern, removeSpaces)) == null)
                    {
                        var value = As<TDataType>(text, min, max);
                        if (choicesAreRequired && choices != null)
                        {
                            var answerAvailable = choices.Contains(value.Value);
                            if (!answerAvailable)
                            {
                                result = _errors["notgiven"];
                            }
                        }
                        if (result == null)
                        {
                            convertedValue = value;
                        }
                    }
                }
            }
            else
            {
                result = _errors["empty"];
            }
            return result;
        }

        /// <summary>
        /// Converts a string
        /// </summary>
        public static ConvertedValue<TDataType> As<TDataType>(string data, double min = DEFAULT_MIN_VALUE, double max = DEFAULT_MAX_VALUE)
        {
            Type expectType = typeof(TDataType);
            bool isNumericType = expectType.IsNumeric();
            var answer = data.ConvertTo<TDataType>();
            if (isNumericType)
            {
                if (!ValueExtensions.InRange(answer.Value, min, max))
                {
                    answer = new ConvertedValue<TDataType>();
                }
            }
            return answer;
        }

        /// <summary>
        /// Checks <paramref name="text"/> for a complete match to <paramref name="pattern"/>
        /// </summary>
        /// <param name="text">The string to match</param>
        /// <param name="pattern">The regular expression pattern to match</param>
        /// <param name="removeSpaces">If true, <paramref name="text"/> will be matched without whitespace</param>
        private static string MatchPattern<TDataType>(string text, string pattern, bool removeSpaces = false)
        {
            string result = null;
            string fixedText = text;
            if (removeSpaces)
            {
                fixedText = TextExtensions.RemoveWhiteSpace(fixedText);
            }
            if (!GenericExtensions.IsEmpty(pattern)
                && !GenericExtensions.IsEmpty(fixedText) 
                && !TextExtensions.CompletelyMatches(fixedText, pattern))
            {
                result = _errors["notvalid"];
            }
            return result;
        }

        /// <summary>
        /// Checks for a valid minimum and maximum
        /// </summary>
        /// <param name="min">Minimum</param>
        /// <param name="max">Maximum</param>
        private static string MinMaxAreValid(double min, double max)
        {
            string result = null;
            if (min > max)
            {
                result = "Minimum value is greater than the maximum value";
            }

            if (min == max)
            {
                result = "The min and max are the same values";
            }
            return result;
        }

        ///// <summary>
        ///// Indicates whether the specified data is within the minimum and maximum specified
        ///// </summary>
        ///// <param name="value">Value to validate</param>
        ///// <param name="min">Smallest possible value</param>
        ///// <param name="max">Largest possible value</param>
        //private static dynamic IsInRange<TDataType>(TDataType value, double min, double max)
        //{
        //    dynamic result = null;
        //    bool inRange = value.InRange(min, max);
        //    if (!inRange)
        //    {
        //        result = _errors["outofrange"];
        //    }
        //    else
        //    {
        //        result = value;
        //    }
        //    return result;
        //}

        /// <summary>
        /// Checks <paramref name="value"/> to see if it in <paramref name="choices"/>
        /// </summary>
        /// <param name="value">Value to locate</param>
        /// <param name="choices">Accepted Answers</param>
        private static bool AnswerIsAvailable<TDataType>(TDataType value, IEnumerable<TDataType> choices)
        {
            var answerAvailable = !(choices != null && !choices.Contains(value));
            return answerAvailable;
        }
    }
}
