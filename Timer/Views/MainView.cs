using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Managers;
using Sprint.Models;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class MainView : Form, IMain
    {
        #region Поля

        private StopwatchStatesEnum _stopwatchState;

        #endregion

        #region Реализация интерфейса IMain

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
        /// Задать или получить таблицу с результатами переднеприводных автомобилей с мощностью до 100 л/с за первый заезд.
        /// </summary>
        public DataTable K100FirstRace
        {
            get { return (DataTable)K100R1DGV.DataSource; }
            set { K100R1DGV.DataSource = value; }
        }

        /// <summary>
        /// Задать или получить таблицу с результатами автомобилей с мощностью до 100 л/с за второй заезд.
        /// </summary>
        public DataTable K100SecondRace
        {
            get { return (DataTable)K100R2DGV.DataSource; }
            set { K100R2DGV.DataSource = value; }
        }

        /// <summary>
        /// Задать или получить таблицу с результатами автомобилей с мощностью от 100 л/с до 160 л/с за первый заезд.
        /// </summary>
        public DataTable K160FirstRace
        {
            get { return (DataTable)K160R1DGV.DataSource; }
            set { K160R1DGV.DataSource = value; }
        }

        /// <summary>
        /// Задать или получить таблицу с результатами автомобилей с мощностью от 100 л/с до 160 л/с за второй заезд.
        /// </summary>
        public DataTable K160SecondRace
        {
            get { return (DataTable)K160R2DGV.DataSource; }
            set { K160R2DGV.DataSource = value; }
        }

        /// <summary>
        /// Задать или получить таблицу с результатами автомобилей с мощностью от 160 л/с за первый заезд.
        /// </summary>
        public DataTable KAFirstRace
        {
            get { return (DataTable)KAR1DGV.DataSource; }
            set { KAR1DGV.DataSource = value; }
        }

        /// <summary>
        /// Задать или получить таблицу с результатами автомобилей с мощностью от 160 л/с за второй заезд.
        /// </summary>
        public DataTable KASecondRace
        {
            get { return (DataTable)KAR2DGV.DataSource; }
            set { KAR2DGV.DataSource = value; }
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
            get
            {
                if (firstCurrentRacer_L.Text == "-")
                {
                    return 0;
                }
                else
                {
                    return int.Parse(firstCurrentRacer_L.Text);
                }
            }
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
            }
        }

        /// <summary>
        /// Задать второго текущего участника на трассе.
        /// </summary>
        public int SecondCurrentRacer
        {
            get
            {
                if (secondCurrentRacer_L.Text == "-")
                {
                    return 0;
                }
                else
                {
                    return int.Parse(secondCurrentRacer_L.Text);
                }
            }
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
            }
        }

        /// <summary>
        /// Задать следующего текущего участника на трассе.
        /// </summary>
        public int NextCurrentRacer
        {
            get
            {
                if (nextCurrentRacer_L.Text == "-")
                {
                    return 0;
                }
                else
                {
                    return int.Parse(nextCurrentRacer_L.Text);
                }
            }
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
            }
        }

        /// <summary>
        /// Задать состояние готовности следующему участнику, который должен будет выходить на трек.
        /// </summary>
        public NextRacerState NextRacerState
        {
            get
            {
                if (StartPanel.Visible)
                {
                    return NextRacerState.Start;
                }
                else
                {
                    return NextRacerState.Stop;
                }
            }
            set
            {
                switch (value)
                {
                    case NextRacerState.Stop:
                        {
                            StopPanel.Visible = true;
                            StartPanel.Visible = false;
                        } break;
                    case NextRacerState.Start:
                        {
                            StopPanel.Visible = false;
                            StartPanel.Visible = true;
                        } break;
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
        /// Задать или получить второй экран приложения.
        /// </summary>
        private ISecondMonitor SecondMonitor { get; set; }

        /// <summary>
        /// Задать или получить менеджер системных хуков.
        /// </summary>
        private WindowHookManager WindowHookManager { get; set; }

        /// <summary>
        /// Задать или получить флаг, показывающий происходит 
        /// автоматическая перезагрузка приложения или ручная.
        /// </summary>
        public bool AutomationResetOrClose { get; set; }

        /// <summary>
        /// Задать или получить глобально состояние доступности 
        /// кнопок выбора текущего класса автомобилей.
        /// </summary>
        private bool GlobalCarClassSelectBtnEnable { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор для главного окна.
        /// </summary>
        public MainView()
        {
            InitMainWindow();
        }

        /// <summary>
        /// Конструктор для главного окна.
        /// </summary>
        /// <param name="secondMonitor">Второй монитор приложения.</param>
        public MainView(ISecondMonitor secondMonitor)
        {
            SecondMonitor = secondMonitor;

            InitMainWindow();
        }

        /// <summary>
        /// Тело основного конструктора.
        /// </summary>
        private void InitMainWindow()
        {
            InitializeComponent();

            GlobalCarClassSelectBtnEnable = true;

            // Включаем на всех таблицах двойную буфферизацию через рефлексию
            Type dgvType = fwdR1DGV.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(fwdR1DGV, true, null);

            KeyPreview = true; // Изменено, чтобы заработали горячие клавиши
            AutomationResetOrClose = false;

            MainPresenter = new MainPresenter(this, SecondMonitor);
            WindowHookManager = new WindowHookManager(false, false);
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
            var checkSensorView = new CheckSensorView();
            checkSensorView.ShowDialog();

            if (!MainPresenter.Racers.Any())
            {
                var newRacerView = new NewRacerView();
                var res = newRacerView.ShowDialog();

                if (res == DialogResult.OK)
                {
                    IAddedRacersProcess wnd = new AddedRacersProcessView();

                    Invoke(new Action(() =>
                                {
                                    wnd.Show();
                                    wnd.Refresh();
                                }));
                    MainPresenter.SetRacersFromNewRacersDialog(newRacerView.NewRacerPresenter.Racers);
                    Invoke(new Action(wnd.CloseForm));
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
            SetValueForAllButtonsForSelectCarClass(false);

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
                        else if (MainPresenter.CurrentRaceNum == 1)
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
                case CarClassesEnum.K100:
                    {
                        carClassesTabs.TabPages[4].ImageIndex = 0;

                        if (MainPresenter.CurrentRaceNum == 0)
                        {
                            k100RacesTabs.TabPages[0].ImageIndex = 0;
                        }
                        else
                        {
                            rwdRacesTabs.TabPages[1].ImageIndex = 0;
                        }
                    } break;
                case CarClassesEnum.K160:
                    {
                        carClassesTabs.TabPages[5].ImageIndex = 0;

                        if (MainPresenter.CurrentRaceNum == 0)
                        {
                            k160RacesTabs.TabPages[0].ImageIndex = 0;
                        }
                        else
                        {
                            k160RacesTabs.TabPages[1].ImageIndex = 0;
                        }
                    } break;
                case CarClassesEnum.KA:
                    {
                        carClassesTabs.TabPages[6].ImageIndex = 0;

                        if (MainPresenter.CurrentRaceNum == 0)
                        {
                            kaRacesTabs.TabPages[0].ImageIndex = 0;
                        }
                        else
                        {
                            kaRacesTabs.TabPages[1].ImageIndex = 0;
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

            SetValueForAllButtonsForSelectCarClass(true);

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
            k100RacesTabs.TabPages[0].ImageIndex = -1;
            k100RacesTabs.TabPages[1].ImageIndex = -1;
            k160RacesTabs.TabPages[0].ImageIndex = -1;
            k160RacesTabs.TabPages[1].ImageIndex = -1;
            kaRacesTabs.TabPages[0].ImageIndex = -1;
            kaRacesTabs.TabPages[1].ImageIndex = -1;
        }
        
        /// <summary>
        /// Действия при закрытии формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = DialogResult.No;

            if (!AutomationResetOrClose)
            {
                res = MessageBox.Show("Вы уверены, что хотите закрыть приложение?", "Подтверждение действия",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if (res == System.Windows.Forms.DialogResult.Yes || AutomationResetOrClose)
            {
                MainPresenter.DeleteAllExcelDocs();
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
            AutomationResetOrClose = false; 
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

        #endregion
    }
}
