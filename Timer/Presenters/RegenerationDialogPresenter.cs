using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Interfaces;

namespace Sprint.Presenters
{
    public class RegenerationDialogPresenter
    {
        #region Свойства

        /// <summary>
        /// Задать или получить диалог восстановления состояния приложения.
        /// </summary>
        private IRegenerationDialog RegenerationDialogView { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="regenerationDialogView"></param>
        public RegenerationDialogPresenter(IRegenerationDialog regenerationDialogView)
        {
            RegenerationDialogView = regenerationDialogView;
        }

        #endregion

        #region Методы



        #endregion				
    }
}
