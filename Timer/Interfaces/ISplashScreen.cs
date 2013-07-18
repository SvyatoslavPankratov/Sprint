using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Interfaces
{
    public interface ISplashScreen
    {
        #region Методы

        /// <summary>
        /// Закрыть сплеш экран.
        /// </summary>
        void CloseSplashScreen();

        /// <summary>
        /// Показать окно.
        /// </summary>
        void Show();

        /// <summary>
        /// Задать положение окна относительно положения рабочего стола.
        /// </summary>
        /// <param name="x">Координата по X.</param>
        /// <param name="y">Координата по Y.</param>
        void SetDesktopLocation(int x, int y);

        #endregion
    }
}
