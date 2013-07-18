using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using Sprint.Models;
using Sprint.Presenters;
using Sprint.Interfaces;

namespace Sprint.Views
{
    public partial class NewRacerView : Form, INewRacer
    {
        #region Реализация интерфейса INewRacerView

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
        /// Задать или получить марку автомобиля.
        /// </summary>
        public string Manufacturer
        {
            get { return manufacturer_TB.Text; }
            set { manufacturer_TB.Text = value; }
        }

        /// <summary>
        /// Задать или получить модель автомобиля.
        /// </summary>
        public string Model
        {
            get { return model_TB.Text; }
            set { model_TB.Text = value; }
        }

        /// <summary>
        /// Задать или получить объем двигателя.
        /// </summary>
        public double EngineSize
        {
            get { return double.Parse(engine_size_TB.Text); }
            set { engine_size_TB.Text = value.ToString("N1"); }
        }

        /// <summary>
        /// Задать или получить мощность двигателя.
        /// </summary>
        public double EnginePower
        {
            get { return double.Parse(engine_power_TB.Text); }
            set { engine_power_TB.Text = value.ToString("N1"); }
        }

        /// <summary>
        /// Задать классы автомобилей.
        /// </summary>
        public List<string> CarClasses
        {
            set
            {
                foreach (var item in value)
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
                return (CarClassesEnum)Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), carClassesCB.SelectedItem.ToString());
            }
            set
            {
                carClassesCB.SelectedItem = value.ToString();
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

        #region Свойства

        /// <summary>
        /// Задать или получить презентер для окна заполнения участников.
        /// </summary>
        public NewRacerPresenter NewRacerPresenter { get; set; }

        /// <summary>
        /// Задать или получить згначение индекса для какого-либо элемента.
        /// </summary>
        private int Index { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public NewRacerView()
        {
            InitializeComponent();

            NewRacerPresenter = new NewRacerPresenter(this);
        } 

        #endregion

        #region Методы

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
                string.IsNullOrEmpty(Manufacturer) || string.IsNullOrWhiteSpace(Manufacturer) ||
                string.IsNullOrEmpty(Model) || string.IsNullOrWhiteSpace(Model) ||
                EngineSize == 0.0 || EnginePower == 0.0 ||
                carClassesCB.SelectedItem == null)
            {
                MessageBox.Show("Введите пожалуйста фамилию, имя, отчетсво участника, класс автомобиля, его производителя, марку, объем двигателя и мощность.", 
                                "Проверка введенных данных",
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
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Действия при удалении пользователем гонщика из списка участников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void racersDGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Index = e.Row.Index;
        }

        /// <summary>
        /// Действия после удалении пользователем гонщика из списка участников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void racersDGV_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            NewRacerPresenter.DeleteRacer(Index);
        }

        #region Внешний вид
        
        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void racersDGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in racersDGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            racersDGV.Columns[0].Width = 50;
            racersDGV.Columns[1].Width = 150;
            racersDGV.Columns[2].Width = 150;
            racersDGV.Columns[3].Width = 150;
            racersDGV.Columns[4].Width = 250;
            racersDGV.Columns[5].Width = 60;


        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void racersDGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in racersDGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Height = 30;
            }
        }

        #endregion

        #endregion        
    }
}
