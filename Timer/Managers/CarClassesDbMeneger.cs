using System;
using System.Collections.Generic;

using NLog;

using Sprint.Data;
using Sprint.Exceptions;

namespace Sprint.Managers
{
    /// <summary>
    /// Менеджер для доступа к данным классов автомобилей в БД.
    /// </summary>
    public static class CarClassesDbMeneger
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
        /// Получить список всех классов автомобилей, которые могут учавствовать в гонках.
        /// </summary>
        /// <returns>Списко всех классов автомобилей, которые могут учавствовать в гонках.</returns>
        public static IEnumerable<CarClass> GetCarClasses()
        {
            try
            {
                return dc.CarClasses;
            }
            catch (Exception ex)
            {
                var exception = new SprintDataException("Не удалось получить список всех классов автомобилей, которые могут учавствовать в гонках.",
                                                        "Sprint.Managers.CarClassesMeneger.GetCarClasses()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        #endregion
    }
}
