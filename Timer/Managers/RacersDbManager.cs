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
        public static IEnumerable<RacerModel> GetRacers()
        {
            try
            {
                var racers = new List<RacerModel>();

                foreach (var racer in dc.Racers)
                {
                    var rm = new RacerModel(racer.FirstName, racer.LastName, racer.MiddleName, racer.Cars.First().Name,
                                            (CarClassesEnum)Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), racer.Cars.First().CarClass.Name));
                    rm.RacerNumber = racer.Number;

                    // Добавить результаты

                    racers.Add(rm);
                }

                return racers;
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
        public static IEnumerable<RacerModel> GetRacers(CarClassesEnum carClass)
        {
            try
            {
                var str_car_class = carClass.ToString();
                var car_class = dc.CarClasses.FirstOrDefault(row => row.Name == str_car_class);

                if (car_class == null)
                {
                    var exception = new SprintDataException("Не удалось получить список участников в заданном классе автомобилей, т.к. заданный класс автомобилей не был найден в БД в таблице [CarClasses].",
                                                            "Sprint.Managers.RacersManager.GetRacers(CarClass carClass)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return null;
                }

                var cc = dc.CarClasses.FirstOrDefault(row => row.Name == str_car_class);

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

                var results = new List<RacerModel>();

                foreach (var racer in racers)
                {
                    var rm = new RacerModel(racer.FirstName, racer.LastName, racer.MiddleName, racer.Cars.First().Name, 
                                            (CarClassesEnum)Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), racer.Cars.First().CarClass.Name));
                    rm.RacerNumber = racer.Number;

                    // Добавить результаты

                    results.Add(rm);
                }

                return results;
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось получить список участников в заданном классе автомобилей.",
                                                        "Sprint.Managers.RacersManager.GetRacers(CarClass carClass)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Сохранить данные о участнике в БД.
        /// </summary>
        /// <param name="racerModel">Сохраняемый участник.</param>
        /// <returns>Результаты операции.</returns>
        public static OperationResult SetRacer(RacerModel racerModel)
        {
            try
            {
                if (racerModel == null)
                {
                    var exception = new SprintDataException("Не удалось сохранить данные о заданном участнике, т.к. не задан сохраняемый участник.",
                                                            "Sprint.Managers.RacersDbManager.SetRacer(Racer racer, IEnumerable<Car> cars, CarClassesEnum carClass, IEnumerable<Result> results)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                if (racerModel.Car == null)
                {
                    var exception = new SprintDataException("Не удалось сохранить данные о заданном участнике, т.к. у участника не заданы автомобили.",
                                                            "Sprint.Managers.RacersDbManager.SetRacer(Racer racer, IEnumerable<Car> cars, CarClassesEnum carClass, IEnumerable<Result> results)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                // Зададим/получим участника
                Racer cur_racer = null;

                if(racerModel.Id != Guid.Empty)
                {
                    cur_racer = dc.Racers.FirstOrDefault(r => r.Id == racerModel.Id);

                    if (cur_racer != null)
                    {
                        cur_racer.FirstName = racerModel.FirstName;
                        cur_racer.LastName = racerModel.LastName;
                        cur_racer.MiddleName = racerModel.MiddleName;
                        cur_racer.Number = racerModel.RacerNumber;
                    }
                    else
                    {
                        cur_racer = new Racer
                                        {
                                            Id = racerModel.Id,
                                            FirstName = racerModel.FirstName,
                                            LastName = racerModel.LastName,
                                            MiddleName = racerModel.MiddleName,
                                            Number = racerModel.RacerNumber
                                        };

                        dc.Racers.Add(cur_racer);
                    }
                }
                else
                {
                    racerModel.Id = Guid.NewGuid();

                    cur_racer = new Racer
                                    {
                                        Id = racerModel.Id,
                                        FirstName = racerModel.FirstName,
                                        LastName = racerModel.LastName,
                                        MiddleName = racerModel.MiddleName,
                                        Number = racerModel.RacerNumber
                                    };

                    dc.Racers.Add(cur_racer);
                }

                for (int i = 0; i < cur_racer.Cars.Count; i++)
                {
                    var c = cur_racer.Cars.ElementAt(i);
                    dc.Cars.Remove(c);
                    i = -1;
                }

                for (int i = 0; i < cur_racer.Results.Count; i++)
                {
                    var r = cur_racer.Results.ElementAt(i);
                    dc.Results.Remove(r);
                    i = -1;
                }

                // Зададим участнику его автомобиль
                var str_car_class = racerModel.Car.CarClass.ToString();
                var car_class = dc.CarClasses.FirstOrDefault(row => row.Name == str_car_class);

                if (car_class == null)
                {
                    var exception = new SprintDataException("Не удалось сохранить данные о заданном участнике, т.к. заданный класс автомобилей не был найден в БД в таблице [CarClasses].",
                                                            "Sprint.Managers.RacersDbManager.SetRacer(Racer racer, IEnumerable<Car> cars, CarClassesEnum carClass, IEnumerable<Result> results)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                var car = new Car
                            {
                                Id = Guid.NewGuid(),
                                CarClass = car_class,
                                Name = racerModel.Car.Name
                            };                

                cur_racer.Cars.Add(car);                

                // Зададим участнику его результаты
                for(int i = 0; i < racerModel.Results.ResultsList.Count(); i++)
                {
                    var race_result = racerModel.Results.ResultsList.ElementAt(i);

                    for (int n = 0; n < race_result.Count(); n++)
                    {
                        var result = race_result.ElementAt(n);
                        var res = new Result
                                        {
                                            Id = Guid.NewGuid(),
                                            RaceNumber = i,
                                            LapNumber = n,
                                            Time = result == null ? (Nullable<long>)null : result.TimeSpan.Ticks,
                                            WarmingUp = result == null ? false : result.WarmingUp
                                        };

                        cur_racer.Results.Add(res);
                    }
                }

                dc.SaveChanges();

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось сохранить данные о заданном участнике.",
                                                    "Sprint.Managers.RacersDbManager.SetRacer(Racer racer, IEnumerable<Car> cars, CarClassesEnum carClass, IEnumerable<Result> results)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Удалить заданного участника.
        /// </summary>
        /// <param name="Id">Идентификатор удаляемого участника.</param>
        /// <returns>Результат удаления участника.</returns>
        public static OperationResult DeleteRacer(Guid Id)
        {
            try
            {
                if (Id == Guid.Empty)
                {
                    var exception = new SprintDataException("Не удалось удалить заданного участника, т.к. не задан идентификатор удаляемого участника.",
                                                            "Sprint.Managers.RacersManager.DeleteRacer(Guid Id)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                var r = dc.Racers.FirstOrDefault(row => row.Id == Id);

                if (r == null)
                {
                    var exception = new SprintDataException("Не удалось удалить заданного участника,, т.к. заданный участник не был найдены в БД в таблице [Racers].",
                                                            "Sprint.Managers.RacersManager.DeleteRacer(Guid Id)");
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
                                                        "Sprint.Managers.RacersManager.DeleteRacer(Guid Id)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Удалить из БД всю информацию о всез участниках.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public static OperationResult DeleteRacers()
        {
            try
            {                
                foreach(var racer in dc.Racers)
                {
                    dc.Racers.Remove(racer);
                }

                dc.SaveChanges();
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось удалить заданного участника.",
                                                        "Sprint.Managers.RacersManager.DeleteRacers()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        #endregion
    }
}
