using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Views;
using Sprint.Interfaces;

namespace Sprint.Presenters
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckSensorPresenter
    {
        #region Свойства

        /// <summary>
        /// Задать или получить интерфейс на визуальное представление формы.
        /// </summary>
        private ICheckSensor CheckSensorView { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="checkSensorView">Ссылку на форму.</param>
        public CheckSensorPresenter(ICheckSensor checkSensorView)
        {
            CheckSensorView = checkSensorView;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Сброс диалога.
        /// </summary>
        public void ClearDialog()
        {
            CheckSensorView.OnSignal = false;
            CheckSensorView.OffSignal = true;
        }

        /// <summary>
        /// Установить в сигнал есть.
        /// </summary>
        public void SetOnSignal()
        {
            CheckSensorView.OnSignal = true;
            CheckSensorView.OffSignal = false;
        }

        #endregion
    }
}
