using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NLog;

using Sprint.Data;

namespace Sprint.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public static class RacersManager
    {
        #region Поля только для чтения

        /// <summary>
        /// Контекст для работы с данными.
        /// </summary>
        private static readonly SprintEntities1 dc = new SprintEntities1();

        /// <summary>
        /// Логгер.
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Методы



        #endregion
    }
}
