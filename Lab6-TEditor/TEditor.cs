using System;
using System.Text.RegularExpressions;

namespace ComplexNumberEditor
{
    public class ComplexNumberEditor
    {
        private const string DecimalSeparator = ",";
        private const string ImaginarySeparator = "+i*";
        private const string ZeroRepresentation = "0,+i*0,";

        private string _numberString;

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

                if (imaginaryPart.Length > 0 && !imaginaryPart.EndsWith(","))
                {
                    if (imaginaryPart.StartsWith("-"))
                    {
                        imaginaryPart = imaginaryPart.Substring(1);
                    }
                    else
                    {
                        imaginaryPart = "-" + imaginaryPart;
                    }
                }
                else if (realPart.Length > 0 && !realPart.EndsWith(","))
                {
                    if (realPart.StartsWith("-"))
                    {
                        realPart = realPart.Substring(1);
                    }
                    else
                    {
                        realPart = "-" + realPart;
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

                if (CanAddDigit(imaginaryPart))
                {
                    imaginaryPart += digitChar;
                }
                else if (CanAddDigit(realPart))
                {
                    realPart += digitChar;
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

                if (imaginaryPart.Length > 0 && !IsOnlySeparator(imaginaryPart))
                {
                    imaginaryPart = imaginaryPart.Substring(0, imaginaryPart.Length - 1);
                    if (imaginaryPart.EndsWith(",") || imaginaryPart == "-" || imaginaryPart == "")
                    {
                        imaginaryPart = "0,";
                    }
                }
                else if (realPart.Length > 0 && !IsOnlySeparator(realPart))
                {
                    realPart = realPart.Substring(0, realPart.Length - 1);
                    if (realPart.EndsWith(",") || realPart == "-" || realPart == "")
                    {
                        realPart = "0,";
                    }
                }

                _numberString = realPart + ImaginarySeparator + imaginaryPart;
            }

            return _numberString;
        }

        public string Clear()
        {
            _numberString = ZeroRepresentation;
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
                default:
                    if (command >= 10 && command <= 19)
                        return AddDigit(command - 10);
                    else
                        throw new ArgumentException("Unknown command");
            }
        }

        private bool IsValidComplexNumber(string number)
        {
            string pattern = @"^-?\d*,(\+i\*|-i\*)-?\d*,$";
            return Regex.IsMatch(number, pattern) || number == ZeroRepresentation;
        }

        private bool CanAddDigit(string part)
        {
            return !part.EndsWith(",") && part != "0" && part != "-0";
        }

        private bool IsOnlySeparator(string part)
        {
            return part == "0," || part == "-0,";
        }

        public override string ToString()
        {
            return _numberString;
        }
    }
}
