using System;

using NLog;

using Sprint.Data;
using Sprint.Exceptions;
using Sprint.Models;

namespace Sprint.Managers
{
    /// <summary>
    /// Менеджер для доступа к данным логов системы.
    /// </summary>
    public static class LogsDbManager
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
        /// Удалить логи системы.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public static OperationResult DeleteLogs()
        {
            try
            {
                foreach (var log in dc.LogEntries)
                {
                    dc.LogEntries.Remove(log);
                }

                dc.SaveChanges();
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось удалить логи системы.",
                                                    "Sprint.Managers.LogsDbManager.DeleteLogs()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        #endregion
    }
}
