using System;
using System.Linq;
using Extensions.Core.Types;

namespace Extensions.Core.Generics
{
    /*
     * Author: Jack Gilliam
     * Date Created: 4/2/2012
     */
    public static class GenericExtensions
    {
        /// <summary>
        /// Indicates whether <paramref name="objectOne"/> is equal to <paramref name="objectTwo"/>
        /// </summary>
        /// <typeparam name="T">The types of <paramref name="objectOne"/> and <paramref name="objectTwo"/></typeparam>
        /// <param name="objectOne">Object one</param>
        /// <param name="objectTwo">Object two</param>
        /// <returns>True if <paramref name="objectOne"/> is the same as <paramref name="objectTwo"/>, 
        /// False if <paramref name="objectOne"/> is not the same as <paramref name="objectTwo"/> or if
        /// either <paramref name="objectOne"/> or <paramref name="objectTwo"/> are null</returns>
        public static bool SameAs<T>(this T objectOne, T objectTwo)
        {
            bool areEqual = false;
            if (objectOne.IsSomething() && objectTwo.IsSomething())
            {
                Type type = objectOne as Type;
                if (type.IsEmpty())
                {
                    type = objectOne.GetType();
                }
                if (type.GetInterface("ICollection") != null)// && (type as IEnumerable) != null)
                {
                    dynamic firstDyn = objectOne;
                    dynamic secondDyn = objectTwo;
                    if (Enumerable.Count(firstDyn).Equals(Enumerable.Count(secondDyn)))
                    {
                        for (int i = 0; i < Enumerable.Count(firstDyn); i++)
                        {
                            dynamic item1 = Enumerable.ToList(firstDyn)[i];
                            dynamic item2 = Enumerable.ToList(secondDyn)[i];
                            areEqual = GenericExtensions.SameAs(item1, item2);
                            if (!areEqual)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    areEqual = objectOne.Equals(objectTwo);
                }
            }
            return areEqual;
        }

        /// <summary>
        /// Indicates whether <paramref name="objectOne"/> is larger than <paramref name="objectTwo"/> (Determined using the ToString method)
        /// </summary>
        /// <typeparam name="T">The types of <paramref name="objectOne"/> and <paramref name="objectTwo"/></typeparam>
        /// <param name="objectOne">Object one</param>
        /// <param name="objectTwo">Object two</param>
        /// <returns>True if <paramref name="objectOne"/> is larger than <paramref name="objectTwo"/>, 
        /// False if <paramref name="objectOne"/> is smaller or equal to <paramref name="objectTwo"/> 
        /// or if either <paramref name="objectOne"/> or <paramref name="objectTwo"/> are null</returns>
        public static bool IsLargerThan<T>(this T objectOne, T objectTwo)
        {
            bool isLarger = objectOne.IsSomething()
                && objectTwo.IsSomething()
                && objectOne.ToString().CompareTo(objectTwo.ToString()) > 0;
            return isLarger;
        }

        /// <summary>
        /// Indicates whether <paramref name="objectOne"/> is smaller than <paramref name="objectTwo"/> (Determined using the ToString method)
        /// </summary>
        /// <typeparam name="T">The types of <paramref name="objectOne"/> and <paramref name="objectTwo"/></typeparam>
        /// <param name="objectOne">Object one</param>
        /// <param name="objectTwo">Object two</param>
        /// <returns>True if <paramref name="objectOne"/> is smaller than <paramref name="objectTwo"/>, 
        /// False if <paramref name="objectOne"/> is larger or equal to <paramref name="objectTwo"/> 
        /// or if either <paramref name="objectOne"/> or <paramref name="objectTwo"/> are null</returns>
        public static bool IsSmallerThan<T>(this T objectOne, T objectTwo)
        {
            bool isSmaller = objectOne.IsSomething()
                && objectTwo.IsSomething()
                && objectOne.ToString().CompareTo(objectTwo.ToString()) < 0;
            return isSmaller;
        }

        /// <summary>
        /// Indicates whether <paramref name="value"/> is null, empty, equal to zero, 
        /// or consisting only of whitespace.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if <paramref name="value"/> is null, empty, equal to zero,
        /// or if <paramref name="value"/> consists only of whitespace.</returns>
        public static bool IsEmpty<T>(this T value, bool includeZero = false)
        {
            bool isEmpty = value == null
                || (value != null
                && (String.IsNullOrWhiteSpace(value.ToString())
                || (includeZero 
                && TypeExtensions.IsNumeric(value)
                && Int32.Parse(value.ToString()).Equals(0))));
            return isEmpty;
        }

        /// <summary>
        /// Indicates whether <paramref name="value"/> holds data
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if <paramref name="value"/> holds data.</returns>
        public static bool IsSomething(this object value, bool includeZero = false)
        {
            bool isSomething = !IsEmpty(value, includeZero);
            return isSomething;
        }

        /// <summary>
        /// Out of <paramref name="objOne"/> and <paramref name="objTwo"/>, the one that is not null is returned
        /// </summary>
        /// <typeparam name="T">The type of the values</typeparam>
        /// <param name="objOne">Object One</param>
        /// <param name="objTwo">Object Two</param>
        /// <returns>The nonnull object out of either <paramref name="objOne"/> and <paramref name="objTwo"/>.
        /// If both <paramref name="objOne"/> and <paramref name="objTwo"/> are not null, <paramref name="objOne"/> is returned.
        /// If both <paramref name="objOne"/> and <paramref name="objTwo"/> are null, null is returned</returns>
        public static T NonNullOf<T>(T objOne, T objTwo)
        {
            T result = default(T);
            if (objOne.IsSomething())
            {
                result = objOne;
            }
            else
            {
                result = objTwo;
            }
            return result;
        }
    }
}
