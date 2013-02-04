using System.Data;

namespace Sprint.Views
{
    /// <summary>
    /// Интерфейс главного окна.
    /// </summary>
    public interface IMainView
    {
        #region Properties

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
        /// Задать или получить диалог для заполнения участников.
        /// </summary>
        NewRacerView NewRacerView { get; set; }

        #endregion
    }
}
