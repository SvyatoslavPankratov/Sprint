﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class AddedRacersProcessView : Form, IAddedRacersProcess
    {
        #region Реализация интерфейса ITimeMessageNotification

        /// <summary>
        /// Закрыть окошко.
        /// </summary>
        public void CloseForm()
        {
            while (Opacity != 0)
            {
                Opacity -= 0.1;
                Thread.Sleep(100);
            }

            Close();
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить презентер для представления.
        /// </summary>
        private AddedRacersProcessPresenter Presenter { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструкор.
        /// </summary>
        public AddedRacersProcessView()
        {
            InitializeComponent();

            Presenter = new AddedRacersProcessPresenter(this);
        }

        #endregion
        
        #region Методы

        #endregion
    }
}
