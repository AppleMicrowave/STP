using Lab1;
namespace Lab.Tests
{
    [TestClass]
    public class Tests1
    {
        // Тесты для MultiplyEvenIndexedElements
        [TestMethod]
        public void MultiplyEvenIndexedElements_ValidArray_ReturnsCorrectProduct()
        {
            // Arrange
            double[] array = { 1.0, 2.0, 3.0, 4.0, 5.0 };

            // Act
            double result = Functions.MultiplyEvenIndexedElements(array);

            // Assert
            Assert.AreEqual(15.0, result); // 1 * 3 * 5 = 15
        }

        [TestMethod]
        public void MultiplyEvenIndexedElements_EmptyArray_ReturnsZero()
        {
            // Arrange
            double[] array = new double[0];

            // Act
            double result = Functions.MultiplyEvenIndexedElements(array);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void MultiplyEvenIndexedElements_NullArray_ReturnsZero()
        {
            // Arrange & Act
            double result = Functions.MultiplyEvenIndexedElements(null);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void MultiplyEvenIndexedElements_SingleElement_ReturnsElement()
        {
            // Arrange
            double[] array = { 7.5 };

            // Act
            double result = Functions.MultiplyEvenIndexedElements(array);

            // Assert
            Assert.AreEqual(7.5, result);
        }

        // Тесты для CyclicShift
        [TestMethod]
        public void CyclicShift_ShiftRight_ValidShift()
        {
            // Arrange
            double[] array = { 1.0, 2.0, 3.0, 4.0, 5.0 };
            double[] expected = { 4.0, 5.0, 1.0, 2.0, 3.0 };

            // Act
            Functions.CyclicShift(array, 2, "right");

            // Assert
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void CyclicShift_ShiftLeft_ValidShift()
        {
            // Arrange
            double[] array = { 1.0, 2.0, 3.0, 4.0, 5.0 };
            double[] expected = { 3.0, 4.0, 5.0, 1.0, 2.0 };

            // Act
            Functions.CyclicShift(array, 2, "left");

            // Assert
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void CyclicShift_ShiftMoreThanLength_CorrectShift()
        {
            // Arrange
            double[] array = { 1.0, 2.0, 3.0 };
            double[] expected = { 3.0, 1.0, 2.0 };

            // Act
            Functions.CyclicShift(array, 4, "right"); // 4 % 3 = 1

            // Assert
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void CyclicShift_EmptyArray_NoChange()
        {
            // Arrange
            double[] array = new double[0];
            double[] expected = new double[0];

            // Act
            Functions.CyclicShift(array, 2, "right");

            // Assert
            CollectionAssert.AreEqual(expected, array);
        }

        // Тесты для FindMaxEvenElementWithEvenIndex
        [TestMethod]
        public void FindMaxEvenElementWithEvenIndex_ValidArray_ReturnsMax()
        {
            // Arrange
            int[] array = { 2, 3, 4, 5, 8, 7, 6, 9 };

            // Act
            int result = Functions.FindMaxEvenElementWithEvenIndex(array);

            // Assert
            Assert.AreEqual(8, result); // Чётные элементы с чётными индексами: 2, 4, 8, 6
        }

        [TestMethod]
        public void FindMaxEvenElementWithEvenIndex_NoSuitableElements_ReturnsMinValue()
        {
            // Arrange
            int[] array = { 1, 2, 3, 4, 5, 6 };

            // Act
            int result = Functions.FindMaxEvenElementWithEvenIndex(array);

            // Assert
            Assert.AreEqual(int.MinValue, result);
        }

        [TestMethod]
        public void FindMaxEvenElementWithEvenIndex_EmptyArray_ReturnsMinValue()
        {
            // Arrange
            int[] array = new int[0];

            // Act
            int result = Functions.FindMaxEvenElementWithEvenIndex(array);

            // Assert
            Assert.AreEqual(int.MinValue, result);
        }

        [TestMethod]
        public void FindMaxEvenElementWithEvenIndex_NullArray_ReturnsMinValue()
        {
            // Arrange & Act
            int result = Functions.FindMaxEvenElementWithEvenIndex(null);

            // Assert
            Assert.AreEqual(int.MinValue, result);
        }

        [TestMethod]
        public void FindMaxEvenElementWithEvenIndex_SingleEvenElement_ReturnsElement()
        {
            // Arrange
            int[] array = { 4 };

            // Act
            int result = Functions.FindMaxEvenElementWithEvenIndex(array);

            // Assert
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void FindMaxEvenElementWithEvenIndex_SingleOddElement_ReturnsMinValue()
        {
            // Arrange
            int[] array = { 3 };

            // Act
            int result = Functions.FindMaxEvenElementWithEvenIndex(array);

            // Assert
            Assert.AreEqual(int.MinValue, result);
        }
    }
}

