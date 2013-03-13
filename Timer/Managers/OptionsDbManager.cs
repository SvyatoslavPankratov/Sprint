﻿using System;
using System.Collections.Generic;
using System.Linq;

using NLog;

using Sprint.Data;
using Sprint.Exceptions;
using Sprint.Models;

namespace Sprint.Managers
{
    /// <summary>
    /// Менеджер для доступа к данным опций системы в БД.
    /// </summary>
    public static class OptionsDbManager
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
        /// Получить список всех опций гонок во всех классах автомобилей.
        /// </summary>
        /// <returns>Списко всех опций.</returns>
        public static IEnumerable<RacesOption> GetOptions()
        {
            try
            {
                return dc.RacesOptions;
            }
            catch (Exception ex)
            {
                var exception = new SprintDataException("Не удалось получить список опций заездов.", 
                                                        "Sprint.Managers.OptionsManager.GetOptions()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Получить список всек опций гонок в заданном классе автомобилей.
        /// </summary>
        /// <param name="carClass">Заданный класс автомобилей.</param>
        /// <returns>Список опций в заданном классе автомобилей.</returns>
        public static RacesOption GetOptions(CarClassesEnum carClass)
        {
            try
            {
                var str_CarClass = carClass.ToString();

                var cc = dc.CarClasses.FirstOrDefault(row => row.Name == str_CarClass);

                if (cc == null)
                {
                    var exception = new SprintDataException("Не удалось получить список опций заездов, т.к. заданный класс автомобилей не был найден в БД в таблице [CarClasses].", 
                                                            "Sprint.Managers.OptionsManager.GetOptions(CarClass carClass)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return null;
                }

                var options = dc.RacesOptions.FirstOrDefault(ro => ro.Id_CarClass == cc.Id);

                if (options == null)
                {
                    var exception = new SprintDataException("Не удалось получить список опций заездов, т.к. опций для заданного класса автомобилей на данный момент не существует.",
                                                            "Sprint.Managers.OptionsManager.GetOptions(CarClass carClass)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return null;
                }

                return options;
            }
            catch (Exception ex)
            {
                var exception = new SprintDataException("Не удалось получить список опций заездов.", 
                                                        "Sprint.Managers.OptionsManager.GetOptions(CarClass carClass)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Задать опции гонок.
        /// </summary>
        /// <param name="options">Задаваемые опции гонок.</param>
        /// <returns>Результат задания опций.</returns>
        public static OperationResult SetOptions(RacesOption options)
        {
            try
            {
                if (options == null)
                {
                    var exception = new SprintDataException("Не удалось задать опции заезда, т.к. не заданы опции заезда.",
                                                            "Sprint.Managers.OptionsManager.SetOptions(RacesOption options)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                if (options.CarClass == null && options.Id_CarClass == Guid.Empty)
                {
                    var exception = new SprintDataException("Не удалось задать опции заезда, т.к. не задан класс автомобилей.",
                                                            "Sprint.Managers.OptionsManager.SetOptions(RacesOption options)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                CarClass cc = null;

                if (options.Id_CarClass == null)
                {
                    cc = dc.CarClasses.FirstOrDefault(row => row.Name == options.CarClass.Name);

                    if (cc == null)
                    {
                        var exception = new SprintDataException("Не удалось задать опции заезда, т.к. заданный класс автомобилей не был найден в БД в таблице [CarClasses].",
                                                                "Sprint.Managers.OptionsManager.SetOptions(RacesOption options)");
                        logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                        return new OperationResult(false, exception.Message, exception);
                    }
                }
                else
                {
                    cc = dc.CarClasses.FirstOrDefault(row => row.Id == options.Id_CarClass);

                    if (cc == null)
                    {
                        var exception = new SprintDataException("Не удалось задать опции заезда, т.к. заданный класс автомобилей не был найден в БД в таблице [CarClasses].",
                                                                "Sprint.Managers.OptionsManager.SetOptions(RacesOption options)");
                        logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                        return new OperationResult(false, exception.Message, exception);
                    }
                }

                var ro = dc.RacesOptions.FirstOrDefault(row => row.Id_CarClass == cc.Id);

                if (ro == null)
                {
                    var racesOptions = new RacesOption
                                            {
                                                Id = Guid.NewGuid(),
                                                Id_CarClass = cc.Id,
                                                LidersCount = options.LidersCount,
                                                RaceCount = options.RaceCount
                                            };

                    dc.RacesOptions.Add(racesOptions);
                }
                else
                {
                    ro.LidersCount = options.LidersCount;
                    ro.RaceCount = options.RaceCount;
                }

                dc.SaveChanges();
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintDataException("Не удалось задать заданные опции заездов в заданном классе автомобилей.",
                                                        "Sprint.Managers.OptionsManager.SetOptions(RacesOption options)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Удалить опции гонок.
        /// </summary>
        /// <param name="carClass">Класс автомобилей, для которых мы удаляем опции.</param>
        /// <returns>Результат удаления опций.</returns>
        public static OperationResult DeleteOptions(CarClassesEnum carClass)
        {
            try
            {
                var str_CarClass = carClass.ToString();

                var cc = dc.CarClasses.FirstOrDefault(row => row.Name == str_CarClass);

                if (cc == null)
                {
                    var exception = new SprintDataException("Не удалось удалить заданные опции гонок, т.к. заданный класс автомобилей не был найден в БД в таблице [CarClasses].",
                                                            "Sprint.Managers.OptionsManager.DeleteOptions(CarClass carClass)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return null;
                }

                var ro = dc.RacesOptions.FirstOrDefault(row => row.Id_CarClass == cc.Id);

                if (ro == null)
                {
                    var exception = new SprintDataException("Не удалось удалить заданные опции гонок, т.к. заданные опции не были найдены в БД в таблице [RaceOptions].",
                                                            "Sprint.Managers.OptionsManager.DeleteOptions(CarClass carClass)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                dc.RacesOptions.Remove(ro);
                dc.SaveChanges();
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintDataException("Не удалось удалить заданные опции гонок.", 
                                                        "Sprint.Managers.OptionsManager.DeleteOptions(Guid Id)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        #endregion
    }
}