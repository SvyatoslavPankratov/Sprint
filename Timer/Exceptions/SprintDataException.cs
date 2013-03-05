using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Exceptions
{
    public class SprintDataException : SprintException
    {
        #region Конструкторы

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        public SprintDataException(string methodName) 
            : base(methodName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        public SprintDataException(string message, string methodName)
            : base(message, methodName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="methodName"></param>
        public SprintDataException(string message, Exception innerException, string methodName) 
            : base(message, innerException, methodName)
        {
        }

        #endregion
    }
}
