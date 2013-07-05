using System;
using Extensions.Core.Generics;
using Extensions.Core.Types;

namespace Extensions.Core.Numeric
{
    /*
     * Author: Jack Gilliam
     * Date Created: 4/2/2012
     */
    public static class NumericExtensions
    {
        /// <summary>
        /// Indicates whether <paramref name="value"/> is equal to zero
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if <paramref name="value"/> is zero.
        /// False if <paramref name="value"/> is not zero.
        public static bool IsZero(this double value)
        {
            bool isZero = GenericExtensions.SameAs(value, (double)0.0);
            return isZero;
        }

        /// <summary>
        /// Indicates whether <paramref name="numberOne"/> is the negative of <paramref name="numberTwo"/>, or vice versa
        /// </summary>
        /// <param name="numberOne">Number one</param>
        /// <param name="numberTwo">Number two</param>
        /// <returns>True if <paramref name="numberOne"/> is the negative of <paramref name="numberTwo"/>.
        /// False if <paramref name="numberOne"/> is not the negative of <paramref name="numberTwo"/>.</returns>
        public static bool IsOppositeOf(this double numberOne, double numberTwo)
        {
            bool isOpposite = (numberOne * -1) == (numberTwo);
            return isOpposite;
        }

        /// <summary>
        /// Indicates whether <paramref name="value"/> is of a numeric type
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="value"/></typeparam>
        /// <param name="value">The value to check</param>
        /// <returns>True if <paramref name="value"/> is of a numeric type
        /// False if <paramref name="value"/> is not numeric.
        /// False if <paramref name="value"/> is null.</returns>
        public static bool IsNumeric<T>(this T value)
        {
            bool isNumeric = false;
            if (GenericExtensions.IsSomething(value))
            {
                Type type = value as Type;
                if (GenericExtensions.IsEmpty(type))
                {
                    type = value.GetType();
                }
                string name = "Parse";
                isNumeric = TypeExtensions.HasMethod(type, name);
            }
            return isNumeric;
        }
    }
}
