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
        /// <param name="firstName">Имя.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="MiddleName">Отчество.</param>
        /// <param name="carClass">Наименование автомобиля.</param>
        /// <param name="carClass">Класс автомобиля.</param>
        public RacerModel(string firstName, string lastName, string middleName, string carName, CarClassesEnum carClass)
        {
            FirstName   = firstName;
            LastName    = lastName;
            MiddleName  = middleName;

            Car = new CarModel(carName, carClass);
            
            Id = Guid.NewGuid();
        }

        #endregion

        #region Methods



        #endregion
    }
}
