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
        #region Constants

        /// <summary>
        /// Максимальное количество заездов.
        /// </summary>
        public const int MaxRaceCount = 2;

        /// <summary>
        /// Максимальное количество кругов в заезде.
        /// </summary>
        public const int MaxCircleCount = 4;

        /// <summary>
        /// Максимальное количество гонщиков одновременно присутствующих на треке.
        /// </summary>
        public const int MaxRacersForRace = 2;

        #endregion

        #region Fields

        private IEnumerable<RacerModel> _racers;

        #endregion

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
        public IEnumerable<RacerModel> Racers
        {
            get
            {
                return _racers;
            }
            set
            {
                _racers = value;

                foreach (var racer in _racers)
                {
                    racer.Results = new ResultsModel(MaxRaceCount, MaxCircleCount);
                }
            }
        }

        /// <summary>
        /// Получить список групп гонщиков по классам автомобилей.
        /// </summary>
        public IEnumerable<RacersGroupModel> RacerGroups
        {
            get
            {
                var res = new List<RacersGroupModel>();

                res.Add(new RacersGroupModel(CarClassesEnum.FWD) { Racers = Racers.Where(r => r.Car.CarClass == CarClassesEnum.FWD) });
                res.Add(new RacersGroupModel(CarClassesEnum.RWD) { Racers = Racers.Where(r => r.Car.CarClass == CarClassesEnum.RWD) });
                res.Add(new RacersGroupModel(CarClassesEnum.AWD) { Racers = Racers.Where(r => r.Car.CarClass == CarClassesEnum.AWD) });
                res.Add(new RacersGroupModel(CarClassesEnum.Sport) { Racers = Racers.Where(r => r.Car.CarClass == CarClassesEnum.Sport) });

                return res;
            }
        }

        /// <summary>
        /// Получить текущий класс автомобилей на трассе.
        /// </summary>
        public RacersGroupModel CurrentRaserGroup
        {
            get { return RacerGroups.FirstOrDefault(rg => rg.CarClass == CurrentCarClass); }
        }

        /// <summary>
        /// Задать или получить список лидеров по классам автомобилей.
        /// </summary>
        public IEnumerable<RacersGroupModel> LiderRacerGroups { get; set; }

        /// <summary>
        /// Задать или получить текущий номер заезда.
        /// </summary>
        public int CurrentRaceNum { get; private set; }

        /// <summary>
        /// Задать или получить текущий класс автомобилей проходящих заезд.
        /// </summary>
        private CarClassesEnum CurrentCarClass { get; set; }

        /// <summary>
        /// Задать или получить модель трека.
        /// </summary>
        private TrackModel Track { get; set; }

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
            LiderRacerGroups    = new List<RacersGroupModel>();
            Track               = new TrackModel(2);

            InitializeAllTables();

            ThreadSync = new Thread(() => StopwatchDataBindingProcess(MainView, Stopwatch));

            ResetFlags();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Задать текущий класс автомобилей, участвующий в текущем заезде.
        /// </summary>
        /// <param name="carClass">Класс автомобилей.</param>
        public void SetCurrentCarClass(CarClassesEnum carClass)
        {
            CurrentCarClass = carClass;
        }

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
        /// Произвести отсечку времени и добавить результат гонщику.
        /// </summary>
        public void CutOffStopwatch()
        {
            var time = new TimeModel(Stopwatch.Time.TimeSpan);

            // Если трек пустой, то добавить на него одного гонщика
            if (Track.CurrentRacers.Count() == 0)
            {
                var racer = RacerGroups.FirstOrDefault(rg => rg.CarClass == CurrentCarClass).Racers.First();
                (Track.CurrentRacers as List<RacerModel>).Add(racer);
            }

            if (Track.CurrentRacer.Results.StartTime == null)
            {
                Track.CurrentRacer.Results.StartTime = time;
            }
            else
            {
                var res = new TimeModel(time.TimeSpan - Track.CurrentRacer.Results.StartTime.TimeSpan);
                Track.CurrentRacer.Results.AddResult(CurrentRaceNum, res);
                Track.CurrentRacer.Results.StartTime = time;
            }
            

            CheckCurrentGroupFinishedRace();

            // Переход к следующему классу автомобилей

            // Закрытие неудачных заездов

            // Поиск повторных заездов

            // Определение следующего автомобиля для выхода на трассу
            DataBind();


            // Вывести все результаты на форму

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
        /// Процесс, осуществляющий моментальный вывод данных секундомера на форму.
        /// </summary>
        /// <param name="mainView"></param>
        private void StopwatchDataBindingProcess(IMainView mainView, StopwatchModel stopwatch)
        {
            while (true)
            {
                mainView.Min = stopwatch.Time.Min;
                mainView.Sec = stopwatch.Time.Sec;
                mainView.Mlsec = stopwatch.Time.Mlsec;
            }
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
        /// Проверить группу на полное финиширование всех участников.
        /// </summary>
        /// <returns></returns>
        private bool CheckCurrentGroupFinishedRace()
        {
            var curCarGroup = RacerGroups.FirstOrDefault(rg => rg.CarClass == CurrentCarClass);

            if (curCarGroup == null)
            {
                throw new Exception("Не удалось определить текущий класс автомобилей, проходящих заезд.");
            }

            foreach (var racer in curCarGroup.Racers)
            {
                if (!racer.Results.Finished)
                {
                    return false;
                }
            }

            return true;
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
            MainView.FwdFirstRace = SetNamesToTable(0);            
            MainView.RwdFirstRace = SetNamesToTable(1);            
            MainView.AwdFirstRace = SetNamesToTable(2);            
            MainView.SportFirstRace = SetNamesToTable(3);
        }

        /// <summary>
        /// Заполним таблицу именами из последней группы участников в списке групп.
        /// </summary>
        /// <param name="groupNumber">Номер группы гонщиков в списке.</param>
        /// <returns>Таблица, заполненная участниками.</returns>
        private DataTable SetNamesToTable(int groupNumber)
        {
            var table = InitializeTable();

            foreach (var racer in RacerGroups.ElementAt(groupNumber).Racers)
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

                (LiderRacerGroups as List<RacersGroupModel>).Add(new RacersGroupModel(rg.CarClass) { Racers = racers.Take(count) });
            }
        }

        /// <summary>
        /// Передвинуть участника вверх на одну позицию.
        /// </summary>
        /// <param name="racerNum">Номер перемещаемого участника.</param>
        public void MoveUpRacer(int racerNum)
        {
            var racer = CurrentRaserGroup.Racers.ElementAt(racerNum);
            var gl_racers = Racers.ToList();

            var gl_index = gl_racers.IndexOf(racer);

            for (int i = gl_index - 1; i >= 0; i--)
            {
                if (gl_racers[i].Car.CarClass == CurrentCarClass)
                {
                    gl_racers.Remove(racer);
                    gl_racers.Insert(i, racer);
                    break;
                }
            }

            Racers = gl_racers;

            DataBind();
        }

        /// <summary>
        /// Передвинуть участника вниз на одну позицию.
        /// </summary>
        /// <param name="racerNum">Номер перемещаемого участника.</param>
        public void MoveDownRacer(int racerNum)
        {
            var racer = CurrentRaserGroup.Racers.ElementAt(racerNum);
            var gl_racers = Racers.ToList();

            var gl_index = gl_racers.IndexOf(racer);

            for (int i = gl_index + 1; i < gl_racers.Count; i++)
            {
                if (gl_racers[i].Car.CarClass == CurrentCarClass)
                {
                    gl_racers.Remove(racer);
                    gl_racers.Insert(i, racer);
                    break;
                }
            }

            Racers = gl_racers;

            DataBind();
        }

        #region Data Bind [Есть недоработки]

        /// <summary>
        /// Привязка всех данных к таблицам на форме.
        /// </summary>
        public void DataBind()
        {
            foreach (var group in RacerGroups)
            {
                switch (group.CarClass)
                {
                    case CarClassesEnum.FWD:
                        {
                            OutFwdResult(group);
                        } break;

                    case CarClassesEnum.RWD:
                        {
                            OutRwdResult(group);
                        } break;

                    case CarClassesEnum.AWD:
                        {
                            OutAwdResult(group);
                        } break;

                    case CarClassesEnum.Sport:
                        {
                            OutSportResult(group);
                        } break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// Вывести результаты на форму у переднеприводного класса автомобилей.
        /// </summary>
        /// <param name="group">Переднеприводная группа гонщиков.</param>
        private void OutFwdResult(RacersGroupModel group)
        {   
            DataTable table = SetNamesToTable(0);

            for (int row = 0; row < group.Racers.Count(); row++)
            {
                var racer = group.Racers.ElementAt(row);
                
                for (int circle = 0; circle < MaxCircleCount; circle++)
                {
                    var time = racer.Results.ResultsList.ElementAt(0).ElementAt(circle);

                    if (time != null)
                    {
                        table.Rows[row][circle + 3] = string.Format("{0} : {1} : {2}",  time.Min.ToString("00"),
                                                                                        time.Sec.ToString("00"),
                                                                                        time.Mlsec.ToString("000"));
                    }
                }

                MainView.FwdFirstRace = table;

                if (racer.Results.ResultsList.Count() > 1)
                {
                    table = MainView.FwdSecondRace;

                    for (int circle = 0; circle < MaxCircleCount; circle++)
                    {
                        var time = racer.Results.ResultsList.ElementAt(1).ElementAt(circle);

                        if(time != null)
                        {
                            table.Rows[row][circle + 3] = string.Format("{0} : {1} : {2}",  time.Min.ToString("00"),
                                                                                            time.Sec.ToString("00"),
                                                                                            time.Mlsec.ToString("000"));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Вывести результаты на форму у заднеприводного класса автомобилей.
        /// </summary>
        /// <param name="group">Заднеприводная группа гонщиков.</param>
        private void OutRwdResult(RacersGroupModel group)
        {
            DataTable table = SetNamesToTable(1);

            for (int row = 0; row < group.Racers.Count(); row++)
            {
                var racer = group.Racers.ElementAt(row);
                
                for (int circle = 0; circle < MaxCircleCount; circle++)
                {
                    var time = racer.Results.ResultsList.ElementAt(0).ElementAt(circle);

                    if (time != null)
                    {
                        table.Rows[row][circle + 3] = string.Format("{0} : {1} : {2}",  time.Min.ToString("00"),
                                                                                        time.Sec.ToString("00"),
                                                                                        time.Mlsec.ToString("000"));
                    }
                }

                MainView.RwdFirstRace = table;
                
                if (racer.Results.ResultsList.Count() > 1)
                {
                    table = MainView.RwdSecondRace;

                    for (int circle = 0; circle < MaxCircleCount; circle++)
                    {
                        var time = racer.Results.ResultsList.ElementAt(1).ElementAt(circle);

                        if (time != null)
                        {
                            table.Rows[row][circle + 3] = string.Format("{0} : {1} : {2}",  time.Min.ToString("00"),
                                                                                            time.Sec.ToString("00"),
                                                                                            time.Mlsec.ToString("000"));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Вывести результаты на форму у полноприводного класса автомобилей.
        /// </summary>
        /// <param name="group">Полноприводная группа гонщиков.</param>
        private void OutAwdResult(RacersGroupModel group)
        {
            DataTable table = SetNamesToTable(2);

            for (int row = 0; row < group.Racers.Count(); row++)
            {
                var racer = group.Racers.ElementAt(row);
                
                for (int circle = 0; circle < MaxCircleCount; circle++)
                {
                    var time = racer.Results.ResultsList.ElementAt(0).ElementAt(circle);

                    if (time != null)
                    {
                        table.Rows[row][circle + 3] = string.Format("{0} : {1} : {2}",  time.Min.ToString("00"),
                                                                                        time.Sec.ToString("00"),
                                                                                        time.Mlsec.ToString("000"));
                    }
                }

                MainView.AwdFirstRace = table;

                if (racer.Results.ResultsList.Count() > 1)
                {
                    table = MainView.AwdSecondRace;

                    for (int circle = 0; circle < MaxCircleCount; circle++)
                    {
                        var time = racer.Results.ResultsList.ElementAt(1).ElementAt(circle);

                        if (time != null)
                        {
                            table.Rows[row][circle + 3] = string.Format("{0} : {1} : {2}", time.Min.ToString("00"),
                                                                                            time.Sec.ToString("00"),
                                                                                            time.Mlsec.ToString("000"));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Вывести результаты на форму у спортивного класса автомобилей.
        /// </summary>
        /// <param name="group">Спортивная группа гонщиков.</param>
        private void OutSportResult(RacersGroupModel group)
        {
            DataTable table = SetNamesToTable(3);

            for (int row = 0; row < group.Racers.Count(); row++)
            {
                var racer = group.Racers.ElementAt(row);
                
                for (int circle = 0; circle < MaxCircleCount; circle++)
                {
                    var time = racer.Results.ResultsList.ElementAt(0).ElementAt(circle);

                    if (time != null)
                    {
                        table.Rows[row][circle + 3] = string.Format("{0} : {1} : {2}",  time.Min.ToString("00"),
                                                                                        time.Sec.ToString("00"),
                                                                                        time.Mlsec.ToString("000"));
                    }
                }

                MainView.SportFirstRace = table;

                if (racer.Results.ResultsList.Count() > 1)
                {
                    table = MainView.SportSecondRace;

                    for (int circle = 0; circle < MaxCircleCount; circle++)
                    {
                        var time = racer.Results.ResultsList.ElementAt(1).ElementAt(circle);

                        if (time != null)
                        {
                            table.Rows[row][circle + 3] = string.Format("{0} : {1} : {2}", time.Min.ToString("00"),
                                                                                            time.Sec.ToString("00"),
                                                                                            time.Mlsec.ToString("000"));
                        }
                    }
                }
            }
        } 

        #endregion

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
