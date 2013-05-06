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

        /// <summary>
        /// Задать или получить менеджер системных хуков.
        /// </summary>
        private WindowHookManager WindowHookManager { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструкторы.
        /// </summary>
        public CheckSensorView()
        {
            InitializeComponent();

            CheckSensorPresenter = new CheckSensorPresenter(this);
            WindowHookManager = new WindowHookManager(true, false);
            WindowHookManager.OnMouseActivity += CheckSensorView_KeyUp;
        } 

        #endregion

        #region Методы

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
        /// Дейсвия при отработке горячих клавиш.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckSensorView_KeyUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Middle:
                    {
                        CheckSensorPresenter.SetOnSignal();
                    } break;
            }
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
        /// Действия при закрытии формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckSensorView_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowHookManager.UnregisterHooks();
        }

        #endregion
    }
}
