using System;
using System.Collections.Generic;
using Extensions.Core.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Tests
{
    [TestClass]
    public class CollectionTests
    {
        [TestMethod]
        public void Collection_Changes_Into_String()
        {
            List<List<int>> matrixOneData = new List<List<int>>()
            {
                new List<int>() {2, 2},
                new List<int>() {3, 4},
            };
            string matrixString = CollectionExtension.AsString(matrixOneData);
            Console.WriteLine(matrixString);
        }
    }
}
