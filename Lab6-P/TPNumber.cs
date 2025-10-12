using System;
using System.Globalization;

namespace Lab4_TPNumber
{
    public class TPNumberException : Exception
    {
        public TPNumberException(string message) : base(message) { }
    }

    public class TPNumber
    {
        private readonly double _number;
        private readonly int _base;
        private readonly int _precision;

        public TPNumber(double a, int b, int c)
        {
            if (b < 2 || b > 16)
                throw new TPNumberException($"Основание системы счисления должно быть в диапазоне [2..16], получено: {b}");
            if (c < 0)
                throw new TPNumberException($"Точность представления должна быть неотрицательной, получено: {c}");

            _number = a;
            _base = b;
            _precision = c;
        }

        public TPNumber(string a, string b, string c)
        {
            if (!int.TryParse(b, out int baseValue) || baseValue < 2 || baseValue > 16)
                throw new TPNumberException($"Основание системы счисления должно быть в диапазоне [2..16], получено: {b}");

            if (!int.TryParse(c, out int precisionValue) || precisionValue < 0)
                throw new TPNumberException($"Точность представления должна быть неотрицательной, получено: {c}");

            // Преобразование строки в число с учетом системы счисления
            double numberValue = ConvertFromBaseString(a, baseValue);

            _number = numberValue;
            _base = baseValue;
            _precision = precisionValue;
        }
        public TPNumber Copy()
        {
            return new TPNumber(_number, _base, _precision);
        }

        public TPNumber Add(TPNumber d)
        {
            ValidateSameBaseAndPrecision(d);
            return new TPNumber(_number + d._number, _base, _precision);
        }

        public TPNumber Multiply(TPNumber d)
        {
            ValidateSameBaseAndPrecision(d);
            return new TPNumber(_number * d._number, _base, _precision);
        }

        public TPNumber Subtract(TPNumber d)
        {
            ValidateSameBaseAndPrecision(d);
            return new TPNumber(_number - d._number, _base, _precision);
        }

        public TPNumber Divide(TPNumber d)
        {
            ValidateSameBaseAndPrecision(d);
            if (d._number == 0)
                throw new TPNumberException("Деление на ноль невозможно");

            return new TPNumber(_number / d._number, _base, _precision);
        }

        // Обратное число
        public TPNumber Reciprocal()
        {
            if (_number == 0)
                throw new TPNumberException("Невозможно вычислить обратное значение для нуля");

            return new TPNumber(1.0 / _number, _base, _precision);
        }

        // Квадрат числа
        public TPNumber Square()
        {
            return new TPNumber(_number * _number, _base, _precision);
        }

        public double GetNumber()
        {
            return _number;
        }

        public string GetNumberString()
        {
            return ConvertToBaseString(_number, _base, _precision);
        }

        public int GetBase()
        {
            return _base;
        }

        public string GetBaseString()
        {
            return _base.ToString();
        }

        public int GetPrecision()
        {
            return _precision;
        }

        public string GetPrecisionString()
        {
            return _precision.ToString();
        }

        // Вспомогательные методы
        private void ValidateSameBaseAndPrecision(TPNumber other)
        {
            if (_base != other._base)
                throw new TPNumberException("Основания систем счисления не совпадают");
            if (_precision != other._precision)
                throw new TPNumberException("Точности представления не совпадают");
        }

        private static string ConvertToBaseString(double number, int numberBase, int precision)
        {
            if (numberBase == 10)
                return number.ToString($"F{precision}", CultureInfo.InvariantCulture);

            bool isNegative = number < 0;
            double absNumber = Math.Abs(number);

            // Целая часть
            long integerPart = (long)absNumber;
            string integerStr = ConvertIntegerPart(integerPart, numberBase);

            // Дробная часть
            double fractionalPart = absNumber - integerPart;
            string fractionalStr = ConvertFractionalPart(fractionalPart, numberBase, precision);

            string result = integerStr;
            if (fractionalStr.Length > 0)
                result += "." + fractionalStr;

            return isNegative ? "-" + result : result;
        }

        private static string ConvertIntegerPart(long number, int numberBase)
        {
            if (number == 0) return "0";

            string digits = "0123456789ABCDEF";
            string result = "";

            while (number > 0)
            {
                long remainder = number % numberBase;
                result = digits[(int)remainder] + result;
                number /= numberBase;
            }

            return result;
        }

        private static string ConvertFractionalPart(double fractional, int numberBase, int precision)
        {
            if (precision == 0) return "";

            string digits = "0123456789ABCDEF";
            string result = "";

            double current = fractional;
            for (int i = 0; i < precision; i++)
            {
                current *= numberBase;
                int digit = (int)current;
                result += digits[digit];
                current -= digit;
            }

            return result;
        }

        private static double ConvertFromBaseString(string numberStr, int numberBase)
        {
            if (string.IsNullOrEmpty(numberStr))
                return 0;

            bool isNegative = numberStr.StartsWith("-");
            if (isNegative)
                numberStr = numberStr.Substring(1);

            string[] parts = numberStr.Split('.');
            string integerPart = parts[0];
            string fractionalPart = parts.Length > 1 ? parts[1] : "";

            double integerValue = ConvertIntegerPart(integerPart, numberBase);
            double fractionalValue = ConvertFractionalPart(fractionalPart, numberBase);

            double result = integerValue + fractionalValue;
            return isNegative ? -result : result;
        }

        private static double ConvertIntegerPart(string integerStr, int numberBase)
        {
            if (string.IsNullOrEmpty(integerStr)) return 0;

            double result = 0;
            string digits = "0123456789ABCDEF";

            for (int i = 0; i < integerStr.Length; i++)
            {
                char c = char.ToUpper(integerStr[i]);
                int digitValue = digits.IndexOf(c);
                if (digitValue == -1 || digitValue >= numberBase)
                    throw new TPNumberException($"Недопустимый символ '{c}' для системы счисления с основанием {numberBase}");

                result = result * numberBase + digitValue;
            }

            return result;
        }

        private static double ConvertFractionalPart(string fractionalStr, int numberBase)
        {
            if (string.IsNullOrEmpty(fractionalStr)) return 0;

            double result = 0;
            string digits = "0123456789ABCDEF";

            for (int i = fractionalStr.Length - 1; i >= 0; i--)
            {
                char c = char.ToUpper(fractionalStr[i]);
                int digitValue = digits.IndexOf(c);
                if (digitValue == -1 || digitValue >= numberBase)
                    throw new TPNumberException($"Недопустимый символ '{c}' для системы счисления с основанием {numberBase}");

                result = (result + digitValue) / numberBase;
            }

            return result;
        }

        // Переопределение методов для удобства тестирования
        public override bool Equals(object obj)
        {
            if (obj is TPNumber other)
            {
                return Math.Abs(_number - other._number) < 1e-10 &&
                       _base == other._base &&
                       _precision == other._precision;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_number, _base, _precision);
        }

        public override string ToString()
        {
            return $"{GetNumberString()}(base {_base}, precision {_precision})";
        }
    }
}
