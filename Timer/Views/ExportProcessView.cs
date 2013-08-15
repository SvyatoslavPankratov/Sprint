using System;
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
    public partial class ExportProcessView : Form, IExportProcess
    {
        #region Реализация интерфейса IExportProcess

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
        /// Задать или получить презентер для данной въюхи.
        /// </summary>
        private ExportProcessPresenter Presenter { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ExportProcessView()
        {
            InitializeComponent();

            Presenter = new ExportProcessPresenter(this);
        }

        #endregion

        #region Методы



        #endregion
    }
}
