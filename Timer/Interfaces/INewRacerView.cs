using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Sprint.Models;

namespace Sprint.Views.Interfaces
{
    /// <summary>
    /// Интерфейс окна ввода участников.
    /// </summary>
    public interface INewRacerView
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
        /// Задать или получить наименование автомобиля участника.
        /// </summary>
        string CarName { get; set; }

        /// <summary>
        /// Получить выбранный класс автомобиля.
        /// </summary>
        CarClassesEnum CarClass { get; set; }

        /// <summary>
        /// Задать список классов автомобилей.
        /// </summary>
        List<string> CarClasses { set; }

        /// <summary>
        /// Задать или получить таблицу с участниками.
        /// </summary>
        DataTable RacersTable { get; set; }

        #endregion
    }
}
