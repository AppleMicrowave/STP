using Lab4_TPNumber;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab.Tests
{
    [TestClass]
    public class Test5
    {
        // ОСНОВНЫЕ ТЕСТЫ КОНСТРУКТОРОВ
        [TestMethod]
        public void Constructor_ValidParameters_CreatesObject()
        {
            TPNumber num = new TPNumber(10.5, 10, 2);
            Assert.AreEqual(10.5, num.GetNumber(), 1e-10);
            Assert.AreEqual(10, num.GetBase());
            Assert.AreEqual(2, num.GetPrecision());
        }

        [TestMethod]
        [ExpectedException(typeof(TPNumberException))]
        public void Constructor_InvalidBase_ThrowsException()
        {
            TPNumber num = new TPNumber(10.5, 1, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(TPNumberException))]
        public void Constructor_NegativePrecision_ThrowsException()
        {
            TPNumber num = new TPNumber(10.5, 10, -1);
        }

        // ТЕСТЫ АРИФМЕТИЧЕСКИХ ОПЕРАЦИЙ
        [TestMethod]
        public void Add_SameBaseAndPrecision_ReturnsCorrectResult()
        {
            TPNumber a = new TPNumber(10.0, 10, 2);
            TPNumber b = new TPNumber(5.0, 10, 2);
            TPNumber result = a.Add(b);
            Assert.AreEqual(15.0, result.GetNumber(), 1e-10);
        }

        [TestMethod]
        [ExpectedException(typeof(TPNumberException))]
        public void Add_DifferentBase_ThrowsException()
        {
            TPNumber a = new TPNumber(10.0, 10, 2);
            TPNumber b = new TPNumber(5.0, 16, 2);
            TPNumber result = a.Add(b);
        }

        [TestMethod]
        public void Multiply_ReturnsCorrectResult()
        {
            TPNumber a = new TPNumber(4.0, 10, 2);
            TPNumber b = new TPNumber(2.5, 10, 2);
            TPNumber result = a.Multiply(b);
            Assert.AreEqual(10.0, result.GetNumber(), 1e-10);
        }

        [TestMethod]
        public void Divide_ReturnsCorrectResult()
        {
            TPNumber a = new TPNumber(10.0, 10, 2);
            TPNumber b = new TPNumber(2.0, 10, 2);
            TPNumber result = a.Divide(b);
            Assert.AreEqual(5.0, result.GetNumber(), 1e-10);
        }

        [TestMethod]
        [ExpectedException(typeof(TPNumberException))]
        public void Divide_ByZero_ThrowsException()
        {
            TPNumber a = new TPNumber(10.0, 10, 2);
            TPNumber b = new TPNumber(0.0, 10, 2);
            TPNumber result = a.Divide(b);
        }

        // ТЕСТЫ СПЕЦИАЛЬНЫХ МЕТОДОВ
        [TestMethod]
        public void Reciprocal_ValidNumber_ReturnsCorrectResult()
        {
            TPNumber a = new TPNumber(4.0, 10, 3);
            TPNumber result = a.Reciprocal();
            Assert.AreEqual(0.25, result.GetNumber(), 1e-10);
        }

        [TestMethod]
        [ExpectedException(typeof(TPNumberException))]
        public void Reciprocal_Zero_ThrowsException()
        {
            TPNumber a = new TPNumber(0.0, 10, 2);
            TPNumber result = a.Reciprocal();
        }

        [TestMethod]
        public void Square_ReturnsCorrectResult()
        {
            TPNumber a = new TPNumber(5.0, 10, 2);
            TPNumber result = a.Square();
            Assert.AreEqual(25.0, result.GetNumber(), 1e-10);
        }

        // ТЕСТЫ ПРЕОБРАЗОВАНИЯ В СТРОКУ
        [TestMethod]
        public void GetNumberString_DecimalBase_ReturnsCorrectString()
        {
            TPNumber num = new TPNumber(15.75, 10, 2);
            string result = num.GetNumberString();
            Assert.AreEqual("15.75", result);
        }

        [TestMethod]
        public void GetNumberString_Hexadecimal_ReturnsCorrectString()
        {
            TPNumber num = new TPNumber(255.0, 16, 0);
            string result = num.GetNumberString();
            Assert.AreEqual("FF", result);
        }

        // ТЕСТЫ СТРОКОВОГО КОНСТРУКТОРА
        [TestMethod]
        public void StringConstructor_Valid_CreatesObject()
        {
            TPNumber num = new TPNumber("10.5", "10", "2");
            Assert.AreEqual(10.5, num.GetNumber(), 1e-10);
            Assert.AreEqual(10, num.GetBase());
            Assert.AreEqual(2, num.GetPrecision());
        }

        [TestMethod]
        public void StringConstructor_Hexadecimal_Valid()
        {
            TPNumber num = new TPNumber("A", "16", "1");
            Assert.AreEqual(10.0, num.GetNumber(), 1e-10);
        }

        [TestMethod]
        [ExpectedException(typeof(TPNumberException))]
        public void StringConstructor_InvalidDigit_ThrowsException()
        {
            TPNumber num = new TPNumber("G", "10", "2");
        }

        // ТЕСТЫ ВСПОМОГАТЕЛЬНЫХ МЕТОДОВ
        [TestMethod]
        public void Copy_ReturnsIdenticalObject()
        {
            TPNumber original = new TPNumber(15.75, 16, 3);
            TPNumber copy = original.Copy();
            Assert.AreEqual(original.GetNumber(), copy.GetNumber(), 1e-10);
            Assert.AreEqual(original.GetBase(), copy.GetBase());
            Assert.AreEqual(original.GetPrecision(), copy.GetPrecision());
        }

        [TestMethod]
        public void GetBaseString_ReturnsCorrectString()
        {
            TPNumber num = new TPNumber(10.0, 16, 2);
            Assert.AreEqual("16", num.GetBaseString());
        }

        [TestMethod]
        public void GetPrecisionString_ReturnsCorrectString()
        {
            TPNumber num = new TPNumber(10.0, 10, 3);
            Assert.AreEqual("3", num.GetPrecisionString());
        }

        // ТЕСТЫ EQUALS
        [TestMethod]
        public void Equals_SameValues_ReturnsTrue()
        {
            TPNumber a = new TPNumber(10.5, 10, 2);
            TPNumber b = new TPNumber(10.5, 10, 2);
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Equals_DifferentNumber_ReturnsFalse()
        {
            TPNumber a = new TPNumber(10.5, 10, 2);
            TPNumber b = new TPNumber(10.6, 10, 2);
            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void Equals_DifferentBase_ReturnsFalse()
        {
            TPNumber a = new TPNumber(10.5, 10, 2);
            TPNumber b = new TPNumber(10.5, 16, 2);
            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void ToString_ReturnsFormattedString()
        {
            TPNumber num = new TPNumber(15.75, 10, 2);
            string result = num.ToString();
            StringAssert.Contains(result, "15.75");
        }
    }
}