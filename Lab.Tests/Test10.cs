using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab10;

namespace Lab10.Tests
{
    [TestClass]
    public class PolynomialMultiplicationTests
    {
        [TestMethod]
        public void Multiply_ZeroPolynomials_ReturnsZero()
        {
            var p1 = new TPoly();
            var p2 = new TPoly();
            var result = p1.Multiply(p2);
            Assert.AreEqual("0*X^0", result.ToString());
            Assert.AreEqual(0, result.Degree());
        }

        [TestMethod]
        public void Multiply_ZeroAndNonZero_ReturnsZero()
        {
            var p1 = new TPoly();
            var p2 = new TPoly(5, 3);
            var result = p1.Multiply(p2);
            Assert.AreEqual("0*X^0", result.ToString());
        }

        [TestMethod]
        public void Multiply_Constants_ReturnsConstant()
        {
            var p1 = new TPoly(3, 0);
            var p2 = new TPoly(4, 0);
            var result = p1.Multiply(p2);
            Assert.AreEqual("12*X^0", result.ToString());
        }

        [TestMethod]
        public void Multiply_LinearPolynomials_ReturnsQuadratic()
        {
            var p1 = new TPoly(2, 1); // 2x
            var p2 = new TPoly(3, 1); // 3x
            var result = p1.Multiply(p2); // 6x²
            Assert.AreEqual("6*X^2", result.ToString());
        }

        [TestMethod]
        public void Multiply_PolynomialWithDifferentDegrees()
        {
            var p1 = new TPoly(2, 1).Add(new TPoly(3, 2)); // 3x² + 2x
            var p2 = new TPoly(1, 0).Add(new TPoly(4, 1)); // 4x + 1

            var result = p1.Multiply(p2);
            // (3x² + 2x)(4x + 1) = 12x³ + 3x² + 8x² + 2x = 12x³ + 11x² + 2x
            Assert.AreEqual("2*X^1 + 11*X^2 + 12*X^3", result.ToString());
        }

        [TestMethod]
        public void Multiply_Binomials_ReturnsDifferenceOfSquares()
        {
            var p1 = new TPoly(1, 0).Add(new TPoly(1, 1)); // x + 1
            var p2 = new TPoly(1, 0).Add(new TPoly(-1, 1)); // -x + 1

            var result = p1.Multiply(p2); // (x+1)(1-x) = -x² + 1
            Assert.AreEqual("1*X^0 + -1*X^2", result.ToString());
        }

        [TestMethod]
        public void Multiply_PolynomialWithZeroMembers_IgnoresZeros()
        {
            var p1 = new TPoly(2, 1).Add(new TPoly(0, 5));
            var p2 = new TPoly(3, 2);

            var result = p1.Multiply(p2); // (2x)(3x²) = 6x³
            Assert.AreEqual("6*X^3", result.ToString());
        }

        [TestMethod]
        public void Multiply_CommutativeProperty()
        {
            var p1 = new TPoly(2, 3).Add(new TPoly(5, 1));
            var p2 = new TPoly(3, 2).Add(new TPoly(4, 0));

            var result1 = p1.Multiply(p2);
            var result2 = p2.Multiply(p1);

            Assert.AreEqual(result1.ToString(), result2.ToString());
            Assert.IsTrue(result1.Equals(result2));
        }

        [TestMethod]
        public void Multiply_AssociativeProperty()
        {
            var p1 = new TPoly(2, 1);
            var p2 = new TPoly(3, 1);
            var p3 = new TPoly(4, 1);

            var result1 = p1.Multiply(p2).Multiply(p3);
            var result2 = p1.Multiply(p2.Multiply(p3));

            Assert.AreEqual(result1.ToString(), result2.ToString());
        }

        [TestMethod]
        public void Multiply_HighDegreePolynomials()
        {
            var p1 = new TPoly(5, 10);
            var p2 = new TPoly(3, 5);

            var result = p1.Multiply(p2); // 15x¹⁵
            Assert.AreEqual("15*X^15", result.ToString());
            Assert.AreEqual(15, result.Degree());
        }
    }
}