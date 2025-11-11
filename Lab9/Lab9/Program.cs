using System;
using System.Linq;

namespace Lab9
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== АТД Множество - Интерактивный режим ===");

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Работа с целыми числами");
                Console.WriteLine("2. Работа с дробями");
                Console.WriteLine("3. Запуск стандартных тестов");
                Console.WriteLine("4. Выход");

                Console.Write("Ваш выбор: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        InteractiveIntMode();
                        break;
                    case "2":
                        InteractiveFracMode();
                        break;
                    case "3":
                        RunStandardTests();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        static void InteractiveIntMode()
        {
            var set = new tset<int>();

            while (true)
            {
                Console.WriteLine($"\nТекущее множество: {set}");
                Console.WriteLine("1. Добавить элемент");
                Console.WriteLine("2. Удалить элемент");
                Console.WriteLine("3. Проверить наличие элемента");
                Console.WriteLine("4. Очистить множество");
                Console.WriteLine("5. Назад");

                Console.Write("Выбор: ");
                var choice = Console.ReadLine();

                if (choice == "5") break;

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите целое число: ");
                        if (int.TryParse(Console.ReadLine(), out int addNum))
                        {
                            bool added = set.AddItem(addNum);
                            Console.WriteLine(added ? "Элемент добавлен" : "Элемент уже существует");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка ввода!");
                        }
                        break;

                    case "2":
                        Console.Write("Введите число для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int removeNum))
                        {
                            bool removed = set.Remove(removeNum);
                            Console.WriteLine(removed ? "Элемент удален" : "Элемент не найден");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка ввода!");
                        }
                        break;

                    case "3":
                        Console.Write("Введите число для проверки: ");
                        if (int.TryParse(Console.ReadLine(), out int checkNum))
                        {
                            bool exists = set.Contains(checkNum);
                            Console.WriteLine(exists ? "Элемент найден" : "Элемент не найден");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка ввода!");
                        }
                        break;

                    case "4":
                        set.Clear();
                        Console.WriteLine("Множество очищено");
                        break;
                }
            }
        }

        static void InteractiveFracMode()
        {
            var set = new tset<TFrac>();

            while (true)
            {
                Console.WriteLine($"\nТекущее множество дробей: {set}");
                Console.WriteLine("1. Добавить дробь");
                Console.WriteLine("2. Удалить дробь");
                Console.WriteLine("3. Проверить наличие дроби");
                Console.WriteLine("4. Назад");

                Console.Write("Выбор: ");
                var choice = Console.ReadLine();

                if (choice == "4") break;

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите числитель: ");
                        if (int.TryParse(Console.ReadLine(), out int num) &&
                            int.TryParse(Console.ReadLine(), out int den))
                        {
                            try
                            {
                                var frac = new TFrac(num, den);
                                bool added = set.AddItem(frac);
                                Console.WriteLine(added ? "Дробь добавлена" : "Эквивалентная дробь уже существует");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Ошибка: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка ввода!");
                        }
                        break;

                    case "2":
                        Console.Write("Введите числитель и знаменатель дроби для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int rNum) &&
                            int.TryParse(Console.ReadLine(), out int rDen))
                        {
                            try
                            {
                                var frac = new TFrac(rNum, rDen);
                                bool removed = set.Remove(frac);
                                Console.WriteLine(removed ? "Дробь удалена" : "Дробь не найдена");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Ошибка: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка ввода!");
                        }
                        break;

                    case "3":
                        Console.Write("Введите числитель и знаменатель для проверки: ");
                        if (int.TryParse(Console.ReadLine(), out int cNum) &&
                            int.TryParse(Console.ReadLine(), out int cDen))
                        {
                            try
                            {
                                var frac = new TFrac(cNum, cDen);
                                bool exists = set.Contains(frac);
                                Console.WriteLine(exists ? "Дробь найдена" : "Дробь не найдена");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Ошибка: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка ввода!");
                        }
                        break;
                }
            }
        }

        static void RunStandardTests()
        {
            // Здесь можно вызвать ваши оригинальные тесты
            Console.WriteLine("\n=== Стандартные тесты ===");
            TestIntSet();
            TestTFracSet();
        }

        // Ваши оригинальные тестовые методы
        static void TestIntSet() { /* ... */ }
        static void TestTFracSet() { /* ... */ }
    }
}