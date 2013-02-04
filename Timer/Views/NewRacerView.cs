using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sprint.Models;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class NewRacerView : Form, INewRacerView
    {
        #region Properties

        /// <summary>
        /// Задать или получить презентер для окна заполнения участников.
        /// </summary>
        public NewRacerPresenter NewRacerPresenter { get; set; }

        /// <summary>
        /// Задать или получить имя гонщика с формы.
        /// </summary>
        public string FirstName
        {
            get { return fnTB.Text; }
            set { fnTB.Text = value; }
        }

        /// <summary>
        /// Задать или получить фамилию гонщика с формы.
        /// </summary>
        public string LastName
        {
            get { return lnTB.Text; }
            set { lnTB.Text = value; }
        }

        /// <summary>
        /// Задать или получить отчетсво гонщика.
        /// </summary>
        public string MiddleName
        {
            get { return mnTB.Text; }
            set { mnTB.Text = value; }
        }

        /// <summary>
        /// Задать или получить наименование автомобиля участника.
        /// </summary>
        public string CarName
        {
            get { return carNameTB.Text; }
            set { carNameTB.Text = value; }
        }

        /// <summary>
        /// Задать классы автомобилей.
        /// </summary>
        public List<string> CarClasses
        {
            set
            {
                foreach(var item in value)
                {
                    carClassesCB.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Получить выбранный класс машины.
        /// </summary>
        public CarClassesEnum CarClass
        {
            get
            {
                return (CarClassesEnum) Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), carClassesCB.SelectedItem.ToString());
            }
        }

        /// <summary>
        /// Задать или получить таблицу с участниками.
        /// </summary>
        public DataTable RacersTable
        {
            get { return (DataTable)racersDGV.DataSource; }
            set { racersDGV.DataSource = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        public NewRacerView()
        {
            InitializeComponent();

            NewRacerPresenter = new NewRacerPresenter(this);
        } 

        #endregion

        #region Methods

        /// <summary>
        /// Добаивть нового участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newRacerB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrEmpty(MiddleName) || string.IsNullOrWhiteSpace(MiddleName) ||
                string.IsNullOrEmpty(CarName) || string.IsNullOrWhiteSpace(CarName) || 
                carClassesCB.SelectedItem == null)
            {
                MessageBox.Show("Введите пожалуйста фамилию, имя, отчетсво участника, наименование и класс его автомобиля.", "Проверка введенных данных",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            NewRacerPresenter.AddNewRacer();
        }

        /// <summary>
        /// Закончить регистрацию.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void completeRegistrationBT_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
