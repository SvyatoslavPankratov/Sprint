using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Linq;

using Sprint.Models;
using Sprint.Views;

namespace Sprint.Presenters
{
    /// <summary>
    /// Презнетер главного окна.
    /// </summary>
    class MainPresenter : IDisposable
    {
        #region Properties

        /// <summary>
        /// Задать или получить интерфейс на главную форму.
        /// </summary>
        private IMainView MainView { get; set; }

        /// <summary>
        /// Задать или получить секундомер.
        /// </summary>
        private StopwatchModel Stopwatch { get; set; }

        /// <summary>
        /// Задать или получить поток для осуществления моментального вывода данных секундомера на форму.
        /// </summary>
        private Thread ThreadSync { get; set; }

        /// <summary>
        /// Задать или получить флаг реверса выводимой отсечки.
        /// </summary>
        private bool Reverse { get; set; }

        /// <summary>
        /// Задать или получить первая-ли у секундомера отсечка после его остановки.
        /// </summary>
        private bool Stoped { get; set; }

        /// <summary>
        /// Задать или получить в какую колонку нужно добавлять новое значение, без учета флага реверса.
        /// </summary>
        private bool LeftRight { get; set; }

        /// <summary>
        /// Задать или получить список гонщиков.
        /// </summary>
        public List<RacerModel> Racers { get; set; }

        /// <summary>
        /// Задать или получить список групп гонщиков по классам автомобилей.
        /// </summary>
        public List<RacersGroupModel> RacerGroups { get; set; }

        /// <summary>
        /// Задать или получить список лидеров по классам автомобилей.
        /// </summary>
        public List<RacersGroupModel> LiderRacerGroups { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="mainView">Интерфейс на главную форму.</param>
        public MainPresenter(IMainView mainView)
        {
            MainView = mainView;

            Racers              = new List<RacerModel>();
            Stopwatch           = new StopwatchModel();
            RacerGroups         = new List<RacersGroupModel>();
            LiderRacerGroups    = new List<RacersGroupModel>();

            InitializeAllTables();

            ThreadSync = new Thread(() => DataBindingProcess(MainView, Stopwatch));

            ResetFlags();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Инициализация всех таблиц.
        /// </summary>
        private void InitializeAllTables()
        {
            MainView.FwdFirstRace       = InitializeTable();
            MainView.FwdSecondRace      = InitializeTable();
            MainView.RwdFirstRace       = InitializeTable();
            MainView.RwdSecondRace      = InitializeTable();
            MainView.AwdFirstRace       = InitializeTable();
            MainView.AwdSecondRace      = InitializeTable();
            MainView.SportFirstRace     = InitializeTable();
            MainView.SportSecondRace    = InitializeTable();
        }

        /// <summary>
        /// Показать диалог для заполнения участников гонки.
        /// </summary>
        public void ShowSetRacersDialog()
        {
            MainView.NewRacerView.ShowDialog();

            Racers = MainView.NewRacerView.NewRacerPresenter.Racers;

            SetRacersForTableForFirstRace();
        }

        /// <summary>
        /// Сброс всех флагов в изначальное состояние.
        /// </summary>
        private void ResetFlags()
        {
            Reverse = false;
            LeftRight = true;
            Stoped = true;
        }

        /// <summary>
        /// Запустить секундомер.
        /// </summary>
        public void StartStopwatch()
        {
            Stopwatch.Start();
            
            try
            {
                ThreadSync.Start();
            }
            catch (ThreadStateException)
            {
                ThreadSync.Resume();
            }
        }

        /// <summary>
        /// Остановить секундомер.
        /// </summary>
        public void StopStopwatch()
        {
            Stopwatch.Stop();
            ThreadSync.Suspend();

            MainView.Min = 0;
            MainView.Sec = 0;
            MainView.Mlsec = 0;

            ResetFlags();
        }

        /// <summary>
        /// Произвести отсечку времени и добавить результат в таблицу.
        /// </summary>
        public void CutOffStopwatch()
        {
            var time = Stopwatch.Time;
            //var row = MainView.Results.NewRow();

            //row[0] = MainView.Results.Rows.Count + 1;
            //row[1] = time.TimeSpan.ToString();

            //var value = string.Empty;

            //if (Stoped)
            //{
            //    value = string.Format("{0} : {1} : {2}",    time.Min.ToString("00"),
            //                                                time.Sec.ToString("00"),
            //                                                time.Mlsec.ToString("000"));

            //    Stoped = false;
            //}
            //else
            //{
            //    var previousRow = MainView.Results.Rows[MainView.Results.Rows.Count - 1];
            //    var timeSpan = time.TimeSpan - TimeSpan.Parse(previousRow[1].ToString());

            //    value = string.Format("{0} : {1} : {2}",    timeSpan.Minutes.ToString("00"),
            //                                                timeSpan.Seconds.ToString("00"),
            //                                                timeSpan.Milliseconds.ToString("000"));
            //}

            //if (LeftRight)
            //{
            //    row[2] = value;
            //}
            //else
            //{
            //    row[3] = value;
            //}

            //if (!Reverse)
            //{
            //    LeftRight = !LeftRight;
            //}

            //MainView.Results.Rows.Add(row);
        }

        /// <summary>
        /// Инициировать флаг реверса выводимой отсечки.
        /// </summary>
        public void ReverseChange()
        {
            Reverse = !Reverse;
            LeftRight = !LeftRight;
        }

        /// <summary>
        /// Очистка таблицы с результатами.
        /// </summary>
        public void ClearResultsTable()
        {
            InitializeAllTables();
            SetRacersForTableForFirstRace();
        }

        /// <summary>
        /// Сгенерировать заголовки для таблицы результатов.
        /// </summary>
        private DataTable InitializeTable()
        {
            var table = new DataTable();

            var column = new DataColumn("№");
            table.Columns.Add(column);

            column = new DataColumn("Ф И О");
            table.Columns.Add(column);

            column = new DataColumn("Автомобиль");
            table.Columns.Add(column);

            column = new DataColumn("Круг №1");
            table.Columns.Add(column);

            column = new DataColumn("Круг №2");
            table.Columns.Add(column);

            column = new DataColumn("Круг №3");
            table.Columns.Add(column);

            column = new DataColumn("Круг №4");
            table.Columns.Add(column);

            return table;
        }

        /// <summary>
        /// Генерация и заполнение таблицы участниками для первого заезда.
        /// </summary>
        public void SetRacersForTableForFirstRace()
        {
            RacerGroups.Add(new RacersGroupModel(CarClassesEnum.FWD) { Racers = Racers.Where(r => r.Car.CarClass == CarClassesEnum.FWD) });
            MainView.FwdFirstRace = SetNamesToTable();

            RacerGroups.Add(new RacersGroupModel(CarClassesEnum.RWD) { Racers = Racers.Where(r => r.Car.CarClass == CarClassesEnum.RWD) });
            MainView.RwdFirstRace = SetNamesToTable();

            RacerGroups.Add(new RacersGroupModel(CarClassesEnum.AWD) { Racers = Racers.Where(r => r.Car.CarClass == CarClassesEnum.AWD) });
            MainView.AwdFirstRace = SetNamesToTable();

            RacerGroups.Add(new RacersGroupModel(CarClassesEnum.Sport) { Racers = Racers.Where(r => r.Car.CarClass == CarClassesEnum.Sport) });
            MainView.SportFirstRace = SetNamesToTable();
        }

        /// <summary>
        /// Задать список лидеров по группам.
        /// </summary>
        /// <param name="count">Количество получаемых лидеров в каждом классе.</param>
        /// <returns>Список лидеров по классам.</returns>
        public void SetLeaders(int count)
        {
            foreach (var rg in RacerGroups)
            {
                var racers = (from racer in rg.Racers
                              orderby racer.Results.GetMinTime(1)
                              select racer).Reverse();

                LiderRacerGroups.Add(new RacersGroupModel(rg.CarClass) { Racers = racers.Take(count) });
            }
        }

        /// <summary>
        /// Заполним таблицу именами из последней группы участников в списке групп.
        /// </summary>
        /// <returns>Таблица, заполненная участниками.</returns>
        private DataTable SetNamesToTable()
        {
            var table = InitializeTable();

            foreach (var racer in RacerGroups.Last().Racers)
            {
                var row = table.NewRow();
                row[0] = racer.RacerNumber;
                row[1] = string.Format("{0} {1} {2}", racer.FirstName, racer.LastName, racer.MiddleName);
                row[2] = racer.Car.Name;
                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// Процесс, осуществляющий моментальный вывод данных секундомера на форму.
        /// </summary>
        /// <param name="mainView"></param>
        private void DataBindingProcess(IMainView mainView, StopwatchModel stopwatch)
        {
            while (true)
            {
                mainView.Min = stopwatch.Time.Min;
                mainView.Sec = stopwatch.Time.Sec;
                mainView.Mlsec = stopwatch.Time.Mlsec;
            }
        }

        /// <summary>
        /// Освободить все занимаемые объектом ресурсы.
        /// </summary>
        public void Dispose()
        {
            if (ThreadSync != null)
            {
                try
                {
                    ThreadSync.Abort();
                }
                catch (ThreadStateException)
                {
                    ThreadSync.Resume();
                    ThreadSync.Abort();
                }

                ThreadSync = null;
            }

            Stopwatch.Dispose();
        }

        #endregion
    }
}
