using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Interfaces
{
    public interface IWindows
    {
        #region Методы

        /// <summary>
        /// Отобразить окно.
        /// </summary>
        void Show();

        /// <summary>
        /// Обновить содержимое окна.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Задать положение окна относительно положения рабочего стола.
        /// </summary>
        /// <param name="x">Координата по X.</param>
        /// <param name="y">Координата по Y.</param>
        void SetDesktopLocation(int x, int y);

        #endregion
    }
}
