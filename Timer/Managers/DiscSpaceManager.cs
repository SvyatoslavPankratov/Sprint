using System;
using System.IO;

using NLog;

using Sprint.Exceptions;

namespace Sprint.Managers
{
	/// <summary>
	/// Менеджер по работе с дисковым пространством.
	/// </summary>
	public static class DiscSpaceManager
	{
        #region Поля только для чтения

        /// <summary>
        /// Логгер.
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

		#region Свойства

        

		#endregion

		#region Конструкторы



		#endregion

		#region Методы

        /// <summary>
        /// Получить количество свободного места на заданном логическом диске.
        /// </summary>
        /// <param name="disc_name">Имя логического диска.</param>
        /// <returns>Количество свободного места на диске.</returns>
        public static long GetFreeSpaceValue(string disc_name)
        {
            try
            {
                var drive = new DriveInfo(disc_name);
                return drive.AvailableFreeSpace;
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось получить информацию о количестве свободного места на заданном логическом диске.",
                                                        "Sprint.Managers.DiscSpaceManager.GetFreeSpaceValue()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

		#endregion
	}
}
