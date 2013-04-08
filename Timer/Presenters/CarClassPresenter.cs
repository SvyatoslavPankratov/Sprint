using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Interfaces;

namespace Sprint.Presenters
{
    /// <summary>
    /// Презентер для одого, заданного класса автомобилей со всей информацией о них.
    /// </summary>
    public class CarClassPresenter
    {
        #region Свойства

        /// <summary>
        /// Задать или получить интерфейс главной формы.
        /// </summary>
        private IMainView MainView { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="mainView">Интерфейс главной формы.</param>
        public CarClassPresenter(IMainView mainView)
        {
            MainView = mainView;
        }

        #endregion

        #region Методы



        #endregion						
    }
}
