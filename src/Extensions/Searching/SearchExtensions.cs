using System.Collections.Generic;
using System.Linq;

namespace Extensions.Core.Searching
{
    /*
     * Author: Jack Gilliam
     * Date Created: 4/2/2012
     */
    public static class SearchExtensions
    {
        /// <summary>
        /// Searches for the specified items
        /// </summary>
        /// <typeparam name="DataType">The type of data to look through</typeparam>
        /// <param name="data">The data to look through</param>
        /// <param name="propertyName">The name of the property on the objects to compare</param>
        /// <param name="compareData">The data to compare the property to</param>
        /// <param name="enableOrdering">Do you want ordering to be enabled</param>
        /// <param name="orderDescending">Order the results in descending order? (Ordering must be enabled)</param>
        /// <param name="orderByProperty">The property to order the results by</param>
        /// <returns>Search results</returns>
        public static ICollection<DataType> StandardSearch<DataType>( this ICollection<DataType> data, string propertyName, object compareData, bool enableOrdering = false, bool orderDescending = false, string orderByProperty = "" )
        {
            ICollection<DataType> result = new List<DataType>( );
            if ( propertyName != null && compareData != null )
            {
                result = data.Where( t => t.GetType( ).GetProperties( ).FirstOrDefault( p => p.Name == propertyName ).GetValue( t, null ).ToString( ).Equals(compareData.ToString( )) ).ToList( );
            }
            if ( enableOrdering )
            {
                if ( orderDescending && orderByProperty != "" )
                {
                    result = result.OrderByDescending( t => t.GetType( ).GetProperties( ).FirstOrDefault( p => p.Name == orderByProperty ) ).ToList( );
                }
                else if ( orderByProperty != "" )
                {
                    result = result.OrderBy( t => t.GetType( ).GetProperties( ).FirstOrDefault( p => p.Name == orderByProperty ) ).ToList( );
                }
            }
            return result;
        }
    }
}