using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Managers;

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
        /// Задать или получить номер заезда начиная с 0.
        /// </summary>
        public int RaceNumber { get; set; }

        /// <summary>
        /// Получить значение финишировала-ли группа.
        /// </summary>
        public bool IsFinished
        {
            get { return Racers.All(racer => racer.Results.Finished); }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="carClass">Класс автомобилей в создаваемой группе.</param>
        public RacersGroupModel(CarClassesEnum carClass)
        {
            CarClass = carClass;
            Racers = new List<RacerModel>();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавим в группу нового участника.
        /// </summary>
        /// <param name="racer"></param>
        public void AddRacer(RacerModel racer)
        {
            var racers = (List<RacerModel>)Racers;
            racers.Add(racer);
            RacersDbManager.SetRacer(racer);
        }

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

        /// <summary>
        /// Получить следующего участника, который будет готовиться к выезду на трассу.
        /// </summary>
        /// <param name="last_racer">Последний участник, вышедший на трассу.</param>
        /// <returns>Участник, который будет готовиться к выезду на трассу.</returns>
        public RacerModel GetNextRacer(RacerModel last_racer)
        {
            if (last_racer == null)
            {
                foreach (var racer in Racers)
                {
                    if (!racer.Results.Finished)
                    {
                        return racer;
                    }
                }
            }
            else
            {
                for(int i = 0; i < Racers.Count(); i++)
                {
                    var racer = Racers.ElementAt(i);

                    if (racer.Id == last_racer.Id)
                    {
                        return Racers.Count() == i + 1 ? null : Racers.ElementAt(i + 1);
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
