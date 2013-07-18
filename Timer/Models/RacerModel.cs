using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Models;

namespace Sprint.Models
{
    /// <summary>
    /// Модель гонщика.
    /// </summary>
    public class RacerModel : PeopleModel
    {
        #region Properties

        /// <summary>
        /// Задать или получить идентификатор гонщика.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Задать или получить номер участника.
        /// </summary>
        public int RacerNumber { get; set; }

        /// <summary>
        /// Задать или получить автомобиль гонщика.
        /// </summary>
        public CarModel Car { get; private set; }

        /// <summary>
        /// Задать или получить результаты гонщика.
        /// </summary>
        public ResultsModel Results { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Констрктор.
        /// </summary>
        /// <param name="first_name">Имя.</param>
        /// <param name="last_name">Фамилия.</param>
        /// <param name="middle_name">Отчество.</param>
        /// <param name="manufacturer">Марка автомобиля.</param>
        /// <param name="model">Модель автомобиля.</param>
        /// <param name="engine_size">Объем двигателя.</param>
        /// <param name="engine_power">Мощность двигателя.</param>
        /// <param name="carClass">Класс автомобиля.</param>
        public RacerModel(string first_name, string last_name, string middle_name, 
                          string manufacturer, string model, double engine_size, double engine_power, 
                          CarClassesEnum carClass)
        {
            FirstName   = first_name;
            LastName    = last_name;
            MiddleName  = middle_name;

            Car = new CarModel(manufacturer, model, engine_size, engine_power, carClass);
            
            Id = Guid.NewGuid();
        }

        #endregion

        #region Methods



        #endregion
    }
}
