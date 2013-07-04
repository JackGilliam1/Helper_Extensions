using System;
using System.Collections.Generic;
using Extensions.Core.Collections;
using Xunit;

namespace Extensions.Tests
{
    public class CollectionTests : TestsBase
    {
        [Fact]
        public void Collection_Changes_Into_String()
        {
            var matrixOneData = new List<List<int>>()
            {
                new List<int>() {2, 2},
                new List<int>() {3, 4},
            };
            string matrixString = CollectionExtension.AsString(matrixOneData);
            Console.WriteLine(matrixString);
        }
    }
}
