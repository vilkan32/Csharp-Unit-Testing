using NUnit.Framework;
using System;

namespace CustomLinkedList.Tests
{
    [TestFixture]
    class CustomLinkedListTests
    {
        private DynamicList<int> customList;
        [SetUp]
        public void SetUp()
        {
             this.customList = new DynamicList<int>();
        }

        [Test]
        public void CannotCallElementWithNegativeIndex()
        {
            // Arrange
            var incorrectIndex = -1;

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var test = this.customList[incorrectIndex];
            }
            , "Provided index is less than zero");
        }

        [Test]
        public void CannotCallElementWithIndexAboveTheRange()
        {
            // Arrange
            var incorrectIndex = 1;

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var test = this.customList[incorrectIndex];
            }
            , "Provided index is greater than the range of the collection");
        }

        [Test]
        public void AddShouldIncreaseCollectionCount()
        {
            // Act
            this.customList.Add(1);

            // Assert
            Assert.AreEqual(1, this.customList.Count, "Adding an element doesn't affect the collection's count");
        }

        [Test]
        [TestCase(1, 0)]
        [TestCase(5, 3)]
        [TestCase(10, 8)]
        [TestCase(15, 14)]
        public void AddShouldSaveTheElementInTheCollection(int numberOfAdditions, int indexToCheck)
        {
            // Act
            for (int i = 0; i < numberOfAdditions; i++)
            {
                this.customList.Add(i);
            }

            // Assert
            Assert.AreEqual(indexToCheck, this.customList[indexToCheck], "Element is not the same as the one added");
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(-5)]
        [TestCase(5)]
        public void RemoveAtIndexOusideTheBoundariesOfTheCollectionIsImpossible(int indexToRemove)
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => this.customList.RemoveAt(indexToRemove));
        }

        [Test]
        [TestCase(3, 1)]
        [TestCase(10, 5)]
        [TestCase(10, 0)]
        [TestCase(10, 8)]
        public void RemoveAtShouldRemoveTheElementAtTheGivenIndex(int numberOfAdditions, int indexToRemove)
        {
            // Arrange
            for (int i = 0; i < numberOfAdditions; i++)
            {
                this.customList.Add(i);
            }

            // Act
            this.customList.RemoveAt(indexToRemove);

            // Assert
            Assert.AreEqual(indexToRemove + 1, customList[indexToRemove]);
        }

        [Test]
        [TestCase(3, 1)]
        [TestCase(5, 4)]
        [TestCase(10, 7)]
        public void IndexOfShouldReturnTheIndexPointingAtTheCurrentLocationOfTheElement(int numberOfAdditions, int keyElement)
        {
            // Arrange
            for (int i = 0; i < numberOfAdditions; i++)
            {
                this.customList.Add(i);
            }
            var expectedIndex = keyElement;

            // Act
            var foundIndex = this.customList.IndexOf(keyElement);

            // Assert
            Assert.AreEqual(expectedIndex, foundIndex, "Returned index is not correct");
        }

        [Test]
        [TestCase(3, 3)]
        [TestCase(5, -1)]
        [TestCase(10, 15)]
        public void IndexOfShouldReturnNegativeIntegerIfTheGivenElementDoesNotExists(int numberOfAdditions, int keyElement)
        {
            // Arrange
            for (int i = 0; i < numberOfAdditions; i++)
            {
                this.customList.Add(i);
            }

            // Act
            var isReturnedValueLessThanZero = this.customList.IndexOf(keyElement) < 0;

            // Assert
            Assert.IsTrue(isReturnedValueLessThanZero, "Returned index is not negative");
        }

        [Test]
        [TestCase(3, 1)]
        [TestCase(10, 5)]
        [TestCase(10, 0)]
        [TestCase(10, 8)]
        [TestCase(10, 9)]
        public void RemoveShouldDeleteParticularElement(int numberOfAdditions, int elementToRemove)
        {
            // Arrange
            for (int i = 0; i < numberOfAdditions; i++)
            {
                this.customList.Add(i);
            }

            // Act
            this.customList.Remove(elementToRemove);

            // Assert
            Assert.AreEqual(-1, this.customList.IndexOf(elementToRemove), "Removed element is still in the collection");
        }

        [Test]
        [TestCase(3, 3)]
        [TestCase(3, -1)]
        [TestCase(3, 10)]
        public void RemoveUnexistentEelementShouldReturnNegativeInteger(int numberOfAdditions, int elementToRemove)
        {
            // Arrange
            for (int i = 0; i < numberOfAdditions; i++)
            {
                this.customList.Add(i);
            }

            // Act
            var isRemovingResultLesThanZero = this.customList.Remove(elementToRemove) < 0;

            // Assert
            Assert.IsTrue(isRemovingResultLesThanZero, "Attempting to remove an unexistent element returns positive integer");
        }

        [Test]
        [TestCase(3, 1)]
        [TestCase(5, 4)]
        [TestCase(10, 7)]
        public void RemoveShouldReturnTheIndexOfTheRemovedElement(int numberOfAdditions, int elementToRemove)
        {
            // Arrange 
            for (int i = 0; i < numberOfAdditions; i++)
            {
                this.customList.Add(i);
            }
            var expectedIndex = elementToRemove;

            // Act
            var returnedIndex = this.customList.Remove(elementToRemove);

            // Assert
            Assert.AreEqual(expectedIndex, returnedIndex, "Remove returns wrong index");
        }
        [Test]
        [TestCase(3, 1)]
        [TestCase(5, 4)]
        [TestCase(10, 7)]
        public void ContainsShouldReturnTrueIfElementExists(int numberOfAdditions, int keyElement)
        {
            // Arrange
            for (int i = 0; i < numberOfAdditions; i++)
            {
                this.customList.Add(i);
            }

            // Assert
            Assert.IsTrue(this.customList.Contains(keyElement), "Contains returns false for existing element");
        }

        [Test]
        [TestCase(3, 3)]
        [TestCase(5, 10)]
        [TestCase(10, 15)]
        public void ContainsShouldReturnfalseIfElementDoesNotExists(int numberOfAdditions, int keyElement)
        {
            // Arrange
            for (int i = 0; i < numberOfAdditions; i++)
            {
                this.customList.Add(i);
            }

            // Assert
            Assert.IsFalse(this.customList.Contains(keyElement), "Contains returns true for element which doesn't exists");
        }
    }
}
