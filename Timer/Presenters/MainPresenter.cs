using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Linq;

using Sprint.Models;
using Sprint.Views;
using Sprint.Extensions;
using Sprint.Views.Interfaces;
using Sprint.Managers;

namespace Sprint.Presenters
{
    /// <summary>
    /// Презнетер главного окна.
    /// </summary>
    class MainPresenter : IDisposable
    {
        #region Константы

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

        #region Поля

        private IEnumerable<RacerModel> _racers;

        #endregion

        #region Свойства

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
                    if (racer.Results == null)
                    {
                        racer.Results = new ResultsModel(MaxRaceCount, MaxCircleCount);
                    }
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

                res.Add(new RacersGroupModel(CarClassesEnum.FWD, Racers) { Racers = Racers.Where(r => r.Car.CarClass == CarClassesEnum.FWD), RaceNumber = 1 });
                res.Add(new RacersGroupModel(CarClassesEnum.RWD, Racers) { Racers = Racers.Where(r => r.Car.CarClass == CarClassesEnum.RWD), RaceNumber = 1 });
                res.Add(new RacersGroupModel(CarClassesEnum.AWD, Racers) { Racers = Racers.Where(r => r.Car.CarClass == CarClassesEnum.AWD), RaceNumber = 1 });
                res.Add(new RacersGroupModel(CarClassesEnum.Sport, Racers) { Racers = Racers.Where(r => r.Car.CarClass == CarClassesEnum.Sport), RaceNumber = 1 });

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
        public CarClassesEnum CurrentCarClass { get; set; }

        /// <summary>
        /// Задать или получить текущий класс автомобилей владельцы которых 
        /// находятся в режиме редактирования.
        /// </summary>
        public CarClassesEnum CurrentEditCarClass { get; set; }

        /// <summary>
        /// Задать или получить текущий номер заезда участники которого 
        /// находятся в режиме редактирования.
        /// </summary>
        public int CurrentEditRaceNumber { get; set; }

        /// <summary>
        /// Задать или получить модель трека.
        /// </summary>
        private TrackModel Track { get; set; }

        #endregion

        #region Конструкторы

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

        #region Методы

        /// <summary>
        /// Взять из диалдога добавления новых гонщиков список участников.
        /// </summary>
        public void SetRacersFromNewRacersDialog()
        {
            Racers = MainView.AddedRacers;           
            SetRacersForTableForFirstRace();
        }

        /// <summary>
        /// Сброс всех флагов в изначальное состояние.
        /// </summary>
        private void ResetFlags()
        {
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
        /// Процесс, осуществляющий постоянный вывод данных секундомера на форму.
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
        /// Задать список лидеров по заданной группе.
        /// </summary>
        /// <param name="count">Количество получаемых лидеров в заданном классе.</param>
        /// <param name="carClass"></param>
        public void SetLeaders(int count, CarClassesEnum carClass)
        {
            var rg = RacerGroups.FirstOrDefault(g => g.CarClass == carClass);

            if (rg == null)
            {
                return;
            }

            (LiderRacerGroups as List<RacersGroupModel>).Add(new RacersGroupModel(carClass, Racers) { Racers = rg.GetLeaders(count) });
        }

        /// <summary>
        /// Экспорт всех данных в Excel.
        /// </summary>
        public void ExportToExcel()
        {
            var tables = new List<TableWithResults>();

            foreach (var group in RacerGroups)
            {
                tables.Add(new TableWithResults { Results = GetTableWithResults(group, group.RaceNumber), CarClass = group.CarClass, RaceNumber = group.RaceNumber });
            }

            ExcelManager.CreateResultExcelDocument(tables);
        }

        #region Работа с таблицами результатов участников

        /// <summary>
        /// Инициализация всех таблиц.
        /// </summary>
        private void InitializeAllTables()
        {
            MainView.FwdFirstRace = InitializeTable();
            MainView.FwdSecondRace = InitializeTable();
            MainView.RwdFirstRace = InitializeTable();
            MainView.RwdSecondRace = InitializeTable();
            MainView.AwdFirstRace = InitializeTable();
            MainView.AwdSecondRace = InitializeTable();
            MainView.SportFirstRace = InitializeTable();
            MainView.SportSecondRace = InitializeTable();
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
            MainView.FwdFirstRace = SetNamesToTable(CarClassesEnum.FWD);
            MainView.RwdFirstRace = SetNamesToTable(CarClassesEnum.RWD);
            MainView.AwdFirstRace = SetNamesToTable(CarClassesEnum.AWD);
            MainView.SportFirstRace = SetNamesToTable(CarClassesEnum.Sport);
        }

        /// <summary>
        /// Заполним таблицу именами из заданной группы участников в списке групп.
        /// </summary>
        /// <param name="carClass">Класс группы автомобилей.</param>
        /// <returns>Таблица, заполненная участниками.</returns>
        private DataTable SetNamesToTable(CarClassesEnum carClass)
        {
            var table = InitializeTable();

            var group = RacerGroups.FirstOrDefault(gr => gr.CarClass == carClass);

            if (group == null)
            {
                return table;
            }

            foreach (var racer in group.Racers)
            {
                var row = table.NewRow();
                row[0] = racer.RacerNumber;
                row[1] = string.Format("{0} {1} {2}", racer.FirstName, racer.LastName, racer.MiddleName);
                row[2] = racer.Car.Name;
                table.Rows.Add(row);
            }

            return table;
        } 

        #endregion

        #region Перемещение гонщиков по списку участников

        /// <summary>
        /// Передвинуть участника вверх на одну позицию.
        /// </summary>
        /// <param name="racerNum">Номер перемещаемого участника.</param>
        public bool MoveUpRacer(int racerNum)
        {
            var racerGroup = RacerGroups.FirstOrDefault(rg => rg.CarClass == CurrentEditCarClass && rg.RaceNumber == CurrentEditRaceNumber);
            var racer = racerGroup.Racers.ElementAt(racerNum);

            if (racer == null || !CheckMoveUp(racer, CurrentEditCarClass, CurrentEditRaceNumber))
            {
                return false;
            }

            racerGroup.MoveUpRacer(racerNum);
            Racers = racerGroup.GlobalRacers;

            DataBind();

            return true;
        }

        /// <summary>
        /// Передвинуть участника вниз на одну позицию.
        /// </summary>
        /// <param name="racerNum">Номер перемещаемого участника.</param>
        public bool MoveDownRacer(int racerNum)
        {
            var racerGroup = RacerGroups.FirstOrDefault(rg => rg.CarClass == CurrentEditCarClass && rg.RaceNumber == CurrentEditRaceNumber);
            var racer = racerGroup.Racers.ElementAt(racerNum);

            if (racer == null || !CheckMoveDown(racer, CurrentEditCarClass, CurrentEditRaceNumber))
            {
                return false;
            }

            racerGroup.MoveDownRacer(racerNum);
            Racers = racerGroup.GlobalRacers;

            DataBind();

            return true;
        }

        /// <summary>
        /// Проверить можно-ли передвинуть гонщика на одну позицию вверх по списку участников.
        /// </summary>
        /// <param name="racer">Перемещаемый гонщик.</param>
        /// <param name="carClass">Номер перемещаемого участника.</param>
        /// <param name="raceNum">Номер заезда внутри которого мы двигаем гонщика.</param>
        /// <returns></returns>
        private bool CheckMoveUp(RacerModel racer, CarClassesEnum carClass, int raceNum)
        {
            // Посмторим, а вообще присутствуют-ли сейчас на трассе участники?
            if (!Track.CurrentRacers.Any())
            {
                return true;
            }

            // Посмотрим, перемещаемый гонщик не на трассе-ли случаянно находится?
            foreach (var r in Track.CurrentRacers)
            {
                if (racer.Id == r.Id)
                {
                    return false;
                }
            }

            // Посмотрим, вышестоящий гонщик не на трассе-ли случаянно находится?
            var raserGroup = RacerGroups.FirstOrDefault(rg => rg.CarClass == carClass && rg.RaceNumber == raceNum);
            var currentRacer = raserGroup.Racers.FirstOrDefault(r => r.Id == racer.Id);
            var currentRacerNumber = raserGroup.Racers.IndexOf(currentRacer);
            var upper_racer = raserGroup.Racers.ElementAt(currentRacerNumber - 1);

            foreach (var r in Track.CurrentRacers)
            {
                if (upper_racer.Id == r.Id)
                {
                    return false;
                }
            }

            // Посмотрим, а не пытаемся-ли мы передвинуть уже проехавшего заезд гонщика?
            if (racer.Results.ResultsList.ElementAt(raceNum - 1).Where(r => r == null).Count() < MaxCircleCount)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверить можно-ли передвинуть гонщика на одну позицию вниз по списку участников.
        /// </summary>
        /// <param name="racer">Перемещаемый гонщик.</param>
        /// <param name="carClass">Номер перемещаемого участника.</param>
        /// <param name="raceNum">Номер заезда внутри которого мы двигаем гонщика.</param>
        /// <returns></returns>
        private bool CheckMoveDown(RacerModel racer, CarClassesEnum carClass, int raceNum)
        {
            // Посмторим, а вообще присутствуют-ли сейчас на трассе участники?
            if (!Track.CurrentRacers.Any())
            {
                return true;
            }

            // Посмотрим, перемещаемый гонщик не на трассе-ли случаянно находится?
            foreach (var r in Track.CurrentRacers)
            {
                if (racer.Id == r.Id)
                {
                    return false;
                }
            }

            // Посмотрим, а не пытаемся-ли мы передвинуть уже проехавшего заезд гонщика?
            if (racer.Results.ResultsList.ElementAt(raceNum - 1).Where(r => r == null).Count() < MaxCircleCount)
            {
                return false;
            }

            return true;
        } 

        #endregion

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
                            OutFwdResultForGroup(group);
                        } break;

                    case CarClassesEnum.RWD:
                        {
                            OutRwdResultForGroup(group);
                        } break;

                    case CarClassesEnum.AWD:
                        {
                            OutAwdResultForGroup(group);
                        } break;

                    case CarClassesEnum.Sport:
                        {
                            OutSportResultForGroup(group);
                        } break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// Вывести результаты на форму у переднеприводного класса автомобилей.
        /// </summary>
        /// <param name="group">Переднеприводная группа гонщиков.</param>
        private void OutFwdResultForGroup(RacersGroupModel group)
        {
            switch (group.RaceNumber)
            {
                case 1:
                    {
                        MainView.FwdFirstRace = GetTableWithResults(group, 1);
                    } break;
                case 2:
                    {
                        MainView.FwdSecondRace = GetTableWithResults(group, 2);
                    } break;
            }
        }

        /// <summary>
        /// Вывести результаты на форму у заднеприводного класса автомобилей.
        /// </summary>
        /// <param name="group">Заднеприводная группа гонщиков.</param>
        private void OutRwdResultForGroup(RacersGroupModel group)
        {
            switch (group.RaceNumber)
            {
                case 1:
                    {
                        MainView.RwdFirstRace = GetTableWithResults(group, 1);
                    } break;
                case 2:
                    {
                        MainView.RwdSecondRace = GetTableWithResults(group, 2);
                    } break;
            }            
        }

        /// <summary>
        /// Вывести результаты на форму у полноприводного класса автомобилей.
        /// </summary>
        /// <param name="group">Полноприводная группа гонщиков.</param>
        private void OutAwdResultForGroup(RacersGroupModel group)
        {
            switch (group.RaceNumber)
            {
                case 1:
                    {
                        MainView.AwdFirstRace = GetTableWithResults(group, 1);
                    } break;
                case 2:
                    {
                        MainView.AwdSecondRace = GetTableWithResults(group, 2);
                    } break;
            }
        }

        /// <summary>
        /// Вывести результаты на форму у спортивного класса автомобилей.
        /// </summary>
        /// <param name="group">Спортивная группа гонщиков.</param>
        private void OutSportResultForGroup(RacersGroupModel group)
        {
            switch (group.RaceNumber)
            {
                case 1:
                    {
                        MainView.SportFirstRace = GetTableWithResults(group, 1);
                    } break;
                case 2:
                    {
                        MainView.SportSecondRace = GetTableWithResults(group, 2);
                    } break;
            }
        }

        /// <summary>
        /// Получить таблицу с результатами участников заданной группы заданного заезда.
        /// </summary>
        /// <param name="group">
        /// Класс автомобилей, для участников которых будет получена таблица с результатами заданной группы заездов.
        /// </param>
        /// <param name="raceNumber">Номер заезда, результаты которого требуется получить.</param>
        /// <returns>Таблица с результатами.</returns>
        private DataTable GetTableWithResults(RacersGroupModel group, int raceNumber)
        {
            var table = SetNamesToTable(group.CarClass);

            for (int row = 0; row < group.Racers.Count(); row++)
            {
                var racer = group.Racers.ElementAt(row);

                switch (raceNumber)
                {
                    case 1:
                        {
                            if (racer.Results.ResultsList.ElementAt(0) == null)
                            {
                                return table;
                            }

                            for (int circle = 0; circle < MaxCircleCount; circle++)
                            {
                                var time = racer.Results.ResultsList.ElementAt(0).ElementAt(circle);

                                if (time != null)
                                {
                                    table.Rows[row][circle + 3] = string.Format("{0} : {1} : {2}", time.Min.ToString("00"),
                                                                                                   time.Sec.ToString("00"),
                                                                                                   time.Mlsec.ToString("000"));
                                }
                            }
                        } break;
                    case 2:
                        {
                            break;
                            throw new NotImplementedException();

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
                        } break;
                }
            }

            return table;
        }

        #endregion

        #region IDisposable Members
        
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

        #endregion
    }
}
