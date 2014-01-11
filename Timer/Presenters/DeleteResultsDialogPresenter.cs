using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Interfaces;
using Sprint.Managers;
using Sprint.Models;

namespace Sprint.Presenters
{
    public class DeleteResultsDialogPresenter
    {
        #region Свойства

        /// <summary>
        /// Задать или получить интерфейс представления диалогового окна 
        /// о выборе варианта удаления всех результатов у участников.
        /// </summary>
        private IDeleteResultsDialog DeleteResultDialogView { get; set; }

        #endregion

        #region Конструторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="view">Представление диалогового окна о выборе варианта удаления всех результатов у участников.</param>
        public DeleteResultsDialogPresenter(IDeleteResultsDialog view)
        {
            DeleteResultDialogView = view;
            DataBind();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Заполнить преставление данными.
        /// </summary>
        private void DataBind()
        {
            var list = Enum.GetValues(typeof(CarClassesEnum)).OfType<CarClassesEnum?>().ToList();
            list.Insert(0, null);

            DeleteResultDialogView.CarClasses = list;
        }

        #endregion
    }
}
