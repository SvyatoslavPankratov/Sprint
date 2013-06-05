using System;
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
        /// Получить флаг финиширования гонщика.
        /// </summary>
        public bool Finished
        {
            get { return CurrentCircleNumber == ConstantsModel.MaxCircleCount; }
        }

        /// <summary>
        /// Задать или получить номер проезжаемого круга начиная с 1.
        /// </summary>
        public int? CurrentCircleNumber { get; set; }

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
        public TimeSpan GetMinTime(int raceNumber)
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

            return min.TimeSpan;
        }

        /// <summary>
        /// Добавить новое время результата.
        /// </summary>
        /// <param name="currentRace">Номер тура результат гонщика в котором добавляется (начинается с 0).</param>
        /// <param name="result">Добавляемый результат.</param>
        public void AddResult(int currentRace, TimeModel result)
        {
            if (!CurrentCircleNumber.HasValue)
            {
                CurrentCircleNumber = 0;
            }

            if (ResultsList as List<List<TimeModel>> != null)
            {
                (ResultsList as List<List<TimeModel>>)[currentRace][CurrentCircleNumber.Value] = result;
                CurrentCircleNumber = CurrentCircleNumber.Value + 1;
            }
        }

        /// <summary>
        /// Сбросить состояние счетчиков.
        /// </summary>
        public void ResetState()
        {
            CurrentCircleNumber = null;
        }

        /// <summary>
        /// Очистить все результаты.
        /// </summary>
        public void Clear()
        {
            InitializeObject();
            ResetState();
        }

        #endregion
    }
}
