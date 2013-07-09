using System;
using System.Collections.Generic;

using Sprint.Exceptions;

namespace Sprint.Models
{
    /// <summary>
    /// Модель, описывающая состояние заезда.
    /// А это какой какой заезд представляет, какие участники в нем участвуют 
    /// и класс автомобилей, в котором все это происходит.
    /// </summary>
    public class RaceStateModel
    {
        #region Свойства

        /// <summary>
        /// Задать или получить класс автомобилей, в котором 
        /// происходит все описанное в данной модели.
        /// </summary>
        public CarClassesEnum CarClass { get; set; }

        /// <summary>
        /// Задать или получить номер заезда, который описывает данная модель.
        /// </summary>
        public int RaceNumber { get; set; }

        /// <summary>
        /// Участники внутри заезда.
        /// </summary>
        public IEnumerable<Guid> Racers { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструкторы.
        /// </summary>
        public RaceStateModel(CarClassesEnum car_class, int race_number)
        {
            CarClass = car_class;
            RaceNumber = race_number;
            Racers = new List<Guid>();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавить в состояние заезда участника.
        /// </summary>
        /// <param name="racer">Добавляемый участник.</param>
        /// <returns>Результат выполнения операции.</returns>
        public OperationResult AddRacer(RacerModel racer)
        {
            try
            {
                var list = (List<Guid>) Racers;
                list.Add(racer.Id);
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось добавить участника в состояние заезда.", "Sprint.Models.RaceStateModel.AddRacer(RacerModel racer)", ex);
                return new OperationResult(false, exception);
            }
        }

        /// <summary>
        /// Добавить в состояние заезда участника.
        /// </summary>
        /// <param name="id_racer">ID добавляемого участника.</param>
        /// <returns>Результат выполнения операции.</returns>
        public OperationResult AddRacer(Guid id_racer)
        {
            try
            {
                var list = (List<Guid>)Racers;
                list.Add(id_racer);
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось добавить участника в состояние заезда.", 
                                                    "Sprint.Models.RaceStateModel.AddRacer(Guid id_racer)", ex);
                return new OperationResult(false, exception);
            }
        }

        /// <summary>
        /// Добавим в группу новых участников.
        /// </summary>
        /// <param name="new_racers">Добавляемые участники.</param>
        public OperationResult AddRacers(IEnumerable<RacerModel> new_racers)
        {
            try
            {
                foreach (var racer in new_racers)
                {
                    var list = (List<Guid>)Racers;
                    list.Add(racer.Id);
                }
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось добавить участников в состояние заезда.",
                                                    "Sprint.Models.RaceStateModel.AddRacers(IEnumerable<RacerModel> new_racers)", ex);
                return new OperationResult(false, exception);
            }
        }

        /// <summary>
        /// Добавим в группу новых участников.
        /// </summary>
        /// <param name="id_racers">Добавляемые участники.</param>
        public OperationResult AddRacers(IEnumerable<Guid> id_racers)
        {
            try
            {
                foreach (var id_racer in id_racers)
                {
                    var list = (List<Guid>)Racers;
                    list.Add(id_racer);
                }
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось добавить участников в состояние заезда.",
                                                    "Sprint.Models.RaceStateModel.AddRacers(IEnumerable<Guid> id_racers)", ex);
                return new OperationResult(false, exception);
            }
        }

        #endregion
    }
}
