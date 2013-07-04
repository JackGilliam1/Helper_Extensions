using System.Collections.Generic;

namespace Extensions.Core.Components
{
    /// <summary>
    /// Handles a dictionary of IComponent objects.
    /// </summary>
    /// <param name="T">The type of IComponent being handled</param>
    public interface IComponentHandler<T> where T : IComponent
    {
        /// <summary>
        /// The list of <seealso cref="IComponent"/>s being handled
        /// </summary>
        List<T> Components
        {
            get;
        }

        /// <summary>
        /// Indicates the number of <seealso cref="IComponent"/>s contained within the dictionry property, <see cref="Components"/>.
        /// </summary>
        long Count
        {
            get;
        }

        /// <summary>
        /// Adds the specified <paramref name="component"/> to be handled
        /// </summary>
        /// <param name="component">The component to add</param>
        /// <exception cref="InvalidArgumentException">Thrown if the property <seealso cref="component.Name"/> is null or already exists within <see cref="Components"/></exception>
        void AddComponent(T component);

        /// <summary>
        /// For more information see <seealso cref="AddComponents(IEnumerable<T>)"/>
        /// </summary>
        void AddComponents(params T[] components);

        /// <summary>
        /// Adds the specified <paramref name="components"/> to be handled
        /// </summary>
        /// <param name="components">An collection of <seealso cref="IComponent"/>s</param>
        /// <exception cref="InvalidArgumentException">Thrown if the property <seealso cref="IComponent.Name"/> is null or already exists within any of the specified <see cref="components"/></exception>
        void AddComponents(IEnumerable<T> components);

        /// <summary>
        /// Removes the IComponent associated with the specified <paramref name="componentName"/>
        /// </summary>
        /// <param name="componentName">The <see cref="IComponent.Name"/> of the <see cref="IComponent"/> being removed</param>
        void RemoveComponentByName(string componentName);

        /// <summary>
        /// Returns the <see cref="IComponent"/> with the <seealso cref="IComponent.Name"/> of <paramref name="componentName">
        /// </summary>
        /// <param name="componentName">The name of the component being retrieve</param>
        /// <returns>An <see cref="IComponent"/></returns>
        T GetComponent(string componentName);

        /// <summary>
        /// Indicates whether the specified <paramref name="component"/> exists within the list of components being handled
        /// </summary>
        /// <param name="component">The <see cref="IComponent"/> to check for</param>
        /// <returns>True if <paramref name="component"/> exists, false if <paramref name="component"/> does not exist</returns>
        bool Contains(T component);

        /// <summary>
        /// Clears the List of <see cref="IComponent"/>s being handled <seealso cref="List.Clear"/>
        /// </summary>
        void Clear();
    }
}
