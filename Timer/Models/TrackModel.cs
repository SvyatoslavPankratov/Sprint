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
                if (CurrentRacers.Count() > 0)
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
    }
}
