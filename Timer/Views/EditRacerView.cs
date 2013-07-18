using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Models;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class EditRacerView : Form, IEditRacer
    {
        #region Реализация интерфейса IEditRacer

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
            get { return (CarClassesEnum) Enum.Parse(typeof (CarClassesEnum), carClassesCB.SelectedItem.ToString()); }
            set { carClassesCB.SelectedItem = value.ToString(); }
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить презентер окна редактирования данных участника.
        /// </summary>
        private EditRacerPresenter Presenter { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public EditRacerView(RacerModel racer)
        {
            InitializeComponent();

            Presenter = new EditRacerPresenter(this, racer);
        }

        #endregion
        
        #region Методы

        /// <summary>
        /// Действия при закрытии диалогового окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Действия пользователя при применении изменений.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Presenter.UpdateRacer(FirstName, LastName, MiddleName, Manufacturer, Model, EngineSize, EnginePower, CarClass);
            Close();
        }

        #endregion
    }
}
