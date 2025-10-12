using System;
using Lab4_TPNumber;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Правильное использование - одинаковые основания
            var num1 = new TPNumber(10.5, 10, 2);    // основание 10
            var num2 = new TPNumber(5.2, 10, 2);     // основание 10 - такое же!

            var sum = num1.Add(num2);
            Console.WriteLine($"Сумма: {sum.GetNumberString()}");

            // Или через строковый конструктор
            var num3 = new TPNumber("A", "16", "3");  // основание 16
            var num4 = new TPNumber("5", "16", "3");  // основание 16 - такое же!

            var product = num3.Multiply(num4);
            Console.WriteLine($"Произведение: {product.GetNumberString()}");

            // Демонстрация разных систем счисления
            DemoDifferentBases();
        }
        catch (TPNumberException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void DemoDifferentBases()
    {
        Console.WriteLine("\nДемонстрация разных систем счисления:");

        // Десятичная система
        var decimalNum = new TPNumber(15.75, 10, 2);
        Console.WriteLine($"Десятичное: {decimalNum.GetNumberString()}");

        // Двоичная система
        var binaryNum = new TPNumber(15.75, 2, 4);
        Console.WriteLine($"Двоичное: {binaryNum.GetNumberString()}");

        // Восьмеричная система
        var octalNum = new TPNumber(15.75, 8, 3);
        Console.WriteLine($"Восьмеричное: {octalNum.GetNumberString()}");

        // Шестнадцатеричная система
        var hexNum = new TPNumber(15.75, 16, 2);
        Console.WriteLine($"Шестнадцатеричное: {hexNum.GetNumberString()}");

        // Арифметика в пределах одной системы
        Console.WriteLine("\nАрифметика в десятичной системе:");
        var a = new TPNumber(10.0, 10, 2);
        var b = new TPNumber(3.0, 10, 2);

        Console.WriteLine($"a + b = {a.Add(b).GetNumberString()}");
        Console.WriteLine($"a - b = {a.Subtract(b).GetNumberString()}");
        Console.WriteLine($"a * b = {a.Multiply(b).GetNumberString()}");
        Console.WriteLine($"a / b = {a.Divide(b).GetNumberString()}");

        // Обратное число и квадрат
        Console.WriteLine($"1/a = {a.Reciprocal().GetNumberString()}");
        Console.WriteLine($"a² = {a.Square().GetNumberString()}");
    }
}
