using System;
using System.Collections.Generic;
using System.Linq;

using Sprint.Interfaces;
using Sprint.Models;

namespace Sprint.Presenters
{
    /// <summary>
    /// Презентер редактирования данных участника.
    /// </summary>
    public class EditRacerPresenter
    {
        #region Поля

        private RacerModel _racer;

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить представление редактора данных участника.
        /// </summary>
        private IEditRacer View { get; set; }

        /// <summary>
        /// Задать или получить редактируемого гонщика.
        /// </summary>
        public RacerModel Racer
        {
            get { return _racer; }
            set
            {
                _racer = value;

                View.FirstName = Racer.FirstName;
                View.LastName = Racer.LastName;
                View.MiddleName = Racer.MiddleName;
                View.CarClass = (CarClassesEnum)Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), Racer.Car.CarClass.ToString());
                View.Manufacturer = Racer.Car.Manufacturer;
                View.Model = Racer.Car.Model;
                View.EngineSize = Racer.Car.EngineSize;
                View.EnginePower = Racer.Car.EnginePower;
            }
        }

        /// <summary>
        /// Получить список слассов автомобилей.
        /// </summary>
        private List<string> CarClasses
        {
            get
            {
                return Enum.GetNames(Type.GetType("Sprint.Models.CarClassesEnum")).ToList();
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="view">Представление редактора данных участника.</param>
        /// <param name="racer">Редактируемый участник.</param>
        public EditRacerPresenter(IEditRacer view, RacerModel racer)
        {
            View = view;

            if (View != null)
            {
                View.CarClasses = CarClasses;
            }

            Racer = racer;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обновить данные гонщика.
        /// </summary>
        /// <param name="first_name">Имя участника.</param>
        /// <param name="last_name">Фамилия участника.</param>
        /// <param name="middle_name">Отчество участника.</param>
        /// <param name="car_class">Класс автомобиля.</param>
        /// <param name="car_name">Наименование автомобиля.</param>
        public void UpdateRacer(string first_name, string last_name, string middle_name, string manufacturer, string model, double engine_size, double engine_power, CarClassesEnum car_class)
        {
            Racer.FirstName = first_name;
            Racer.LastName = last_name;
            Racer.MiddleName = middle_name;
            Racer.Car.CarClass = car_class;
            Racer.Car.Manufacturer = manufacturer;
            Racer.Car.Model = model;
            Racer.Car.EngineSize = engine_size;
            Racer.Car.EnginePower = engine_power;
        }

        #endregion
    }
}
