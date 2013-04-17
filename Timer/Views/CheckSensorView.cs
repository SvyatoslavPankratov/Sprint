using System;
using System.Windows.Forms;

using Sprint.Managers;
using Sprint.Presenters;
using Sprint.Views.Interfaces;

namespace Sprint.Views
{
    public partial class CheckSensorView : Form, ICheckSensorView
    {
        #region Реализация интерфейса ICheckSensorView

        /// <summary>
        /// Задать или получить скрыта или нет панель с информацией о том, что сигнал от датчика отсечки есть.
        /// </summary>
        public bool OnSignal
        {
            get { return On_Panel.Visible; }
            set { On_Panel.Visible = value; }
        }

        /// <summary>
        /// Задать или получить скрыта или нет панель с информацией о том, что сигнала от датчика отсечки нет.
        /// </summary>
        public bool OffSignal
        {
            get { return Off_Panel.Visible; }
            set { Off_Panel.Visible = value; }
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить представление формы для проверки сигнала с датчиком отсечки.
        /// </summary>
        private CheckSensorPresenter CheckSensorPresenter { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструкторы.
        /// </summary>
        public CheckSensorView()
        {
            InitializeComponent();

            CheckSensorPresenter = new CheckSensorPresenter(this);
        } 

        #endregion

        #region Системные методы

         /// <summary>
         /// Обработчик сообщений Windows.
         /// </summary>
         /// <param name="m">Сообщение Windows.</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WindowsHotKeysManager.WM_HOTKEY:
                    {
                        if (m.LParam == (IntPtr)1310720)
                        {
                            CheckSensorView_KeyDown(this, new KeyEventArgs(Keys.CapsLock));
                        }
                    } break;
            }

            base.WndProc(ref m);
        }

        #endregion

        #region Методы

        /// <summary>
        /// Действия при загрузке формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckSensorView_Load(object sender, System.EventArgs e)
        {
            WindowsHotKeysManager.RegisterHotKey(this, Keys.CapsLock);
        }

        /// <summary>
        /// Действия, происходящие при закрытии диалога.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Действия при попытке повторно провети проверку сигнала от датчика отсечки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, System.EventArgs e)
        {
            CheckSensorPresenter.ClearDialog();
        }

        /// <summary>
        /// Дейсвия при отработке горячих клавиш.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckSensorView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.CapsLock)       // Отсечка
            {
                CheckSensorPresenter.SetOnSignal();
            }
        }

        /// <summary>
        /// Действия при закрытии формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckSensorView_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowsHotKeysManager.UnregisterHotKey(this, Keys.CapsLock);
        }

        #endregion
    }
}
