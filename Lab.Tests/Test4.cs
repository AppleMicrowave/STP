using Lab4_Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab.Tests
{
    [TestClass]
    public class Test4
    {
        [TestClass]
        public class MatrixTests
        {
            [TestMethod]
            [ExpectedException(typeof(MyException))]
            public void Matrix_Expected_MyException_i()
            {
                Matrix a = new Matrix(0, 2);
            }

            [TestMethod]
            [ExpectedException(typeof(MyException))]
            public void Matrix_Expected_MyException_j()
            {
                Matrix a = new Matrix(2, -1);
            }
        }

        // ТЕСТЫ КОНСТРУКТОРА - покрытие решений для проверок размеров
        [TestMethod]
        public void Matrix_ValidSize_NoException()
        {
            // Решение: i > 0 = true, j > 0 = true
            Matrix a = new Matrix(2, 2);
            Assert.AreEqual(2, a.I);
            Assert.AreEqual(2, a.J);
        }

        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void Matrix_Expected_MyException_i()
        {
            // Решение: i > 0 = false
            Matrix a = new Matrix(0, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void Matrix_Expected_MyException_j()
        {
            // Решение: j > 0 = false
            Matrix a = new Matrix(2, -1);
        }

        // ТЕСТЫ ИНДЕКСАТОРА - покрытие граничных значений и решений
        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void this_Expected_MyException_set_j()
        {
            Matrix a = new Matrix(2, 2);
            a[1, 3] = 2; // j > J-1 = true
        }

        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void this_Expected_MyException_get_i()
        {
            Matrix a = new Matrix(2, 2);
            int r = a[3, 1]; // i > I-1 = true
        }

        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void this_Expected_MyException_set_i_negative()
        {
            Matrix a = new Matrix(2, 2);
            a[-1, 1] = 2; // i < 0 = true
        }

        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void this_Expected_MyException_get_j_negative()
        {
            Matrix a = new Matrix(2, 2);
            int r = a[1, -1]; // j < 0 = true
        }

        [TestMethod]
        public void this_ValidIndices_NoException()
        {
            // Решения: все проверки границ = false
            Matrix a = new Matrix(2, 2);
            a[0, 0] = 1; // нижняя граница
            a[1, 1] = 2; // верхняя граница
            Assert.AreEqual(1, a[0, 0]);
            Assert.AreEqual(2, a[1, 1]);
        }

        // ТЕСТЫ ОПЕРАТОРА РАВЕНСТВА - покрытие всех решений
        [TestMethod]
        public void Equal_SameMatrices_True()
        {
            Matrix a = new Matrix(2, 2);
            a[0, 0] = 1; a[0, 1] = 1; a[1, 0] = 1; a[1, 1] = 1;
            Matrix b = new Matrix(2, 2);
            b[0, 0] = 1; b[0, 1] = 1; b[1, 0] = 1; b[1, 1] = 1;

            Assert.IsTrue(a == b);
        }

        [TestMethod]
        public void Equal_DifferentMatrices_False()
        {
            Matrix a = new Matrix(2, 2);
            a[0, 0] = 1; a[0, 1] = 1;
            Matrix b = new Matrix(2, 2);
            b[0, 0] = 1; b[0, 1] = 2; // разные значения

            Assert.IsFalse(a == b);
        }

        [TestMethod]
        public void Equal_DifferentSizes_False()
        {
            Matrix a = new Matrix(2, 2);
            Matrix b = new Matrix(3, 3); // разные размеры

            Assert.IsFalse(a == b);
        }

        [TestMethod]
        public void NotEqual_Operator_Correct()
        {
            Matrix a = new Matrix(2, 2);
            a[0, 0] = 1;
            Matrix b = new Matrix(2, 2);
            b[0, 0] = 2;

            Assert.IsTrue(a != b);
        }

        // ТЕСТЫ СЛОЖЕНИЯ - покрытие решений для проверки размеров
        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void Addition_DifferentSizes_ExpectedException()
        {
            // Решение: a.I != b.I || a.J != b.J = true
            Matrix a = new Matrix(2, 2);
            Matrix b = new Matrix(3, 3);

            Matrix result = a + b;
        }

        [TestMethod]
        public void Summa_Correct()
        {
            // Решение: a.I != b.I || a.J != b.J = false
            Matrix a = new Matrix(2, 2);
            a[0, 0] = 1; a[0, 1] = 1; a[1, 0] = 1; a[1, 1] = 1;
            Matrix b = new Matrix(2, 2);
            b[0, 0] = 2; b[0, 1] = 2; b[1, 0] = 2; b[1, 1] = 2;
            Matrix expected = new Matrix(2, 2);
            expected[0, 0] = 3; expected[0, 1] = 3;
            expected[1, 0] = 3; expected[1, 1] = 3;

            Matrix actual = a + b;

            Assert.IsTrue(actual == expected);
        }

        // ТЕСТЫ ВЫЧИТАНИЯ - покрытие решений
        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void Subtraction_DifferentSizes_ExpectedException()
        {
            Matrix a = new Matrix(2, 2);
            Matrix b = new Matrix(3, 3);

            Matrix result = a - b;
        }

        [TestMethod]
        public void Subtraction_Correct()
        {
            Matrix a = new Matrix(2, 2);
            a[0, 0] = 5; a[0, 1] = 5;
            a[1, 0] = 5; a[1, 1] = 5;

            Matrix b = new Matrix(2, 2);
            b[0, 0] = 2; b[0, 1] = 2;
            b[1, 0] = 2; b[1, 1] = 2;

            Matrix expected = new Matrix(2, 2);
            expected[0, 0] = 3; expected[0, 1] = 3;
            expected[1, 0] = 3; expected[1, 1] = 3;

            Matrix actual = a - b;

            Assert.IsTrue(actual == expected);
        }

        // ТЕСТЫ УМНОЖЕНИЯ - покрытие решений
        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void Multiplication_IncompatibleSizes_ExpectedException()
        {
            // Решение: a.J != b.I = true
            Matrix a = new Matrix(2, 3);
            Matrix b = new Matrix(2, 2);

            Matrix result = a * b;
        }

        [TestMethod]
        public void Multiplication_Correct()
        {
            // Решение: a.J != b.I = false
            Matrix a = new Matrix(2, 2);
            a[0, 0] = 1; a[0, 1] = 2;
            a[1, 0] = 3; a[1, 1] = 4;

            Matrix b = new Matrix(2, 2);
            b[0, 0] = 2; b[0, 1] = 0;
            b[1, 0] = 1; b[1, 1] = 2;

            Matrix expected = new Matrix(2, 2);
            expected[0, 0] = 4; expected[0, 1] = 4;
            expected[1, 0] = 10; expected[1, 1] = 8;

            Matrix actual = a * b;

            Assert.IsTrue(actual == expected);
        }

        // ТЕСТЫ ТРАНСПОНИРОВАНИЯ - покрытие решений
        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void Transpose_NonSquareMatrix_ExpectedException()
        {
            // Решение: I != J = true
            Matrix a = new Matrix(2, 3);

            Matrix result = a.Transp();
        }

        [TestMethod]
        public void Transpose_Correct()
        {
            // Решение: I != J = false
            Matrix a = new Matrix(2, 2);
            a[0, 0] = 1; a[0, 1] = 2;
            a[1, 0] = 3; a[1, 1] = 4;

            Matrix expected = new Matrix(2, 2);
            expected[0, 0] = 1; expected[0, 1] = 3;
            expected[1, 0] = 2; expected[1, 1] = 4;

            Matrix actual = a.Transp();

            Assert.IsTrue(actual == expected);
        }

        // ТЕСТЫ МЕТОДА Min() - покрытие различных сценариев
        [TestMethod]
        public void Min_Correct()
        {
            Matrix a = new Matrix(2, 2);
            a[0, 0] = 5; a[0, 1] = 2;
            a[1, 0] = 3; a[1, 1] = 8;

            int expected = 2;

            int actual = a.Min();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Min_WithNegativeNumbers()
        {
            Matrix a = new Matrix(2, 2);
            a[0, 0] = -5; a[0, 1] = 2;
            a[1, 0] = 3; a[1, 1] = -1;

            int expected = -5;

            int actual = a.Min();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Min_SingleElement()
        {
            Matrix a = new Matrix(1, 1);
            a[0, 0] = 7;

            int expected = 7;

            int actual = a.Min();

            Assert.AreEqual(expected, actual);
        }

        // ТЕСТЫ ToString()
        [TestMethod]
        public void ToString_Correct()
        {
            Matrix a = new Matrix(2, 2);
            a[0, 0] = 1; a[0, 1] = 2;
            a[1, 0] = 3; a[1, 1] = 4;

            string expected = "{{1,2},{3,4}}";

            string actual = a.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString_SingleElement()
        {
            Matrix a = new Matrix(1, 1);
            a[0, 0] = 5;

            string expected = "{{5}}";

            string actual = a.ToString();

            Assert.AreEqual(expected, actual);
        }

        // ТЕСТЫ Equals() и GetHashCode()
        [TestMethod]
        public void Equals_WithNull_False()
        {
            Matrix a = new Matrix(2, 2);

            Assert.IsFalse(a.Equals(null));
        }

        [TestMethod]
        public void Equals_WithDifferentType_False()
        {
            Matrix a = new Matrix(2, 2);

            Assert.IsFalse(a.Equals("string"));
        }
    }
}