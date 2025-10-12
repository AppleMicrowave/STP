using ConsoleApplicationMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Tests
{
    internal class Test4
    {
        [TestClass]
        public class MatrixTests
        {
            [TestMethod]
            [ExpectedException(typeof(MyException))] //Тип ожидаемого исключения.
            public void Matrix_Expected_MyException_i()
            {
                Matrix a = new Matrix(0, 2);
            }
            [TestMethod]
            [ExpectedException(typeof(MyException))]//Тип ожидаемого исключения.
            public void Matrix_Expected_MyException_j()
            {
                Matrix a = new Matrix(2, -1);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void Matrix_Expected_MyException_i()
        {
            //act (выполнить)
            Matrix a = new Matrix(0, 2);
        }
        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void Matrix_Expected_MyException_j()
        {
            //act (выполнить)
            Matrix a = new Matrix(2, -1);
        }
        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void this_Expected_MyException_set_j()
        {
            //act (выполнить)
            Matrix a = new Matrix(2, 2);
            a[1, 3] = 2;
        }
        [TestMethod]
        [ExpectedException(typeof(MyException))]
        public void this_Expected_MyException_get_i()
        {
            //act (выполнить)
            Matrix a = new Matrix(2, 2);
            int r = a[3, 1];
        }
        [TestMethod]
        public void Equel()
        {
            //arrange(обеспечить)
            Matrix a = new Matrix(2, 2);
            a[0, 0] = 1; a[0, 1] = 1; a[1, 0] = 1; a[1, 1] = 1;
            Matrix b = new Matrix(2, 2);
            b[0, 0] = 1; b[0, 1] = 1; b[1, 0] = 1; b[1, 1] = 1;
            //act (выполнить)
            //bool r = a == b;
            //assert(доказать)
            //Assert.IsTrue(r);
            Assert.AreEqual(a, b);
        }
        [TestMethod]
        public void Summa()
        {
            //arrange(обеспечить)
            Matrix a = new Matrix(2, 2);
            a[0, 0] = 1; a[0, 1] = 1; a[1, 0] = 1; a[1, 1] = 1;
            Matrix b = new Matrix(2, 2);
            b[0, 0] = 2; b[0, 1] = 2; b[1, 0] = 2; b[1, 1] = 2;
            Matrix expected = new Matrix(2, 2);
            expected[0, 0] = 3; expected[0, 1] = 3;
            expected[1, 0] = 3; expected[1, 1] = 3;
            Matrix actual = new Matrix(2, 2);
            //act (выполнить)
            actual = a + b;
            //assert(доказать)
            Assert.IsTrue(actual == expected);//Оракул
        }
    }
}
