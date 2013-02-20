using System;

namespace Sprint.Models
{
    /// <summary>
    /// Модель времени на секундомере.
    /// </summary>
    public class TimeModel
    {
        #region Properties

        /// <summary>
        /// Задать или получить количество минут.
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Задать или получить количество секунд.
        /// </summary>
        public int Sec { get; set; }

        /// <summary>
        /// Задать или получить количесво миллисекунд.
        /// </summary>
        public int Mlsec { get; set; }

        /// <summary>
        /// Задать или получиьт время на секундомере в единой структуре.
        /// </summary>
        public TimeSpan TimeSpan { get; set; }

        #endregion
    }
}
