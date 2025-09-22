using Lab2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab.Tests
{
    [TestClass]
    public class Test2
    {
        // Тесты для SortDescending
        [TestMethod]
        public void SortDescending_ValidArray_SortsInDescendingOrder()
        {
            // Arrange
            int[] numbers = { 3, 7 };

            // Act
            StaticClass.SortDescending(numbers);

            // Assert
            Assert.AreEqual(7, numbers[0]);
            Assert.AreEqual(3, numbers[1]);
        }

        [TestMethod]
        public void SortDescending_AlreadySorted_KeepsOrder()
        {
            // Arrange
            int[] numbers = { 7, 3 };

            // Act
            StaticClass.SortDescending(numbers);

            // Assert
            Assert.AreEqual(7, numbers[0]);
            Assert.AreEqual(3, numbers[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SortDescending_NullArray_ThrowsArgumentException()
        {
            // Arrange
            int[] numbers = null;

            // Act
            StaticClass.SortDescending(numbers);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SortDescending_WrongLengthArray_ThrowsArgumentException()
        {
            // Arrange
            int[] numbers = { 1, 2, 3 };

            // Act
            StaticClass.SortDescending(numbers);
        }

        // Тесты для ProductOfEvenValues
        [TestMethod]
        public void ProductOfEvenValues_ValidArrayWithEvenNumbers_ReturnsCorrectProduct()
        {
            // Arrange
            int[,] array = {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };

            // Act
            long result = StaticClass.ProductOfEvenValues(array);

            // Assert
            Assert.AreEqual(2L * 4 * 6 * 8, result); // 2 * 4 * 6 * 8 = 384
        }

        [TestMethod]
        public void ProductOfEvenValues_ArrayWithNoEvenNumbers_ReturnsZero()
        {
            // Arrange
            int[,] array = {
                { 1, 3, 5 },
                { 7, 9, 11 },
                { 13, 15, 17 }
            };

            // Act
            long result = StaticClass.ProductOfEvenValues(array);

            // Assert
            Assert.AreEqual(0L, result);
        }

        [TestMethod]
        public void ProductOfEvenValues_ArrayWithOneEvenNumber_ReturnsThatNumber()
        {
            // Arrange
            int[,] array = {
                { 1, 3, 2 },
                { 5, 7, 9 }
            };

            // Act
            long result = StaticClass.ProductOfEvenValues(array);

            // Assert
            Assert.AreEqual(2L, result);
        }

        [TestMethod]
        public void ProductOfEvenValues_ArrayWithZero_ReturnsZero()
        {
            // Arrange
            int[,] array = {
                { 1, 0, 3 },
                { 4, 5, 6 }
            };

            // Act
            long result = StaticClass.ProductOfEvenValues(array);

            // Assert
            Assert.AreEqual(0L, result); // 0 * 4 * 6 = 0
        }

        // Тесты для SumEvenAboveSecondaryDiagonal
        [TestMethod]
        public void SumEvenAboveSecondaryDiagonal_ValidSquareArray_ReturnsCorrectSum()
        {
            // Arrange
            double[,] array = {
                { 1.0, 2.0, 3.0 },
                { 4.0, 5.0, 6.0 },
                { 7.0, 8.0, 9.0 }
            };

            // Act
            double result = StaticClass.SumEvenAboveSecondaryDiagonal(array);

            // Assert
            // Элементы выше побочной диагонали: [0,0]=1, [0,1]=2, [0,2]=3, [1,0]=4, [1,1]=5, [2,0]=7
            // Чётные из них: 2.0, 4.0
            Assert.AreEqual(6.0, result); // 2.0 + 4.0 = 6.0
        }

        [TestMethod]
        public void SumEvenAboveSecondaryDiagonal_ArrayWithNoEvenNumbers_ReturnsZero()
        {
            // Arrange
            double[,] array = {
                { 1.0, 3.0, 5.0 },
                { 7.0, 9.0, 11.0 },
                { 13.0, 15.0, 17.0 }
            };

            // Act
            double result = StaticClass.SumEvenAboveSecondaryDiagonal(array);

            // Assert
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SumEvenAboveSecondaryDiagonal_NonSquareArray_ThrowsArgumentException()
        {
            // Arrange
            double[,] array = {
                { 1.0, 2.0, 3.0 },
                { 4.0, 5.0, 6.0 }
            };

            // Act
            StaticClass.SumEvenAboveSecondaryDiagonal(array);
        }

        [TestMethod]
        public void SumEvenAboveSecondaryDiagonal_SingleElementArray_ReturnsElementIfEven()
        {
            // Arrange
            double[,] array = { { 4.0 } };

            // Act
            double result = StaticClass.SumEvenAboveSecondaryDiagonal(array);

            // Assert
            Assert.AreEqual(4.0, result);
        }

        [TestMethod]
        public void SumEvenAboveSecondaryDiagonal_SingleElementArray_ReturnsZeroIfOdd()
        {
            // Arrange
            double[,] array = { { 3.0 } };

            // Act
            double result = StaticClass.SumEvenAboveSecondaryDiagonal(array);

            // Assert
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public void SumEvenAboveSecondaryDiagonal_ArrayWithElementsOnDiagonal_IncludesDiagonalElements()
        {
            // Arrange
            double[,] array = {
                { 2.0, 3.0 },
                { 4.0, 6.0 }
            };

            // Act
            double result = StaticClass.SumEvenAboveSecondaryDiagonal(array);

            // Assert
            // Элементы выше побочной диагонали: [0,0]=2, [0,1]=3, [1,0]=4
            // Чётные из них: 2.0, 4.0
            Assert.AreEqual(6.0, result); // 2.0 + 4.0 = 6.0
        }
    }
}