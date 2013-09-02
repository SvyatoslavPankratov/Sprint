using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Extensions
{
    public static class EnumerableExtesions
    {
        #region Methods

        /// <summary>
        /// Получить индекс элемента в коллекции.
        /// </summary>
        /// <typeparam name="T">Тип искомого в коллекции элемента.</typeparam>
        /// <param name="source">Исходная коллекция.</param>
        /// <param name="value">Искомэй в коллекции элемент.</param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> source, T value)
        {
            int index = 0;
            var comparer = EqualityComparer<T>.Default;
            foreach (T item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }
            return -1;
        }

        #endregion
    }
}
