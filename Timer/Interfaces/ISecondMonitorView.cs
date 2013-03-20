
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

        #endregion
    }
}
