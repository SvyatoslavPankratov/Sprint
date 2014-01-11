﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Exceptions
{
    class SprintSystemException : SprintException
    {
        #region Конструкторы

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        public SprintSystemException(string methodName) 
            : base(methodName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        public SprintSystemException(string message, string methodName)
            : base(message, methodName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="innerException"></param>
        public SprintSystemException(string message, string methodName, Exception innerException) 
            : base(message, methodName, innerException)
        {
        }

        #endregion
    }
}
