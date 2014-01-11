using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Models;
using System.Windows.Forms;

namespace Sprint.Interfaces
{
    public interface IDeleteResultsDialog
    {
        #region Свойства

        /// <summary>
        /// Задать список классов автомобилей.
        /// </summary>
        IEnumerable<CarClassesEnum?> CarClasses { set; }

        /// <summary>
        /// Получить выбранный класс автомобилей для 
        /// которых будет производиться удаление результатов.
        /// </summary>
        CarClassesEnum? SelectedCarClass { get; }

        /// <summary>
        /// Получить номер заезда, в выбранном классе автомобилей, у 
        /// всех учатсников которого будут удалены все результаты.
        /// </summary>
        int RaceNumber { get; }

        /// <summary>
        /// Получить флаг, говорящий о том надо ли сделать резервную 
        /// копию данных перед удалением результатов.
        /// </summary>
        bool BackupData { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Отобразить диалоговое окно.
        /// </summary>
        DialogResult ShowDialog();

        #endregion
    }
}
