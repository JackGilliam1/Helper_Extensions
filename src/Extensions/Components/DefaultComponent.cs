
namespace Extensions.Core.Components
{
    /// <summary>
    /// Provides a basic implementation of an IComponent
    /// </summary>
    public class DefaultComponent : IComponent
    {
        public virtual string Name
        {
            get;
            set;
        }

        public virtual object Value
        {
            get;
            set;
        }

        /// <summary>
        /// Constructs a DefaultComponent with the specified name
        /// </summary>
        /// <param name="name">The name of this component, it must be unique</param>
        public DefaultComponent(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Constructs a DefaultComponent with the specified value
        /// </summary>
        /// <param name="value">The value contained within this component</param>
        public DefaultComponent(object value)
        {
            Value = value;
        }

        /// <summary>
        /// Constructs a DefaultComponent with the specified name and value
        /// </summary>
        /// <param name="name">The name of this component, it must be unique</param>
        /// <param name="value">The value contained within this component</param>
        public DefaultComponent(string name, object value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Disposes of this component
        /// </summary>
        public virtual void Dispose()
        {
            Name = null;
            Value = null;
        }
    }
}
