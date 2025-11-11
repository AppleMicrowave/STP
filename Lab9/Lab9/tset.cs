using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab9
{
    public class tset<T> : IEnumerable<T>
    {
        private readonly HashSet<T> _set;

        public tset()
        {
            _set = new HashSet<T>();
        }

        public tset(IEnumerable<T> collection)
        {
            _set = new HashSet<T>(collection);
        }

        // Реализация IEnumerable<T> для поддержки инициализации коллекций
        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Для удобства использования синтаксиса инициализации коллекции
        public void Add(T item)
        {
            _set.Add(item);
        }

        // Опустошить
        public void Clear() => _set.Clear();

        // Добавить (с возвратом bool)
        public bool AddItem(T item) => _set.Add(item);

        // Удалить
        public bool Remove(T item) => _set.Remove(item);

        // Пусто?
        public bool IsEmpty() => _set.Count == 0;

        // Принадлежит?
        public bool Contains(T item) => _set.Contains(item);

        // Объединить
        public tset<T> Union(tset<T> other)
        {
            var result = new tset<T>(_set);
            foreach (var item in other._set)
                result.Add(item);
            return result;
        }

        // Вычесть
        public tset<T> Subtract(tset<T> other)
        {
            var result = new tset<T>();
            foreach (var item in _set)
            {
                if (!other.Contains(item))
                    result.Add(item);
            }
            return result;
        }

        // Пересечь
        public tset<T> Intersect(tset<T> other)
        {
            var result = new tset<T>();
            foreach (var item in _set)
            {
                if (other.Contains(item))
                    result.Add(item);
            }
            return result;
        }

        // Количество элементов
        public int Count() => _set.Count;

        // Элемент по индексу
        public T ElementAt(int index)
        {
            if (index < 0 || index >= _set.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");

            return _set.ElementAt(index);
        }

        // Для перебора элементов
        public IEnumerable<T> GetItems() => _set;

        // Для отладки
        public override string ToString()
        {
            return $"{{{string.Join(", ", _set)}}}";
        }
    }
}