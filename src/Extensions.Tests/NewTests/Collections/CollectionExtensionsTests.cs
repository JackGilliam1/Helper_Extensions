using System.Collections.Generic;
using Extensions.Core.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Extensions.Tests.Collections
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public void CollectionsSameCount()
        {
            var firstCollection = new List<object>()
            {
                new object(),
                new object()
            };

            var secondCollection = new List<object>()
            {
                new object(),
                new object()
            };

            firstCollection.SameCount(secondCollection).Should("The Collections have the same count, but were rejected").Be.True();
        }

        [TestMethod]
        public void CollectionsDifferentCount()
        {
            var firstCollection = new List<object>()
            {
                new object(),
                new object()
            };

            var secondCollection = new List<object>()
            {
                new object()
            };

            firstCollection.SameCount(secondCollection).Should("The Collections have different counts, but were accepted").Be.False();
        }

        [TestMethod]
        public void CollectionContainingCollectionsSameCount()
        {
            var firstCollection = new List<IEnumerable<object>>()
            {
                new List<object>() {
                    new object(),
                    new object()
                },
                new List<object>() {
                    new object(),
                    new object()
                }
            };

            var secondCollection = new List<IEnumerable<object>>()
            {
                new List<object>() {
                    new object(),
                    new object()
                },
                new List<object>() {
                    new object(),
                    new object()
                }
            };

            firstCollection.SameCount(secondCollection).Should("The Collections of Collections have the same count, but were rejected").Be.True();
        }

        [TestMethod]
        public void CollectionsContainingCollectionsDifferentCount()
        {
            var firstCollection = new List<IEnumerable<object>>()
            {
                new List<object>() {
                    new object(),
                    new object()
                },
                new List<object>() {
                    new object()
                }
            };

            var secondCollection = new List<IEnumerable<object>>()
            {
                new List<object>() {
                    new object(),
                    new object()
                },
                new List<object>() {
                    new object(),
                    new object()
                }
            };

            firstCollection.SameCount(secondCollection).Should("The Collections of Collections have different counts, but were accepted").Be.False();
        }
    }
}
