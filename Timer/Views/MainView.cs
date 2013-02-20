using System;
using System.Data;
using System.Resources;
using System.Windows.Forms;

using Sprint.Managers;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class MainView : Form, IMainView
    {
        #region Properties

        /// <summary>
        /// Задать или получить таблицу с результатми.
        /// </summary>
        public DataTable Results
        {
            get { return (DataTable) resultsGV.DataSource; }
            set { resultsGV.DataSource = value; }
        }

        /// <summary>
        /// Задать количество минут.
        /// </summary>
        public int Min
        {
            set { minLbl.Invoke(new Action(() => minLbl.Text = value.ToString("000"))); }
        }

        /// <summary>
        /// Задать количество секунд.
        /// </summary>
        public int Sec
        {
            set { secLbl.Invoke(new Action(() => secLbl.Text = value.ToString("00"))); }
        }

        /// <summary>
        /// Задать количество милисекунд.
        /// </summary>
        public int Mlsec
        {
            set { mlsecLbl.Invoke(new Action(() => mlsecLbl.Text = value.ToString("000"))); }
        }

        /// <summary>
        /// Задать или получить презентер для главного окна.
        /// </summary>
        private MainPresenter MainPresenter { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор для главного окна.
        /// </summary>
        public MainView()
        {
            InitializeComponent();

            KeyPreview = true;

            MainPresenter = new MainPresenter(this);
        }

        #endregion

        #region System methods

        /// <summary>
        /// Обработчик сообщений Windows.
        /// </summary>
        /// <param name="m">Сообщение Windows.</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WindowsShellManager.WM_HOTKEY)
            {
                MainView_KeyDown(this, new KeyEventArgs(Keys.CapsLock));
            }

            base.WndProc(ref m);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Действия при загрузке формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_Load(object sender, EventArgs e)
        {
            WindowsShellManager.RegisterHotKey(this, Keys.CapsLock);
        }

        /// <summary>
        /// Действия при нажатии на кнопку пуска секундомера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startBtn_Click(object sender, System.EventArgs e)
        {
            startBtn.Enabled    = false;
            cutOffBtn.Enabled   = true;
            stopBtn.Enabled     = true;

            MainPresenter.StartStopwatch();
        }

        /// <summary>
        /// Действия при остановке секундомера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopBtn_Click(object sender, EventArgs e)
        {
            startBtn.Enabled    = true;
            cutOffBtn.Enabled   = false;
            stopBtn.Enabled     = false;

            MainPresenter.StopStopwatch();
        }

        /// <summary>
        /// Действия при отсечке секундомера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cutOffBtn_Click(object sender, EventArgs e)
        {
            MainPresenter.CutOffStopwatch();
        }

        /// <summary>
        /// Дейсвия при отработке горячих клавиш.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && startBtn.Enabled)                    // Старт
            {
                startBtn.Enabled    = false;
                cutOffBtn.Enabled   = true;
                stopBtn.Enabled     = true;

                MainPresenter.StartStopwatch();
            }
            else if (e.KeyCode == Keys.F && !startBtn.Enabled)              // Стоп
            {
                startBtn.Enabled = true;
                cutOffBtn.Enabled = false;
                stopBtn.Enabled = false;

                MainPresenter.StopStopwatch();
            }
            else if (e.KeyCode == Keys.CapsLock && !startBtn.Enabled)       // Отсечка
            {
                MainPresenter.CutOffStopwatch();
            }
        }

        /// <summary>
        /// Действия при нажатии в меню очистки таблицы с результатами.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainPresenter.ClearResultsTable();
        }

        /// <summary>
        /// Действия при закрытии формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Вы уверены, что хотите закрыть приложение?", 
                                        "Подтверждение закрытия приложения", 
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                WindowsShellManager.UnregisterHotKey(this, Keys.CapsLock);
                MainPresenter.Dispose();
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Дейсвия при нажатии выхода в меню приложения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Действия при нажатии на кнопку о программе в меню приложения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: Панкратов Святослав \nE-mail: Svatoslav.Pankratov@gmail.com \n\nver 1.0 \n2013 год", "О программе", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
