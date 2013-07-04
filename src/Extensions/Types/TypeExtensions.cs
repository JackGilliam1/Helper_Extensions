using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Extensions.Core.Types
{
    /*
     * Author: Jack Gilliam
     * Date Created: 4/2/2012
     */
    /// <summary>
    /// Provides extra functions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Indicates whether <paramref name="source"/> holds a method with the name of <paramref name="name"/>
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="source"/></typeparam>
        /// <param name="source">The location to search</param>
        /// <param name="name">The name of the method to locate</param>
        /// <returns>True if <paramref name="source"/> contains a method with the name of <paramref name="name"/>.
        /// False a method is not found in <paramref name="source"/>. 
        /// False if either <paramref name="source"/> or <paramref name="name"/> are null</returns>
        public static bool HasMethod<T>(this T source, string name)
        {
            bool hasMethod = Meth(source, name) != null;
            return hasMethod;
        }

        private static MethodInfo GetMeth<T>(this T source, string name)
        {
            MethodInfo methodInfo = null;
            var methods = source.Type().GetMethods();
            foreach (var method in methods)
            {
                if (method.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    methodInfo = method;
                    break;
                }
            }
            return methodInfo;
        }

        /// <summary>
        /// Returns all methods with names containing <paramref name="pattern"/> within type <paramref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="source"/></typeparam>
        /// <param name="source">Search location</param>
        /// <param name="pattern">Regex to match against</param>
        /// <returns>Methods with names containing <paramref name="pattern"/>,
        /// null if no methods were found or if <paramref name="source"/> is null</returns>
        public static IEnumerable<MethodInfo> Meths<T>(this T source, string name)
        {
            List<MethodInfo> methods = null;
            if (source != null)
            {
                var foundMethods = new List<MethodInfo>();
                Type type = source.Type();
                var sourceMethods = type.GetMethods();
                foreach (var method in sourceMethods)
                {
                    if (Regex.IsMatch(method.Name, name, RegexOptions.IgnoreCase))
                    {
                        foundMethods.Add(method);
                    }
                }
                if (foundMethods.Count > 0)
                {
                    methods = foundMethods;
                }
            }
            return methods;
        }

        /// <summary>
        /// Returns the first method with a name matching <paramref name="name"/> within type <paramref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="source"/></typeparam>
        /// <param name="source">Object containing the method</param>
        /// <param name="name">The name of the method to locate</param>
        /// <returns>A method with a name that matches <paramref name="name"/>"/>,
        /// null if a method was not found or if the <paramref name="source"/> is null</returns>
        public static MethodInfo Meth<T>(this T source, string name)
        {
            MethodInfo method = null;
            if (source != null)
            {
                method = source.GetMeth(name);
            }
            return method;
        }

        #region Props

        /// <summary>
        /// Returns all properties with names containing <paramref name="pattern"/> within type <paramref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="source"/></typeparam>
        /// <param name="source">Search location</param>
        /// <param name="pattern">Pattern to match against</param>
        /// <returns>All properties with names containing <paramref name="pattern"/>,
        /// null if none were found or if <paramref name="source"/> is null</returns>
        public static IEnumerable<PropertyInfo> Props<T>(this T source, string pattern) where T : class
        {
            IEnumerable<PropertyInfo> properties = null;
            if (source != null)
            {
                properties = source.GetType().GetProperties()
                    .Where(p => Regex.IsMatch(p.Name, pattern, RegexOptions.IgnoreCase));
            }
            return properties;
        }

        /// <summary>
        /// Returns all properties contained within type <paramref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="source"/></typeparam>
        /// <param name="source">Object to locate properties</param>
        /// <returns>All properties within <paramref name="T"/>, 
        /// null if none were found or if <paramref name="source"/> is null</returns>
        public static IEnumerable<PropertyInfo> Props<T>(this T source) where T : class
        {
            IEnumerable<PropertyInfo> properties = null;
            if (source != null)
            {
                Type sourceType = source as Type;
                if (sourceType == null)
                {
                    sourceType = source.GetType();
                }
                //Sidenote: Properties are not found if the property does not have a get or set
                properties = sourceType.GetProperties();
            }
            return properties;
        }

        /// <summary>
        /// Returns a property matching with a name matching <paramref name="propertyName"/> within type <paramref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="source"/></typeparam>
        /// <param name="source">Object to locate property in</param>
        /// <param name="propertyName">The name of the property to locate</param>
        /// <returns>A property with a name that matches <paramref name="propertyName"/>, 
        /// null if a property was not found or if <paramref name="source"/> is null</returns>
        public static PropertyInfo Prop<T>(this T source, string propertyName) where T : class
        {
            PropertyInfo property = null;
            if (source != null)
            {
                //Sidenote: Properties are not found if the property does not have a get or set
                var properties = source.GetType().GetProperties();
                property = properties.FirstOrDefault(p => p.Name.ToLower() == propertyName.ToLower());
            }
            return property;
        }

        /// <summary>
        /// Returns the values of all properties in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="source"/></typeparam>
        /// <param name="source">Object to obtain values from</param>
        /// <returns>The values of all properties within <paramref name="source"/>,
        /// null if none were found or if <paramref name="source"/> is null</returns>
        public static IEnumerable<object> Vals<T>(this T source) where T : class
        {
            List<object> values = null;
            if (source != null)
            {
                Type type = source.Type();
                var vals = new List<object>();
                var properties = type.GetProperties();
                foreach (var property in properties)
                {
                    vals.Add(property.GetValue(source, null));
                }
                if (vals.Count > 0)
                {
                    values = vals;
                }
            }
            return values;
        }

        /// <summary>
        /// Returns the value of a property with a name matching <paramref name="propertyName"/> from <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="source"/></typeparam>
        /// <param name="source">Location of the value</param>
        /// <param name="propertyName">Name of the property to locate</param>
        /// <returns>The property value of <paramref name="source"/>, 
        /// null if property <paramref name="propertyName"/> was not found or <paramref name="source"/> is null</returns>
        public static object Val<T>(this T source, string propertyName)
        {
            object value = null;
            if (source != null)
            {
                Type type = source.Type();
                var properties = type.GetProperties();
                PropertyInfo property = null;
                foreach (var prop in properties)
                {
                    if (prop.Name.ToLower().Equals(propertyName.ToLower()))
                    {
                        property = prop;
                        break;
                    }
                }
                if (property != null)
                {
                    var val = property.GetValue(source, null);
                    value = val;
                }
            }
            return value;
        }

        /// <summary>
        /// Sets the value of a property with a name matching <paramref name="propertyName"/> of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="source"/></typeparam>
        /// <param name="source">The location of the property</param>
        /// <param name="propertyName">The name of the property to set</param>
        /// <param name="value">The value to set the property as</param>
        /// <returns>True if <paramref name="propertyName"/> was set to <paramref name="value"/>, 
        /// False if the property was not found or <paramref name="source"/> is null</returns>
        public static bool SetVal<T>(this T source, string propertyName, object value) where T : class
        {
            bool successful = false;
            if (source != null)
            {
                PropertyInfo property = source.GetType().GetProperties()
                    .FirstOrDefault(p => p.Name.ToLower() == propertyName.ToLower());
                successful = property != null;
                if (successful)
                {
                    property.SetValue(source, value, null);
                }
            }
            return successful;
        }

        #endregion

        #region General
        /// <summary>
        /// Returns the type of an object
        /// </summary>
        /// <param name="value">Contains the type to return</param>
        /// <returns>The type of value</returns>
        public static Type Type<T>(this T value)
        {
            Type type = null;
            if (value != null)
            {
                type = value as Type;
                if (type == null)
                {
                    type = value.GetType();
                }
            }
            return type;
        }

        /// <summary>
        /// Returns the default of the type of a specified object
        /// </summary>
        /// <param name="value">The value to get the default value</param>
        /// <returns>The default value of type T</returns>
        public static T Default<T>(this T value)
        {
            return default(T);
        }

        /// <summary>
        /// Indicates whether <paramref name="value"/> is of a numeric type
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="value"/></typeparam>
        /// <param name="value">The value to check</param>
        /// <returns>True if <paramref name="value"/> is of a numeric type
        /// False if <paramref name="value"/> is not numeric.
        /// False if <paramref name="value"/> is null.</returns>
        public static bool IsNumeric<T>()
        {
            var defaultVal = default(T);
            double tempNum;
            bool isNumeric = defaultVal != null
                && Double.TryParse(defaultVal.ToString(), out tempNum);
            return isNumeric;
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
            double temp;
            bool isNumeric = value != null
                && Double.TryParse(value.ToString(), out temp);
            return isNumeric;
        }
        #endregion
    }
}
