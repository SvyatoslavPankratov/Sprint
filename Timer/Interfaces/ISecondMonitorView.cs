
namespace Sprint.Interfaces{
    
    /// <summary>
    /// Интерфейс второго монитора.
    /// </summary>
    public interface ISecondMonitorView
    {
        #region Свойства

        /// <summary>
        /// Задать номер участника готовящегося к выезду на трек.
        /// </summary>
        int NextRacerNumber { set; }

        /// <summary>
        /// Задать номер первого участника находящегося в данный момент на треке.
        /// </summary>
        int FirstCurrentRacerNumber { set; }

        /// <summary>
        /// Задать номер второго участника находящегося в данный момент на треке.
        /// </summary>
        int SecondCurrentRacerNumber { set; }

        #endregion
    }
}
