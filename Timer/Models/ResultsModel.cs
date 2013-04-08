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
        #region Поля

        int currentCircleNumber;

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить список результатов по кругам по этапам.
        /// </summary>
        public IEnumerable<IEnumerable<TimeModel>> ResultsList { get; private set; }

        /// <summary>
        /// Задать или получить флаг финиширования гонщика.
        /// </summary>
        public bool Finished { get; set; }
        
        /// <summary>
        /// Задать или получить номер проезжаемого круга.
        /// </summary>
        public int CurrentCircleNumber
        {
            get
            {
                return currentCircleNumber;
            }
            set
            {
                currentCircleNumber = value;

                if (currentCircleNumber == MaxCircleCount)
                {
                    Finished = true;
                }
            }
        }

        /// <summary>
        /// Задать или получить максимальное количество кругов в заезде.
        /// </summary>
        public int MaxCircleCount { get; set; }

        /// <summary>
        /// Задать или получить значение времени начала круга.
        /// </summary>
        public TimeModel StartTime { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="race_count">Количество заездов.</param>
        /// <param name="lap_count">Количество кругов в заезде.</param>
        public ResultsModel(int race_count, int lap_count)
        {
            MaxCircleCount = lap_count;

            var resultsList = new List<List<TimeModel>>(1); //race_count

            for (int race = 0; race < 1; race++)            //race_count
            {
                var race_TimeModel = new List<TimeModel>(lap_count);

                for (int lap = 0; lap < lap_count; lap++)
                {
                    race_TimeModel.Add(null);
                }

                resultsList.Add(race_TimeModel);
            }

            ResultsList = resultsList;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получить минимальное время заданного заезда.
        /// </summary>
        /// <param name="raceNumber">Номер заезда (начиная с 1).</param>
        /// <returns>Минимальное время за круг.</returns>
        public TimeModel GetMinTime(int raceNumber)
        {
            return (from result in (ResultsList as List<List<TimeModel>>)[raceNumber - 1]
                    where result.TimeSpan == (ResultsList as List<List<TimeModel>>)[raceNumber - 1].Min(r => r.TimeSpan)
                    select result).FirstOrDefault();
        }

        /// <summary>
        /// Добавить новое время результата.
        /// </summary>
        /// <param name="result">Добавляемый результат.</param>
        public void AddResult(int currentRace, TimeModel result)
        {
            (ResultsList as List<List<TimeModel>>)[currentRace][CurrentCircleNumber] = result;
            CurrentCircleNumber++;
        }

        #endregion
    }
}
