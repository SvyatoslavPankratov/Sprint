using System.Data;

namespace Timer.Views
{
    interface IMainView
    {
        #region Properties

        /// <summary>
        /// Задать или получить таблицу с результатами.
        /// </summary>
        DataTable Results { get; set; }

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

        #endregion
    }
}
