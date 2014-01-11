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

                message += ex.TargetSite == null
                                ? string.Empty
                                : Environment.NewLine + Environment.NewLine +
                                    "Method name: " + ex.TargetSite.Name;

                message += string.IsNullOrEmpty(ex.StackTrace)
                                ? string.Empty
                                : Environment.NewLine + Environment.NewLine +
                                    "StackTrace: " + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine +
                                    "-----------------------------------------------------------------------------------------------------------------" + Environment.NewLine;

                message += Environment.NewLine + Environment.NewLine + CreateExceptionMessage(ex.InnerException);
            }

            return message;
        }

        #endregion
    }
}
