using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Exceptions
{
    public class SprintException : Exception
    {
        #region Свойства

        /// <summary>
        /// Задать или получить имя метода из которого вылетело исключение.
        /// </summary>
        public string MethodName { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        public SprintException(string methodName)
        {
            MethodName = methodName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        public SprintException(string message, string methodName)
            : base(message)
        {
            MethodName = methodName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="innerException"></param>
        public SprintException(string message, string methodName, Exception innerException) 
            : base(message, innerException)
        {
            MethodName = methodName;
        }

        #endregion
    }
}
