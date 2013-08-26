using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    class CarClassEventArgs : EventArgs
    {
        #region Свойства

        /// <summary>
        /// Задать или получить класс автомобилей.
        /// </summary>
        public CarClassesEnum CarClass { get; private set; }

        /// <summary>
        /// Задать или получить номер заезда (0 или 1).
        /// </summary>
        public int RaceNumber { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструткор.
        /// </summary>
        /// <param name="car_class">Класс автомобилей.</param>
        /// <param name="race_number">Номер заезда (0 или 1).</param>
        public CarClassEventArgs(CarClassesEnum car_class, int race_number)
        {
            CarClass = car_class;
            RaceNumber = race_number;
        }

        #endregion
    }
}
