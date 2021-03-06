﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprint.Data;

namespace Sprint.Models
{
    /// <summary>
    /// Модель состояния приложения.
    /// </summary>
    public class ApplicationStateModel
    {
        #region Свойства

        /// <summary>
        /// Задать или получить состояния заедов по классам автомобилей.
        /// </summary>
        public IEnumerable<RaceStateModel> RaceStates { get; set; } 

        /// <summary>
        /// Задать или получить id всех гонщиков на треке в текущий момент времени.
        /// </summary>
        public IEnumerable<Guid> RacersAtTheTrack { get; set; }

        /// <summary>
        /// Задать или получить текущий класс автомобилей.
        /// </summary>
        public CarClassesEnum CurrentCarClass { get; set; }

        /// <summary>
        /// Задать или получить id текущего гонщика, который пересекет финишную прямую.
        /// </summary>
        public Guid? CurrentRacer { get; set; }

        /// <summary>
        /// Задать или получить номер проводимого тура.
        /// </summary>
        public int? CurrentRaceNumber { get; set; }
        
        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ApplicationStateModel()
        {
            RaceStates = new List<RaceStateModel>();
            RacersAtTheTrack = new List<Guid>();
        }

        #endregion
    }
}
