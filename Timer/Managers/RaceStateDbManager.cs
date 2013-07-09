using System;
using System.Collections.Generic;
using System.Linq;

using NLog;

using Sprint.Data;
using Sprint.Exceptions;
using Sprint.Models;

namespace Sprint.Managers
{
    /// <summary>
    /// Менеджер по работе с состояниями всех заездов по классам автомобилей и номерам заездов.
    /// Какие участники внутри каждого класса и заезда.
    /// </summary>
    public static class RaceStateDbManager
    {
        #region Поля только для чтения

        /// <summary>
        /// Контекст для работы с данными.
        /// </summary>
        private static readonly SprintEntities dc = new SprintEntities();

        /// <summary>
        /// Логгер.
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Методы

        /// <summary>
        /// Получить состояния всех заездов на текущий момент времени.
        /// </summary>
        /// <returns>Состояние всех заездов на текущий момент времени.</returns>
        public static IEnumerable<RaceStateModel> GetRaceStates()
        {
            try
            {
                // Получим состояние во всех классах автомобилей (какие участники в каких и на каких заездах)
                var racer_states = new List<RaceStateModel>();

                foreach (CarClassesEnum cc in Enum.GetValues(typeof(CarClassesEnum)))
                {
                    var cc_str = cc.ToString();

                    // Вначале закидываем всех участников у каждого класса автомобилей для 1 заезда
                    var race_number = 0;
                    var racers = from race_state in dc.RaceStates
                                 where race_state.CarClass.Name == cc_str
                                       && race_state.RaceNumber == race_number
                                 orderby race_state.IndexInsideCarClass
                                 select race_state.FK_Racer;

                    if (racers.Any())
                    {
                        racer_states.Add(new RaceStateModel(cc, race_number) { Racers = racers.ToArray() });
                    }

                    // Потом для 2 заезда
                    race_number = 1;
                    racers = from race_state in dc.RaceStates
                             where race_state.CarClass.Name == cc_str
                                   && race_state.RaceNumber == race_number
                             orderby race_state.IndexInsideCarClass
                             select race_state.FK_Racer;

                    if (racers.Any())
                    {
                        racer_states.Add(new RaceStateModel(cc, race_number) { Racers = racers.ToArray() });
                    }
                }

                return racer_states;
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось получить состояние всех заездов на текущий момент времени.",
                                                    "Sprint.Managers.RaceStateDbManager.GetRaceStates()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Задать состояние заезда.
        /// </summary>
        /// <param name="race_state">Задаваемое состояние заезда.</param>
        /// <returns>Результат операции.</returns>
        public static OperationResult SetRaceStates(RaceStateModel race_state)
        {
            if (race_state == null)
            {
                var exception = new SprintException("Не удалось задать состояние заданного заезда, так как добавляемое состояние равно null.",
                                                    "Sprint.Managers.RaceStateDbManager.SetRaceStates(RaceStateModel race_state)");
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                return new OperationResult(false);
            }

            try
            {
                // Удалим все состояния
                foreach (var id in race_state.Racers)
                {
                    var cc_str = race_state.CarClass.ToString();

                    // Проверим есть-ли в базе уже такое состояние
                    var old_rs = (from rs in dc.RaceStates
                                  where rs.CarClass.Name == cc_str
                                        && rs.RaceNumber == race_state.RaceNumber
                                        && rs.FK_Racer == id
                                  select rs).FirstOrDefault();

                    // Удалим старое значение, если оно есть
                    if (old_rs != null)
                    {
                        dc.RaceStates.Remove(old_rs);
                    }
                }

                var index = 0;

                // Зададим все состояния
                foreach (var id in race_state.Racers)
                {
                    var cc_str = race_state.CarClass.ToString();
                    var fk_cc = (from cc in dc.CarClasses
                                 where cc.Name == cc_str
                                 select cc).FirstOrDefault();

                    // Добавим новое значение
                    var new_rs = new RaceState
                                        {
                                            Id = Guid.NewGuid(),
                                            FK_CarClass = fk_cc.Id,
                                            RaceNumber = race_state.RaceNumber,
                                            FK_Racer = id,
                                            IndexInsideCarClass = index
                                        };

                    dc.RaceStates.Add(new_rs);

                    index++;
                }

                dc.SaveChanges();

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось добавить состояние заданного заезда.",
                                                    "Sprint.Managers.RaceStateDbManager.SetRaceStates(RaceStateModel race_state)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                return new OperationResult(false, exception);
            }
        }

        /// <summary>
        /// Задать состояние заезда.
        /// </summary>
        /// <param name="race_state">Коллекция задаваемых состояний заездов.</param>
        /// <returns>Результат операции.</returns>
        public static OperationResult SetRaceStates(IEnumerable<RaceStateModel> race_states)
        {
            if (race_states == null || !race_states.Any())
            {
                var exception = new SprintException("Не удалось задать коллекцию состояний заданных заездов, так как добавляемая коллекция равна null или пустая.",
                                                    "Sprint.Managers.RaceStateDbManager.SetRaceStates(IEnumerable<RaceStateModel> race_states)");
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                return new OperationResult(false);
            }

            try
            {
                foreach (var race_state in race_states)
                {
                    // Удалим все состояния
                    foreach (var id in race_state.Racers)
                    {
                        var cc_str = race_state.CarClass.ToString();

                        // Проверим есть-ли в базе уже такое состояние
                        var old_rs = (from rs in dc.RaceStates
                                      where rs.CarClass.Name == cc_str
                                            && rs.RaceNumber == race_state.RaceNumber
                                            && rs.FK_Racer == id
                                      select rs).FirstOrDefault();

                        // Удалим старое значение, если оно есть
                        if (old_rs != null)
                        {
                            dc.RaceStates.Remove(old_rs);
                        }
                    }

                    var index = 0;

                    // Зададим все состояния
                    foreach (var id in race_state.Racers)
                    {
                        var cc_str = race_state.CarClass.ToString();
                        var fk_cc = (from cc in dc.CarClasses
                                     where cc.Name == cc_str
                                     select cc).FirstOrDefault();

                        // Добавим новое значение
                        var new_rs = new RaceState
                                            {
                                                Id = Guid.NewGuid(),
                                                FK_CarClass = fk_cc.Id,
                                                RaceNumber = race_state.RaceNumber,
                                                FK_Racer = id,
                                                IndexInsideCarClass = index
                                            };

                        dc.RaceStates.Add(new_rs);

                        index++;
                    }
                }

                dc.SaveChanges();

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось задать коллекцию состояний заданных заездов.",
                                                    "Sprint.Managers.RaceStateDbManager.SetRaceStates(IEnumerable<RaceStateModel> race_states)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                return new OperationResult(false, exception);
            }
        }

        /// <summary>
        /// Удалить состояние заданного заезда у заданного класса автомобилей.
        /// </summary>
        /// <param name="car_class">Класс автомобилей для которого удаляем состояние заезда.</param>
        /// <param name="race_namber">Номер заезда внутри заданного класса автомобилей для которого удаляем состояние заезда.</param>
        /// <returns>Результат опреации.</returns>
        public static OperationResult DeleteRaceStates(CarClassesEnum car_class, int race_namber)
        {
            try
            {
                var racer_states = from rs in dc.RaceStates
                                   where rs.CarClass.Name == car_class.ToString()
                                         && rs.RaceNumber == race_namber
                                   select rs;

                foreach (var racer_state in racer_states)
                {
                    dc.RaceStates.Remove(racer_state);
                }

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось удалить состояние заданного заезда у заданного класса автомобилей.",
                                                    "Sprint.Managers.RaceStateDbManager.DeleteRaceStates(CarClassesEnum car_class, int race_namber)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                return new OperationResult(false, exception);
            }
        }

        /// <summary>
        /// Удалить состояния всех заездов у автомобилей.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public static OperationResult DeleteAllStates()
        {
            try
            {
                var racer_states = from rs in dc.RaceStates 
                                   select rs;

                foreach (var racer_state in racer_states)
                {
                    dc.RaceStates.Remove(racer_state);
                }

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось удалить состояния всех заездов у автомобилей.",
                                                    "Sprint.Managers.RaceStateDbManager.DeleteAllStates()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                return new OperationResult(false, exception);
            }
        }

        #endregion
    }
}
