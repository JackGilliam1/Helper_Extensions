using System;
using System.Collections.Generic;

namespace Extensions.Tests.TestObjects
{
    public class TestObject<DataType>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double RandomValue { get; set; }
        public string SomeString { get; set; }
        public DateTime dateTime { get; set; }
        public List<DataType> Stuff { get; set; }

        public int AdditionNumber {get; set;}

        public TestObject()
        {
            AdditionNumber = 2;
        }

        /// <summary>
        /// Adds one to the instance variable AdditionNumber
        /// </summary>
        public void Add()
        {
            AdditionNumber++;
        }

        /// <summary>
        /// Multiplies the specified value and the instance variable AdditionNumber together
        /// </summary>
        /// <param name="value">The value to multiply</param>
        public void Multiply(int value)
        {
            AdditionNumber *= value;
        }
    }
}
