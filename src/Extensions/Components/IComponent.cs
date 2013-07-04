using System;

namespace Extensions.Core.Components
{
    /// <summary>
    /// A disposable base component containing a name and a value
    /// </summary>
    public interface IComponent : IDisposable
    {
        /// <summary>
        /// The name of this component
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The value contained within this component
        /// </summary>
        object Value
        {
            get;
            set;
        }
    }
}
