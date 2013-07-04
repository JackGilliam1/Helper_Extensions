using System.Threading;

namespace Extensions.Core.Components
{
    /// <summary>
    /// Provides a base implementation of an <see cref="IRunnableComponent"/>
    /// </summary>
    public abstract class DefaultRunnableComponent : DefaultComponent, IRunnableComponent
    {
        public System.Threading.Thread WorkerThread
        {
            get;
            set;
        }

        protected bool _running;

        public bool IsRunning
        {
            get
            {
                return _running && WorkerThread != null && !WorkerThread.IsAlive;
            }
        }

        public DefaultRunnableComponent(string name)
            : base(name)
        {

        }

        public DefaultRunnableComponent(object value)
            : base(value)
        {

        }

        public DefaultRunnableComponent(string name, object value)
            : base(name, value)
        {

        }

        public void Start()
        {
            if (WorkerThread == null || !WorkerThread.IsAlive)
            {
                WorkerThread = new Thread(DoWork);
                WorkerThread.Name = base.Name;
                WorkerThread.Start();
            }
        }

        public void Stop()
        {
            _running = false;
        }

        public abstract void DoWork();

        public override void Dispose()
        {
            Stop();
            WorkerThread = null;
        }
    }
}
