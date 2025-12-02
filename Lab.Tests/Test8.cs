using Lab7_MN;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lab8.Tests
{
    [TestClass]
    public class TProcTests
    {
        [TestMethod]
        public void Add_Ints()
        {
            var proc = new TProc<int>();
            proc.Lop_Res = 5;
            proc.Rop = 3;

            proc.OprtnSet(TOprtn.Add);
            proc.OprtnRun();

            Assert.AreEqual(8, proc.Lop_Res);
        }

        [TestMethod]
        public void Sub_Ints()
        {
            var proc = new TProc<int>();
            proc.Lop_Res = 10;
            proc.Rop = 4;

            proc.OprtnSet(TOprtn.Sub);
            proc.OprtnRun();

            Assert.AreEqual(6, proc.Lop_Res);
        }

        [TestMethod]
        public void Mul_Ints()
        {
            var proc = new TProc<int>();
            proc.Lop_Res = 7;
            proc.Rop = 6;

            proc.OprtnSet(TOprtn.Mul);
            proc.OprtnRun();

            Assert.AreEqual(42, proc.Lop_Res);
        }

        [TestMethod]
        public void Div_Ints()
        {
            var proc = new TProc<int>();
            proc.Lop_Res = 20;
            proc.Rop = 5;

            proc.OprtnSet(TOprtn.Dvd);
            proc.OprtnRun();

            Assert.AreEqual(4, proc.Lop_Res);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Div_By_Zero_Throws()
        {
            var proc = new TProc<int>();
            proc.Lop_Res = 10;
            proc.Rop = 0;

            proc.OprtnSet(TOprtn.Dvd);
            proc.OprtnRun();
        }

        [TestMethod]
        public void Func_Sqr()
        {
            var proc = new TProc<int>();
            proc.Rop = 5;

            proc.FuncRun(TFunc.Sqr);

            Assert.AreEqual(25, proc.Rop);
        }

        [TestMethod]
        public void Func_Rev_Int()
        {
            var proc = new TProc<double>();
            proc.Rop = 2.0;

            proc.FuncRun(TFunc.Rev);

            Assert.AreEqual(0.5, proc.Rop);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Func_Rev_Zero_Throws()
        {
            var proc = new TProc<double>();
            proc.Rop = 0.0;

            proc.FuncRun(TFunc.Rev);
        }

        [TestMethod]
        public void Reset_Works()
        {
            var proc = new TProc<int>();
            proc.Lop_Res = 5;
            proc.Rop = 10;
            proc.OprtnSet(TOprtn.Add);

            proc.ReSet();

            Assert.AreEqual(0, proc.Lop_Res);
            Assert.AreEqual(0, proc.Rop);
            Assert.AreEqual(TOprtn.None, proc.Operation);
        }
    }

    [TestClass]
    public class TFracTests
    {
        [TestMethod]
        public void Fraction_Addition_Works()
        {
            var a = new TFrac(1, 2);
            var b = new TFrac(1, 3);

            var result = a + b;

            Assert.AreEqual("5/6", result.ToString());
        }

        [TestMethod]
        public void Simplifies_On_Creation()
        {
            var frac = new TFrac(4, 8);

            Assert.AreEqual("1/2", frac.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Denominator_Zero_Throws()
        {
            var f = new TFrac(5, 0);
        }

        [TestMethod]
        public void Parses_String_Fraction()
        {
            var frac = new TFrac("6/9");

            Assert.AreEqual("2/3", frac.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Invalid_String_Throws()
        {
            var frac = new TFrac("abc");
        }

        [TestMethod]
        public void Negative_Denominator_Flips()
        {
            var frac = new TFrac(1, -3);

            Assert.AreEqual("-1/3", frac.ToString());
        }
    }
}
