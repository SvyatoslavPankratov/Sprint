using System.Collections.Generic;

using Sprint.Models;

namespace Sprint.Interfaces
{
    /// <summary>
    /// Интерфейс представления печати.
    /// </summary>
    public interface IPrint
    {
        #region Свойства

        /// <summary>
        /// Задать или получить классы автомобилей.
        /// </summary>
        IEnumerable<string> CarClasses { set; }

        /// <summary>
        /// Задать или получить доступные номера заездов в выбранном классе автомобилей.
        /// </summary>
        IEnumerable<int> RacesNumbers { set; }

        /// <summary>
        /// Получить выбранный класс автомобилей.
        /// </summary>
        string SelectedCarClass { get; }

        /// <summary>
        /// Получить выбранный номер заезда в выбранном классе автомобилей.
        /// </summary>
        int SelectedRaceNumber { get; }

        #endregion
    }
}
