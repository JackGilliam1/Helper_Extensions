using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Extensions.Core.Generics;
using Extensions.Core.Types;

namespace Extensions.Core.Collections
{
    public static class CollectionExtension
    {
        public static bool SameCount<T>(this ICollection<T> collectionOne, ICollection<T> collectionTwo) where T : class 
        {
            bool hasSameCount = true;
            if (collectionOne != null && collectionTwo != null)
            {
                int numberOfItemsInFirstCollection = 0;
                if ((numberOfItemsInFirstCollection = collectionOne.Count).Equals(collectionTwo.Count))
                {
                    for (int i = 0; i < numberOfItemsInFirstCollection; i++)
                    {
                        var firstItem = collectionOne.Get(i);
                        var secondItem = collectionTwo.Get(i);
                        if (firstItem != null && secondItem != null)
                        {
                            ICollection<dynamic> firstCollection;
                            if ((firstCollection = firstItem as ICollection<dynamic>) != null)
                            {
                                ICollection<dynamic> secondCollection;
                                if ((secondCollection = secondItem as ICollection<dynamic>) != null)
                                {
                                    if (firstItem.GetType() == secondItem.GetType())
                                    {
                                        hasSameCount = firstCollection.SameCount(secondCollection);
                                    }
                                    else
                                    {
                                        hasSameCount = false;
                                    }
                                }
                                else
                                {
                                    hasSameCount = false;
                                }
                            }
                            else if (secondItem is ICollection<dynamic>)
                            {
                                hasSameCount = false;
                            }
                        }
                        else if(firstItem == null && secondItem != null)
                        {
                            hasSameCount = false;
                        }
                        else if (firstItem != null && secondItem == null)
                        {
                            hasSameCount = false;
                        }
                        else
                        {
                            hasSameCount = true;
                        }
                        if (!hasSameCount)
                        {
                            i = numberOfItemsInFirstCollection;
                        }
                    }
                }
                else
                {
                    hasSameCount = false;
                }
            }
            else
            {
                hasSameCount = false;
            }
            return hasSameCount;
        }

        /// <summary>
        /// Returns items with a specified property value
        /// </summary>
        /// <param name="source">Collection to take from</param>
        /// <param name="propertyName">Name of property to find</param>
        /// <param name="value">Value within found properties</param>
        /// <returns>Items found, else null</returns>
        public static IEnumerable<T> GetBy<T>(this IEnumerable<T> source, string propertyName, object value) where T : class 
        {
            List<T> found = null;
            if (source != null)
            {
                List<T> foundObjects = new List<T>();
                foreach (var obj in source)
                {
                    if (TypeExtensions.Val(obj, propertyName).Equals(value))
                    {
                        foundObjects.Add(obj);
                    }
                }
                found = foundObjects;
            }
            return found;
        }

        /// <summary>
        /// Returns the item at the specified index
        /// </summary>
        /// <param name="source">Location to search</param>
        /// <param name="index">Position of needed item</param>
        /// <returns>Item found, else the Type default</returns>
        public static T Get<T>(this IEnumerable<T> source, int index)
        {
            T item = default(T);
            if (source.Count() > index)
            {
                item = source.ToList()[index];
            }
            return item;
        }

        ///// <summary>
        ///// Removes a specified object from a collection
        ///// </summary>
        ///// <param name="source">Collection to remove from</param>
        ///// <param name="obj">Object to remove</param>
        ///// <returns>The changed source</returns>
        //public static IEnumerable<T> Remove<T>( this IEnumerable<T> source, T obj )
        //{
        //    source.ToList( ).Remove( obj );
        //    return source;
        //}

        ///// <summary>
        ///// Removes specified objects from a collection
        ///// </summary>
        ///// <param name="source">Collection to remove from</param>
        ///// <param name="objects">Objects to remove</param>
        ///// <returns>True if successful</returns>
        //public static IEnumerable<T> Remove<T>( this IEnumerable<T> source, IEnumerable<T> objects )
        //{
        //    foreach ( var obj in objects )
        //    {
        //        source = source.Remove(obj);
        //    }
        //    return source;
        //}

        ///// <summary>
        ///// Removes objects from a collection with a specified property with a specified value
        ///// </summary>
        ///// <param name="source">Collection to remove from</param>
        ///// <param name="name">Name of property to check</param>
        ///// <param name="value">Value to find</param>
        ///// <returns>True if successful</returns>
        //public static IEnumerable<T> Remove<T>( this IEnumerable<T> source, string name, object value )
        //{
        //    source = source.Remove( source.Where( o => o.Val( name ) == value ) );
        //    return source;
        //}

        /// <summary>
        /// Converts <paramref name="data"/> into a single string
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="data"/></typeparam>
        /// <param name="data">The data to convert</param>
        /// <returns>The string version of <paramref name="data"/></returns>
        public static string AsString<T>(this T data)
        {
            StringBuilder builder = new StringBuilder();
            dynamic source = data;

            if (GenericExtensions.IsSomething(source))
            {
                if (source.GetType().GetInterface("ICollection") != null)
                {
                    StringBuilder lineBuilder = new StringBuilder();
                    foreach (dynamic item in source)
                    {
                        lineBuilder.Append(CollectionExtension.AsString(item) + " ");
                    }
                    builder.AppendLine(Regex.Replace(lineBuilder.ToString().Trim(), "\\s{1}", ","));
                }
                else
                {
                    builder.Append(source.ToString());
                }
            }
            return Regex.Replace(builder.ToString(), "[,]{2,}", "\r\n");
        }

        /// <summary>
        /// Indicates whether <paramref name="item"/> is the last item of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/> as well as the type of <paramref name="item"/></typeparam>
        /// <param name="source">The sequence to search</param>
        /// <param name="item">Element to locate</param>
        /// <returns>True if <paramref name="item"/> is located last in <paramref name="source"/>,
        /// False if <paramref name="item"/> was not found, <paramref name="item"/> is null or empty, or
        /// if <paramref name="source"/> is null</returns>
        public static bool HasLastOf<T>(this IEnumerable<T> source, T item)
        {
            bool isLast = false;
            if (GenericExtensions.IsSomething(item) && GenericExtensions.IsSomething(source) && CollectionExtension.HasItems(source))
            {
                T compareObject = source.Last();
                isLast = GenericExtensions.SameAs(compareObject, item);
            }
            return isLast;
        }

        /// <summary>
        /// Indicates whether <paramref name="source"/> has elements
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/></typeparam>
        /// <param name="source">Sequence to determine element count of</param>
        /// <returns>True if <paramref name="source"/> has elements
        /// False if <paramref name="source"/> contains no elements, 
        /// or if <paramref name="source"/> is null</returns>
        public static bool HasItems<T>(this IEnumerable<T> source)
        {
            bool hasItems = (GenericExtensions.IsSomething(source) && source.Count() > 0);
            return hasItems;
        }

        /// <summary>
        /// Indicates whether <paramref name="index"/> is the last position of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/></typeparam>
        /// <param name="source">The sequence to check</param>
        /// <param name="index">Position to locate</param>
        /// <returns>True if <paramref name="index"/> is the last index of <paramref name="source"/>.
        /// False if <paramref name="index"/> is not the last index of <paramref name="source"/>.
        /// False if <paramref name="source"/> is null.</returns>
        public static bool HasLastIndexOf<T>(this IEnumerable<T> source, int index)
        {
            return GenericExtensions.IsSomething(source) && GenericExtensions.SameAs(index, source.Count());
        }

        /// <summary>
        /// Converts <paramref name="source"/> into a sequence of strings
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/></typeparam>
        /// <param name="source">The sequence to convert</param>
        /// <returns>A sequence containing the items of <paramref name="source"/> in string for,
        /// null if <paramref name="source"/> is null</returns>
        public static IEnumerable<string> ToMessages<T>(this IEnumerable<T> source)
        {
            List<string> messages = null;
            if (source != null)
            {
                messages = new List<string>();
                foreach (var item in source)
                {
                    messages.Add(item.ToString());
                }
            }
            return messages;
        }

        /// <summary>
        /// Indicates whether <paramref name="source"/> contains a value that is infinite
        /// </summary>
        /// <param name="source">The source to check</param>
        /// <returns>True if Infinity or -Infinity is found</returns>
        public static bool ContainsInfinity(this IEnumerable<double> source)
        {
            bool containsInfinite = false;
            if (source != null)
            {
                foreach (double value in source)
                {
                    containsInfinite = Double.IsInfinity(value);
                    if (containsInfinite)
                    {
                        break;
                    }
                }
            }
            return containsInfinite;
        }

        /// <summary>
        /// Indicates whether <paramref name="source"/> contains a value that is infinite
        /// </summary>
        /// <param name="source">The source to check</param>
        /// <returns>True if NaN is found</returns>
        public static bool ContainsNaN(this IEnumerable<double> source)
        {
            bool containsInfinite = false;
            if (source != null)
            {
                foreach (double value in source)
                {
                    containsInfinite = Double.IsNaN(value);
                    if (containsInfinite)
                    {
                        break;
                    }
                }
            }
            return containsInfinite;
        }
    }
}
