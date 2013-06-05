using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;

using Sprint.Extensions;
using Sprint.Interfaces;
using Sprint.Managers;
using Sprint.Models;
using Sprint.Views;

namespace Sprint.Presenters
{
    /// <summary>
    /// Презнетер главного окна.
    /// </summary>
    partial class MainPresenter : IDisposable
    {
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
        /// Задать или получить список гонщиков.
        /// </summary>
        public IEnumerable<RacerModel> Racers
        {
            get
            {
                return _racers;
            }
            private set
            {
                _racers = value;

                foreach (var racer in Racers)
                {
                    if (racer.Results == null)
                    {
                        racer.Results = new ResultsModel();
                    }
                    var racer_group = RacerGroups.FirstOrDefault(rg => rg.CarClass == racer.Car.CarClass);
                    racer_group.AddRacer(racer);
                }
            }
        }

        /// <summary>
        /// Задать или получить список групп гонщиков по классам автомобилей.
        /// </summary>
        public IEnumerable<RacersGroupModel> RacerGroups { get; set; }

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
        public int? CurrentRaceNum { get; private set; }

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

        /// <summary>
        /// Задать или получить опции гонок по классам автомобилей.
        /// </summary>
        private IEnumerable<RaceOptionsModel> RaceOptions { get; set; }

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
            Track               = new TrackModel(ConstantsModel.MaxRacersForRace);
            RacerGroups = new List<RacersGroupModel>
                                    {
                                        new RacersGroupModel(CarClassesEnum.FWD)    { RaceNumber = 0 },
                                        new RacersGroupModel(CarClassesEnum.RWD)    { RaceNumber = 0 },
                                        new RacersGroupModel(CarClassesEnum.AWD)    { RaceNumber = 0 },
                                        new RacersGroupModel(CarClassesEnum.Sport)  { RaceNumber = 0 },
                                        new RacersGroupModel(CarClassesEnum.K100)   { RaceNumber = 0 },
                                        new RacersGroupModel(CarClassesEnum.K160)   { RaceNumber = 0 },
                                        new RacersGroupModel(CarClassesEnum.KA)     { RaceNumber = 0 },
                                    };

            InitializeAllTables();

            ThreadSync = new Thread(() => StopwatchDataBindingProcess(MainView, Stopwatch));

            // Попробуем  загрузить гонщиков из бд
            var racers = RacersDbManager.GetRacers();

            if (racers.Any())
            {
                var sm = new ScreenManager();

                var reg_dlg = new RegenerationDialogView();
                var center_screen = new Point(sm.Screens[0].WorkingArea.Width / 2, sm.Screens[0].WorkingArea.Height / 2);
                var center_window = new Point(reg_dlg.Width / 2, reg_dlg.Height / 2);
                reg_dlg.SetDesktopLocation(sm.ScreenPoints[0].X + (int)center_screen.X - (int)center_window.X, 
                                           sm.ScreenPoints[0].Y + (int)center_screen.Y - (int)center_window.Y);

                var app_state = ApplicationStateDbManager.GetApplicationState();

                reg_dlg.ShowDialog();

                switch (((IRegenerationDialogView)reg_dlg).SelectedAppRegenerationType)
                {
                    case AppRegenerationTypesEnum.AllLapReRun:                              // Текущие участники на треке перезаезжают полностью свои заезды
                        {
                            ClearAllResultsForRacersAtTheTrack(racers, app_state);
                            SetApplicationState(app_state);
                            Racers = racers;
                            DataBind();
                        } break;
                    case AppRegenerationTypesEnum.NullLapReRun:                             // Участники на треке перезаезжают только неудачные круги
                        {
                            SetApplicationState(app_state);
                            Racers = racers;
                            DataBind();
                        } break;
                    case AppRegenerationTypesEnum.LoadData:                                 // Просто загрузить всех участников с их результатами
                        {
                            SetApplicationState(app_state);
                            Racers = racers;
                            DataBind();
                        } break;
                }
            }
        }        

        #endregion

        #region Методы

        /// <summary>
        /// Задать состояние приложения.
        /// </summary>
        /// <param name="app_state">Состояние приложения.</param>
        private void SetApplicationState(ApplicationStateModel app_state)
        {
            CurrentRaceNum = app_state.CurrentRaceNumber;

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Удалить все результаты у участников, которые находятся на треке в данный момент времени.
        /// </summary>
        /// <param name="racers">Список вссех участников в соревнованиях.</param>
        /// <param name="app_state">Текущее состояние приложения.</param>
        private static void ClearAllResultsForRacersAtTheTrack(IEnumerable<RacerModel> racers, ApplicationStateModel app_state)
        {
            if (app_state != null)
            {
                var racers_at_the_track = app_state.RacersAtTheTrack;

                if (racers_at_the_track != null)
                {
                    foreach (var racer_id in racers_at_the_track)
                    {
                        var racer = racers.FirstOrDefault(r => r.Id == racer_id);

                        if (racer != null)
                        {
                            racer.Results.Clear();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Задать в приложении новых гонщиков.
        /// </summary>
        public void SetRacersFromNewRacersDialog(IEnumerable<RacerModel> addedRacers)
        {
            Racers = addedRacers;           
            SetRacersForTableForFirstRace();
        }

        /// <summary>
        /// Добавить в приложение новых гонщиков.
        /// </summary>
        /// <param name="adding_racers">Список добавляемых участников.</param>
        public void AddRacerAddNewRacer(IEnumerable<RacerModel> adding_racers)
        {
            if (adding_racers == null)
            {
                return;
            }

            foreach(var racer in adding_racers)
            {
                if (racer.Results == null)
                {
                    racer.Results = new ResultsModel();
                }

                var racer_group = RacerGroups.FirstOrDefault(rg => rg.CarClass == racer.Car.CarClass);
                racer_group.AddRacer(racer);
            }
            DataBind();
        }

        /// <summary>
        /// Запустить секундомер.
        /// </summary>
        public void StartStopwatch()
        {
            CurrentRaceNum = CurrentRaserGroup.RaceNumber;

            if (CurrentRaserGroup.Racers.Any())
            {
                var racer = CurrentRaserGroup.GetNextRacer(Track.CurrentRacer);
                MainView.NextCurrentRacer = racer == null ? 0 : racer.RacerNumber;
            }

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

            MainView.StopwatchState = StopwatchStatesEnum.Stop;
        }

        /// <summary>
        /// Произвести отсечку времени и добавить результат гонщику.
        /// </summary>
        public void CutOffStopwatch()
        {
            if (!CurrentRaserGroup.Racers.Any() || Track.CurrentRacers as List<RacerModel> == null)
            {
                return;
            }

            // Определимся какому участнику мы сейчас будем отрабатывать отсечку
            if (Track.CurrentRacer != null && Track.CurrentRacers.Count() == 2 && Track.CurrentRacerNum == 0 && Track.CurrentRacer.Results.CurrentCircleNumber >= 3)
            {
                Track.CurrentRacerNum = 1;
            }
            else
            {
                Track.CurrentRacerNum = 0;
            }

            var time = new TimeModel(Stopwatch.Time.TimeSpan);
            var racer = CurrentRaserGroup.GetNextRacer(Track.CurrentRacer);

            // Если трек пустой, то добавить на него одного гонщика
            if (!Track.CurrentRacers.Any())
            {
                racer.Results.StartTime = time;
                (Track.CurrentRacers as List<RacerModel>).Add(racer);
                MainView.FirstCurrentRacer = racer.RacerNumber;
                racer = SetNextRacerInfo(racer);
            } 
            // Если на треке уже есть участник и он проезжает уже 2 круг, то добавим еще одного участника
            else if (Track.CurrentRacer != null && Track.CurrentRacer.Results.CurrentCircleNumber == 2 && Track.CurrentRacers.Count() == 1 && racer != null)
            {
                racer.Results.StartTime = time;
                (Track.CurrentRacers as List<RacerModel>).Add(racer);
                MainView.SecondCurrentRacer = racer.RacerNumber;
                racer = SetNextRacerInfo(racer);
            }
            // Запишем текущему автомобилю новое время, если он завершает круг
            else if (Track.CurrentRacer != null && Track.CurrentRacer.Results.StartTime != null && CurrentRaceNum.HasValue)
            {
                var res = new TimeModel(time.TimeSpan - Track.CurrentRacer.Results.StartTime.TimeSpan);
                Track.CurrentRacer.Results.AddResult(CurrentRaceNum.Value, res);
                Track.CurrentRacer.Results.StartTime = time;
                RacersDbManager.SetRacer(Track.CurrentRacer);
            }

            // Если автомобиль финишировал, то убираем его с трека
            if (Track.CurrentRacer != null && Track.CurrentRacer.Results.Finished)
            {
                var list = new List<RacerModel>(Track.CurrentRacers);
                list.Remove(Track.CurrentRacer);
                Track.CurrentRacers = list;

                MainView.FirstCurrentRacer = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                MainView.SecondCurrentRacer = 0;
            }                 

            // Закрытие неудачных заездов

            // Поиск повторных заездов
            
            // Переход к следующему классу автомобилей

            SaveApplicationState();
            DataBind();

            if (CheckCurrentGroupFinishedRace())
            {
                SaveApplicationState();
                StopStopwatch();
                return;
            }
        }

        /// <summary>
        /// Задать информацию о следующем участнике, готовящемся для выхода на трассу.
        /// </summary>
        /// <param name="last_racer">Текущий участник, который последний вышел на трассу.</param>
        /// <returns></returns>
        private RacerModel SetNextRacerInfo(RacerModel last_racer)
        {
            last_racer = CurrentRaserGroup.GetNextRacer(last_racer);
            MainView.NextCurrentRacer = last_racer == null ? 0 : last_racer.RacerNumber;
            return last_racer;
        }

        /// <summary>
        /// Сохраним состояние приложения.
        /// </summary>
        private void SaveApplicationState()
        {
            ApplicationStateDbManager.SetApplicationState(new ApplicationStateModel
                                                                {
                                                                    CurrentCarClass = CurrentCarClass,
                                                                    CurrentRacer = Track.CurrentRacer == null ? (Guid?) null : Track.CurrentRacer.Id,
                                                                    RacersAtTheTrack = Track.CurrentRacers.Select(r => r.Id)
                                                                });
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

            return curCarGroup.Racers.All(racer => racer.Results.Finished);
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

            if (LiderRacerGroups as List<RacersGroupModel> != null)
            {
                (LiderRacerGroups as List<RacersGroupModel>).Add(new RacersGroupModel(carClass)
                                                                        {
                                                                            Racers = rg.GetLeaders(count)
                                                                        });
            }
        }

        /// <summary>
        /// Экспорт всех данных в Excel.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public OperationResult ExportToExcel()
        {
            var tables = new List<TableWithResults>();

            foreach (var group in RacerGroups)
            {
                if (group.Racers.Any())
                {
                    tables.Add(new TableWithResults
                                    {
                                        Results = GetTableWithResults(group, group.RaceNumber),
                                        CarClass = group.CarClass,
                                        RaceNumber = group.RaceNumber
                                    });
                }
            }

            return ExcelManager.CreateResultExcelDocument(tables);
        }

        /// <summary>
        /// Удалить все логи программы в БД.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public OperationResult DeleteLogs()
        {
            return LogsDbManager.DeleteLogs();
        }

        /// <summary>
        /// Удалить все данные программы в БД.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public OperationResult DeleteData()
        {
            var res_1 = ApplicationStateDbManager.DeleteApplicationState();
            var res_2 = RacersDbManager.DeleteRacers();
            var res_3 = OptionsDbManager.DeleteAllOptions();

            if (!res_1.Result)
            {
                return res_1;
            }

            if (!res_2.Result)
            {
                return res_2;
            }

            if (!res_3.Result)
            {
                return res_3;
            }

            return new OperationResult(true);
        }

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
            var res = false;

            // Посмторим, а вообще присутствуют-ли сейчас на трассе участники?
            if (!Track.CurrentRacers.Any())
            {
                res = true;
            }

            // Посмотрим, а не первым-ли в списке находится перемещаемый участник
            var raserGroup = RacerGroups.FirstOrDefault(rg => rg.CarClass == carClass && rg.RaceNumber == raceNum);
            var currentRacer = raserGroup.Racers.FirstOrDefault(r => r.Id == racer.Id);
            var currentRacerNumber = raserGroup.Racers.IndexOf(currentRacer);

            if (currentRacerNumber == 0)
            {
                res = false;
            }

            // Посмотрим, перемещаемый гонщик не на трассе-ли случаянно находится?
            if (Track.CurrentRacers.Any() && currentRacerNumber > 0)
            {
                foreach (var r in Track.CurrentRacers)
                {
                    if (racer.Id == r.Id)
                    {
                        res = false;
                    }
                }

                // Посмотрим, вышестоящий гонщик не на трассе-ли случаянно находится?
                var upper_racer = raserGroup.Racers.ElementAt(currentRacerNumber - 1);

                foreach (var r in Track.CurrentRacers)
                {
                    if (upper_racer.Id == r.Id)
                    {
                        res = false;
                    }
                }

                // Посмотрим, а не пытаемся-ли мы передвинуть уже проехавшего заезд гонщика?
                if (racer.Results.ResultsList.ElementAt(raceNum - 1).Count(r => r == null) < ConstantsModel.MaxCircleCount)
                {
                    res = false;
                }

            }

            return res;
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
            var res = false;

            // Посмторим, а вообще присутствуют-ли сейчас на трассе участники?
            if (!Track.CurrentRacers.Any())
            {
                res = true;
            }

            // Посмотрим, а не последним-ли в списке находится перемещаемый участник
            var raserGroup = RacerGroups.FirstOrDefault(rg => rg.CarClass == carClass && rg.RaceNumber == raceNum);
            var currentRacer = raserGroup.Racers.FirstOrDefault(r => r.Id == racer.Id);
            var currentRacerNumber = raserGroup.Racers.IndexOf(currentRacer);

            if (currentRacerNumber == raserGroup.Racers.Count() - 1)
            {
                res = false;
            }

            // Посмотрим, перемещаемый гонщик не на трассе-ли случаянно находится?
            if (Track.CurrentRacers.Any() && currentRacerNumber < raserGroup.Racers.Count() - 1)
            {
                foreach (var r in Track.CurrentRacers)
                {
                    if (racer.Id == r.Id)
                    {
                        res = false;
                    }
                }

                // Посмотрим, а не пытаемся-ли мы передвинуть уже проехавшего заезд гонщика?
                if (racer.Results.ResultsList.ElementAt(raceNum - 1).Count(r => r == null) < ConstantsModel.MaxCircleCount)
                {
                    res = false;
                }
            }

            return res;
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
