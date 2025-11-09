using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab7_MN;

namespace Lab.Tests
{
    [TestClass]
    public class Test7
    {
        [TestMethod]
        public void TMemory_DefaultConstructor_CreatesMemoryWithDefaultValueAndOffState()
        {
            var memory = new TMemory<TFrac>();

            Assert.AreEqual(0, memory.Number.Numerator);
            Assert.AreEqual(1, memory.Number.Denominator);
            Assert.AreEqual(MemoryState.Off, memory.State);
        }

        [TestMethod]
        public void TMemory_ConstructorWithParameter_CreatesMemoryWithGivenValueAndOnState()
        {
            var initialFrac = new TFrac(1, 2);

            var memory = new TMemory<TFrac>(initialFrac);

            Assert.AreEqual(1, memory.Number.Numerator);
            Assert.AreEqual(2, memory.Number.Denominator);
            Assert.AreEqual(MemoryState.On, memory.State);
        }

        [TestMethod]
        public void TMemory_Store_SetsNumberAndTurnsOnState()
        {
            var memory = new TMemory<TFrac>();
            var newFrac = new TFrac(3, 4);

            memory.Store(newFrac);

            Assert.AreEqual(3, memory.Number.Numerator);
            Assert.AreEqual(4, memory.Number.Denominator);
            Assert.AreEqual(MemoryState.On, memory.State);
        }

        [TestMethod]
        public void TMemory_Add_AddsNumberToStoredValueAndTurnsOnState()
        {
            var initialFrac = new TFrac(1, 4);
            var memory = new TMemory<TFrac>(initialFrac);
            var addFrac = new TFrac(1, 2);

            memory.Add(addFrac);

            Assert.AreEqual(3, memory.Number.Numerator);
            Assert.AreEqual(4, memory.Number.Denominator);
            Assert.AreEqual(MemoryState.On, memory.State);
        }

        [TestMethod]
        public void TMemory_Clear_SetsDefaultValueAndTurnsOffState()
        {
            var initialFrac = new TFrac(5, 6);
            var memory = new TMemory<TFrac>(initialFrac);

            memory.Clear();

            Assert.AreEqual(0, memory.Number.Numerator);
            Assert.AreEqual(1, memory.Number.Denominator);
            Assert.AreEqual(MemoryState.Off, memory.State);
        }

        [TestMethod]
        public void TMemory_GetState_ReturnsCorrectStateString()
        {
            var memoryOn = new TMemory<TFrac>(new TFrac(1, 2));
            var memoryOff = new TMemory<TFrac>();

            Assert.AreEqual("On", memoryOn.GetState());
            Assert.AreEqual("Off", memoryOff.GetState());
        }

        [TestMethod]
        public void TMemory_StateProperty_ReturnsCorrectState()
        {
            var memoryOn = new TMemory<TFrac>(new TFrac(1, 2));
            var memoryOff = new TMemory<TFrac>();

            Assert.AreEqual(MemoryState.On, memoryOn.State);
            Assert.AreEqual(MemoryState.Off, memoryOff.State);
        }

        [TestMethod]
        public void TMemory_NumberProperty_ReturnsCorrectNumber()
        {
            var testFrac = new TFrac(4, 9);
            var memory = new TMemory<TFrac>(testFrac);

            Assert.AreEqual(4, memory.Number.Numerator);
            Assert.AreEqual(9, memory.Number.Denominator);
        }

        [TestMethod]
        public void TMemory_ToString_ReturnsCorrectFormat()
        {
            var memory = new TMemory<TFrac>(new TFrac(2, 5));

            var result = memory.ToString();

            StringAssert.Contains(result, "2/5");
            StringAssert.Contains(result, "State: On");
        }

        [TestMethod]
        public void TMemory_AddWithNegativeFractions_WorksCorrectly()
        {
            var initialFrac = new TFrac(1, 3);
            var memory = new TMemory<TFrac>(initialFrac);
            var negativeFrac = new TFrac(-1, 6);

            memory.Add(negativeFrac);

            Assert.AreEqual(1, memory.Number.Numerator);
            Assert.AreEqual(6, memory.Number.Denominator);
        }

        [TestMethod]
        public void TMemory_StoreOverwritesPreviousValue()
        {
            var memory = new TMemory<TFrac>(new TFrac(1, 2));
            var newFrac = new TFrac(3, 4);

            memory.Store(newFrac);

            Assert.AreEqual(3, memory.Number.Numerator);
            Assert.AreEqual(4, memory.Number.Denominator);
        }

        [TestMethod]
        public void TMemory_MultipleOperations_WorkCorrectlyInSequence()
        {
            var memory = new TMemory<TFrac>();

            memory.Store(new TFrac(1, 3));
            Assert.AreEqual(1, memory.Number.Numerator);
            Assert.AreEqual(3, memory.Number.Denominator);

            memory.Add(new TFrac(1, 6));
            Assert.AreEqual(1, memory.Number.Numerator);
            Assert.AreEqual(2, memory.Number.Denominator);

            memory.Clear();
            Assert.AreEqual(0, memory.Number.Numerator);
            Assert.AreEqual(1, memory.Number.Denominator);
            Assert.AreEqual(MemoryState.Off, memory.State);
        }

        [TestMethod]
        public void TFrac_Addition_WorksCorrectly()
        {
            var frac1 = new TFrac(1, 2);
            var frac2 = new TFrac(1, 3);

            var result = frac1 + frac2;

            Assert.AreEqual(5, result.Numerator);
            Assert.AreEqual(6, result.Denominator);
        }

        [TestMethod]
        public void TFrac_Simplify_WorksCorrectly()
        {
            var frac = new TFrac(2, 4);

            Assert.AreEqual(1, frac.Numerator);
            Assert.AreEqual(2, frac.Denominator);
        }

        [TestMethod]
        public void TFrac_ConstructorWithString_ParsesCorrectly()
        {
            var frac = new TFrac("3/5");

            Assert.AreEqual(3, frac.Numerator);
            Assert.AreEqual(5, frac.Denominator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TFrac_ConstructorWithZeroDenominator_ThrowsException()
        {
            var frac = new TFrac(1, 0);
        }
    }
}