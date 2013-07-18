using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Models;

namespace Sprint.Interfaces
{
    /// <summary>
    /// Интерфейс окна редактирования данных участника.
    /// </summary>
    public interface IEditRacer
    {
        #region Свойства

        /// <summary>
        /// Задать или получить имя
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Задать или получить фамилию.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Задать или получить отчество.
        /// </summary>
        string MiddleName { get; set; }

        /// <summary>
        /// Задать или получить марку автомобиля.
        /// </summary>
        string Manufacturer { get; set; }

        /// <summary>
        /// Задать или получить модель автомобиля.
        /// </summary>
        string Model { get; set; }

        /// <summary>
        /// Задать или получить объем двигателя.
        /// </summary>
        double EngineSize { get; set; }

        /// <summary>
        /// Задать или получить мощность двигателя.
        /// </summary>
        double EnginePower { get; set; }

        /// <summary>
        /// Получить выбранный класс автомобиля.
        /// </summary>
        CarClassesEnum CarClass { get; set; }

        /// <summary>
        /// Задать список классов автомобилей.
        /// </summary>
        List<string> CarClasses { set; }

        #endregion
    }
}
