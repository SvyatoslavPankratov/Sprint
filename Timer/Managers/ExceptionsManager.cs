using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Exceptions;

namespace Sprint.Managers
{
    public static class ExceptionsManager
    {
        #region Методы

        /// <summary>
        /// Собрать сообщение об исключении.
        /// </summary>
        /// <param name="ex">Исключение.</param>
        public static string CreateExceptionMessage(Exception ex)
        {
            var message = string.Empty;

            if (ex != null)
            {
                message += "Message: " + ex.Message;
                message += (string.IsNullOrWhiteSpace(message) && string.IsNullOrEmpty(message))
                                ? string.Empty
                                : Environment.NewLine + Environment.NewLine +
                                    "--------------------------------------------------------------------------------------" + Environment.NewLine +
                                    "StackTrace: " + Environment.NewLine + Environment.NewLine + 
                                    ex.StackTrace;

                message += CreateExceptionMessage(ex.InnerException);
            }

            message = ex == null
                        ? message
                        : "Method name: " + (ex is SprintException
                                                ? ((SprintException)ex).MethodName + Environment.NewLine + Environment.NewLine + message
                                                : ex.TargetSite == null
                                                    ? message
                                                    : ex.TargetSite.Name + Environment.NewLine + Environment.NewLine + message);

            return message;
        }

        #endregion
    }
}
