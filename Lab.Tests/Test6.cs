using System;
using System.Text.RegularExpressions;

namespace Lab6_TEditor
{
    public class ComplexNumberEditor
    {
        private const string DecimalSeparator = ",";
        private const string ImaginarySeparator = "+i*";
        private const string ZeroRepresentation = "0,+i*0,";

        private string _numberString;
        private bool _isEditingRealPart = true;

        public ComplexNumberEditor()
        {
            Clear();
        }

        public ComplexNumberEditor(string initialString)
        {
            SetString(initialString);
        }

        public string NumberString
        {
            get { return _numberString; }
        }

        public void SetString(string value)
        {
            if (IsValidComplexNumber(value))
            {
                _numberString = value;
            }
            else
            {
                throw new ArgumentException("Invalid complex number format");
            }
        }

        public bool IsZero
        {
            get
            {
                return _numberString == ZeroRepresentation;
            }
        }

        public string ToggleSign()
        {
            string[] parts = _numberString.Split(new[] { ImaginarySeparator }, StringSplitOptions.None);

            if (parts.Length == 2)
            {
                string realPart = parts[0];
                string imaginaryPart = parts[1];

            
                if (_isEditingRealPart)
                {
                    if (realPart.StartsWith("-"))
                    {
                        realPart = realPart.Substring(1);
                    }
                    else if (realPart != "0,")
                    {
                        realPart = "-" + realPart;
                    }
                }
                else 
                {
                    if (imaginaryPart.StartsWith("-"))
                    {
                        imaginaryPart = imaginaryPart.Substring(1);
                    }
                    else if (imaginaryPart != "0,")
                    {
                        imaginaryPart = "-" + imaginaryPart;
                    }
                }

                _numberString = realPart + ImaginarySeparator + imaginaryPart;
            }

            return _numberString;
        }

        public string AddDigit(int digit)
        {
            if (digit < 0 || digit > 9)
                throw new ArgumentException("Digit must be from 0 to 9");

            char digitChar = digit.ToString()[0];
            string[] parts = _numberString.Split(new[] { ImaginarySeparator }, StringSplitOptions.None);

            if (parts.Length == 2)
            {
                string realPart = parts[0];
                string imaginaryPart = parts[1];

                if (_isEditingRealPart)
                {
                    if (CanAddDigit(realPart))
                    {
                        realPart = realPart.Insert(realPart.Length - 1, digitChar.ToString());
                    }
                    else if (realPart == "0,")
                    {
                        realPart = digitChar + ",";
                    }
                }
                else
                {
                    if (CanAddDigit(imaginaryPart))
                    {
                        imaginaryPart = imaginaryPart.Insert(imaginaryPart.Length - 1, digitChar.ToString());
                    }
                    else if (imaginaryPart == "0,")
                    {
                        imaginaryPart = digitChar + ",";
                    }
                }

                _numberString = realPart + ImaginarySeparator + imaginaryPart;
            }

            return _numberString;
        }

        public string AddZero()
        {
            return AddDigit(0);
        }

        public string Backspace()
        {
            string[] parts = _numberString.Split(new[] { ImaginarySeparator }, StringSplitOptions.None);

            if (parts.Length == 2)
            {
                string realPart = parts[0];
                string imaginaryPart = parts[1];

                if (_isEditingRealPart && realPart.Length > 2)
                {

                    realPart = realPart.Remove(realPart.Length - 2, 1);
                    if (realPart == "-," || realPart == ",")
                    {
                        realPart = "0,";
                    }
                }
                else if (!_isEditingRealPart && imaginaryPart.Length > 2)
                {
          
                    imaginaryPart = imaginaryPart.Remove(imaginaryPart.Length - 2, 1);
                    if (imaginaryPart == "-," || imaginaryPart == ",")
                    {
                        imaginaryPart = "0,";
                    }
                }
                else
                {
  
                    if (_isEditingRealPart && realPart.Length == 2 && realPart != "0,")
                    {
                        realPart = "0,";
                    }
                    else if (!_isEditingRealPart && imaginaryPart.Length == 2 && imaginaryPart != "0,")
                    {
                        imaginaryPart = "0,";
                    }
                }

                _numberString = realPart + ImaginarySeparator + imaginaryPart;
            }

            return _numberString;
        }

        public string Clear()
        {
            _numberString = ZeroRepresentation;
            _isEditingRealPart = true;
            return _numberString;
        }

        public string SwitchPart()
        {
            _isEditingRealPart = !_isEditingRealPart;
            return _numberString;
        }

        public string Edit(int command)
        {
            switch (command)
            {
                case 0: return ToggleSign();
                case 1: return AddZero();
                case 2: return Backspace();
                case 3: return Clear();
                case 4: return SwitchPart(); 
                default:
                    if (command >= 10 && command <= 19)
                        return AddDigit(command - 10);
                    else
                        throw new ArgumentException("Unknown command");
            }
        }

        private bool IsValidComplexNumber(string number)
        {
            string pattern = @"^-?\d*,(\+i\*)-?\d*,$";
            return Regex.IsMatch(number, pattern) || number == ZeroRepresentation;
        }

        private bool CanAddDigit(string part)
        {
     
            return part != "0," && part != "-0," && !part.EndsWith(DecimalSeparator + DecimalSeparator);
        }

        public override string ToString()
        {
            return _numberString;
        }
    }
}