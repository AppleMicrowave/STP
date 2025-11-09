using System.Text.RegularExpressions;

namespace Lab6_TEditor
{
    public class TEditor
    {
        private const string DECIMAL_SEPARATOR = ",";
        private const string IMAGINARY_SEPARATOR = "+i*";
        private const string ZERO_REPRESENTATION = "0,+i*0,";

        private string строка;

        public TEditor()
        {
            очистить();
        }
        public TEditor(string initialString)
        {
            писатьСтрокаВформатеСтроки(initialString);
        }

        public string читатьСтрокаВформатеСтроки
        {
            get { return строка; }
        }

        public void писатьСтрокаВформатеСтроки(string value)
        {
            if (IsValidComplexNumber(value))
            {
                строка = value;
            }
            else
            {
                throw new ArgumentException("Некорректный формат комплексного числа");
            }
        }

        public bool комплексноеЧислоЕстьНоль
        {
            get
            {
                return строка == ZERO_REPRESENTATION;
            }
        }

        public string добавитьЗнак()
        {
            // Разделяем на действительную и мнимую части
            string[] parts = строка.Split(new[] { IMAGINARY_SEPARATOR }, StringSplitOptions.None);

            if (parts.Length == 2)
            {
                string realPart = parts[0];
                string imaginaryPart = parts[1];

                // Определяем, какую часть менять (последнюю изменяемую)
                if (imaginaryPart.Length > 0 && !imaginaryPart.EndsWith(","))
                {
                    // Меняем знак мнимой части
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
                    // Меняем знак действительной части
                    if (realPart.StartsWith("-"))
                    {
                        realPart = realPart.Substring(1);
                    }
                    else
                    {
                        realPart = "-" + realPart;
                    }
                }

                строка = realPart + IMAGINARY_SEPARATOR + imaginaryPart;
            }

            return строка;
        }

        public string добавитьЦифру(int digit)
        {
            if (digit < 0 || digit > 9)
                throw new ArgumentException("Цифра должна быть от 0 до 9");

            char digitChar = digit.ToString()[0];
            string[] parts = строка.Split(new[] { IMAGINARY_SEPARATOR }, StringSplitOptions.None);

            if (parts.Length == 2)
            {
                string realPart = parts[0];
                string imaginaryPart = parts[1];

                // Определяем, в какую часть добавлять цифру
                if (CanAddDigit(imaginaryPart))
                {
                    imaginaryPart += digitChar;
                }
                else if (CanAddDigit(realPart))
                {
                    realPart += digitChar;
                }

                строка = realPart + IMAGINARY_SEPARATOR + imaginaryPart;
            }

            return строка;
        }

        public string добавитьНоль()
        {
            return добавитьЦифру(0);
        }

        public string забойСимвола()
        {
            string[] parts = строка.Split(new[] { IMAGINARY_SEPARATOR }, StringSplitOptions.None);

            if (parts.Length == 2)
            {
                string realPart = parts[0];
                string imaginaryPart = parts[1];

                // Удаляем из мнимой части, если есть что удалять
                if (imaginaryPart.Length > 0 && !IsOnlySeparator(imaginaryPart))
                {
                    imaginaryPart = imaginaryPart.Substring(0, imaginaryPart.Length - 1);
                    // Если удалили все цифры, оставляем разделитель
                    if (imaginaryPart.EndsWith(",") || imaginaryPart == "-" || imaginaryPart == "")
                    {
                        imaginaryPart = "0,";
                    }
                }
                else if (realPart.Length > 0 && !IsOnlySeparator(realPart))
                {
                    // Удаляем из действительной части
                    realPart = realPart.Substring(0, realPart.Length - 1);
                    // Если удалили все цифры, оставляем разделитель
                    if (realPart.EndsWith(",") || realPart == "-" || realPart == "")
                    {
                        realPart = "0,";
                    }
                }

                строка = realPart + IMAGINARY_SEPARATOR + imaginaryPart;
            }

            return строка;
        }

        public string очистить()
        {
            строка = ZERO_REPRESENTATION;
            return строка;
        }

        public string редактировать(int command)
        {
            switch (command)
            {
                case 0: return добавитьЗнак();
                case 1: return добавитьНоль();
                case 2: return забойСимвола();
                case 3: return очистить();
                default:
                    if (command >= 10 && command <= 19)
                        return добавитьЦифру(command - 10);
                    else
                        throw new ArgumentException("Неизвестная команда");
            }
        }

        // Вспомогательные методы

        private bool IsValidComplexNumber(string number)
        {
            // Простая проверка формата: [знак]число,[знак]число,
            string pattern = @"^-?\d*,(\+i\*|-i\*)-?\d*,$";
            return Regex.IsMatch(number, pattern) || number == ZERO_REPRESENTATION;
        }

        private bool CanAddDigit(string part)
        {
            // Можно добавить цифру, если часть не заканчивается запятой и не является полным числом
            return !part.EndsWith(",") && part != "0" && part != "-0";
        }

        private bool IsOnlySeparator(string part)
        {
            return part == "0," || part == "-0,";
        }

        public override string ToString()
        {
            return строка;
        }
    }
}
