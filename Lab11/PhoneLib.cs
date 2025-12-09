namespace Lab11
{
    public class Abonent
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Abonent(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"{Name}: {PhoneNumber}";
        }
    }

    public class AbonentList
    {
        private SortedDictionary<string, List<Abonent>> abonents;

        public AbonentList()
        {
            abonents = new SortedDictionary<string, List<Abonent>>();
        }

        public bool AddAbonent(string name, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            var abonent = new Abonent(name, phoneNumber);

            if (!abonents.ContainsKey(name))
            {
                abonents[name] = new List<Abonent>();
            }

            if (!abonents[name].Any(a => a.PhoneNumber == phoneNumber))
            {
                abonents[name].Add(abonent);
                return true;
            }

            return false;
        }

        public List<Abonent> FindByName(string name)
        {
            if (abonents.ContainsKey(name))
            {
                return abonents[name];
            }
            return new List<Abonent>();
        }

        public bool RemoveAbonent(string name, string phoneNumber)
        {
            if (abonents.ContainsKey(name))
            {
                var abonentToRemove = abonents[name].FirstOrDefault(a => a.PhoneNumber == phoneNumber);
                if (abonentToRemove != null)
                {
                    abonents[name].Remove(abonentToRemove);

                    if (abonents[name].Count == 0)
                    {
                        abonents.Remove(name);
                    }
                    return true;
                }
            }
            return false;
        }

        public bool RemoveAllByName(string name)
        {
            return abonents.Remove(name);
        }

        public void Clear()
        {
            abonents.Clear();
        }

        public List<Abonent> GetAllAbonents()
        {
            var result = new List<Abonent>();
            foreach (var entry in abonents)
            {
                result.AddRange(entry.Value);
            }
            return result;
        }

        public void SaveToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var abonent in GetAllAbonents())
                    {
                        writer.WriteLine($"{abonent.Name}|{abonent.PhoneNumber}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при сохранении файла: {ex.Message}");
            }
        }

        public void LoadFromFile(string filePath)
        {
            try
            {
                Clear();

                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 2)
                        {
                            AddAbonent(parts[0], parts[1]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при загрузке файла: {ex.Message}");
            }
        }

        public int Count()
        {
            return GetAllAbonents().Count;
        }
    }

    public class PhoneBookUI
    {
        private AbonentList abonentList;
        private const string DataFile = "phonebook.txt";

        public PhoneBookUI()
        {
            abonentList = new AbonentList();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                abonentList.LoadFromFile(DataFile);
                Console.WriteLine("Данные загружены успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Внимание: {ex.Message}");
                Console.WriteLine("Будет создана новая телефонная книга.");
            }
        }

        private void SaveData()
        {
            try
            {
                abonentList.SaveToFile(DataFile);
                Console.WriteLine("Данные сохранены успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ТЕЛЕФОННАЯ КНИГА ===");
                Console.WriteLine($"Всего записей: {abonentList.Count()}");
                Console.WriteLine();
                Console.WriteLine("1. Показать все записи");
                Console.WriteLine("2. Добавить запись");
                Console.WriteLine("3. Найти запись по имени");
                Console.WriteLine("4. Удалить запись");
                Console.WriteLine("5. Очистить телефонную книгу");
                Console.WriteLine("6. Сохранить данные");
                Console.WriteLine("0. Выход");
                Console.WriteLine();
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllAbonents();
                        break;
                    case "2":
                        AddAbonent();
                        break;
                    case "3":
                        FindAbonent();
                        break;
                    case "4":
                        RemoveAbonent();
                        break;
                    case "5":
                        ClearPhoneBook();
                        break;
                    case "6":
                        SaveData();
                        WaitForKey();
                        break;
                    case "0":
                        SaveData();
                        Console.WriteLine("До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        WaitForKey();
                        break;
                }
            }
        }

        private void ShowAllAbonents()
        {
            Console.Clear();
            Console.WriteLine("=== ВСЕ ЗАПИСИ ===");

            var allAbonents = abonentList.GetAllAbonents();

            if (allAbonents.Count == 0)
            {
                Console.WriteLine("Телефонная книга пуста.");
            }
            else
            {
                int index = 1;
                foreach (var abonent in allAbonents)
                {
                    Console.WriteLine($"{index}. {abonent}");
                    index++;
                }
            }

            WaitForKey();
        }

        private void AddAbonent()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ ЗАПИСИ ===");

            Console.Write("Введите имя абонента: ");
            string name = Console.ReadLine();

            Console.Write("Введите номер телефона: ");
            string phone = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Ошибка: Имя и номер телефона не могут быть пустыми!");
                WaitForKey();
                return;
            }

            if (abonentList.AddAbonent(name, phone))
            {
                Console.WriteLine("Запись успешно добавлена!");
            }
            else
            {
                Console.WriteLine("Ошибка: Такая запись уже существует или данные некорректны!");
            }

            WaitForKey();
        }
        private void FindAbonent()
        {
            Console.Clear();
            Console.WriteLine("=== ПОИСК ПО ИМЕНИ ===");

            Console.Write("Введите имя для поиска: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: Имя не может быть пустым!");
                WaitForKey();
                return;
            }

            var foundAbonents = abonentList.FindByName(name);

            if (foundAbonents.Count == 0)
            {
                Console.WriteLine($"Записей с именем '{name}' не найдено.");
            }
            else
            {
                Console.WriteLine($"Найдено {foundAbonents.Count} записей:");
                int index = 1;
                foreach (var abonent in foundAbonents)
                {
                    Console.WriteLine($"{index}. {abonent}");
                    index++;
                }
            }

            WaitForKey();
        }

        private void RemoveAbonent()
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ ЗАПИСИ ===");

            Console.WriteLine("1. Удалить конкретную запись");
            Console.WriteLine("2. Удалить все записи с определенным именем");
            Console.Write("Выберите тип удаления: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RemoveSpecificAbonent();
                    break;
                case "2":
                    RemoveAllByName();
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    WaitForKey();
                    break;
            }
        }

        private void RemoveSpecificAbonent()
        {
            Console.Write("Введите имя абонента: ");
            string name = Console.ReadLine();

            Console.Write("Введите номер телефона: ");
            string phone = Console.ReadLine();

            if (abonentList.RemoveAbonent(name, phone))
            {
                Console.WriteLine("Запись успешно удалена!");
            }
            else
            {
                Console.WriteLine("Запись не найдена!");
            }

            WaitForKey();
        }

        private void RemoveAllByName()
        {
            Console.Write("Введите имя абонента: ");
            string name = Console.ReadLine();

            if (abonentList.RemoveAllByName(name))
            {
                Console.WriteLine($"Все записи с именем '{name}' успешно удалены!");
            }
            else
            {
                Console.WriteLine($"Записи с именем '{name}' не найдены!");
            }

            WaitForKey();
        }

        private void ClearPhoneBook()
        {
            Console.Clear();
            Console.WriteLine("=== ОЧИСТКА ТЕЛЕФОННОЙ КНИГИ ===");

            Console.Write("Вы уверены, что хотите очистить всю телефонную книгу? (да/нет): ");
            string confirmation = Console.ReadLine().ToLower();

            if (confirmation == "да" || confirmation == "yes" || confirmation == "y")
            {
                abonentList.Clear();
                Console.WriteLine("Телефонная книга очищена.");
            }
            else
            {
                Console.WriteLine("Очистка отменена.");
            }

            WaitForKey();
        }

        private void WaitForKey()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}
