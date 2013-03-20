﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Interfaces;

namespace Sprint.Presenters
{
    class SecondMonitorPresenter
    {
        #region Свойства

        /// <summary>
        /// Задать иои получить представление второго монитора.
        /// </summary>
        private ISecondMonitorView SecondMonitorView { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="secondMonitorView">Экземпляр интерфейса на представление второго монитора.</param>
        public SecondMonitorPresenter(ISecondMonitorView secondMonitorView)
        {
            SecondMonitorView = secondMonitorView;
        }

        #endregion
    }
}
