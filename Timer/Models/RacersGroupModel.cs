using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    /// <summary>
    /// Модель группы гонщиков.
    /// </summary>
    class RacersGroupModel
    {
        #region Properties

        /// <summary>
        /// Задать или получить класс автомобилей в данной группе.
        /// </summary>
        public CarClassesEnum CarClass { get; private set; }

        /// <summary>
        /// Задать или получить список гонщиков.
        /// </summary>
        public IEnumerable<RacerModel> Racers { get; set; }

        /// <summary>
        /// Задать или получить номер заезда.
        /// </summary>
        public int RaceNumber { get; set; }

        /// <summary>
        /// Задать или получить глобальную коллекцию гонщиков по всем классам автомобилей в сумме.
        /// </summary>
        public IEnumerable<RacerModel> GlobalRacers { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="carClass">Класс автомобилей в создаваемой группе.</param>
        public RacersGroupModel(CarClassesEnum carClass, IEnumerable<RacerModel> globalRacers)
        {
            CarClass = carClass;
            GlobalRacers = globalRacers;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Передвинуть участника вверх на одну позицию.
        /// </summary>
        /// <param name="racerNum">Номер перемещаемого участника.</param>
        public void MoveUpRacer(int racerNum)
        {
            var racer       = GlobalRacers.ElementAt(racerNum);
            var gl_racers   = GlobalRacers.ToList();
            var gl_index    = gl_racers.IndexOf(racer);

            for (int i = gl_index - 1; i >= 0; i--)
            {
                if (gl_racers[i].Car.CarClass == CarClass)
                {
                    gl_racers.Remove(racer);
                    gl_racers.Insert(i, racer);
                    break;
                }
            }

            GlobalRacers = gl_racers;
        }

        /// <summary>
        /// Передвинуть участника вниз на одну позицию.
        /// </summary>
        /// <param name="racerNum">Номер перемещаемого участника.</param>
        public void MoveDownRacer(int racerNum)
        {
            var racer       = GlobalRacers.ElementAt(racerNum);
            var gl_racers   = GlobalRacers.ToList();
            var gl_index    = gl_racers.IndexOf(racer);

            for (int i = gl_index + 1; i < gl_racers.Count; i++)
            {
                if (gl_racers[i].Car.CarClass == CarClass)
                {
                    gl_racers.Remove(racer);
                    gl_racers.Insert(i, racer);
                    break;
                }
            }

            GlobalRacers = gl_racers;
        }

        /// <summary>
        /// Получить коллекцию лидеров по текущей группе.
        /// </summary>
        /// <param name="count">Количество получаемых лидеров в заданном классе.</param>
        public IEnumerable<RacerModel> GetLeaders(int count)
        {
            var racers = (from racer in Racers
                          orderby racer.Results.GetMinTime(RaceNumber)
                          select racer).Reverse();

            return racers.Take(count);
        }

        #endregion
    }
}
