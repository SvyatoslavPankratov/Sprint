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

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        public TimeModel()
        {
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="time">Время для инициализации объекта.</param>
        public TimeModel(TimeSpan time) : this()
        {
            TimeSpan    = time;
            Min         = time.Minutes;
            Sec         = time.Seconds;
            Mlsec       = time.Milliseconds;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получить текстовое представление заданного времени.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Min.ToString("D3") + " : " + Sec.ToString("D2") + " : " + Mlsec.ToString("D3");
        }

        #endregion
    }
}
