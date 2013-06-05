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
    /// Менеджер для работы с состоянием приложения в БД.
    /// </summary>
    public class ApplicationStateDbManager
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
        /// Получить состояние приложения на текущий моент времени.
        /// </summary>
        /// <returns>Состояние приложения на текущий момент времени.</returns>
        public static ApplicationStateModel GetApplicationState()
        {
            try
            {
                var state = dc.ApplicationStates.FirstOrDefault();
                var racers = dc.RacersAtTheTracks.ToArray();

                var app_state = new ApplicationStateModel { RacersAtTheTrack = racers.Select(r => r.Racer.Id).ToList() };

                if (state != null)
                {
                    app_state.CurrentCarClass = (CarClassesEnum)Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), state.CarClass.Name);
                    app_state.CurrentRacer = state.CurrentRacer == null ? (Guid?) null : state.CurrentRacer.Id;
                    app_state.CurrentRaceNumber = state.CurrentRaceNumber;
                }

                return app_state;
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось получить состояние приложения на текущий момент времени.",
                                                    "Sprint.Managers.ApplicationStateDbManager.GetApplicationState()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Задать состояние приложения.
        /// </summary>
        /// <param name="state">Задаваемое состояние приложения.</param>
        /// <returns>Результат задания состояния приложения.</returns>
        public static OperationResult SetApplicationState(ApplicationStateModel state)
        {
            try
            {
                if (state == null)
                {
                    var exception = new SprintDataException("Не удалось задать состояние приложения, т.к. оно задано как null.",
                                                            "Sprint.Managers.ApplicationStateDbManager.SetApplicationState(ApplicationStateModel state)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                var str_cc = state.CurrentCarClass.ToString();
                var cc = dc.CarClasses.FirstOrDefault(row => row.Name == str_cc);

                if (cc == null)
                {
                    var exception = new SprintDataException("Не удалось задать состояние приложения, т.к. заданный класс автомобилей не был найден в БД в таблице [CarClasses].",
                                                            "Sprint.Managers.ApplicationStateDbManager.SetApplicationState(ApplicationStateModel state)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                var ap_st = dc.ApplicationStates.FirstOrDefault();

                if (ap_st != null)
                {
                    DeleteApplicationState();
                }

                dc.ApplicationStates.Add(new ApplicationState
                                                {
                                                    Id = Guid.NewGuid(),
                                                    FK_CurrentCarClass = cc.Id,
                                                    FK_CurrentRacer = state.CurrentRacer,
                                                    CurrentRaceNumber = state.CurrentRaceNumber
                                                });

                foreach (var id in state.RacersAtTheTrack)
                {
                    dc.RacersAtTheTracks.Add(new RacersAtTheTrack
                                                    {
                                                        Id = Guid.NewGuid(),
                                                        FK_Racer = id
                                                    });
                }

                dc.SaveChanges();
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось задать состояние приложения.",
                                                    "Sprint.Managers.ApplicationStateDbManager.SetApplicationState(ApplicationStateModel state)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Удалить состояние приложения.
        /// </summary>
        /// <returns>Результат удаления состояния приложения.</returns>
        public static OperationResult DeleteApplicationState()
        {
            try
            {
                foreach (var racer in dc.RacersAtTheTracks)
                {
                    dc.RacersAtTheTracks.Remove(racer);
                }

                foreach (var state in dc.ApplicationStates)
                {
                    dc.ApplicationStates.Remove(state);
                }

                dc.SaveChanges();
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось удалить состояние приложения.",
                                                    "Sprint.Managers.ApplicationStateDbManager.DeleteApplicationState()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        #endregion
    }
}
