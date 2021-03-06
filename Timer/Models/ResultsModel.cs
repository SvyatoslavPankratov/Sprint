﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Sprint.Models
{
    /// <summary>
    /// Модель результатов.
    /// </summary>
    public class ResultsModel
    {
        #region Свойства

        /// <summary>
        /// Задать или получить список результатов по кругам по этапам.
        /// </summary>
        public IEnumerable<IEnumerable<TimeModel>> ResultsList { get; private set; }

        /// <summary>
        /// Задать или получить номер проезжаемого круга начиная с 0.
        /// Номер элемента в списке соответсвует номеру заезда.
        /// </summary>
        private IEnumerable<int?> CurrentCircleNumber { get; set; }

        /// <summary>
        /// Задать или получить состояния заездов.
        /// </summary>
        private IEnumerable<RacerRaceStateEnum> RaceStates { get; set; } 

        /// <summary>
        /// Задать или получить значение времени начала круга.
        /// </summary>
        public TimeModel StartTime { get; set; }

        #endregion

        #region Конструкторы
        
        /// <summary>
        /// Конструктор.
        /// </summary>
        public ResultsModel()
        {
            InitializeObject();
            ResetState();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Инициализация объекта.
        /// </summary>
        private void InitializeObject()
        {
            CurrentCircleNumber = new List<int?>(2) {null, null};

            var resultsList = new List<List<TimeModel>>(ConstantsModel.MaxRaceCount);

            for (int race = 0; race < ConstantsModel.MaxRaceCount; race++)
            {
                var race_TimeModel = new List<TimeModel>(ConstantsModel.MaxCircleCount);

                for (int lap = 0; lap < ConstantsModel.MaxCircleCount; lap++)
                {
                    race_TimeModel.Add(null);
                }

                resultsList.Add(race_TimeModel);
            }

            ResultsList = resultsList;
        }

        /// <summary>
        /// Узнать имеет участник в заданном заезде какие-либо результаты.
        /// </summary>
        /// <param name="raceNumber">Номер заезда (начиная с 0).</param>
        /// <returns>Имеет-ли участник результаты в заданном заезде.</returns>
        public bool HasValues(int raceNumber)
        {
            var results = ResultsList.ElementAt(raceNumber);

            for (int i = 1; i < results.Count(); i++)
            {
                if (results.ElementAt(i) != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Получить минимальное время заданного заезда.
        /// </summary>
        /// <param name="raceNumber">Номер заезда (начиная с 0).</param>
        /// <returns>Минимальное время за круг.</returns>
        public TimeSpan? GetMinTime(int raceNumber)
        {
            TimeModel min = null;
            var results = ResultsList.ElementAt(raceNumber);

            for(int i = 1; i < results.Count(); i++)
            {
                var time = results.ElementAt(i);

                if(time != null)
                {
                    if (min == null)
                    {
                        min = time;
                    }
                    else if(min.TimeSpan > time.TimeSpan)
                    {
                        min = time;
                    }
                }
            }

            return min == null ? (TimeSpan?) null : min.TimeSpan;
        }

        /// <summary>
        /// Добавить новое время результата.
        /// </summary>
        /// <param name="currentRace">Номер тура результат гонщика в котором добавляется (начинается с 0).</param>
        /// <param name="result">Добавляемый результат.</param>
        public void AddResult(int currentRace, TimeModel result)
        {
            if (!GetCurrentCircleNumber(currentRace).HasValue)
            {
                SetCurrentCircleNumber(currentRace, 0);
            }

            if (ResultsList as List<List<TimeModel>> != null)
            {
                (ResultsList as List<List<TimeModel>>)[currentRace][GetCurrentCircleNumber(currentRace).Value] = result;
                SetCurrentCircleNumber(currentRace, GetCurrentCircleNumber(currentRace).Value + 1);
            }
        }

        /// <summary>
        /// Сбросить состояние счетчиков.
        /// </summary>
        public void ResetState()
        {
            var list_1 = new List<int?>(ConstantsModel.MaxRaceCount);

            for (int i = 0; i < CurrentCircleNumber.Count(); i++)
            {
                list_1.Add(null);
            }

            CurrentCircleNumber = list_1;


            var list_2 = new List<RacerRaceStateEnum>(ConstantsModel.MaxRaceCount);

            for (int i = 0; i < CurrentCircleNumber.Count(); i++)
            {
                list_2.Add(RacerRaceStateEnum.Run);
            }

            RaceStates = list_2;
        }

        /// <summary>
        /// Удалить все результаты всех заездов.
        /// </summary>
        public void Clear()
        {
            InitializeObject();
            ResetState();
        }

        /// <summary>
        /// Удалить все результаты заданного заезда.
        /// </summary>
        /// <param name="race_number">Номер заезда в котором будут удалены все результаты (Начиная с 0).</param>
        public void Clear(int race_number)
        {
            switch (race_number)
            {
                case 0:
                    {
                        Clear();
                    } break;
                case 1:
                    {
                        SetCurrentCircleNumber(1, null);
                        SetRaceState(0, RacerRaceStateEnum.Run);

                        var results = ResultsList.ElementAt(race_number) as List<TimeModel>;

                        for (int i = 0; i < ConstantsModel.MaxCircleCount; i++)
                        {
                            results[i] = null;
                        }
                    } break;
                default: break;
            }
        }

        /// <summary>
        /// Удалить последний результат в списке результатов участника.
        /// </summary>
        /// <param name="raceNumber">Номер заезда (начиная с 0).</param>
        public void DeleteLastResult(int raceNumber)
        {
            if (GetCurrentCircleNumber(raceNumber).HasValue)
            {
                var list = ResultsList.ElementAt(raceNumber) as List<TimeModel>;

                if (list != null)
                {
                    list[GetCurrentCircleNumber(raceNumber).Value - 1] = null;
                }
            }
        }

        /// <summary>
        /// Узнать финишировал-ли участник заданый номер заезда.
        /// </summary>
        /// <param name="race_number">Номер заезда (начиная с 0).</param>
        /// <returns>Результат.</returns>
        public bool IsFinished(int race_number)
        {
            var cn = GetCurrentCircleNumber(race_number);
            return cn.HasValue ? cn.Value == ConstantsModel.MaxCircleCount : false;
        }

        /// <summary>
        /// Получить номер проезжаемого круга (начиная с 0) в заданном заезде.
        /// </summary>
        /// <param name="race_number">Номер заезда.</param>
        /// <returns>Результат.</returns>
        public int? GetCurrentCircleNumber(int race_number)
        {
            return CurrentCircleNumber.ElementAt(race_number);
        }

        /// <summary>
        /// Задать номер проезжаемого круга (начиная с 0) в заданном заезде.
        /// </summary>
        /// <param name="race_number">Номер заезда.</param>
        /// <param name="value">Задаваемое значение.</param>
        public void SetCurrentCircleNumber(int race_number, int? value)
        {
            var list = CurrentCircleNumber as List<int?>;

            if (list != null)
            {
                list[race_number] = value;
            }
        }

        /// <summary>
        /// Задать значение состояния заданного заезда у участника.
        /// </summary>
        /// <param name="race_number">Заданный номер заезда (от 0 до 1)</param>
        /// <param name="state">Задаваемое значение заезда.</param>
        public void SetRaceState(int race_number, RacerRaceStateEnum state)
        {
            var list = RaceStates as List<RacerRaceStateEnum>;

            if (list != null)
            {
                list[race_number] = state;
            }
        }

        /// <summary>
        /// Получить состояние заданного заезда у участника.
        /// </summary>
        /// <param name="race_number">Заданный номер заезда (от 0 до 1).</param>
        /// <returns>Состояние заезда.</returns>
        public RacerRaceStateEnum GetRaceState(int race_number)
        {
            return RaceStates.ElementAt(race_number);
        }

        #endregion
    }
}
