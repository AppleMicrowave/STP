using System;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа 1");
            Console.WriteLine("=====================");

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nВыберите операцию:");
                Console.WriteLine("1. Умножение элементов с четными индексами");
                Console.WriteLine("2. Циклический сдвиг массива");
                Console.WriteLine("3. Поиск максимального четного элемента с четным индексом");
                Console.WriteLine("4. Выход");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProcessMultiplyEvenIndexed();
                        break;
                    case "2":
                        ProcessCyclicShift();
                        break;
                    case "3":
                        ProcessFindMaxEvenElement();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }

            Console.WriteLine("Программа завершена.");
        }

        static void ProcessMultiplyEvenIndexed()
        {
            Console.WriteLine("\n=== Умножение элементов с четными индексами ===");

            double[] array = ReadDoubleArray();
            if (array == null) return;

            double result = Functions.MultiplyEvenIndexedElements(array);

            Console.WriteLine("Исходный массив: " + ArrayToString(array));
            Console.WriteLine($"Произведение элементов с четными индексами: {result}");
        }

        static void ProcessCyclicShift()
        {
            Console.WriteLine("\n=== Циклический сдвиг массива ===");

            double[] array = ReadDoubleArray();
            if (array == null) return;

            Console.Write("Введите величину сдвига: ");
            if (!int.TryParse(Console.ReadLine(), out int shift))
            {
                Console.WriteLine("Ошибка: введите целое число для сдвига.");
                return;
            }

            Console.Write("Введите направление (left/right): ");
            string direction = Console.ReadLine().ToLower();

            if (direction != "left" && direction != "right")
            {
                Console.WriteLine("Ошибка: направление должно быть 'left' или 'right'.");
                return;
            }

            Console.WriteLine("Исходный массив: " + ArrayToString(array));

            Functions.CyclicShift(array, shift, direction);

            Console.WriteLine($"Массив после сдвига на {shift} в направлении {direction}: " + ArrayToString(array));
        }

        static void ProcessFindMaxEvenElement()
        {
            Console.WriteLine("\n=== Поиск максимального четного элемента с четным индексом ===");

            int[] array = ReadIntArray();
            if (array == null) return;

            int result = Functions.FindMaxEvenElementWithEvenIndex(array);

            Console.WriteLine("Исходный массив: " + ArrayToString(array));

            if (result == int.MinValue)
            {
                Console.WriteLine("Четные элементы с четными индексами не найдены.");
            }
            else
            {
                Console.WriteLine($"Максимальный четный элемент с четным индексом: {result}");
            }
        }

        static double[] ReadDoubleArray()
        {
            Console.Write("Введите элементы массива (через пробел): ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: ввод не может быть пустым.");
                return null;
            }

            string[] elements = input.Split(' ');
            double[] array = new double[elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {
                if (!double.TryParse(elements[i], out array[i]))
                {
                    Console.WriteLine($"Ошибка: '{elements[i]}' не является числом.");
                    return null;
                }
            }

            return array;
        }

        static int[] ReadIntArray()
        {
            Console.Write("Введите элементы массива (целые числа через пробел): ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: ввод не может быть пустым.");
                return null;
            }

            string[] elements = input.Split(' ');
            int[] array = new int[elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {
                if (!int.TryParse(elements[i], out array[i]))
                {
                    Console.WriteLine($"Ошибка: '{elements[i]}' не является целым числом.");
                    return null;
                }
            }

            return array;
        }

        static string ArrayToString<T>(T[] array)
        {
            return "[" + string.Join(", ", array) + "]";
        }
    }
}