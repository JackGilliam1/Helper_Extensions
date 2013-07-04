using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Extensions.Core.Generics;

namespace Extensions.Core.Events
{
        /*
         * Author: Jack Gilliam
         * Date Created: 4/2/2012
         */
        /// <summary>
        /// Stores the events
        /// </summary>
        public static class EventExtensions
        {
            private struct ListenObject
            {
                public dynamic Listener;
                public string RefName;
                public string EventName;
                public EventHandler Function;
                public Thread Thread;
            }
 
            private static List<ListenObject> listeners = new List<ListenObject>();

            /// <summary>
            /// Adds an event listener to <paramref name="listener"/>
            /// </summary>
            /// <typeparam name="T">Type of <paramref name="listener"/></typeparam>
            /// <param name="listener">Listens to event <paramref name="eventName"/></param>
            /// <param name="refName">A name to reference <paramref name="listener"/></param>
            /// <param name="function">Function to invoke on event firing</param>
            /// <param name="eventName">Event to listen for</param>
            public static void Listen<T>(this T listener, string refName, string eventName, EventHandler function) where T : class
            {
                var listenObj = new ListenObject()
                {
                    Listener = listener,
                    RefName = refName,
                    EventName = eventName,
                    Function = function
                };
                listeners.Add(listenObj);
            }

            /// <summary>
            /// Stops <paramref name="listener"/> from listening
            /// </summary>
            /// <typeparam name="T">The type of <paramref name="listener"/></typeparam>
            /// <param name="listener">The object to stop listening</param>
            /// <returns>True if <paramref name="listener"/> has stopped listening</returns>
            public static bool Unlisten<T>(this T listener) where T : class
            {
                bool success = false;
                if (GenericExtensions.IsSomething(listeners))
                {
                    dynamic listenerDyn = listener;
                    ListenObject toRemove = listeners.Single(o => o.Listener.SameAs(listener));//(o.Val("RefName") as string) == refName );
                    success = listeners.Remove(toRemove);
                }
                else
                {
                    UnlistenAll();
                }
                return success;
            }

            /// <summary>
            /// Fires an event by name
            /// </summary>
            /// <param name="eventName">Event to fire</param>
            /// <param name="parameters">Parameters to send on event firing</param>
            public static bool Dispatch(string eventName, object[] parameters)
            {
                bool waiting = false;
                if (GenericExtensions.IsSomething(listeners))
                {
                    var firingObjects = listeners.Where(l => l.EventName.ToLower().Equals(eventName.ToLower())).ToList();//.GetBy("EventName", eventName);
                    for (int i = 0; i < firingObjects.Count;i++)//ListenObject listener in firingObjects)
                    {
                        var listener = firingObjects[i];
                        listener.Thread = new Thread(new ThreadStart(delegate
                            {
                                listener.Function.Invoke(listener.Listener, new CustomEventArgs(parameters));
                            }));
                        waiting = true;
                        listener.Thread.Start();
                    }
                }
                return waiting;
            }

            /// <summary>
            /// Removes all listening objects
            /// </summary>
            public static void UnlistenAll()
            {
                listeners = new List<ListenObject>();
            }
        }
    public class CustomEventArgs : EventArgs
    {
        public object[] Parameters { get; set; }

        public CustomEventArgs(object[] parameters)
            : base()
        {
            Parameters = parameters;
        }
    }
}

