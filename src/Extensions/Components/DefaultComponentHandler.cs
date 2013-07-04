using System.Collections.Generic;
using System.Linq;

namespace Extensions.Core.Components
{
    public class DefaultComponentHandler<T> : IComponentHandler<T> where T : IComponent
    {
        public List<T> Components
        {
            get;
            set;
        }

        public DefaultComponentHandler()
        {
            Components = new List<T>();
        }

        public long Count
        {
            get
            {
                long count = Components.Count;
                return count;
            }
        }

        public void AddComponent(T component)
        {
            lock (Components)
            {
                if (!Contains(component))
                {
                    Components.Add(component);
                }
            }
        }

        public void AddComponents(params T[] components)
        {
            if (components.Length > 0)
            {
                var length = components.Length;
                lock (Components)
                {
                    for (int i = 0; i < length; i += 1)
                    {
                        var component = components[i];
                        if (!Contains(component))
                        {
                            Components.Add(component);
                        }
                    }
                }
            }
        }

        public void AddComponents(IEnumerable<T> components)
        {
            var count = components.Count();
            if (count > 0)
            {
                var listOfComponents = components.ToList();
                var length = count - 1;
                lock (Components)
                {
                    for (int i = 0; i < length; i += 1)
                    {
                        var component = listOfComponents[i];
                        if (!Contains(component))
                        {
                            Components.Add(component);
                        }
                    }
                }
            }
        }

        public void RemoveComponentByName(string componentName)
        {
            lock (Components)
            {
                Components.Remove(this[componentName]);
            }
        }

        public T GetComponent(string componentName)
        {
            T component = default(T);
            lock (Components)
            {
                component = this[componentName];
            }
            return component;
        }

        public T this[string componentName]
        {
            get
            {
                bool found = false;
                T component = default(T);
                lock (Components)
                {
                    for (int i = 0; i < Count && !found; i += 1)
                    {
                        var current = Components[i];
                        if (found = (current.Name.Equals(componentName)))
                        {
                            component = current;
                        }
                    }
                }
                return component;
            }
        }

        public void Clear()
        {
            lock (Components)
            {
                Components.Clear();
            }
        }

        public bool Contains(T component)
        {
            bool contains = false;
            lock (Components)
            {
                for (int i = 0; i < Count && !contains; i += 1)
                {
                    var toCompare = Components[i];
                    contains = component.Name.Equals(toCompare.Name);
                }
            }
            return contains;
        }
    }
}
