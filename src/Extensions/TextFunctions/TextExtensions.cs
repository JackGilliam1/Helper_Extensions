using Extensions.Core.Types;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Extensions.Core.TextFunctions
{
    /*
     * Author: Jack Gilliam
     * Date Created: 4/2/2012
     */
    /// <summary>
    /// Provides methods to manipulate strings
    /// </summary>
    public static class TextExtensions
    {
        /// <summary>
        /// Indicates whether <paramref name="text"/> can be converted to a number
        /// </summary>
        /// <param name="text">The text to check</param>
        /// <returns>True if <paramref name="text"/> can be converted to a number</returns>
        public static bool CanConvertToNumber(this string text)
        {
            double temp = 0;
            bool canConvertToNumber = Double.TryParse(text, out temp);
            return canConvertToNumber;
        }

        /// <summary>
        /// Converts <paramref name="data"/> to <paramref name="DataType"/>
        /// </summary>
        /// <typeparam name="DataType">The type to convert <paramref name="data"/> to</typeparam>
        /// <param name="data">The text to convert</param>
        /// <returns><paramref name="data"/> converted into the type <paramref name="DataType"/></returns>
        public static dynamic ConvertTo<DataType>(this string data)
        {
            dynamic resultingValue = null;
            var convertType = typeof(DataType);
            //DataType resultingValue = default(DataType);
            double temp;
            if (TypeExtensions.HasMethod(convertType, "Parse")
                && Double.TryParse(data, out temp))
            {
                resultingValue = TypeExtensions.Meth(convertType, "Parse")
                    .Invoke(convertType, new[] { data })
                    .CastAs<DataType>();
            }
            else if (!TypeExtensions.HasMethod(convertType, "Parse"))
            {
                resultingValue = data.CastAs<DataType>();
            }
            else
            {
                resultingValue = data + " is not a valid number!";
            }
            return resultingValue;
        }

        /// <summary>
        /// Indicates whether <paramref name="text"/> matches the pattern completely
        /// </summary>
        /// <param name="text">The text to match</param>
        /// <param name="pattern">The pattern to match</param>
        /// <returns>True if text matches the pattern completely.
        /// False if <paramref name="text"/> does not match.
        /// False if <paramref name="text"/> or <paramref name="pattern"/> is null</returns>
        public static bool CompletelyMatches(this string text, string pattern)
        {
            bool isCompleteMatch = false;
            if (text != null && pattern != null)
            {
                string replacedText = Regex.Replace(text, pattern, "");
                isCompleteMatch = replacedText.Equals("");
            }
            return isCompleteMatch;
        }

        /// <summary>
        /// Matches a string to regex
        /// </summary>
        /// <param name="data">Data to match</param>
        /// <param name="pattern">Pattern to match against</param>
        /// <param name="option">Options</param>
        /// <returns>True if the pattern is found within the text</returns>
        public static bool Matches(this string text, string pattern, RegexOptions option = RegexOptions.None)
        {
            return Regex.IsMatch(text, pattern, option);
        }

        #region ToFunction
        static TextExtensions()
        {
            providerOptions = new Dictionary<string, string>();
            providerOptions.Add("CompilerVersion", "v4.0");
            provider = new CSharpCodeProvider(providerOptions);
            parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                parameters.ReferencedAssemblies.Add(entryAssembly.GetName().Name + ".exe");
            }
        }

        static Dictionary<string, string> providerOptions;

        static CSharpCodeProvider provider;
        static CompilerParameters parameters;

        /// <summary>
        /// Creates a method that accepts <paramref name="methodParameters"/>, using the parameters (if any) it will perform <paramref name="methodBody"/>
        /// Then it returns the type of <paramref name="returnType"/>
        /// <remarks>(WRITE THESE STRING PARAMETERS IN C# SYNTAX, INCLUDE THE REQUIRED USING STATEMENTS AS WELL)</remarks>
        /// <param name="methodBody">The method body</param>
        /// <param name="returnType">The method return type</param>
        /// <param name="methodParameters">The parameters the method accepts</param>
        /// <example> This sample shows how to call the <see cref="TextExts.ToFunction"/> method
        /// <code>
        /// public class TestClass
        /// {
        ///     public static void Main()
        ///     {
        ///         string methodBody = @"
        ///             int result = 0;
        ///             if(listOfNumbers == null) 
        ///             {
        ///                 result = numberOne * numberTwo;
        ///             }
        ///             else
        ///             {
        ///                 result = listOfNumbers[0];
        ///             }
        ///             return result;
        ///         ";
        ///         string methodParameters = "int numberOne, int numberTwo, System.Collections.Generic.List<int> listOfNumbers";
        ///         string methodReturnType = "int";
        ///         int firstNumber = 2;
        ///         int secondNumber = 3;
        ///         List&lt;int&gt; listOfNumbers = null;
        ///         try
        ///         {
        ///             MethodInfo method = methodBody.ToFunction(methodReturnType, methodParameters);
        ///             int result = Int32.Parse(method.Invoke(null, new object[] { firstNumber, secondNumber, listOfNumbers }).ToString());
        ///         }
        ///         catch (Exception e)
        ///         {
        ///             Console.WriteLine(e);
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <returns>A <see cref="System.ReflectionInfo.MethodInfo"/> object that can be invoked</returns>
        /// <exception>Thrown if there were errors when creating the method</exception>
        public static MethodInfo ToFunction(this string methodBody, string methodReturnType, string methodParameters)
        {
            MethodInfo returnMethod = null;
            string functionToCall = @"
                                public class Evaluator
                                {
                                    public static " + methodReturnType + @" Evaluate(" + methodParameters + @")
                                    {
                                        " + methodBody + @"
                                    }
                                }";
            CompilerResults results = provider.CompileAssemblyFromSource
                (
                    parameters, functionToCall
                );
            if (results.Errors.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (CompilerError err in results.Errors)
                {
                    builder.Append("\n" + err.ToString());
                }
                throw new Exception("There were errors here they are: " + builder.ToString());
            }
            else
            {
                Type type = results.CompiledAssembly.GetType("Evaluator");
                MethodInfo method = type.GetMethod("Evaluate");
                returnMethod = method;
            }
            return returnMethod;
        }
        #endregion

        /// <summary>
        /// Removes white spaces from <paramref name="text"/>
        /// </summary>
        /// <param name="text">Text to remove white space from</param>
        /// <returns><paramref name="text"/> without white spaces</returns>
        public static string RemoveWhiteSpace(this string text, bool includeNewLines = true)
        {
            string result = null;
            if (includeNewLines)
            {
                result = Regex.Replace(text, "\\s", "");
            }
            else
            {
                result = Regex.Replace(text, "[ ]", "");
            }
            return result;
        }

        #region Private Methods

        /// <summary>
        /// Casts <paramref name="obj"/> to type <paramref name="CastType"/>
        /// </summary>
        /// <typeparam name="CastType">The type to cast to</typeparam>
        /// <param name="obj">The object to cast</param>
        /// <returns>A casted object</returns>
        private static CastType CastAs<CastType>(this object obj)
        {
            return (CastType)obj;
        }

        #endregion
    }
}
