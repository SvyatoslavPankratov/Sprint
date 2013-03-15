using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NLog;

using Sprint.Data;
using Sprint.Exceptions;
using Sprint.Models;

namespace Sprint.Managers
{
    /// <summary>
    /// Менеджер для доступа к данным участников гонок в БД.
    /// </summary>
    public static class RacersDbManager
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
        /// Получить список всех участников.
        /// </summary>
        /// <returns>Список всех участников.</returns>
        public static IEnumerable<Racer> GetRacers()
        {
            try
            {
                return dc.Racers.ToList();
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось получить список участников.",
                                                        "Sprint.Managers.RacersManager.GetRacers()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Получить список участников в заданном классе автомобилей.
        /// </summary>
        /// <param name="carClass">Класс автомобилей.</param>
        /// <returns>Список участников в заданном классе автомобилей.</returns>
        public static IEnumerable<Racer> GetRacers(CarClassesEnum carClass)
        {
            try
            {
                var cc = dc.CarClasses.FirstOrDefault(row => row.Name == carClass.ToString());

                if (cc == null)
                {
                    var exception = new SprintDataException("Не удалось получить список участников в заданном классе автомобилей, т.к. заданный класс автомобилей не был найден в БД в таблице [CarClasses].",
                                                            "Sprint.Managers.RacersManager.GetRacers(CarClass carClass)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return null;
                }

                var racers = dc.Racers.Where(r => r.Cars.Any(c => c.CarClass.Id== cc.Id));

                if (racers == null)
                {
                    var exception = new SprintDataException("Не удалось получить список участников в заданном классе автомобилей, т.к. участников для заданного класса автомобилей на данный момент не существует.",
                                                            "Sprint.Managers.RacersManager.GetRacers(CarClass carClass)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return null;
                }

                return racers;
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось получить список участников в заданном классе автомобилей.",
                                                        "Sprint.Managers.RacersManager.GetRacers(CarClass carClass)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        public static OperationResult SetRacers(Racer racer)
        {
            throw new NotImplementedException();


        }

        /// <summary>
        /// Удалить заданного участника.
        /// </summary>
        /// <param name="Id">Идентификатор удаляемого участника.</param>
        /// <returns>Результат удаления участника.</returns>
        public static OperationResult DeleteRacers(Guid Id)
        {
            try
            {
                if (Id == Guid.Empty)
                {
                    var exception = new SprintDataException("Не удалось удалить заданного участника, т.к. не задан идентификатор удаляемого участника.",
                                                            "Sprint.Managers.RacersManager.DeleteRacers(Guid Id)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                var r = dc.Racers.FirstOrDefault(row => row.Id == Id);

                if (r == null)
                {
                    var exception = new SprintDataException("Не удалось удалить заданного участника,, т.к. заданный участник не был найдены в БД в таблице [Racers].",
                                                            "Sprint.Managers.RacersManager.DeleteRacers(Guid Id)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                dc.Racers.Remove(r);
                dc.SaveChanges();
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось удалить заданного участника.",
                                                        "Sprint.Managers.RacersManager.DeleteRacers(Guid Id)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        #endregion
    }
}
