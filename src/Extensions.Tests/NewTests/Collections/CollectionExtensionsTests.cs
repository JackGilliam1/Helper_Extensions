using System.Collections.Generic;
using Extensions.Core.Collections;

using SharpTestsEx;
using Xunit;
using Xunit.Extensions;

namespace Extensions.Tests.Collections
{
    public class CollectionExtensionsTests
    {

        [Theory,
        InlineData(0, 0, 0, 0),
        InlineData(0, 1, 0, 2),
        InlineData(0, 1, 2, 0),
        InlineData(2, 0, 8, 0),
        InlineData(1, 0, 1, 0),
        InlineData(1, 2, 1, 2),
        InlineData(2, 2, 2, 2),
        InlineData(10, 2, 10, 2)]
        public void CollectionContainingCollectionsSameCount(
            int numberOfLevelsOne, int objectsPerLevelOne, int numberOfLevelsTwo, int objectsPerLevelTwo)
        {
            var firstCollection = GetTestCollection(numberOfLevelsOne, objectsPerLevelOne);

            var secondCollection = GetTestCollection(numberOfLevelsTwo, objectsPerLevelTwo);

            firstCollection.SameCount(secondCollection)
                .Should("The Collections of Collections have the same count, but were rejected").Be.True();
        }

        [Theory,
        InlineData(0, 1, 2, 1),
        InlineData(0, 1, 2, 3),
        InlineData(2, 2, 2, 3),
        InlineData(2, 1, 2, 2),
        InlineData(2, 2, 2, 1),
        InlineData(2, 0, 2, 1),
        InlineData(1, 2, 2, 2),
        InlineData(2, 2, 1, 2),
        InlineData(1, 1, 2, 0)]
        public void CollectionsContainingCollectionsDifferentCount(
            int numberOfLevelsOne, int objectsPerLevelOne, int numberOfLevelsTwo, int objectsPerLevelTwo)
        {
            var firstCollection = GetTestCollection(numberOfLevelsOne, objectsPerLevelOne);

            var secondCollection = GetTestCollection(numberOfLevelsTwo, objectsPerLevelTwo);

            firstCollection.SameCount(secondCollection)
                .Should("The Collections of Collections have different counts, but were accepted").Be.False();
        }

        public ICollection<object> GetTestCollection(int numberOfLevels, int objectsPerLevel, int currentLevel = 1)
        {
            var parentCollection = new List<object>();
            if (numberOfLevels > 0 && objectsPerLevel > 0 && currentLevel > 0)
            {
                if (currentLevel < numberOfLevels)
                {
                    currentLevel += 1;
                    for (int i = 0; i < objectsPerLevel; i++)
                    {
                        var childCollection = GetTestCollection(numberOfLevels, objectsPerLevel, currentLevel);
                        parentCollection.Add(childCollection);
                    }
                }
                else
                {
                    for (int i = 0; i < objectsPerLevel; i++)
                    {
                        parentCollection.Add(new object());
                    }
                }
            }
            return parentCollection;
        }
    }
}
