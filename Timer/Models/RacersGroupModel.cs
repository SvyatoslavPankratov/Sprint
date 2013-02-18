using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    /// <summary>
    /// Модель группы гонщиков.
    /// </summary>
    class RacersGroupModel
    {
        #region Properties

        /// <summary>
        /// Задать или получить класс автомобилей в данной группе.
        /// </summary>
        public CarClassesEnum CarClass { get; private set; }

        /// <summary>
        /// Задать или получить список гонщиков.
        /// </summary>
        public IEnumerable<RacerModel> Racers { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="carClass">Класс автомобилей в создаваемой группе.</param>
        public RacersGroupModel(CarClassesEnum carClass)
        {
            CarClass = carClass;
        }

        #endregion

        #region Methods



        #endregion
    }
}
