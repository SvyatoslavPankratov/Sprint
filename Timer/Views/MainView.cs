using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Managers;
using Sprint.Models;
using Sprint.Presenters;
using Sprint.Views.Interfaces;

namespace Sprint.Views
{
    public partial class MainView : Form, IMainView
    {
        #region Поля

        private StopwatchStatesEnum _stopwatchState;

        #endregion

        #region Реализация интерфейса IMainView

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

        /// <summary>
        /// Задать первого текущего участника на трассе.
        /// </summary>
        public int FirstCurrentRacer
        {
            set 
            {
                if (value == 0)
                {
                    firstCurrentRacer_L.Text = "-";
                }
                else
                {
                    firstCurrentRacer_L.Text = value.ToString("D3");
                }

                if (SecondMonitor != null)
                {
                    SecondMonitor.FirstCurrentRacerNumber = value;
                }
            }
        }

        /// <summary>
        /// Задать второго текущего участника на трассе.
        /// </summary>
        public int SecondCurrentRacer
        {
            set
            {
                if (value == 0)
                {
                    secondCurrentRacer_L.Text = "-";
                }
                else
                {
                    secondCurrentRacer_L.Text = value.ToString("D3");
                }

                if (SecondMonitor != null)
                {
                    SecondMonitor.SecondCurrentRacerNumber = value;
                }
            }
        }

        /// <summary>
        /// Задать следующего текущего участника на трассе.
        /// </summary>
        public int NextCurrentRacer
        {
            set
            {
                if (value == 0)
                {
                    nextCurrentRacer_L.Text = "-";
                }
                else
                {
                    nextCurrentRacer_L.Text = value.ToString("D3");
                }

                if (SecondMonitor != null)
                {
                    SecondMonitor.NextRacerNumber = value;
                }
            }
        }

        /// <summary>
        /// Задать или получить состояние секундомера.
        /// </summary>
        public StopwatchStatesEnum StopwatchState
        {
            get
            {
                return _stopwatchState;
            }
            set
            {
                if (value != _stopwatchState)
                {
                    _stopwatchState = value;

                    switch (value)
                    {
                        case StopwatchStatesEnum.Start:
                            {
                                StartStopwatch();
                            } break;
                        case StopwatchStatesEnum.Stop:
                            {
                                StopStopwatch();
                            } break;
                    }
                }
            }
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить презентер для главного окна.
        /// </summary>
        private MainPresenter MainPresenter { get; set; }

        /// <summary>
        /// Задать или получить сплеш скрин.
        /// </summary>
        private ISplashScreenView SpleshScreen { get; set; }

        /// <summary>
        /// Задать или получить второй экран приложения.
        /// </summary>
        private ISecondMonitorView SecondMonitor { get; set; }

        /// <summary>
        /// Задать или получить менеджер системных хуков.
        /// </summary>
        private WindowHookManager WindowHookManager { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор для главного окна.
        /// </summary>
        public MainView()
        {
            InitializeComponent();

            Type dgvType = fwdR1DGV.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(fwdR1DGV, true, null);

            KeyPreview = true;                          // Изменено, чтобы заработали горячие клавиши

            MainPresenter = new MainPresenter(this);
            WindowHookManager = new WindowHookManager(false, false);
        }

        /// <summary>
        /// Конструктор для главного окна.
        /// </summary>
        /// <param name="splashScreen">Сплеш скрин приложения.</param>
        /// <param name="secondMonitor">Второй монитор приложения.</param>
        public MainView(ISplashScreenView splashScreen, ISecondMonitorView secondMonitor) 
            : this()
        {
            SpleshScreen = splashScreen;
            SecondMonitor = secondMonitor;
        }

        #endregion
        
        #region Методы

        /// <summary>
        /// Действия при первом отображении формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_Shown(object sender, EventArgs e)
        {
            SpleshScreen.CloseSplashScreen();

            var checkSensorView = new CheckSensorView();
            checkSensorView.ShowDialog();

            if (!MainPresenter.Racers.Any())
            {
                var newRacerView = new NewRacerView();
                var res = newRacerView.ShowDialog();

                if (res == DialogResult.OK)
                {
                    var wnd = new AddedRacersProcessView();
                    Invoke(new Action(() => wnd.Show()));

                    MainPresenter.SetRacersFromNewRacersDialog(newRacerView.NewRacerPresenter.Racers);

                    Invoke(new Action(() => wnd.Close()));
                }
            }

            WindowHookManager.RegisterHooks(true, false);
            WindowHookManager.OnMouseActivity += CutOffStopwatch;
        }

        /// <summary>
        /// Действия при нажатии на кнопку пуска секундомера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startBtn_Click(object sender, System.EventArgs e)
        {
            StopwatchState = StopwatchStatesEnum.Start;
        }

        /// <summary>
        /// Запутить секундомер.
        /// </summary>
        private void StartStopwatch()
        {
            LockAllButtonsForSelectCarClass();

            startBtn.Enabled = false;
            cutOffBtn.Enabled = true;
            stopBtn.Enabled = true;

            switch (MainPresenter.CurrentCarClass)
            {
                case CarClassesEnum.FWD:
                    {
                        carClassesTabs.TabPages[0].ImageIndex = 0;

                        if (MainPresenter.CurrentRaceNum == 0)
                        {
                            fwdRacesTabs.TabPages[0].ImageIndex = 0;
                        }
                        else
                        {
                            fwdRacesTabs.TabPages[1].ImageIndex = 0;
                        }
                    } break;
                case CarClassesEnum.RWD:
                    {
                        carClassesTabs.TabPages[1].ImageIndex = 0;

                        if (MainPresenter.CurrentRaceNum == 0)
                        {
                            rwdRacesTabs.TabPages[0].ImageIndex = 0;
                        }
                        else
                        {
                            rwdRacesTabs.TabPages[1].ImageIndex = 0;
                        }
                    } break;
                case CarClassesEnum.AWD:
                    {
                        carClassesTabs.TabPages[2].ImageIndex = 0;

                        if (MainPresenter.CurrentRaceNum == 0)
                        {
                            awdRacesTabs.TabPages[0].ImageIndex = 0;
                        }
                        else
                        {
                            awdRacesTabs.TabPages[1].ImageIndex = 0;
                        }
                    } break;
                case CarClassesEnum.Sport:
                    {
                        carClassesTabs.TabPages[3].ImageIndex = 0;

                        if (MainPresenter.CurrentRaceNum == 0)
                        {
                            sportRacesTabs.TabPages[0].ImageIndex = 0;
                        }
                        else
                        {
                            sportRacesTabs.TabPages[1].ImageIndex = 0;
                        }
                    } break;
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
            StopwatchState = StopwatchStatesEnum.Stop;
        }

        /// <summary>
        /// Остановить секундомер.
        /// </summary>
        private void StopStopwatch()
        {
            startBtn.Enabled = false;
            cutOffBtn.Enabled = false;
            stopBtn.Enabled = false;

            MainPresenter.StopStopwatch();

            UnlockAllButtonsForSelectCarClass();

            ResetSelectedCarClass();
        }

        /// <summary>
        /// Произвести отсечку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutOffStopwatch(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)                    // Отсечка
            {
                MainPresenter.CutOffStopwatch();
            }
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
        private void MainView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && startBtn.Enabled)            // Старт
            {
                StopwatchState = StopwatchStatesEnum.Start;
            }
            else if (e.KeyCode == Keys.F && !startBtn.Enabled)      // Стоп
            {
                StopwatchState = StopwatchStatesEnum.Stop;
            }
        }

        /// <summary>
        /// Сбросить выделенный класс автомобилей которые проходят в данный момент заезд.
        /// </summary>
        private void ResetSelectedCarClass()
        {
            foreach (TabPage tab in carClassesTabs.TabPages)
            {
                tab.ImageIndex = -1;
            }

            fwdRacesTabs.TabPages[0].ImageIndex = -1;
            fwdRacesTabs.TabPages[1].ImageIndex = -1;
            rwdRacesTabs.TabPages[0].ImageIndex = -1;
            rwdRacesTabs.TabPages[1].ImageIndex = -1;
            awdRacesTabs.TabPages[0].ImageIndex = -1;
            awdRacesTabs.TabPages[1].ImageIndex = -1;
            sportRacesTabs.TabPages[0].ImageIndex = -1;
            sportRacesTabs.TabPages[1].ImageIndex = -1;
        }
        
        /// <summary>
        /// Действия при закрытии формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Вы уверены, что хотите закрыть приложение?",
                                        "Подтверждение действия",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                WindowHookManager.UnregisterHooks();
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
            new AboutView().ShowDialog();
        }

        #region Главное меню

        /// <summary>
        /// Действия при нажатии пользователем в меню проверки датчика отсечки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void проверкаДатчикаОтсечкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowHookManager.UnregisterHooks();

            var checkSensorView = new CheckSensorView();
            checkSensorView.ShowDialog();

            WindowHookManager.RegisterHooks(false, true);
        }

        /// <summary>
        /// Действия при нажатии пользователя в меню выгрузки всех данных в Excel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wnd = new ExportProcessView();

            this.Invoke(new Action(() => wnd.Show()));

            MainPresenter.ExportToExcel();

            this.Invoke(new Action(() => wnd.Close()));
        }

        /// <summary>
        /// Действия при нажатии пользователем кнопки очистки данных программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void очиститьДанныеПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var q_res = MessageBox.Show("Вы уверены, что хотите удалить все данные программы?", "Подтверждение действия",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (q_res == System.Windows.Forms.DialogResult.Yes)
            {
                var res = MainPresenter.DeleteData();

                if (!res.Result)
                {
                    MessageBox.Show("Не удалось удалить данные программы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Действия при нажатии пользователем кнопки очистки логов программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void очиститьЛогиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var q_res = MessageBox.Show("Вы уверены, что хотите удалить все логи программы?", "Подтверждение действия", 
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (q_res == System.Windows.Forms.DialogResult.Yes)
            {
                var res = MainPresenter.DeleteLogs();

                if (!res.Result)
                {
                    MessageBox.Show("Не удалось удалить логи программы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Действия при нажатии пользователем в меню настройки программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void опцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wnd = new OptionsView();
            wnd.ShowDialog();
        }

        /// <summary>
        /// Действия при нажатии пользователем в меню печати результатов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void печатьРезультатовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView();
            wnd.Show();
        }

        #endregion

        #region Печать

        /// <summary>
        /// Действия при нажатии на печать результатов переднеприводных автомобилей 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.FWD.ToString(), 1, false);
            wnd.Show();
        }

        /// <summary>
        /// Действия при нажатии на печать результатов переднеприводных автомобилей 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.FWD.ToString(), 2, false);
            wnd.Show();
        }

        /// <summary>
        /// Действия при нажатии на печать результатов заднеприводных автомобилей 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.RWD.ToString(), 1, false);
            wnd.Show();
        }

        /// <summary>
        /// Действия при нажатии на печать результатов заднеприводных автомобилей 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.RWD.ToString(), 2, false);
            wnd.Show();
        }

        /// <summary>
        /// Действия при нажатии на печать результатов полноприводных автомобилей 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.AWD.ToString(), 1, false);
            wnd.Show();
        }

        /// <summary>
        /// Действия при нажатии на печать результатов полноприводных автомобилей 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.AWD.ToString(), 2, false);
            wnd.Show();
        }

        /// <summary>
        /// Действия при нажатии на печать результатов спортивных автомобилей 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.Sport.ToString(), 1, false);
            wnd.Show();
        }

        /// <summary>
        /// Действия при нажатии на печать результатов спортивных автомобилей 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.Sport.ToString(), 2, false);
            wnd.Show();
        }

        #endregion        

        #region Фиксация выбранного класса автомобилей

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (передний привод 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            LockAllButtonsForSelectCarClass();
            selectFwdCarClass1_BT.Enabled = true;
            MainPresenter.CurrentCarClass = CarClassesEnum.FWD;
            ReverseBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (задний привод 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            LockAllButtonsForSelectCarClass();
            selectRwdCarClass1_BT.Enabled = true;
            MainPresenter.CurrentCarClass = CarClassesEnum.RWD;
            ReverseBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (полный привод 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            LockAllButtonsForSelectCarClass();
            selectAwdCarClass1_BT.Enabled = true;
            MainPresenter.CurrentCarClass = CarClassesEnum.AWD;
            ReverseBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (спорт 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            LockAllButtonsForSelectCarClass();
            selectSportCarClass1_BT.Enabled = true;
            MainPresenter.CurrentCarClass = CarClassesEnum.Sport;
            ReverseBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (передний привод 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectFwdCarClass2_BT_Click(object sender, EventArgs e)
        {
            LockAllButtonsForSelectCarClass();
            selectFwdCarClass2_BT.Enabled = true;
            MainPresenter.CurrentCarClass = CarClassesEnum.FWD;
            ReverseBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (задний привод 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectRwdCarClass2_BT_Click(object sender, EventArgs e)
        {
            LockAllButtonsForSelectCarClass();
            selectRwdCarClass2_BT.Enabled = true;
            MainPresenter.CurrentCarClass = CarClassesEnum.RWD;
            ReverseBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (полный привод 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAwdCarClass2_BT_Click(object sender, EventArgs e)
        {
            LockAllButtonsForSelectCarClass();
            selectAwdCarClass2_BT.Enabled = true;
            MainPresenter.CurrentCarClass = CarClassesEnum.AWD;
            ReverseBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (спорт 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectSportCarClass2_BT_Click(object sender, EventArgs e)
        {
            LockAllButtonsForSelectCarClass();
            selectSportCarClass2_BT.Enabled = true;
            MainPresenter.CurrentCarClass = CarClassesEnum.Sport;
            ReverseBtnEnable();
        }

        /// <summary>
        /// Заблокировать все кнопки выбора текущего класса автомобилей.
        /// </summary>
        private void LockAllButtonsForSelectCarClass()
        {
            selectFwdCarClass1_BT.Enabled = false;
            selectFwdCarClass2_BT.Enabled = false;
            selectRwdCarClass1_BT.Enabled = false;
            selectRwdCarClass2_BT.Enabled = false;
            selectAwdCarClass1_BT.Enabled = false;
            selectAwdCarClass2_BT.Enabled = false;
            selectSportCarClass1_BT.Enabled = false;
            selectSportCarClass2_BT.Enabled = false;
        }

        /// <summary>
        /// Разблокировать все кнопки выбора текущего класса автомобилей.
        /// </summary>
        private void UnlockAllButtonsForSelectCarClass()
        {
            selectFwdCarClass1_BT.Enabled = true;
            selectFwdCarClass2_BT.Enabled = true;
            selectRwdCarClass1_BT.Enabled = true;
            selectRwdCarClass2_BT.Enabled = true;
            selectAwdCarClass1_BT.Enabled = true;
            selectAwdCarClass2_BT.Enabled = true;
            selectSportCarClass1_BT.Enabled = true;
            selectSportCarClass2_BT.Enabled = true;
        }

        /// <summary>
        /// Сделать кнопку запуска секундомера доступной.
        /// </summary>
        private void ReverseBtnEnable()
        {
            startBtn.Enabled = !startBtn.Enabled;
        } 

        #endregion        

        #endregion        
    }
}
