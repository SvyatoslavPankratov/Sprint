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
        /// Задать или получить марку автомобиля.
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Задать или получить модель автомобиля.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Задать или получить объем двигателя.
        /// </summary>
        public double EngineSize { get; set; }

        /// <summary>
        /// Задать или получить мощность двигателя.
        /// </summary>
        public double EnginePower { get; set; }

        /// <summary>
        /// Получить полное наименование автомобиля с объемом двигателя и мощностью.
        /// </summary>
        public string FullName
        {
            get { return ToString(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="manufacturer">Производитель автомобиля.</param>
        /// <param name="model">Модель автомобиля.</param>
        /// <param name="engine_size">Объем двигателя.</param>
        /// <param name="engine_power">Мощность двигателя.</param>
        /// <param name="carClass">Класс автомобиля.</param>
        public CarModel(string manufacturer, string model, double engine_size, double engine_power, CarClassesEnum carClass)
        {
            Manufacturer = manufacturer;
            Model = model;
            EnginePower = engine_power;
            EngineSize = engine_size;
            CarClass = carClass;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Получить текстовое представление модели текущего автомобиля.
        /// </summary>
        /// <returns>Текстовое представление модели текущего автомобиля.</returns>
        public override string ToString()
        {
            return string.Format("{0} {1} {2} л {3} л/с", Manufacturer, Model, EngineSize, EnginePower);
        }

        #endregion
    }
}
