﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Sprint.Managers;
using Sprint.Models;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class MainView : Form, IMainView
    {
        #region Члены интерфейса IMainView

        /// <summary>
        /// Задать или получить таблицу с результатами переднеприводных автомобилей за 1 заезд.
        /// </summary>
        public DataTable FwdFirstRace
        {
            get { return (DataTable)fwdR1DGV.DataSource; }
            set { fwdR1DGV.DataSource = value; }
        }

        /// <summary>
        /// Задать или получить таблицу с результатами переднеприводных автомобилей за 2 заезд.
        /// </summary>
        public DataTable FwdSecondRace
        {
            get { return (DataTable)fwdR2DGV.DataSource; }
            set { fwdR2DGV.DataSource = value; }
        }

        /// <summary>
        /// Задать или получить таблицу с результатами заднеприводных автомобилей за 1 заезд.
        /// </summary>
        public DataTable RwdFirstRace
        {
            get { return (DataTable)rwdR1DGV.DataSource; }
            set { rwdR1DGV.DataSource = value; }
        }

        /// <summary>
        /// Задать или получить таблицу с результатами заднеприводных автомобилей за 2 заезд.
        /// </summary>
        public DataTable RwdSecondRace
        {
            get { return (DataTable)rwdR2DGV.DataSource; }
            set { rwdR2DGV.DataSource = value; }
        }

        /// <summary>
        /// Задать или получить таблицу с результатами полноприводных автомобилей за 1 заезд.
        /// </summary>
        public DataTable AwdFirstRace
        {
            get { return (DataTable)awdR1DGV.DataSource; }
            set { awdR1DGV.DataSource = value; }
        }

        /// <summary>
        /// Задать или получить таблицу с результатами полноприводных автомобилей за 2 заезд.
        /// </summary>
        public DataTable AwdSecondRace
        {
            get { return (DataTable)awdR2DGV.DataSource; }
            set { awdR2DGV.DataSource = value; }
        }

        /// <summary>
        /// Задать или получить таблицу с результатами спортивных автомобилей за 1 заезд.
        /// </summary>
        public DataTable SportFirstRace
        {
            get { return (DataTable)sportR1DGV.DataSource; }
            set { sportR1DGV.DataSource = value; }
        }

        /// <summary>
        /// Задать или получить таблицу с результатами спортивных автомобилей за 2 заезд.
        /// </summary>
        public DataTable SportSecondRace
        {
            get { return (DataTable)sportR2DGV.DataSource; }
            set { sportR2DGV.DataSource = value; }
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

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить презентер для главного окна.
        /// </summary>
        private MainPresenter MainPresenter { get; set; }

        /// <summary>
        /// Задать или получить диалог для заполнения участников.
        /// </summary>
        public NewRacerView NewRacerView { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор для главного окна.
        /// </summary>
        public MainView()
        {
            InitializeComponent();

            KeyPreview = true;                          // Изменено, чтобы заработали горячие клавиши

            NewRacerView = new NewRacerView();
            MainPresenter = new MainPresenter(this);
        }

        #endregion

        #region Системные методы

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

        #region Методы

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
        /// Действия при первом отображении формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_Shown(object sender, EventArgs e)
        {
            MainPresenter.ShowSetRacersDialog();
        }

        /// <summary>
        /// Действия при нажатии на кнопку пуска секундомера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startBtn_Click(object sender, System.EventArgs e)
        {
            startBtn.Enabled = false;
            cutOffBtn.Enabled = true;
            stopBtn.Enabled = true;

            if (carClassesTabs.SelectedIndex == 0)
            {
                MainPresenter.CurrentCarClass = CarClassesEnum.FWD;
            }
            else if (carClassesTabs.SelectedIndex == 1)
            {
                MainPresenter.CurrentCarClass = CarClassesEnum.RWD;
            }
            else if (carClassesTabs.SelectedIndex == 2)
            {
                MainPresenter.CurrentCarClass = CarClassesEnum.AWD;
            }
            else if (carClassesTabs.SelectedIndex == 3)
            {
                MainPresenter.CurrentCarClass = CarClassesEnum.Sport;
            }

            MainPresenter.StartStopwatch();
        }

        /// <summary>
        /// Действия при остановке секундомера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopBtn_Click(object sender, EventArgs e)
        {
            startBtn.Enabled = true;
            cutOffBtn.Enabled = false;
            stopBtn.Enabled = false;

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
                startBtn.Enabled = false;
                cutOffBtn.Enabled = true;
                stopBtn.Enabled = true;

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

        #region Панель интсрументов
        
        #region Первый заезд переднеприводных автомобилей

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на переднеприводных автомобилях вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (fwdR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = fwdR1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                fwdR1DGV.ClearSelection();

                if (index > 0)
                {
                    fwdR1DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    fwdR1DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на переднеприводных автомобилях вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            if (fwdR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = fwdR1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                fwdR1DGV.ClearSelection();

                if (index < fwdR1DGV.Rows.Count - 1)
                {
                    fwdR1DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    fwdR1DGV.Rows[index].Selected = true;
                }
            }
        } 

        #endregion

        #endregion

        /// <summary>
        /// Действия при нажатии в меню очистки таблицы с результатами.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            MainPresenter.ClearResultsTable();
        }

        #endregion        
    }
}
