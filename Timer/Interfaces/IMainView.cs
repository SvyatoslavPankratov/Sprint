using System.Collections.Generic;
using System.Data;
using Sprint.Models;

namespace Sprint.Views.Interfaces
{
    /// <summary>
    /// Интерфейс главного окна.
    /// </summary>
    public interface IMainView
    {
        #region Свойства

        /// <summary>
        /// Задать или получить таблицу с результатами переднеприводных автомобилей за первый заезд.
        /// </summary>
        DataTable FwdFirstRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами переднеприводных автомобилей за второй заезд.
        /// </summary>
        DataTable FwdSecondRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами заднеприводных автомобилей за первый заезд.
        /// </summary>
        DataTable RwdFirstRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами заднеприводных автомобилей за второй заезд.
        /// </summary>
        DataTable RwdSecondRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами полноприводныэх автомобилей за первый заезд.
        /// </summary>
        DataTable AwdFirstRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами полноприводных автомобилей за второй заезд.
        /// </summary>
        DataTable AwdSecondRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами спортивных автомобилей за первый заезд.
        /// </summary>
        DataTable SportFirstRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами спортивных автомобилей за второй заезд.
        /// </summary>
        DataTable SportSecondRace { get; set; }

        /// <summary>
        /// Задать или получть количество минут.
        /// </summary>
        int Min { set; }

        /// <summary>
        /// Задать или получить количество секунд.
        /// </summary>
        int Sec { set; }

        /// <summary>
        /// Задать или получить количество милисекунд.
        /// </summary>
        int Mlsec { set; }

        /// <summary>
        /// Задать или получить список участников, которые были добавлены в диалоге регистрации участников.
        /// </summary>
        IEnumerable<RacerModel> AddedRacers { get; }

        #endregion
    }
}
