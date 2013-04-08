using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class PrintView : Form, IPrintView
    {
        #region Реализация интерфейса IPrintView

        /// <summary>
        /// Задать классы автомобилей.
        /// </summary>
        public IEnumerable<string> CarClasses
        {
            set { carClassesLB.DataSource = value; }
        }

        /// <summary>
        /// Задать доступные номера заездов в выбранном классе автомобилей.
        /// </summary>
        public IEnumerable<int> RacesNumbers
        {
            set { raceNumberLB.DataSource = value; }
        }

        /// <summary>
        /// Получить выбранный класс автомобилей.
        /// </summary>
        public string SelectedCarClass
        {
            get { return carClassesLB.SelectedItem.ToString(); }
        }

        /// <summary>
        /// Получить выбранный номер заезда в выбранном классе автомобилей.
        /// </summary>
        public int SelectedRaceNumber { get { return int.Parse(raceNumberLB.SelectedItem.ToString()); } }

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить презентер печати.
        /// </summary>
        private PrintPresenter PrintPresenter { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public PrintView()
        {
            InitializeComponent();

            PrintPresenter = new PrintPresenter(this);

            carClassesLB_SelectedIndexChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="enableEdit"></param>
        public PrintView(bool enableEdit)
            : this()
        {
            carClassesLB.Enabled = enableEdit;
            raceNumberLB.Enabled = enableEdit;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="carClass"></param>
        /// <param name="raceNumber"></param>
        public PrintView(string carClass, int raceNumber)
            : this()
        {
            carClassesLB.SelectedItem = carClass;
            raceNumberLB.SelectedItem = raceNumber;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="carClass"></param>
        /// <param name="raceNumber"></param>
        public PrintView(string carClass, int raceNumber, bool enableEdit)
            : this(enableEdit)
        {
            carClassesLB.SelectedItem = carClass;
            raceNumberLB.SelectedItem = raceNumber;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Действия при нажатии на кнопку закрытии окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Действия при изменеии класса автомбилей.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carClassesLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PrintPresenter != null)
            {
                PrintPresenter.SetRaceNumbers();
                RefreshReport();
            }
        }

        /// <summary>
        /// Действие при изменении номера заезда.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void raceNumberLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshReport();
        }

        /// <summary>
        /// Действия при загрузке представения отчетов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintView_Load(object sender, EventArgs e)
        {
            RefreshReport();
        }

        /// <summary>
        /// Обновление отчета.
        /// </summary>
        private void RefreshReport()
        {
            reportViewer.RefreshReport();
        }

        #endregion
    }
}
