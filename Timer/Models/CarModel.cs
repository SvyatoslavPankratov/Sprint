using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    /// <summary>
    /// Модель автомобиля.
    /// </summary>
    public class CarModel
    {
        #region Properties

        /// <summary>
        /// Задать или получить класс автомобиля.
        /// </summary>
        public CarClassesEnum CarClass { get; set; }

        /// <summary>
        /// Задать или получить наименование автомобиля (марка, модель и тд).
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="carName">Наименование автомобиля.</param>
        /// <param name="carClass">Класс автомобиля.</param>
        public CarModel(string carName, CarClassesEnum carClass)
        {
            Name = carName;
            CarClass = carClass;
        }

        #endregion

        #region Methods



        #endregion
    }
}
