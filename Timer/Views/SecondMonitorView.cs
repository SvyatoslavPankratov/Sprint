using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class SecondMonitorView : Form, ISecondMonitorView
    {
        #region Реализация интерфейса ISecondMonitorView

        /// <summary>
        /// Задать номер участника готовящегося к выезду на трек.
        /// </summary>
        public int NextRacerNumber
        {
            set
            {
                if (value == 0)
                {
                    nextRacerNumber_L.Text = "-";
                }
                else
                {
                    nextRacerNumber_L.Text = value.ToString("D3");
                }
            }
        }

        /// <summary>
        /// Задать номер первого участника находящегося в данный момент на треке.
        /// </summary>
        public int FirstCurrentRacerNumber
        {
            set
            {
                if (value == 0)
                {
                    firstCurrentRacerNumber_L.Text = "-";
                }
                else
                {
                    firstCurrentRacerNumber_L.Text = value.ToString("D3");
                }
            }
        }

        /// <summary>
        /// Задать номер второго участника находящегося в данный момент на треке.
        /// </summary>
        public int SecondCurrentRacerNumber
        {
            set
            {
                if (value == 0)
                {
                    secondCurrentRacerNumber_L.Text = "-";
                }
                else
                {
                    secondCurrentRacerNumber_L.Text = value.ToString("D3");
                }
            }
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить презентер второго монитора.
        /// </summary>
        private SecondMonitorPresenter SecondMonitorPresenter { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public SecondMonitorView()
        {
            InitializeComponent();

            SecondMonitorPresenter = new SecondMonitorPresenter(this);
        }

        #endregion
    }
}
