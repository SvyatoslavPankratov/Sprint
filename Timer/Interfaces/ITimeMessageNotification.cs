using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Interfaces
{
    public interface ITimeMessageNotification : IWindows
    {
        #region Свойства

        /// <summary>
        /// Задать или получить текст информационного сообщения.
        /// </summary>
        string TextMessage { get; set; }

        #endregion

        #region Методы

        /// <summary>
        /// Закрыть окошко.
        /// </summary>
        void CloseForm();

        #endregion
    }
}
