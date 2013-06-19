using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Managers;
using Sprint.Models;
using Sprint.Presenters;

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
            InitializeComponent();

            GlobalCarClassSelectBtnEnable = true;

            Type dgvType = fwdR1DGV.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(fwdR1DGV, true, null);

            KeyPreview = true;                          // Изменено, чтобы заработали горячие клавиши
            AutomationResetOrClose = false;

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
        
        #region Фиксация выбранного класса автомобилей

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (передний привод 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.FWD;

            if (MainPresenter.CarClassIsFinished(car_class, 1).Result)
            {
                MessageBox.Show("Класс автомобилей с передним приводом уже закончил первый заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectFwdCarClass1_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (задний привод 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.RWD;

            if (MainPresenter.CarClassIsFinished(car_class, 1).Result)
            {
                MessageBox.Show("Класс автомобилей с задним приводом уже закончил первый заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectRwdCarClass1_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (полный привод 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.AWD;

            if (MainPresenter.CarClassIsFinished(car_class, 1).Result)
            {
                MessageBox.Show("Класс автомобилей с полним приводом уже закончил первый заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectAwdCarClass1_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (спорт 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.Sport;

            if (MainPresenter.CarClassIsFinished(car_class, 1).Result)
            {
                MessageBox.Show("Класс спортивных автомобилей уже закончил первый заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectSportCarClass1_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (передний привод 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectFwdCarClass2_BT_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.FWD;

            if (MainPresenter.CarClassIsFinished(car_class, 2).Result)
            {
                MessageBox.Show("Класс автомобилей с передним приводом уже закончил второй заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectFwdCarClass2_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (задний привод 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectRwdCarClass2_BT_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.RWD;

            if (MainPresenter.CarClassIsFinished(car_class, 2).Result)
            {
                MessageBox.Show("Класс автомобилей с задним приводом уже закончил второй заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectRwdCarClass2_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (полный привод 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAwdCarClass2_BT_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.AWD;

            if (MainPresenter.CarClassIsFinished(car_class, 2).Result)
            {
                MessageBox.Show("Класс автомобилей с полным приводом уже закончил второй заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectAwdCarClass2_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (спорт 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectSportCarClass2_BT_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.Sport;

            if (MainPresenter.CarClassIsFinished(car_class, 2).Result)
            {
                MessageBox.Show("Класс спортивных автомобилей уже закончил второй заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectSportCarClass2_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (до 100 л/с 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectK100CarClass1_BT_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.K100;

            if (MainPresenter.CarClassIsFinished(car_class, 1).Result)
            {
                MessageBox.Show("Класс автомобилей с мощностью до 100 л/с уже закончил первый заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectK100CarClass1_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }
        
        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (до 100 л/с 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectK100CarClass2_BT_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.K100;

            if (MainPresenter.CarClassIsFinished(car_class, 2).Result)
            {
                MessageBox.Show("Класс автомобилей с мощностью до 100 л/с уже закончил второй заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectK100CarClass2_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (от 100 л/с до 160 л/с 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectK160CarClass1_BT_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.K160;

            if (MainPresenter.CarClassIsFinished(car_class, 1).Result)
            {
                MessageBox.Show("Класс автомобилей с мощностью от 100 л/с до 160 л/с уже закончил первый заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectK160CarClass1_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }
               
        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (от 100 л/с до 160 л/с 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectK160CarClass2_BT_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.K160;

            if (MainPresenter.CarClassIsFinished(car_class, 2).Result)
            {
                MessageBox.Show("Класс автомобилей с мощностью от 100 л/с до 160 л/с уже закончил второй заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectK160CarClass2_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (свыше 160 л/с 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectKACarClass1_BT_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.KA;

            if (MainPresenter.CarClassIsFinished(car_class, 1).Result)
            {
                MessageBox.Show("Класс автомобилей с мощностью свыше 160 л/с уже закончил первый заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectKACarClass1_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (свыше 160 л/с 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectKACarClass2_BT_Click(object sender, EventArgs e)
        {
            var car_class = CarClassesEnum.KA;

            if (MainPresenter.CarClassIsFinished(car_class, 2).Result)
            {
                MessageBox.Show("Класс автомобилей с мощностью свыше 160 л/с уже закончил второй заезд.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            selectKACarClass2_BT.Enabled = true;
            MainPresenter.CurrentCarClass = car_class;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Задать значение доступности у всех кнопок выбора текущего класса автомобилей.
        /// </summary>
        private void SetValueForAllButtonsForSelectCarClass(bool value)
        {
            selectFwdCarClass1_BT.Enabled = value;
            selectFwdCarClass2_BT.Enabled = value;
            selectRwdCarClass1_BT.Enabled = value;
            selectRwdCarClass2_BT.Enabled = value;
            selectAwdCarClass1_BT.Enabled = value;
            selectAwdCarClass2_BT.Enabled = value;
            selectSportCarClass1_BT.Enabled = value;
            selectSportCarClass2_BT.Enabled = value;
            selectK100CarClass1_BT.Enabled = value;
            selectK100CarClass2_BT.Enabled = value;
            selectK160CarClass1_BT.Enabled = value;
            selectK160CarClass2_BT.Enabled = value;
            selectKACarClass1_BT.Enabled = value;
            selectKACarClass2_BT.Enabled = value;
        }

        /// <summary>
        /// Сделать кнопку запуска секундомера доступной.
        /// </summary>
        private void ReverseStartBtnEnable()
        {
            startBtn.Enabled = !startBtn.Enabled;
        } 

        #endregion        

        #endregion
        }
}
