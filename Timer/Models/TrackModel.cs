using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    public class TrackModel
    {
        #region Свойства

        /// <summary>
        /// Задать или получить текущих гонщиков на треке.
        /// </summary>
        public IEnumerable<RacerModel> CurrentRacers { get; set; }

        /// <summary>
        /// Задать или получить номер гонщика, который должен будет пересечь отсечку 
        /// (0 или 1, в зависимости от того, сколько участников одновременно могут находиться на треке).
        /// </summary>
        public int CurrentRacerNum { get; set; }

        /// <summary>
        /// Задать или получить гонщика, который должен будет пересечь отсечку.
        /// </summary>
        public RacerModel CurrentRacer
        {
            get
            {
                if (CurrentRacers.Count() > 0 && CurrentRacers.Count() < 3)
                {
                    return CurrentRacers.ElementAt(CurrentRacerNum);
                }

                return null;
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="maxRacersForRace">Максимальное количество гонщиков одновременно присутствующих на треке.</param>
        public TrackModel(int maxRacersForRace)
        {
            CurrentRacers = new List<RacerModel>(maxRacersForRace);
        }

        #endregion

        #region Методы

        /// <summary>
        /// Проверить заданного гонщика на наличие его на треке в текущий момент времени.
        /// </summary>
        /// <param name="racer">Проверяемый участник.</param>
        /// <returns>Результат проверки.</returns>
        public bool CheckRacer(RacerModel racer)
        {
            return CurrentRacers.Any(r => r.Id == racer.Id);
        }

        /// <summary>
        /// Проверить заданного гонщика не он ли сейчас будет пересекать финишную прямую.
        /// </summary>
        /// <param name="racer">Проверяемый участник.</param>
        /// <returns>Результата проверки.</returns>
        public bool CheckCurrentRacer(RacerModel racer)
        {
            return CurrentRacer.Id == racer.Id;
        }

        #endregion
    }
}
