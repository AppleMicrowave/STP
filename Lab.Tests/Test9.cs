using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab9;
using System.Collections.Generic;

namespace Lab.Tests
{
    [TestClass]
    public class tsetTests
    {
        [TestMethod]
        public void Test1_Создание_пустого_множества()
        {
            var set = new tset<int>();
            Assert.IsTrue(set.IsEmpty());
            Assert.AreEqual(0, set.Count());
        }

        [TestMethod]
        public void Test2_Добавление_уникального_элемента()
        {
            var set = new tset<int>();
            bool added = set.AddItem(10);

            Assert.IsTrue(added);
            Assert.AreEqual(1, set.Count());
            Assert.IsTrue(set.Contains(10));
        }

        [TestMethod]
        public void Test3_Добавление_дубликата()
        {
            var set = new tset<int>();
            set.AddItem(10);
            bool addedDuplicate = set.AddItem(10);

            Assert.IsFalse(addedDuplicate);
            Assert.AreEqual(1, set.Count());
        }

        [TestMethod]
        public void Test4_Удаление_элемента()
        {
            var set = new tset<int>();
            set.AddItem(10);
            set.AddItem(20);

            bool removed = set.Remove(10);

            Assert.IsTrue(removed);
            Assert.AreEqual(1, set.Count());
            Assert.IsFalse(set.Contains(10));
            Assert.IsTrue(set.Contains(20));
        }

        [TestMethod]
        public void Test5_Объединение_множеств()
        {
            var set1 = new tset<int>(new List<int> { 1, 2, 3 });
            var set2 = new tset<int>(new List<int> { 3, 4, 5 });

            var union = set1.Union(set2);

            Assert.AreEqual(5, union.Count());
            Assert.IsTrue(union.Contains(1));
            Assert.IsTrue(union.Contains(2));
            Assert.IsTrue(union.Contains(3));
            Assert.IsTrue(union.Contains(4));
            Assert.IsTrue(union.Contains(5));
        }

        [TestMethod]
        public void Test6_Пересечение_множеств()
        {
            var set1 = new tset<int>(new List<int> { 1, 2, 3, 4 });
            var set2 = new tset<int>(new List<int> { 3, 4, 5, 6 });

            var intersect = set1.Intersect(set2);

            Assert.AreEqual(2, intersect.Count());
            Assert.IsTrue(intersect.Contains(3));
            Assert.IsTrue(intersect.Contains(4));
        }

        [TestMethod]
        public void Test7_Разность_множеств()
        {
            var set1 = new tset<int>(new List<int> { 1, 2, 3, 4 });
            var set2 = new tset<int>(new List<int> { 3, 4, 5 });

            var subtract = set1.Subtract(set2);

            Assert.AreEqual(2, subtract.Count());
            Assert.IsTrue(subtract.Contains(1));
            Assert.IsTrue(subtract.Contains(2));
            Assert.IsFalse(subtract.Contains(3));
        }

        [TestMethod]
        public void Test8_Проверка_на_пустоту()
        {
            var set = new tset<int>();
            Assert.IsTrue(set.IsEmpty());

            set.AddItem(1);
            Assert.IsFalse(set.IsEmpty());

            set.Remove(1);
            Assert.IsTrue(set.IsEmpty());
        }

        [TestMethod]
        public void Test9_Количество_элементов()
        {
            var set = new tset<int>();
            Assert.AreEqual(0, set.Count());

            set.AddItem(1);
            Assert.AreEqual(1, set.Count());

            set.AddItem(2);
            Assert.AreEqual(2, set.Count());

            set.Remove(1);
            Assert.AreEqual(1, set.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Test10_ElementAt_некорректный_индекс()
        {
            var set = new tset<int>(new List<int> { 1, 2, 3 });
            var item = set.ElementAt(5);
        }

        // ===== Тесты для дробей =====

        [TestMethod]
        public void Test11_Дроби_равенство()
        {
            var frac1 = new TFrac(1, 2);
            var frac2 = new TFrac(2, 4);
            var frac3 = new TFrac(3, 4);

            Assert.AreEqual(frac1, frac2);
            Assert.AreNotEqual(frac1, frac3);
        }

        [TestMethod]
        public void Test12_Множество_дробей_без_дубликатов()
        {
            var set = new tset<TFrac>();

            bool added1 = set.AddItem(new TFrac(1, 2));
            bool added2 = set.AddItem(new TFrac(2, 4));

            Assert.IsTrue(added1);
            Assert.IsFalse(added2);
            Assert.AreEqual(1, set.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void Test13_Дробь_с_нулевым_знаменателем()
        {
            var frac = new TFrac(1, 0);
        }

        [TestMethod]
        public void Test14_Операции_с_дробями()
        {
            var set1 = new tset<TFrac>();
            set1.AddItem(new TFrac(1, 2));
            set1.AddItem(new TFrac(3, 4));

            var set2 = new tset<TFrac>();
            set2.AddItem(new TFrac(3, 4));
            set2.AddItem(new TFrac(5, 6));

            var union = set1.Union(set2);
            Assert.AreEqual(3, union.Count());
        }

        // ===== Табличные тесты из задания =====

        [TestMethod]
        public void Табличный_тест1()
        {
            // Пустые множества
            var set1 = new tset<int>(new List<int>());
            var set2 = new tset<int>(new List<int>());

            var result = set1.Union(set2);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Табличный_тест2()
        {
            // {0} ∪ {} = {0}
            var set1 = new tset<int>(new List<int> { 0 });
            var set2 = new tset<int>(new List<int>());

            var result = set1.Union(set2);
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(0));
        }

        [TestMethod]
        public void Табличный_тест3()
        {
            // {1} ∪ {0} = {1, 0}
            var set1 = new tset<int>(new List<int> { 1 });
            var set2 = new tset<int>(new List<int> { 0 });

            var result = set1.Union(set2);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(1));
            Assert.IsTrue(result.Contains(0));
        }

        [TestMethod]
        public void Табличный_тест4()
        {
            // {1, 0} ∪ {1, 0} = {1, 0}
            var set1 = new tset<int>(new List<int> { 1, 0 });
            var set2 = new tset<int>(new List<int> { 1, 0 });

            var result = set1.Union(set2);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(1));
            Assert.IsTrue(result.Contains(0));
        }

        [TestMethod]
        public void Табличный_тест5()
        {
            // {1, 2, 3} ∪ {3, 4, 5} = {1, 2, 3, 4, 5}
            var set1 = new tset<int>(new List<int> { 1, 2, 3 });
            var set2 = new tset<int>(new List<int> { 3, 4, 5 });

            var result = set1.Union(set2);
            Assert.AreEqual(5, result.Count());
            Assert.IsTrue(result.Contains(1));
            Assert.IsTrue(result.Contains(2));
            Assert.IsTrue(result.Contains(3));
            Assert.IsTrue(result.Contains(4));
            Assert.IsTrue(result.Contains(5));
        }
    }
}