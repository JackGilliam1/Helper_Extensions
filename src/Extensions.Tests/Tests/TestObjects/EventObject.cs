using System;

namespace Extensions.Tests.TestObjects
{
    public class EventObject<T>
    {
        private TestObject<T> TestObject;
        private int MultiplyValue;

        /// <summary>
        /// Instantiates a new EventObject
        /// </summary>
        /// <param name="testObject">The object for testing</param>
        /// <param name="multiplyValue">Value to use when Multiply is called</param>
        public EventObject(ref TestObject<T> testObject, int multiplyValue = 1)
        {
            TestObject = testObject;
            MultiplyValue = multiplyValue;
        }

        /// <summary>
        /// Calls the add method of the TestObject
        /// </summary>
        /// <param name="source">Caller</param>
        /// <param name="e">Event information</param>
        public void CallAdd(object source, EventArgs e)
        {
            TestObject.Add();
        }

        /// <summary>
        /// Calls the multiply method of the TestObject
        /// </summary>
        /// <param name="source">Caller</param>
        /// <param name="e">Event information</param>
        public void CallMultiply(object source, EventArgs e)
        {
            object parameter = e.GetType().GetProperty("Parameters").GetValue(e, null);
            int value = int.Parse(parameter.ToString());
            TestObject.Multiply(value);
        }
    }
}
