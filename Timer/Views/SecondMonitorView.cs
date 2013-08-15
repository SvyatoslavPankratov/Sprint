using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Models;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class SecondMonitorView : Form, ISecondMonitor
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

        /// <summary>
        /// Задать результат первого круга первого участника находящегося в данный момент на треке.
        /// </summary>
        public TimeModel FirstRacer1Time
        {
            set
            {
                if (value == null)
                {
                    R1_1_L.Text = "-";
                }
                else
                {
                    R1_1_L.Text = value.ToString();
                }
            }
        }

        /// <summary>
        /// Задать результат второго круга первого участника находящегося в данный момент на треке.
        /// </summary>
        public TimeModel FirstRacer2Time
        {
            set
            {
                if (value == null)
                {
                    R1_2_L.Text = "-";
                }
                else
                {
                    R1_2_L.Text = value.ToString();
                }
            }
        }

        /// <summary>
        /// Задать результат третьего круга первого участника находящегося в данный момент на треке.
        /// </summary>
        public TimeModel FirstRacer3Time
        {
            set
            {
                if (value == null)
                {
                    R1_3_L.Text = "-";
                }
                else
                {
                    R1_3_L.Text = value.ToString();
                }
            }
        }

        /// <summary>
        /// Задать результат первого круга второго участника находящегося в данный момент на треке.
        /// </summary>
        public TimeModel SecondRacer1Time
        {
            set
            {
                if (value == null)
                {
                    R2_1_L.Text = "-";
                }
                else
                {
                    R2_1_L.Text = value.ToString();
                }
            }
        }

        /// <summary>
        /// Задать результат второго круга второго участника находящегося в данный момент на треке.
        /// </summary>
        public TimeModel SecondRacer2Time
        {
            set
            {
                if (value == null)
                {
                    R2_2_L.Text = "-";
                }
                else
                {
                    R2_2_L.Text = value.ToString();
                }
            }
        }

        /// <summary>
        /// Задать результат третьего круга второго участника находящегося в данный момент на треке.
        /// </summary>
        public TimeModel SecondRacer3Time
        {
            set
            {
                if (value == null)
                {
                    R2_3_L.Text = "-";
                }
                else
                {
                    R2_3_L.Text = value.ToString();
                }
            }
        }

        /// <summary>
        /// Задать состояние готовности следующему участнику, который должен будет выходить на трек.
        /// </summary>
        public NextRacerState NextRacerState
        {
            set
            {
                switch (value)
                {
                    case NextRacerState.Stop:
                        {
                            StopPanel.Visible = true;
                            StartPanel.Visible = false;
                        } break;
                    case NextRacerState.Start:
                        {
                            StopPanel.Visible = false;
                            StartPanel.Visible = true;
                        } break;
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
