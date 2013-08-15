using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Interfaces;

namespace Sprint.Presenters
{
    public class AddedRacersProcessPresenter
    {
        #region Свойства

        /// <summary>
        /// Задать или получить представление для презентера.
        /// </summary>
        private IAddedRacersProcess View { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="view">Представление для презенетра.</param>
        public AddedRacersProcessPresenter(IAddedRacersProcess view)
        {
            View = view;
        }

        #endregion

        #region Методы



        #endregion
    }
}
