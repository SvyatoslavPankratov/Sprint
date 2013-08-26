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
    public partial class TimeMessageNotificationView : Form, ITimeMessageNotification
    {
        #region Реализация интерфейса ITimeMessageNotification

        /// <summary>
        /// Задать или получить текст информационного сообщения.
        /// </summary>
        public string TextMessage
        {
            get { return text_message.Text; }
            set { text_message.Text = value; }
        }

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
        private TimeMessageNotificationPresenter Presenter { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public TimeMessageNotificationView()
        {
            InitializeComponent();

            Presenter = new TimeMessageNotificationPresenter(this);
        }

        #endregion
        
        #region Методы



        #endregion
    }
}
