using Sprint.Models;

namespace Sprint.Interfaces{
    
    /// <summary>
    /// Интерфейс второго монитора.
    /// </summary>
    public interface ISecondMonitor : IWindows
    {
        #region Свойства

        /// <summary>
        /// Задать номер участника готовящегося к выезду на трек.
        /// </summary>
        int NextRacerNumber { set; }

        /// <summary>
        /// Задать состояние готовности следующему участнику, который должен будет выходить на трек.
        /// </summary>
        NextRacerState NextRacerState { set; }

        /// <summary>
        /// Задать номер первого участника находящегося в данный момент на треке.
        /// </summary>
        int FirstCurrentRacerNumber { set; }

        /// <summary>
        /// Задать результат первого круга первого участника находящегося в данный момент на треке.
        /// </summary>
        TimeModel FirstRacer1Time { set; }

        /// <summary>
        /// Задать результат второго круга первого участника находящегося в данный момент на треке.
        /// </summary>
        TimeModel FirstRacer2Time { set; }

        /// <summary>
        /// Задать результат третьего круга первого участника находящегося в данный момент на треке.
        /// </summary>
        TimeModel FirstRacer3Time { set; }

        /// <summary>
        /// Задать номер второго участника находящегося в данный момент на треке.
        /// </summary>
        int SecondCurrentRacerNumber { set; }

        /// <summary>
        /// Задать результат первого круга второго участника находящегося в данный момент на треке.
        /// </summary>
        TimeModel SecondRacer1Time { set; }

        /// <summary>
        /// Задать результат второго круга второго участника находящегося в данный момент на треке.
        /// </summary>
        TimeModel SecondRacer2Time { set; }

        /// <summary>
        /// Задать результат третьего круга второго участника находящегося в данный момент на треке.
        /// </summary>
        TimeModel SecondRacer3Time { set; }

        #endregion

        #region Методы

        

        #endregion
    }
}
