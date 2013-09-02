using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows;

using NLog;

using Sprint.Exceptions;
using Sprint.Extensions;
using Sprint.Interfaces;
using Sprint.Managers;
using Sprint.Models;
using Sprint.Views;
using Point = System.Windows.Point;

namespace Sprint.Presenters
{
    /// <summary>
    /// Презнетер главного окна.
    /// </summary>
    partial class MainPresenter : IDisposable
    {
        #region Поля только для чтения

        /// <summary>
        /// Логгер.
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

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

        /// <summary>
        /// Задать или получить статус заезда у участников текущего класса автомобилей, 
        /// который учитывается в текущий момент времени.
        /// </summary>
        private RacerRaceStateEnum CurrentRacerRaceState { get; set; }

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

            MainView.TablePainted = (sender, args) =>  SetColorsForRows();

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
        /// Задать цвета всем строчкам, где есть участники выбывшие из заезда, 
        /// перезаезжающие заезд с нуля или с неудачного круга.
        /// </summary>
        private void SetColorsForRows()
        {
            var race_state = RacerRaceStateEnum.Break;
            var color = Color.Firebrick;
            SetColor(race_state, color);

            race_state = RacerRaceStateEnum.Rerun;
            color = Color.Gold;
            SetColor(race_state, color);

            race_state = RacerRaceStateEnum.Restart;
            color = Color.DarkOrange;
            SetColor(race_state, color);
        }

        /// <summary>
        /// Задать заданный цвет строчкам с участниками у всех классов автомобилей для заданного состояния заеда.
        /// </summary>
        /// <param name="race_state"Состояние заезда.></param>
        /// <param name="color">Цвет, в который окрасится строчка.</param>
        private void SetColor(RacerRaceStateEnum race_state, Color color)
        {
            var car_class = CarClassesEnum.FWD;

            var race_num = 0;
            var fwd_r1_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < fwd_r1_racers.Count(); i++)
            {
                var racer = fwd_r1_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            race_num = 1;
            var fwd_r2_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < fwd_r2_racers.Count(); i++)
            {
                var racer = fwd_r2_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            car_class = CarClassesEnum.RWD;

            race_num = 0;
            var rwd_r1_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < rwd_r1_racers.Count(); i++)
            {
                var racer = rwd_r1_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            race_num = 1;
            var rwd_r2_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < rwd_r2_racers.Count(); i++)
            {
                var racer = rwd_r2_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            car_class = CarClassesEnum.AWD;

            race_num = 0;
            var awd_r1_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < awd_r1_racers.Count(); i++)
            {
                var racer = awd_r1_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            race_num = 1;
            var awd_r2_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < awd_r2_racers.Count(); i++)
            {
                var racer = awd_r2_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            car_class = CarClassesEnum.Sport;

            race_num = 0;
            var sport_r1_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < sport_r1_racers.Count(); i++)
            {
                var racer = sport_r1_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            race_num = 1;
            var sport_r2_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < sport_r2_racers.Count(); i++)
            {
                var racer = sport_r2_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            car_class = CarClassesEnum.K100;

            race_num = 0;
            var K100_r1_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < K100_r1_racers.Count(); i++)
            {
                var racer = K100_r1_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            race_num = 1;
            var K100_r2_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < K100_r2_racers.Count(); i++)
            {
                var racer = K100_r2_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            car_class = CarClassesEnum.K100;

            race_num = 0;
            var K160_r1_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < K160_r1_racers.Count(); i++)
            {
                var racer = K160_r1_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            race_num = 1;
            var K160_r2_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < K160_r2_racers.Count(); i++)
            {
                var racer = K160_r2_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            car_class = CarClassesEnum.KA;

            race_num = 0;
            var KA_r1_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < KA_r1_racers.Count(); i++)
            {
                var racer = KA_r1_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }

            race_num = 1;
            var KA_r2_racers = RacerGroups.FirstOrDefault(rg => rg.CarClass == car_class && rg.RaceNumber == race_num).Racers;

            for (int i = 0; i < KA_r2_racers.Count(); i++)
            {
                var racer = KA_r2_racers.ElementAt(i);

                if (racer.Results.GetRaceState(0) == race_state)
                {
                    MainView.SetRowColor(car_class, race_num, i, color);
                }
            }
        }

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
            CurrentRacerRaceState = RacerRaceStateEnum.Run;

            if (CurrentRaserGroup.Racers.Any())
            {
                var racer = CurrentRaserGroup.GetNextRacer(Track.CurrentRacer, RacerRaceStateEnum.Run);
                MainView.NextCurrentRacer = racer == null ? 0 : racer.RacerNumber;
                MainView.NextRacerState = NextRacerState.Start;

                if (SecondView != null)
                {
                    SecondView.NextRacerNumber = racer == null ? 0 : racer.RacerNumber;
                    SecondView.NextRacerState = NextRacerState.Start;
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
            if (Track.CurrentRacers.Count() == 2
                && Track.CurrentRacer != null
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
            var racer = CurrentRaserGroup.GetNextRacer(Track.CurrentRacer, CurrentRacerRaceState);

            // Если трек пустой, то добавить на него одного гонщика
            if (!Track.CurrentRacers.Any() && racer != null)
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
            // Если на треке уже есть участник и он проезжает уже 3 круг, то добавим еще одного участника
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

                // Если автомобиль теперь проехал 2 круга и пошел на 3-й, то дадим сигнал следующему участнику, чтобы он выезжал на трассу
                if (Track.CurrentRacer.Results.GetCurrentCircleNumber(CurrentRaceNum.Value) == 2 && Track.CurrentRacers.Count() == 1 && racer != null)
                {
                    MainView.NextRacerState = NextRacerState.Start;

                    if (SecondView != null)
                    {
                        SecondView.NextRacerState = NextRacerState.Start;
                    }
                }
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

            // Если все участники с текущим статусом заезда закрылись
            // То начинаем закрывать неудачные заезды
            // Для этого мы предупреждаем оператора временным сообщением и ищем повторные заезды
            if (CurrentRacerRaceState == RacerRaceStateEnum.Run
                && Track.CurrentRacer == null
                && CurrentRaserGroup.GetNextRacer(Track.CurrentRacer, CurrentRacerRaceState) == null
                && !CurrentRaserGroup.IsFinished)
            {
                CurrentRacerRaceState = RacerRaceStateEnum.Rerun;

                var worker = new BackgroundWorker();
                worker.DoWork += (sender, args) =>
                                    {
                                        ITimeMessageNotification wnd = new TimeMessageNotificationView
                                                                            {
                                                                                TextMessage = "На трассу выходят участники, которые будут доезжать не закрытый заезд"
                                                                            };

                                        wnd.Show();
                                        wnd.Refresh();

                                        Thread.Sleep(5000);

                                        wnd.CloseForm();
                                    };

                worker.RunWorkerAsync();

                var next_racer = CurrentRaserGroup.GetNextRacer(Track.CurrentRacer, CurrentRacerRaceState);

                if (next_racer != null)
                {
                    // Обновим данные на форме
                    MainView.FirstCurrentRacer = 0;
                    MainView.SecondCurrentRacer = 0;
                    

                    if (SecondView != null)
                    {
                        SecondView.FirstCurrentRacerNumber = 0;
                        SecondView.SecondCurrentRacerNumber = 0;
                        SecondView.NextRacerState = NextRacerState.Start;
                    }

                    racer = SetNextRacerInfo(racer);

                    MainView.NextRacerState = NextRacerState.Start;

                    if (SecondView != null)
                    {
                        SecondView.NextRacerState = NextRacerState.Start;
                    }
                }
            }

            // Если все участники с текущим статусом заезда закрылись
            // То начинаем закрывать тех, кто перезаезжает заезды с нуля
            // Для этого мы предупреждаем оператора временным сообщением и ищем повторные заезды
            if (CurrentRacerRaceState == RacerRaceStateEnum.Rerun
                && Track.CurrentRacer == null
                && CurrentRaserGroup.GetNextRacer(Track.CurrentRacer, CurrentRacerRaceState) == null
                && !CurrentRaserGroup.IsFinished)
            {
                CurrentRacerRaceState = RacerRaceStateEnum.Restart;

                var worker = new BackgroundWorker();
                worker.DoWork += (sender, args) =>
                                    {
                                        ITimeMessageNotification wnd = new TimeMessageNotificationView
                                                                            {
                                                                                TextMessage = "На трассу выходят участники, которые будут перезаезжать заезд с нуля"
                                                                            };

                                        wnd.Show();
                                        wnd.Refresh();

                                        Thread.Sleep(5000);

                                        wnd.CloseForm();
                                    };

                worker.RunWorkerAsync();

                var next_racer = CurrentRaserGroup.GetNextRacer(Track.CurrentRacer, CurrentRacerRaceState);

                if (next_racer != null)
                {
                    // Обновим данные на форме
                    MainView.FirstCurrentRacer = 0;
                    MainView.SecondCurrentRacer = 0;


                    if (SecondView != null)
                    {
                        SecondView.FirstCurrentRacerNumber = 0;
                        SecondView.SecondCurrentRacerNumber = 0;
                        SecondView.NextRacerState = NextRacerState.Start;
                    }

                    racer = SetNextRacerInfo(racer);

                    MainView.NextRacerState = NextRacerState.Start;

                    if (SecondView != null)
                    {
                        SecondView.NextRacerState = NextRacerState.Start;
                    }
                }
            }

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
        /// Проверим на треке-ли сейчас находится заданный участник.
        /// </summary>
        /// <param name="racer">Заданный участник.</param>
        /// <returns>Результат проверки.</returns>
        public bool CheckRacerForSetRerunStatus(RacerModel racer)
        {
            if (racer == null)
            {
                return false;
            }

            return Track.CheckRacer(racer);
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
        /// <param name="second_racer">Текущий участник, который последний вышел на трассу.</param>
        /// <returns></returns>
        private RacerModel SetNextRacerInfo(RacerModel second_racer)
        {
            var next_racer = CurrentRaserGroup.GetNextRacer(second_racer, CurrentRacerRaceState);
            MainView.NextCurrentRacer = next_racer == null ? 0 : next_racer.RacerNumber;
            MainView.NextRacerState = NextRacerState.Stop;

            if (SecondView != null)
            {
                SecondView.NextRacerNumber = next_racer == null ? 0 : next_racer.RacerNumber;
                SecondView.NextRacerState = NextRacerState.Stop;
            }

            return next_racer;
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
        /// Убираем заданного участника с трека с перезаездом текущего круга вместе с не закрытым заездом.
        /// </summary>
        /// <param name="racer">Участник, для которого будет перезаезд проезжаемого круга.</param>
        public bool RerunCurrenrRacer(RacerModel racer)
        {
            if (racer == null)
            {
                return false;
            }

            try
            {
                // Проверим на треке-ли находится участник
                if (Track.CheckRacer(racer))
                {
                    // Изменим у участника статус у текущего заезда
                    racer.Results.SetRaceState(CurrentRaceNum.Value, RacerRaceStateEnum.Rerun);

                    // Снимем участника с трека
                    // Для этого нам вначале надо определить не он ли сейчас будет пересекать финишную прямую
                    if (Track.CurrentRacer != null && Track.CurrentRacers.Count() == 2
                        && Track.CurrentRacerNum == 0 && Track.CurrentRacer.Results.GetCurrentCircleNumber(CurrentRaceNum.Value) >= 3)
                    {
                        Track.CurrentRacerNum = 1;
                    }
                    else
                    {
                        Track.CurrentRacerNum = 0;
                    }

                    // Если он
                    if (Track.CheckCurrentRacer(racer))
                    {
                        // То надо вначале убрать его с трека
                        var list = new List<RacerModel>(Track.CurrentRacers);
                        list.Remove(Track.CurrentRacer);
                        Track.CurrentRacers = list;

                        // Затем, переместить указатель текущего участника (который сечас будет пересекать финишную прямую) 
                        // на следующего участника, находящегося на треке
                        Track.CurrentRacerNum = 0;
    
                        // Обновим данные на форме
                        MainView.FirstCurrentRacer = !Track.CurrentRacers.Any() ? 0: Track.CurrentRacers.ElementAt(0).RacerNumber;
                        MainView.SecondCurrentRacer = 0;

                        if (SecondView != null)
                        {
                            SecondView.FirstCurrentRacerNumber = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                            SecondView.SecondCurrentRacerNumber = 0;
                        }
                    }
                    // Если сейчас не он будет пересекать финишную прямую
                    else
                    {
                        // То просто уберем его с трека
                        var list = new List<RacerModel>(Track.CurrentRacers);
                        list.Remove(racer);
                        Track.CurrentRacers = list;

                        // Обновим данные на форме
                        MainView.FirstCurrentRacer = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                        MainView.SecondCurrentRacer = 0;

                        if (SecondView != null)
                        {
                            SecondView.FirstCurrentRacerNumber = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                            SecondView.SecondCurrentRacerNumber = 0;
                        }
                    }
                }

                // Если участник готовится к заезду, то у него нету текущего круга
                // Если участник уже проехал заезд, то у него тоже уже нету текущего круна

                RacersDbManager.SetRacer(racer);
                SaveApplicationState();
                DataBind();
                SetRacerResultsToSecondView();

                return true;
            }
            catch (Exception ex)
            {
                var message = "Не удалось убрать заданного участника с трека с перезаездом текущего круга вместе с не закрытым заездом.";
                var exception = new SprintException(message, "Sprint.Presenters.MainPresenter.RerunCurrenrRacer(RacerModel racer)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                return false;
            }
        }

        /// <summary>
        /// Убираем заданного участника с трека (или не допускаем его выход на трек, 
        /// если он на нем еще не был) с закрытием текущего заезда.
        /// </summary>
        /// <param name="racer">Участник, для которого будет закрыт текущий заезд.</param>
        public bool BreakSelectedRacer(RacerModel racer)
        {
            if (racer == null)
            {
                return false;
            }

            try
            {
                // Проверим на треке-ли находится участник
                if (Track.CheckRacer(racer))
                {
                    // Поставим ему текущим последний круг
                    racer.Results.SetCurrentCircleNumber(CurrentRaceNum.Value, ConstantsModel.MaxCircleCount);

                    // Изменим у участника статус у текущего заезда
                    racer.Results.SetRaceState(CurrentRaceNum.Value, RacerRaceStateEnum.Break);

                    // Снимем участника с трека
                    // Для этого нам вначале надо определить не он ли сейчас будет пересекать финишную прямую
                    if (Track.CurrentRacer != null && Track.CurrentRacers.Count() == 2
                        && Track.CurrentRacerNum == 0 &&
                        Track.CurrentRacer.Results.GetCurrentCircleNumber(CurrentRaceNum.Value) >= 3)
                    {
                        Track.CurrentRacerNum = 1;
                    }
                    else
                    {
                        Track.CurrentRacerNum = 0;
                    }

                    // Если он
                    if (Track.CheckCurrentRacer(racer))
                    {
                        // То надо вначале убрать его с трека
                        var list = new List<RacerModel>(Track.CurrentRacers);
                        list.Remove(Track.CurrentRacer);
                        Track.CurrentRacers = list;

                        // Затем, переместить указатель текущего участника (который сечас будет пересекать финишную прямую) 
                        // на следующего участника, находящегося на треке
                        Track.CurrentRacerNum = 0;

                        // Обновим данные на форме
                        MainView.FirstCurrentRacer = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                        MainView.SecondCurrentRacer = 0;

                        if (SecondView != null)
                        {
                            SecondView.FirstCurrentRacerNumber = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                            SecondView.SecondCurrentRacerNumber = 0;
                        }
                    }
                    // Если сейчас не он будет пересекать финишную прямую
                    else
                    {
                        // То просто уберем его с трека
                        var list = new List<RacerModel>(Track.CurrentRacers);
                        list.Remove(racer);
                        Track.CurrentRacers = list;

                        // Обновим данные на форме
                        MainView.FirstCurrentRacer = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                        MainView.SecondCurrentRacer = 0;

                        if (SecondView != null)
                        {
                            SecondView.FirstCurrentRacerNumber = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                            SecondView.SecondCurrentRacerNumber = 0;
                        }
                    }
                }
                // Проверим участник готовится к заезду
                else if (CurrentRaserGroup.GetNextRacer(Track.CurrentRacer, CurrentRacerRaceState).Id == racer.Id)
                {
                    // Поставим ему текущий круг последним
                    racer.Results.SetCurrentCircleNumber(CurrentRaceNum.Value, ConstantsModel.MaxCircleCount);

                    // Изменим у участника статус у текущего заезда
                    racer.Results.SetRaceState(CurrentRaceNum.Value, RacerRaceStateEnum.Break);

                    // После всего этого обновим данные на форме, чтобы готовящийся к выезду участник на форме изменился
                    racer = SetNextRacerInfo(racer);
                }
                else
                {
                    // Если участник не идет следующим, то поставим ему текущий круг последним и ничего с ним больше делать не надо
                    racer.Results.SetCurrentCircleNumber(CurrentRaceNum.Value, ConstantsModel.MaxCircleCount);

                    // Изменим у участника статус у текущего заезда
                    racer.Results.SetRaceState(CurrentRaceNum.Value, RacerRaceStateEnum.Break);
                }

                // Если участник уже проехал заезд, то его заезд уже нельзя закрыть

                RacersDbManager.SetRacer(racer);
                SaveApplicationState();
                DataBind();
                SetRacerResultsToSecondView();

                return true;
            }
            catch (Exception ex)
            {
                var message ="Не удалось убрать заданного участника с трека с закрытием текущего заездом или просто снять участника с гонок.";
                var exception = new SprintException(message, "Sprint.Presenters.MainPresenter.BreakSelectedRacer(RacerModel racer)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                return false;
            }
        }

        /// <summary>
        /// Перезаезд у заданного участника всего заезда с нуля.
        /// </summary>
        /// <param name="racer">Заданный участник.</param>
        /// <returns>Результат выполнения операции.</returns>
        public bool RestartCurrentRacer(RacerModel racer)
        {
            if (racer == null)
            {
                return false;
            }

            try
            {
                // Проверим на треке-ли находится участник
                if (Track.CheckRacer(racer))
                {
                    // Сбросим у участника все круги
                    racer.Results.SetCurrentCircleNumber(CurrentRaceNum.Value, 0);

                    // Очистим у него все результаты
                    racer.Results.Clear();

                    // Изменим у участника статус у текущего заезда
                    racer.Results.SetRaceState(CurrentRaceNum.Value, RacerRaceStateEnum.Restart);

                    // Снимем участника с трека
                    // Для этого нам вначале надо определить не он ли сейчас будет пересекать финишную прямую
                    if (Track.CurrentRacer != null && Track.CurrentRacers.Count() == 2
                        && Track.CurrentRacerNum == 0 && Track.CurrentRacer.Results.GetCurrentCircleNumber(CurrentRaceNum.Value) >= 3)
                    {
                        Track.CurrentRacerNum = 1;
                    }
                    else
                    {
                        Track.CurrentRacerNum = 0;
                    }

                    // Если он
                    if (Track.CheckCurrentRacer(racer))
                    {
                        // То надо вначале убрать его с трека
                        var list = new List<RacerModel>(Track.CurrentRacers);
                        list.Remove(Track.CurrentRacer);
                        Track.CurrentRacers = list;

                        // Затем, переместить указатель текущего участника (который сечас будет пересекать финишную прямую) 
                        // на следующего участника, находящегося на треке
                        Track.CurrentRacerNum = 0;

                        // Обновим данные на форме
                        MainView.FirstCurrentRacer = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                        MainView.SecondCurrentRacer = 0;

                        if (SecondView != null)
                        {
                            SecondView.FirstCurrentRacerNumber = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                            SecondView.SecondCurrentRacerNumber = 0;
                        }
                    }
                    // Если сейчас не он будет пересекать финишную прямую
                    else
                    {
                        // То просто уберем его с трека
                        var list = new List<RacerModel>(Track.CurrentRacers);
                        list.Remove(racer);
                        Track.CurrentRacers = list;

                        // Обновим данные на форме
                        MainView.FirstCurrentRacer = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                        MainView.SecondCurrentRacer = 0;

                        if (SecondView != null)
                        {
                            SecondView.FirstCurrentRacerNumber = !Track.CurrentRacers.Any() ? 0 : Track.CurrentRacers.ElementAt(0).RacerNumber;
                            SecondView.SecondCurrentRacerNumber = 0;
                        }
                    }
                }
                // Если участник уже проехал заезд, то надо сбросить у него все результаты
                else if(racer.Results.IsFinished(CurrentRaceNum.Value))
                {
                    // Сбросим у участника все круги
                    racer.Results.SetCurrentCircleNumber(CurrentRaceNum.Value, 0);

                    // Очистим у него все результаты
                    racer.Results.Clear();

                    // Изменим у участника статус у текущего заезда
                    racer.Results.SetRaceState(CurrentRaceNum.Value, RacerRaceStateEnum.Restart);
                }

                // Если участник готовится к заезду, то у него нечего сбрасывать

                RacersDbManager.SetRacer(racer);
                SaveApplicationState();
                DataBind();
                SetRacerResultsToSecondView();

                return true;
            }
            catch (Exception ex)
            {
                var message = "Не удалось задать у заданного учатсника перезаезд всего заезда.";
                var exception = new SprintException(message, "Sprint.Presenters.MainPresenter.RestartCurrentRacer(RacerModel racer)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                return false;
            }
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
            //SetRacersState(CurrentEditCarClass, CurrentEditRaceNumber);
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
            //SetRacersState(CurrentEditCarClass, CurrentEditRaceNumber);
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
