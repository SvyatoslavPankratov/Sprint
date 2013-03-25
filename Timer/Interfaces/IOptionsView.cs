using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Models;

namespace Sprint.Interfaces
{
    public interface IOptionsView
    {
        #region Свойства

        /// <summary>
        /// Задать или получить опции гонок для заданного класса автомобилей.
        /// </summary>
        RaceOptionsModel RaceOptionsForCarClass { get; set; }

        #endregion

        #region Конструкторы



        #endregion

        #region Методы



        #endregion				
    }
}
