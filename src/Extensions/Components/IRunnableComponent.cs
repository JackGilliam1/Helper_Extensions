using System.Threading;

namespace Extensions.Core.Components
{
    /// <summary>
    /// Provides a runnable implementation of the <see cref="IComponent"/>
    /// </summary>
    public interface IRunnableComponent : IComponent
    {
        /// <summary>
        /// The thread running within this component
        /// </summary>
        Thread WorkerThread
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether this component is running 
        /// </summary>
        bool IsRunning
        {
            get;
        }

        /// <summary>
        /// Starts this component
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this component
        /// </summary>
        void Stop();

        /// <summary>
        /// The work this component will perform within a thread
        /// </summary>
        void DoWork();
    }
}
