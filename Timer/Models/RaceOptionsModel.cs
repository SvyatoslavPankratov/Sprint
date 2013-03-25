using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    /// <summary>
    /// Модель опций гонок для заданного класса автомобилей.
    /// </summary>
    public class RaceOptionsModel
    {
        #region Свойства

        /// <summary>
        /// Задать или получить количество проводимых туров в текущем классе автомобилей.
        /// </summary>
        public int RaceCount { get; set; }

        /// <summary>
        /// Задать или получить количество отбираемых лидеров для проведения финального тура.
        /// </summary>
        public int LidersCount { get; set; }

        /// <summary>
        /// Задать или получить класс автомобилей для которых действуют данные опции.
        /// </summary>
        public CarClassesEnum CarClass { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="carClass">Класс автомобилей, для которых предназначены данные опции.</param>
        public RaceOptionsModel(CarClassesEnum carClass)
        {
            CarClass = carClass;
        }

        #endregion

        #region Методы



        #endregion							
    }
}
