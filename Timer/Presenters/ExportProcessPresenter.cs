using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Interfaces;

namespace Sprint.Presenters
{
    public class ExportProcessPresenter
    {
        #region Свойства

        /// <summary>
        /// Задать или получить въюху для данного презентера.
        /// </summary>
        private IExportProcess View { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="view">Въюха для данного презентера.</param>
        public ExportProcessPresenter(IExportProcess view)
        {
            View = view;
        }

        #endregion

        #region Методы



        #endregion
    }
}
