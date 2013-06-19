using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Interfaces
{
    public interface ICheckSensorView
    {
        #region Свойства

        /// <summary>
        /// Задать или получить скрыта или нет панель с информацией о том, что сигнал от датчика отсечки есть.
        /// </summary>
        bool OnSignal { get; set; }

        /// <summary>
        /// Задать или получить скрыта или нет панель с информацией о том, что сигнала от датчика отсечки нет.
        /// </summary>
        bool OffSignal { get; set; }

        #endregion
    }
}
