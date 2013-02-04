using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Models;

namespace Sprint.Models
{
    /// <summary>
    /// Модель результатов.
    /// </summary>
    public class ResultsModel
    {
        #region Propeties

        /// <summary>
        /// Задать или получить список результатов по кругам по этапам.
        /// </summary>
        public List<List<TimeModel>> ResultsList { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="race_count">Количество заездов.</param>
        /// <param name="lap_count">Количество кругов в заезде.</param>
        public ResultsModel(int race_count, int lap_count)
        {
            ResultsList = new List<List<TimeModel>>(race_count);

            for (int i = 0; i < race_count; i++)
            {
                ResultsList[i] = new List<TimeModel>(lap_count);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Получить минимальное время заданного заезда.
        /// </summary>
        /// <param name="raceNumber">Номер заезда (начиная с 1).</param>
        /// <returns>Минимальное время за круг.</returns>
        public TimeModel GetMinTime(int raceNumber)
        {
            return (from result in ResultsList[raceNumber - 1]
                    where result.TimeSpan == ResultsList[raceNumber - 1].Min(r => r.TimeSpan)
                    select result).FirstOrDefault();
        }

        #endregion
    }
}
