using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    public class TableWithResults
    {
        #region Свойства

        /// <summary>
        /// Задать или получить таблицу с результатами 
        /// участников в заданном классе.
        /// </summary>
        public DataTable Results { get; set; }

        /// <summary>
        /// Задать или получить класс автомобилей для которого 
        /// предназначена данная таблица с результатами.
        /// </summary>
        public CarClassesEnum CarClass { get; set; }

        /// <summary>
        /// Задать или получить номер заезда у текущего класса автомобилей.
        /// </summary>
        public int RaceNumber { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public TableWithResults()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="results">Таблица с результатами.</param>
        /// <param name="carClass">Класс автомобиля.</param>
        /// <param name="raceNumber">Номер заезда.</param>
        public TableWithResults(DataTable results, CarClassesEnum carClass, int raceNumber)
            :this()
        {
            Results = results;
            CarClass = carClass;
            RaceNumber = raceNumber;
        }

        #endregion
    }
}
