using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab6_TEditor;

namespace Lab.Tests
{
    [TestClass]
    public class Test6
    {
        [TestMethod]
        public void Constructor_Default_SetsZeroRepresentation()
        {
            var editor = new ComplexNumberEditor();
            Assert.AreEqual("0,+i*0,", editor.NumberString);
        }

        [TestMethod]
        public void Constructor_WithValidString_SetsCorrectValue()
        {
            var editor = new ComplexNumberEditor("123,+i*456,");
            Assert.AreEqual("123,+i*456,", editor.NumberString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithInvalidString_ThrowsException()
        {
            var editor = new ComplexNumberEditor("invalid");
        }

        [TestMethod]
        public void SetString_ValidFormat_SetsValueCorrectly()
        {
            var editor = new ComplexNumberEditor();
            editor.SetString("-123,+i*456,");
            Assert.AreEqual("-123,+i*456,", editor.NumberString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetString_InvalidFormat_ThrowsException()
        {
            var editor = new ComplexNumberEditor();
            editor.SetString("invalid format");
        }

        [TestMethod]
        public void IsZero_WithZeroValue_ReturnsTrue()
        {
            var editor = new ComplexNumberEditor("0,+i*0,");
            Assert.IsTrue(editor.IsZero);
        }

        [TestMethod]
        public void IsZero_WithNonZeroValue_ReturnsFalse()
        {
            var editor = new ComplexNumberEditor("1,+i*0,");
            Assert.IsFalse(editor.IsZero);
        }

        [TestMethod]
        public void ToggleSign_PositiveReal_TogglesToNegative()
        {
            var editor = new ComplexNumberEditor("123,+i*0,");
            var result = editor.ToggleSign();
            Assert.AreEqual("-123,+i*0,", result);
        }

        [TestMethod]
        public void ToggleSign_NegativeReal_TogglesToPositive()
        {
            var editor = new ComplexNumberEditor("-123,+i*0,");
            var result = editor.ToggleSign();
            Assert.AreEqual("123,+i*0,", result);
        }

        [TestMethod]
        public void ToggleSign_PositiveImaginary_TogglesToNegative()
        {
            var editor = new ComplexNumberEditor("0,+i*456,");
            var result = editor.ToggleSign();
            Assert.AreEqual("0,+i*-456,", result);
        }

        [TestMethod]
        public void ToggleSign_NegativeImaginary_TogglesToPositive()
        {
            var editor = new ComplexNumberEditor("0,+i*-456,");
            var result = editor.ToggleSign();
            Assert.AreEqual("0,+i*456,", result);
        }

        [TestMethod]
        public void ToggleSign_BothParts_TogglesImaginaryPart()
        {
            var editor = new ComplexNumberEditor("123,+i*456,");
            var result = editor.ToggleSign();
            Assert.AreEqual("123,+i*-456,", result);
        }

        [TestMethod]
        public void AddDigit_ValidDigit_AddsToImaginaryPart()
        {
            var editor = new ComplexNumberEditor("123,+i*45,");
            var result = editor.AddDigit(6);
            Assert.AreEqual("123,+i*456,", result);
        }

        [TestMethod]
        public void AddDigit_ValidDigit_AddsToRealPart()
        {
            var editor = new ComplexNumberEditor("12,+i*456,");
            var result = editor.AddDigit(3);
            Assert.AreEqual("123,+i*456,", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddDigit_InvalidDigit_ThrowsException()
        {
            var editor = new ComplexNumberEditor();
            editor.AddDigit(10);
        }

        [TestMethod]
        public void AddZero_AddsZeroDigit()
        {
            var editor = new ComplexNumberEditor("123,+i*45,");
            var result = editor.AddZero();
            Assert.AreEqual("123,+i*450,", result);
        }

        [TestMethod]
        public void Backspace_ImaginaryPartNotEmpty_RemovesLastDigit()
        {
            var editor = new ComplexNumberEditor("123,+i*456,");
            var result = editor.Backspace();
            Assert.AreEqual("123,+i*45,", result);
        }

        [TestMethod]
        public void Backspace_RealPartNotEmpty_RemovesLastDigit()
        {
            var editor = new ComplexNumberEditor("123,+i*0,");
            var result = editor.Backspace();
            Assert.AreEqual("12,+i*0,", result);
        }

        [TestMethod]
        public void Backspace_ImaginaryPartSingleDigit_SetsToZero()
        {
            var editor = new ComplexNumberEditor("123,+i*4,");
            var result = editor.Backspace();
            Assert.AreEqual("123,+i*0,", result);
        }

        [TestMethod]
        public void Backspace_RealPartSingleDigit_SetsToZero()
        {
            var editor = new ComplexNumberEditor("1,+i*456,");
            var result = editor.Backspace();
            Assert.AreEqual("0,+i*456,", result);
        }

        [TestMethod]
        public void Backspace_ImaginaryPartWithNegativeSign_SetsToZero()
        {
            var editor = new ComplexNumberEditor("123,+i*-4,");
            var result = editor.Backspace();
            Assert.AreEqual("123,+i*0,", result);
        }

        [TestMethod]
        public void Clear_SetsToZeroRepresentation()
        {
            var editor = new ComplexNumberEditor("123,+i*456,");
            var result = editor.Clear();
            Assert.AreEqual("0,+i*0,", result);
        }

        [TestMethod]
        public void Edit_Command0_TogglesSign()
        {
            var editor = new ComplexNumberEditor("123,+i*456,");
            var result = editor.Edit(0);
            Assert.AreEqual("123,+i*-456,", result);
        }

        [TestMethod]
        public void Edit_Command1_AddsZero()
        {
            var editor = new ComplexNumberEditor("123,+i*45,");
            var result = editor.Edit(1);
            Assert.AreEqual("123,+i*450,", result);
        }

        [TestMethod]
        public void Edit_Command2_Backspace()
        {
            var editor = new ComplexNumberEditor("123,+i*456,");
            var result = editor.Edit(2);
            Assert.AreEqual("123,+i*45,", result);
        }

        [TestMethod]
        public void Edit_Command3_Clear()
        {
            var editor = new ComplexNumberEditor("123,+i*456,");
            var result = editor.Edit(3);
            Assert.AreEqual("0,+i*0,", result);
        }

        [TestMethod]
        public void Edit_Command10to19_AddsDigit()
        {
            var editor = new ComplexNumberEditor("12,+i*45,");
            var result = editor.Edit(13);
            Assert.AreEqual("123,+i*45,", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Edit_UnknownCommand_ThrowsException()
        {
            var editor = new ComplexNumberEditor();
            editor.Edit(99);
        }

        [TestMethod]
        public void ToString_ReturnsNumberString()
        {
            var editor = new ComplexNumberEditor("123,+i*456,");
            Assert.AreEqual("123,+i*456,", editor.ToString());
        }

        [TestMethod]
        public void ComplexScenario_MultipleOperations()
        {
            var editor = new ComplexNumberEditor();

            editor.AddDigit(1);
            editor.AddDigit(2);
            editor.AddDigit(3);
            Assert.AreEqual("123,+i*0,", editor.NumberString);

            editor.ToggleSign();
            Assert.AreEqual("-123,+i*0,", editor.NumberString);

            editor.AddDigit(4);
            editor.AddDigit(5);
            editor.AddDigit(6);
            Assert.AreEqual("-123,+i*456,", editor.NumberString);

            editor.Backspace();
            Assert.AreEqual("-123,+i*45,", editor.NumberString);

            editor.Clear();
            Assert.AreEqual("0,+i*0,", editor.NumberString);
        }
    }
}