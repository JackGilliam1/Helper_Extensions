using System.Collections.Generic;
using Extensions.Core.Collections;

using SharpTestsEx;
using Xunit;

namespace Extensions.Tests.Collections
{
    public class CollectionExtensionsTests
    {
        [Fact]
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

        [Fact]
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

        [Fact]
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

        [Fact]
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
