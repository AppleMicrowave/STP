using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab6_TEditor;

namespace Lab.Tests
{
    [TestClass]
    public class Test6
    {
        [TestMethod]
        public void Constructor_Default_CreatesZeroNumber()
        {
            var editor = new ComplexNumberEditor();
            Assert.AreEqual("0,+i*0,", editor.NumberString);
        }

        [TestMethod]
        public void Constructor_WithValidString_SetsNumber()
        {
            var editor = new ComplexNumberEditor("12,+i*34,");
            Assert.AreEqual("12,+i*34,", editor.NumberString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithInvalidString_ThrowsException()
        {
            var editor = new ComplexNumberEditor("invalid");
        }

        [TestMethod]
        public void SetString_ValidNumber_UpdatesValue()
        {
            var editor = new ComplexNumberEditor();
            editor.SetString("-5,+i*7,");
            Assert.AreEqual("-5,+i*7,", editor.NumberString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetString_InvalidNumber_ThrowsException()
        {
            var editor = new ComplexNumberEditor();
            editor.SetString("abc,+i*def,");
        }

        [TestMethod]
        public void IsZero_ForZeroNumber_ReturnsTrue()
        {
            var editor = new ComplexNumberEditor("0,+i*0,");
            Assert.IsTrue(editor.IsZero);
        }

        [TestMethod]
        public void IsZero_ForNonZeroNumber_ReturnsFalse()
        {
            var editor = new ComplexNumberEditor("1,+i*0,");
            Assert.IsFalse(editor.IsZero);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddDigit_DigitLessThanZero_ThrowsException()
        {
            var editor = new ComplexNumberEditor();
            editor.AddDigit(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddDigit_DigitGreaterThanNine_ThrowsException()
        {
            var editor = new ComplexNumberEditor();
            editor.AddDigit(10);
        }

        [TestMethod]
        public void Clear_SetsToZero()
        {
            var editor = new ComplexNumberEditor("123,+i*456,");
            var result = editor.Clear();
            Assert.AreEqual("0,+i*0,", result);
        }

        [TestMethod]
        public void Edit_Command_Clear()
        {
            var editor = new ComplexNumberEditor("123,+i*456,");
            var result = editor.Edit(3);
            Assert.AreEqual("0,+i*0,", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Edit_InvalidCommand_ThrowsException()
        {
            var editor = new ComplexNumberEditor();
            editor.Edit(20);
        }

        [TestMethod]
        public void ToString_ReturnsNumberString()
        {
            var editor = new ComplexNumberEditor("12,+i*34,");
            Assert.AreEqual("12,+i*34,", editor.ToString());
        }

        [TestMethod]
        public void AddDigit_WhenPartIsZero_DoesNotAdd()
        {
            var editor = new ComplexNumberEditor("0,+i*0,");
            var result = editor.AddDigit(5);
            Assert.AreEqual("0,+i*0,", result);
        }

        [TestMethod]
        public void AddDigit_WhenPartIsNegativeZero_DoesNotAdd()
        {
            var editor = new ComplexNumberEditor("-0,+i*0,");
            var result = editor.AddDigit(5);
            Assert.AreEqual("-0,+i*0,", result);
        }

        [TestMethod]
        public void IsValidComplexNumber_ValidNumbers_ReturnsTrue()
        {
            var editor = new ComplexNumberEditor();
            Assert.IsTrue(IsValidComplexNumberPrivate(editor, "123,+i*456,"));
            Assert.IsTrue(IsValidComplexNumberPrivate(editor, "-123,+i*456,"));
            Assert.IsTrue(IsValidComplexNumberPrivate(editor, "123,+i*-456,"));
            Assert.IsTrue(IsValidComplexNumberPrivate(editor, "-123,+i*-456,"));
        }

        [TestMethod]
        public void IsValidComplexNumber_InvalidNumbers_ReturnsFalse()
        {
            var editor = new ComplexNumberEditor();
            Assert.IsFalse(IsValidComplexNumberPrivate(editor, "123,i*456,"));
            Assert.IsFalse(IsValidComplexNumberPrivate(editor, "123,+i*456"));
            Assert.IsFalse(IsValidComplexNumberPrivate(editor, "abc,+i*def,"));
            Assert.IsFalse(IsValidComplexNumberPrivate(editor, "123.45,+i*67.89,"));
        }

        private bool IsValidComplexNumberPrivate(ComplexNumberEditor editor, string number)
        {
            var method = typeof(ComplexNumberEditor).GetMethod("IsValidComplexNumber",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (bool)method.Invoke(editor, new object[] { number });
        }
    }
}