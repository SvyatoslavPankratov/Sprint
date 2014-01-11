using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Models;

namespace Sprint.Interfaces
{
    public interface IOptions
    {
        #region Свойства

        /// <summary>
        /// Задать или получить опции гонок для заданного класса автомобилей.
        /// </summary>
        object RaceOptionsForCarClass { get; set; }

        /// <summary>
        /// Задать или получить время задержки которая активизируется после защитаной отсечки, 
        /// но после которой в течении заданного времени все следующие отсечки игнорируются. 
        /// Этот параметр нужен для того, чтобы решить проблему с ложными отсечками, 
        /// происходящими сразу после корректной отсечки. 
        /// </summary>
        double DelayTime { get; set; }

        /// <summary>
        /// Задать или получить выбранный класс автомобилей.
        /// </summary>
        CarClassesEnum SelectedCarClass { get; set; }

        #endregion

        #region Конструкторы



        #endregion

        #region Методы



        #endregion				
    }
}
