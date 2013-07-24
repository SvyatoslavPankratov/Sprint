using System;
using System.Collections.Generic;
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
        #region Свойства

        /// <summary>
        /// Задать или получить интерфейс на главную форму.
        /// </summary>
        private IMain MainView { get; set; }

        /// <summary>
        /// Задать или получить интерфейс на второй экран.
        /// </summary>
        private ISecondMonitor SecondView { get; set; }

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
        public IEnumerable<RacerModel> Racers { get;private set; }

        /// <summary>
        /// Задать или получить список групп гонщиков по классам автомобилей.
        /// </summary>
        public IEnumerable<RacersGroupModel> RacerGroups { get; set; }

        /// <summary>
        /// Получить текущий класс автомобилей на трассе.
        /// </summary>
        public RacersGroupModel CurrentRaserGroup
        {
            get { return RacerGroups.FirstOrDefault(rg => rg.CarClass == CurrentCarClass && rg.RaceNumber == CurrentRaceNum); }
        }

        /// <summary>
        /// Задать или получить текущий номер заезда (начиная с 0).
        /// </summary>
        public int? CurrentRaceNum { get; set; }

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
        /// <param name="secondView">Интерфейс на второй экран.</param>
        public MainPresenter(IMain mainView, ISecondMonitor secondView)
        {
            MainView = mainView;
            SecondView = secondView;

            Racers              = new List<RacerModel>();
            Stopwatch           = new StopwatchModel();
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
                                        new RacersGroupModel(CarClassesEnum.FWD)    { RaceNumber = 1 },
                                        new RacersGroupModel(CarClassesEnum.RWD)    { RaceNumber = 1 },
                                        new RacersGroupModel(CarClassesEnum.AWD)    { RaceNumber = 1 },
                                        new RacersGroupModel(CarClassesEnum.Sport)  { RaceNumber = 1 },
                                        new RacersGroupModel(CarClassesEnum.K100)   { RaceNumber = 1 },
                                        new RacersGroupModel(CarClassesEnum.K160)   { RaceNumber = 1 },
                                        new RacersGroupModel(CarClassesEnum.KA)     { RaceNumber = 1 }
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

                reg_dlg.ShowDialog();

                var app_state = ApplicationStateDbManager.GetApplicationState();

                DatabaseBackupManager.CreateNewDatabaseBackup();

                switch (((IRegenerationDialog)reg_dlg).SelectedAppRegenerationType)
                {
                    case AppRegenerationTypesEnum.AllLapReRun:                              // Текущие участники на треке перезаезжают полностью свои заезды
                        {
                            SetApplicationState(app_state);
                            ClearAllResultsForRacersAtTheTrack(racers, app_state);
                            SetRacers(racers, app_state);
                            DataBind();
                        } break;
                    case AppRegenerationTypesEnum.LastLapReRun:                             // Участники на треке перезаезжают только последние круги, при проезде которых произошел сбой системы
                        {
                            SetApplicationState(app_state);
                            SetRacers(racers, app_state);
                            DataBind();
                        } break;
                    case AppRegenerationTypesEnum.LoadData:                                 // Просто загрузить всех участников с их результатами
                        {
                            SetApplicationState(app_state);
                            SetRacers(racers, app_state);
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
            if (app_state != null)
            {
                CurrentRaceNum = app_state.CurrentRaceNumber;
            }
        }

        /// <summary>
        ///  Восстановить всех участников во всех заездах во всех классах автомобилей.
        /// </summary>
        /// <param name="racers"></param>
        /// <param name="app_state"></param>
        private OperationResult SetRacers(IEnumerable<RacerModel> racers, ApplicationStateModel app_state)
        {
            if (app_state != null && app_state.RaceStates != null && racers != null)
            {
                Racers = racers;

                foreach (var rs in app_state.RaceStates)
                {
                    var racer_group = (from rg in RacerGroups
                                       where rg.CarClass == rs.CarClass
                                             && rg.RaceNumber == rs.RaceNumber
                                       select rg).FirstOrDefault();

                    if (racer_group != null)
                    {
                        foreach (var id_racer in rs.Racers)
                        {
                            var racer = Racers.FirstOrDefault(r => r.Id == id_racer);

                            if (racer != null)
                            {
                                racer_group.AddRacer(racer);
                            }
                            else
                            {
                                return new OperationResult(false);
                            }
                        }
                    }
                    else
                    {
                        return new OperationResult(false);
                    }
                }

                return new OperationResult(true);
            }

            return new OperationResult(false);
        }

        /// <summary>
        ///  Задать участников во всех заездах во всех классах автомобилей, если они впервые проходят регистрации в текущем сеансе гонок.
        /// </summary>
        /// <param name="racers"></param>
        private void SetRacers(IEnumerable<RacerModel> racers)
        {
            if (racers != null && racers.Any())
            {
                Racers = racers;

                foreach (var racer in Racers)
                {
                    if (racer.Results == null)
                    {
                        racer.Results = new ResultsModel();
                    }

                    var racer_group = RacerGroups.FirstOrDefault(rg => rg.CarClass == racer.Car.CarClass && rg.RaceNumber == 0);
                    racer_group.AddRacer(racer);
                }
            }
        }

        /// <summary>
        /// Удалить все последние круги у участников, которые находятся на треке в данный момент времени.
        /// </summary>
        /// <param name="racers">Список вссех участников в соревнованиях.</param>
        /// <param name="app_state">Текущее состояние приложения.</param>
        private void ClearLastResultsForRacersAtTheTrack(IEnumerable<RacerModel> racers, ApplicationStateModel app_state)
        {
            if (app_state != null)
            {
                var racers_at_the_track = app_state.RacersAtTheTrack;

                if (racers_at_the_track != null)
                {
                    foreach (var racer_id in racers_at_the_track)
                    {
                        var racer = racers.FirstOrDefault(r => r.Id == racer_id);

                        if (racer != null && CurrentRaceNum.HasValue)
                        {
                            racer.Results.DeleteLastResult(CurrentRaceNum.Value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Удалить все результаты у участников, которые находятся на треке в данный момент времени.
        /// </summary>
        /// <param name="racers">Список вссех участников в соревнованиях.</param>
        /// <param name="app_state">Текущее состояние приложения.</param>
        private void ClearAllResultsForRacersAtTheTrack(IEnumerable<RacerModel> racers, ApplicationStateModel app_state)
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
        /// Задать новых гонщиков.
        /// </summary>
        public void SetRacersFromNewRacersDialog(IEnumerable<RacerModel> addedRacers)
        {
            SetRacers(addedRacers);
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

                if (SecondView != null)
                {
                    SecondView.NextRacerNumber = racer == null ? 0 : racer.RacerNumber;
                }
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
            if (Track.CurrentRacer != null 
                && Track.CurrentRacers.Count() == 2 
                && Track.CurrentRacerNum == 0 
                && Track.CurrentRacer.Results.GetCurrentCircleNumber(CurrentRaceNum.Value) >= 3)
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

                if (SecondView != null)
                {
                    SecondView.FirstCurrentRacerNumber = racer.RacerNumber;
                }

                racer = SetNextRacerInfo(racer);
            } 
            // Если на треке уже есть участник и он проезжает уже 2 круг, то добавим еще одного участника
            else if (Track.CurrentRacer != null 
                     && Track.CurrentRacer.Results.GetCurrentCircleNumber(CurrentRaceNum.Value) == 2 
                     && Track.CurrentRacers.Count() == 1 && racer != null)
            {
                racer.Results.StartTime = time;
                (Track.CurrentRacers as List<RacerModel>).Add(racer);
                MainView.SecondCurrentRacer = racer.RacerNumber;

                if (SecondView != null)
                {
                    SecondView.SecondCurrentRacerNumber = racer.RacerNumber;
                }

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
            if (Track.CurrentRacer != null && Track.CurrentRacer.Results.IsFinished(CurrentRaceNum.Value))
            {
                var list = new List<RacerModel>(Track.CurrentRacers);
                list.Remove(Track.CurrentRacer);
                Track.CurrentRacers = list;

                MainView.FirstCurrentRacer = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                MainView.SecondCurrentRacer = 0;

                if (SecondView != null)
                {
                    SecondView.FirstCurrentRacerNumber = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                    SecondView.SecondCurrentRacerNumber = 0;
                }
            }

            // Закрытие неудачных заездов

            // Поиск повторных заездов
            
            // Если заезд завершен, то закрываем текущей заезд у текущего класса автомобилей 
            if (CheckCurrentGroupFinishedRace())
            {
                StopStopwatch();

                if (CurrentRaceNum.Value == 0)
                {
                    SetLidersForSecondRound(CurrentCarClass);
                    SetRacersState(CurrentCarClass, CurrentRaceNum.Value + 1);
                }
            }

            SaveApplicationState();
            DataBind();
            SetRacerResultsToSecondView();
        }

        /// <summary>
        /// Зададим состояние заезда для заданного класса автомобилей и заданного заезда.
        /// </summary>
        /// <param name="carClass">Заданный класс автомобилей.</param>
        /// <param name="race_number">Заданный номер заезда.</param>
        private void SetRacersState(CarClassesEnum carClass, int race_number)
        {
            var crg = (from rg in RacerGroups
                       where rg.CarClass == carClass
                             && rg.RaceNumber == race_number
                       select rg).FirstOrDefault();

            if (crg != null)
            {
                var rs = new RaceStateModel(carClass, race_number);
                rs.AddRacers(crg.Racers);
                RaceStateDbManager.SetRaceStates(rs);
            }
        }

        /// <summary>
        /// Вывести результаты всех участников на трассе на второй экран.
        /// </summary>
        private void SetRacerResultsToSecondView()
        {
            if (SecondView == null || !CurrentRaceNum.HasValue)
            {
                return;
            }

            switch (Track.CurrentRacers.Count())
            {
                case 1:
                    {
                        OutFirstRacerResultsToSecondView();
                        ResetSecondRacerResultsToSecondView();
                    }
                    break;
                case 2:
                    {
                        OutFirstRacerResultsToSecondView();
                        OutSecondRacerResultsToSecondView();
                    }
                    break;
                default:
                    {
                        ResetFirstRacerResultsToSecondView();
                        ResetSecondRacerResultsToSecondView();
                    }
                    break;
            }
        }

        /// <summary>
        /// Вывести результаты первого участника на второй экран.
        /// </summary>
        private void OutFirstRacerResultsToSecondView()
        {
            if (!CurrentRaceNum.HasValue)
            {
                return;
            }

            var racer_first = Track.CurrentRacers.ElementAt(0);

            if (racer_first != null)
            {
                SecondView.FirstRacer1Time = racer_first.Results.ResultsList.ElementAt(CurrentRaceNum.Value).ElementAt(1);
                SecondView.FirstRacer2Time = racer_first.Results.ResultsList.ElementAt(CurrentRaceNum.Value).ElementAt(2);
                SecondView.FirstRacer3Time = racer_first.Results.ResultsList.ElementAt(CurrentRaceNum.Value).ElementAt(3);
            }
            else
            {
                ResetFirstRacerResultsToSecondView();
            }
        }
        
        /// <summary>
        /// Вывести результаты второго участника на второй экран.
        /// </summary>
        private void OutSecondRacerResultsToSecondView()
        {
            if (!CurrentRaceNum.HasValue)
            {
                return;
            }

            var racer_second = Track.CurrentRacers.ElementAt(1);

            if (racer_second != null)
            {
                SecondView.SecondRacer1Time = racer_second.Results.ResultsList.ElementAt(CurrentRaceNum.Value).ElementAt(1);
                SecondView.SecondRacer2Time = racer_second.Results.ResultsList.ElementAt(CurrentRaceNum.Value).ElementAt(2);
                SecondView.SecondRacer3Time = racer_second.Results.ResultsList.ElementAt(CurrentRaceNum.Value).ElementAt(3);
            }
            else
            {
                ResetSecondRacerResultsToSecondView();
            }
        }

        /// <summary>
        /// Сбросить результаты первого участника на втором экране.
        /// </summary>
        private void ResetFirstRacerResultsToSecondView()
        {
            SecondView.FirstRacer1Time = null;
            SecondView.FirstRacer2Time = null;
            SecondView.FirstRacer3Time = null;
        }

        /// <summary>
        /// Сбросить результаты второго участника на втором экране.
        /// </summary>
        private void ResetSecondRacerResultsToSecondView()
        {
            SecondView.SecondRacer1Time = null;
            SecondView.SecondRacer2Time = null;
            SecondView.SecondRacer3Time = null;
        }

        /// <summary>
        /// Определить список финалистов в заданной группе и вывести их в список участников 
        /// второго тура текущего класса атомобилей.
        /// </summary>
        /// <param name="carClass">Заданный класс автомобилей.</param>
        /// <param name="race_number">Заданный номер заезда.</param>
        private void SetLidersForSecondRound(CarClassesEnum carClass)
        {
            var racer_group = RacerGroups.FirstOrDefault(g => g.CarClass == carClass && g.RaceNumber == 0);

            if (racer_group == null)
            {
                return;
            }

            var options = OptionsDbManager.GetOptions(carClass);
            var race_count = options == null ? 2 : options.RaceCount;
            
            if (race_count == 2)
            {
                var liders_count = options == null ? 5 : options.LidersCount;
                var liders = racer_group.GetLeaders(liders_count);

                if (liders != null)
                {
                    var new_racer_groups = RacerGroups.FirstOrDefault(g => g.CarClass == carClass && g.RaceNumber == 1);
                    new_racer_groups.Racers = liders;
                }
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

            if (SecondView != null)
            {
                SecondView.NextRacerNumber = last_racer == null ? 0 : last_racer.RacerNumber;
            }

            return last_racer;
        }

        /// <summary>
        /// Сохраним состояние приложения.
        /// </summary>
        private void SaveApplicationState()
        {
            ApplicationStateDbManager.SetApplicationState(new ApplicationStateModel
                                                                {
                                                                    CurrentRaceNumber = CurrentRaceNum,
                                                                    CurrentCarClass = CurrentCarClass,
                                                                    CurrentRacer = Track.CurrentRacer == null ? (Guid?) null : Track.CurrentRacer.Id,
                                                                    RacersAtTheTrack = Track.CurrentRacers.Select(r => r.Id)
                                                                });
        }

        /// <summary>
        /// Процесс, осуществляющий постоянный вывод данных секундомера на форму.
        /// </summary>
        /// <param name="mainView"></param>
        private void StopwatchDataBindingProcess(IMain mainView, StopwatchModel stopwatch)
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

            return curCarGroup.Racers.All(racer => racer.Results.IsFinished(CurrentRaceNum.Value));
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
            var res_2 = RaceStateDbManager.DeleteAllStates();
            var res_3 = RacersDbManager.DeleteRacers();
            var res_4 = OptionsDbManager.DeleteAllOptions();

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

            if (!res_4.Result)
            {
                return res_4;
            }

            return new OperationResult(true);
        }

        /// <summary>
        /// Перезаезд текущего круга вместе с не закрытым заездом для заданного участника.
        /// </summary>
        /// <param name="racer">Участник, для которого будет перезаезд проезжаемого круга.</param>
        public void SetNullResultForCurrenrRacer(RacerModel racer)
        {
            // Проверим на треке-ли находится участник

            // Если участник готовится к заезду, то его круги уже нельзя обнулить

            // Если участник уже проехал заезд, то его круги уже нельзя обнулить
        }

        /// <summary>
        /// Закрытие текущего заезда для заданного участника.
        /// </summary>
        /// <param name="racer">Участник, для которого будет закрыт текущий заезд.</param>
        public void CloseCurrentRaceForCurrenrRacer(RacerModel racer)
        {
            // Проверим на треке-ли находится участник

            // Проверим участник готовится к заезду

            // Если участник уже проехал заезд, то его заезд уже нельзя обнулить
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
            SetRacersState(CurrentEditCarClass, CurrentEditRaceNumber);
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
            SetRacersState(CurrentEditCarClass, CurrentEditRaceNumber);
            DataBind();

            return true;
        }

        /// <summary>
        /// Проверить можно-ли передвинуть гонщика на одну позицию вверх по списку участников.
        /// </summary>
        /// <param name="racer">Перемещаемый гонщик.</param>
        /// <param name="carClass">Номер перемещаемого участника.</param>
        /// <param name="raceNum">Номер заезда внутри которого мы двигаем гонщика (начиная с 0).</param>
        /// <returns></returns>
        private bool CheckMoveUp(RacerModel racer, CarClassesEnum carClass, int raceNum)
        {
            var res = true;

            // Посмотрим, а не пытаемся-ли мы передвинуть уже проехавшего заезд гонщика?
            if (racer.Results.ResultsList.ElementAt(raceNum).Count(r => r == null) < ConstantsModel.MaxCircleCount)
            {
                return false;
            }

            // Посмотрим, а не первым-ли в списке находится перемещаемый участник
            var raserGroup = RacerGroups.FirstOrDefault(rg => rg.CarClass == carClass && rg.RaceNumber == raceNum);
            var currentRacer = raserGroup.Racers.FirstOrDefault(r => r.Id == racer.Id);
            var currentRacerNumber = raserGroup.Racers.IndexOf(currentRacer);

            if (currentRacerNumber == 0)
            {
                return false;
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
            }

            return res;
        }

        /// <summary>
        /// Проверить можно-ли передвинуть гонщика на одну позицию вниз по списку участников.
        /// </summary>
        /// <param name="racer">Перемещаемый гонщик.</param>
        /// <param name="carClass">Номер перемещаемого участника.</param>
        /// <param name="raceNum">Номер заезда внутри которого мы двигаем гонщика (начиная с 0).</param>
        /// <returns></returns>
        private bool CheckMoveDown(RacerModel racer, CarClassesEnum carClass, int raceNum)
        {
            var res = true;

            // Посмотрим, а не пытаемся-ли мы передвинуть уже проехавшего заезд гонщика?
            if (racer.Results.ResultsList.ElementAt(raceNum).Count(r => r == null) < ConstantsModel.MaxCircleCount)
            {
                return false;
            }
            
            // Посмотрим, а не последним-ли в списке находится перемещаемый участник
            var raserGroup = RacerGroups.FirstOrDefault(rg => rg.CarClass == carClass && rg.RaceNumber == raceNum);
            var currentRacer = raserGroup.Racers.FirstOrDefault(r => r.Id == racer.Id);
            var currentRacerNumber = raserGroup.Racers.IndexOf(currentRacer);

            if (currentRacerNumber == raserGroup.Racers.Count() - 1)
            {
                return false;
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
            }

            return res;
        } 

        /// <summary>
        /// Удалить все резервные копии данных приложения.
        /// </summary>
        /// <returns>Результат выполнения операции.</returns>
        public OperationResult DeleteBackupFiles()
        {
            try
            {
                return DatabaseBackupManager.DeleteDatabaseBackups();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось удалить резервные копии данных приложения.", "Сообщение",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return new OperationResult(false);
            }
        }

        /// <summary>
        /// Удалить все экземпляры сгенерированных Excel-документов.
        /// </summary>
        /// <returns></returns>
        public OperationResult DeleteAllExcelDocs()
        {
            try
            {
                return ExcelManager.DeleteAllExcelDocs();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось удалить экземпляры сгенерированных Excel-документов.", "Сообщение",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return new OperationResult(false);
            }
        }

        /// <summary>
        /// Получить участника по индексу.
        /// </summary>
        /// <param name="index">Индексу участника в списке класса.</param>
        /// <returns>Требуемый участник.</returns>
        public RacerModel GetRacerFromIndex(int index)
        {
            var car_class = RacerGroups.FirstOrDefault(rg => rg.CarClass == CurrentEditCarClass && rg.RaceNumber == CurrentEditRaceNumber);

            if (car_class == null)
            {
                return null;
            }

            var racers = car_class.Racers;

            if (racers.Any() && racers.Count() >= index + 1)
            {
                return racers.ElementAt(index);
            }

            return null;
        }

        /// <summary>
        /// Обновить данные участника в БД.
        /// </summary>
        /// <param name="racer">Данные обновляемого участника.</param>
        /// <returns>Результат выполнения операции.</returns>
        public OperationResult UpdateRacer(RacerModel racer)
        {
            return RacersDbManager.SetRacer(racer);
        }

        /// <summary>
        /// Проверить заданный заезд у заданного класса на финиширование.
        /// </summary>
        /// <param name="car_class">Заданный класс автомобилей.</param>
        /// <param name="race_number">Номер заезда для проверки (начиная с 0).</param>
        /// <returns></returns>
        public OperationResult CarClassIsFinished(CarClassesEnum car_class, int race_number)
        {
            var cc = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_number && rg.IsFinished);
            return cc == null ? new OperationResult(false) : new OperationResult(true);
        }

        /// <summary>
        /// Обнуление заезда у заданного участника.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public OperationResult NullableRaceForRacer()
        {


            throw  new NotImplementedException();
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
