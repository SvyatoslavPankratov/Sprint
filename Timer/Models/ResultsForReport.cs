using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    public class ResultsForReport
    {
        #region Свойства
        
        /// <summary>
        /// Задать или получить ФИО участника.
        /// </summary>
        public string RacerName { get; set; }

        /// <summary>
        /// Задать или получить имя автомобиля участника.
        /// </summary>
        public string CarName { get; set; }

        /// <summary>
        /// Задать или получить номер заезда (тура).
        /// </summary>
        public int RaceNumber { get; set; }

        /// <summary>
        /// Задать или получить номер участника.
        /// </summary>
        public int RacerNumber { get; set; }

        /// <summary>
        /// Задать или получить время лучшего круга.
        /// </summary>
        public string MinTime { get; set; }

        /// <summary>
        /// Задать или получить время первого круга.
        /// </summary>
        public string Time1 { get; set; }

        /// <summary>
        /// Задать или получить время второго круга.
        /// </summary>
        public string Time2 { get; set; }

        /// <summary>
        /// Задать или получить время третьего круга.
        /// </summary>
        public string Time3 { get; set; }

        #endregion

        #region Конструкторы



        #endregion

        #region Методы



        #endregion				
    }
}
