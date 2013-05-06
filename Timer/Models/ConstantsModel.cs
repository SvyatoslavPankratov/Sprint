using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    public static class ConstantsModel
    {
        #region Константы

        /// <summary>
        /// Максимальное количество заездов.
        /// </summary>
        public const int MaxRaceCount = 2;

        /// <summary>
        /// Максимальное количество кругов в заезде.
        /// </summary>
        public const int MaxCircleCount = 4;

        /// <summary>
        /// Максимальное количество гонщиков одновременно присутствующих на треке.
        /// </summary>
        public const int MaxRacersForRace = 2;

        /// <summary>
        /// Количество проводимых туров по умолчанию в каждом классе автомобилей (максимум 2).
        /// </summary>
        public const int RaceCount = 2;

        /// <summary>
        /// Количество отбираемых лидеров по результатам первого тура для проведения финального тура.
        /// </summary>
        public const int LidersCount = 5;

        #endregion
    }
}
