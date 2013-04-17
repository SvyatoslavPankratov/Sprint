﻿using System;
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
        #region Свойства

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

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="carClass">Класс автомобилей в создаваемой группе.</param>
        public RacersGroupModel(CarClassesEnum carClass)
        {
            CarClass = carClass;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Передвинуть участника вверх на одну позицию.
        /// </summary>
        /// <param name="racerNum">Номер перемещаемого участника.</param>
        public void MoveUpRacer(int racerNum)
        {
            var racer = Racers.ElementAt(racerNum);
            var list = Racers.ToList();
            var index = list.IndexOf(racer);

            list.Remove(racer);
            list.Insert(index - 1, racer);

            Racers = list;
        }

        /// <summary>
        /// Передвинуть участника вниз на одну позицию.
        /// </summary>
        /// <param name="racerNum">Номер перемещаемого участника.</param>
        public void MoveDownRacer(int racerNum)
        {
            var racer = Racers.ElementAt(racerNum);
            var list = Racers.ToList();
            var index = list.IndexOf(racer);

            list.Remove(racer);
            list.Insert(index + 1, racer);

            Racers = list;
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
