using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Interfaces
{
    public interface IExportProcess : IWindows
    {
        #region Свойства



        #endregion

        #region Методы

        /// <summary>
        /// Закрыть окно.
        /// </summary>
        void CloseForm();

        #endregion
    }
}
