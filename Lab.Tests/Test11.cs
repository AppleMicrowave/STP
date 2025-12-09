using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Lab11;

namespace Lab11.Tests
{
    [TestClass]
    public class AbonentTests
    {
        [TestMethod]
        public void Abonent_Constructor_SetsPropertiesCorrectly()
        {
            var abonent = new Abonent("Иван Иванов", "1234567");

            Assert.AreEqual("Иван Иванов", abonent.Name);
            Assert.AreEqual("1234567", abonent.PhoneNumber);
        }

        [TestMethod]
        public void Abonent_ToString_ReturnsCorrectFormat()
        {
            var abonent = new Abonent("Иван Иванов", "1234567");

            var result = abonent.ToString();

            Assert.AreEqual("Иван Иванов: 1234567", result);
        }

        [TestMethod]
        public void Abonent_Properties_CanBeModified()
        {
            var abonent = new Abonent("Иван", "111");

            abonent.Name = "Петр";
            abonent.PhoneNumber = "222";

            Assert.AreEqual("Петр", abonent.Name);
            Assert.AreEqual("222", abonent.PhoneNumber);
        }
    }

    [TestClass]
    public class AbonentListTests
    {
        private AbonentList _abonentList;

        [TestInitialize]
        public void TestInitialize()
        {
            _abonentList = new AbonentList();
        }

        [TestMethod]
        public void AddAbonent_ValidData_ReturnsTrueAndAdds()
        {
            var result = _abonentList.AddAbonent("Иван", "1234567");

            Assert.IsTrue(result);
            Assert.AreEqual(1, _abonentList.Count());
        }

        [TestMethod]
        public void AddAbonent_EmptyName_ReturnsFalse()
        {
            var result = _abonentList.AddAbonent("", "1234567");

            Assert.IsFalse(result);
            Assert.AreEqual(0, _abonentList.Count());
        }

        [TestMethod]
        public void AddAbonent_EmptyPhone_ReturnsFalse()
        {
            var result = _abonentList.AddAbonent("Иван", "");

            Assert.IsFalse(result);
            Assert.AreEqual(0, _abonentList.Count());
        }

        [TestMethod]
        public void AddAbonent_Duplicate_ReturnsFalse()
        {
            _abonentList.AddAbonent("Иван", "1234567");

            var result = _abonentList.AddAbonent("Иван", "1234567");

            Assert.IsFalse(result);
            Assert.AreEqual(1, _abonentList.Count());
        }

        [TestMethod]
        public void AddAbonent_MultiplePhonesForSameName_AddsAll()
        {
            _abonentList.AddAbonent("Иван", "111");
            _abonentList.AddAbonent("Иван", "222");
            _abonentList.AddAbonent("Иван", "333");

            Assert.AreEqual(3, _abonentList.Count());
        }

        [TestMethod]
        public void FindByName_ExistingName_ReturnsList()
        {
            _abonentList.AddAbonent("Иван", "111");
            _abonentList.AddAbonent("Иван", "222");
            _abonentList.AddAbonent("Петр", "333");

            var result = _abonentList.FindByName("Иван");

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(a => a.PhoneNumber == "111"));
            Assert.IsTrue(result.Any(a => a.PhoneNumber == "222"));
        }

        [TestMethod]
        public void FindByName_NonExistingName_ReturnsEmptyList()
        {
            _abonentList.AddAbonent("Иван", "111");

            var result = _abonentList.FindByName("Несуществующий");

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void RemoveAbonent_ExistingAbonent_ReturnsTrueAndRemoves()
        {
            _abonentList.AddAbonent("Иван", "111");
            _abonentList.AddAbonent("Иван", "222");

            var result = _abonentList.RemoveAbonent("Иван", "111");

            Assert.IsTrue(result);
            Assert.AreEqual(1, _abonentList.Count());
            var remaining = _abonentList.FindByName("Иван");
            Assert.AreEqual(1, remaining.Count);
            Assert.AreEqual("222", remaining[0].PhoneNumber);
        }

        [TestMethod]
        public void RemoveAbonent_NonExistingAbonent_ReturnsFalse()
        {
            _abonentList.AddAbonent("Иван", "111");

            var result = _abonentList.RemoveAbonent("Иван", "999");

            Assert.IsFalse(result);
            Assert.AreEqual(1, _abonentList.Count());
        }

        [TestMethod]
        public void RemoveAbonent_LastAbonentForName_RemovesNameFromDictionary()
        {
            _abonentList.AddAbonent("Иван", "111");

            var result = _abonentList.RemoveAbonent("Иван", "111");

            Assert.IsTrue(result);
            Assert.AreEqual(0, _abonentList.Count());
            var found = _abonentList.FindByName("Иван");
            Assert.AreEqual(0, found.Count);
        }

        [TestMethod]
        public void RemoveAllByName_ExistingName_ReturnsTrueAndRemoves()
        {
            _abonentList.AddAbonent("Иван", "111");
            _abonentList.AddAbonent("Иван", "222");
            _abonentList.AddAbonent("Петр", "333");

            var result = _abonentList.RemoveAllByName("Иван");

            Assert.IsTrue(result);
            Assert.AreEqual(1, _abonentList.Count());
            var found = _abonentList.FindByName("Иван");
            Assert.AreEqual(0, found.Count);
        }

        [TestMethod]
        public void RemoveAllByName_NonExistingName_ReturnsFalse()
        {
            _abonentList.AddAbonent("Иван", "111");

            var result = _abonentList.RemoveAllByName("Несуществующий");

            Assert.IsFalse(result);
            Assert.AreEqual(1, _abonentList.Count());
        }

        [TestMethod]
        public void Clear_RemovesAllAbonents()
        {
            _abonentList.AddAbonent("Иван", "111");
            _abonentList.AddAbonent("Петр", "222");
            _abonentList.AddAbonent("Сергей", "333");

            _abonentList.Clear();

            Assert.AreEqual(0, _abonentList.Count());
        }

        [TestMethod]
        public void GetAllAbonents_ReturnsAllInSortedOrder()
        {
            _abonentList.AddAbonent("Сергей", "333");
            _abonentList.AddAbonent("Анна", "111");
            _abonentList.AddAbonent("Борис", "222");

            var result = _abonentList.GetAllAbonents();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Анна", result[0].Name);
            Assert.AreEqual("Борис", result[1].Name);
            Assert.AreEqual("Сергей", result[2].Name);
        }

        [TestMethod]
        public void SaveToFileAndLoadFromFile_WorksCorrectly()
        {
            var testFile = "test_save.txt";
            _abonentList.AddAbonent("Иван", "111");
            _abonentList.AddAbonent("Иван", "222");
            _abonentList.AddAbonent("Петр", "333");

            _abonentList.SaveToFile(testFile);

            var newAbonentList = new AbonentList();
            newAbonentList.LoadFromFile(testFile);

            Assert.AreEqual(3, newAbonentList.Count());
            var ivanAbonents = newAbonentList.FindByName("Иван");
            Assert.AreEqual(2, ivanAbonents.Count);
            var petrAbonents = newAbonentList.FindByName("Петр");
            Assert.AreEqual(1, petrAbonents.Count);

            File.Delete(testFile);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SaveToFile_InvalidPath_ThrowsException()
        {
            var invalidPath = "C:/несуществующая_папка/test.txt";

            _abonentList.SaveToFile(invalidPath);
        }

        [TestMethod]
        public void LoadFromFile_NonExistentFile_CreatesEmptyList()
        {
            var nonExistentFile = "non_existent_file.txt";

            _abonentList.LoadFromFile(nonExistentFile);

            Assert.AreEqual(0, _abonentList.Count());
        }

        [TestMethod]
        public void LoadFromFile_CorruptedFile_HandlesGracefully()
        {
            var corruptFile = "corrupt.txt";
            File.WriteAllLines(corruptFile, new[] { "Иван|111", "НекорректнаяЗапись", "Петр|222" });

            _abonentList.LoadFromFile(corruptFile);

            Assert.AreEqual(2, _abonentList.Count());

            File.Delete(corruptFile);
        }

        [TestMethod]
        public void Count_ReturnsCorrectNumber()
        {
            _abonentList.AddAbonent("Иван", "111");
            _abonentList.AddAbonent("Иван", "222");
            _abonentList.AddAbonent("Петр", "333");

            var count = _abonentList.Count();

            Assert.AreEqual(3, count);
        }
    }
}
