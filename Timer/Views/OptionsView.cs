using System;
using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Presenters;
using Sprint.Models;
using System.Drawing;

namespace Sprint.Views
{
    public partial class OptionsView : Form, IOptions
    {
        #region Fields

        private object _raceOptionsForCarClass;

        private double _delayTime;

        #endregion

        #region Реализация интерфейса IOptionView

        /// <summary>
        /// Задать или получить выбранный класс автомобилей.
        /// </summary>
        public CarClassesEnum SelectedCarClass { get; set; }

        /// <summary>
        /// Задать или получить модель с опциями для выбранного класса автомобилей.
        /// </summary>
        public object RaceOptionsForCarClass
        {
            get { return _raceOptionsForCarClass; }
            set
            {
                _raceOptionsForCarClass = value;

                optionsPropertyGrid.SelectedObject = value;
            }
        }

        /// <summary>
        /// Задать или получить время задержки которая активизируется после защитаной отсечки, 
        /// но после которой в течении заданного времени все следующие отсечки игнорируются. 
        /// Этот параметр нужен для того, чтобы решить проблему с ложными отсечками, 
        /// происходящими сразу после корректной отсечки. 
        /// </summary>
        public double DelayTime
        {
            get { return _delayTime; }
            set
            {
                _delayTime = value;

                delay_time_tb.Text = value.ToString("F1");
            }
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить перезентер окна с опциями.
        /// </summary>
        private OptionsPresenter OptionsPresenter { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public OptionsView()
        {
            InitializeComponent();
            
            OptionsPresenter = new OptionsPresenter(this);
            listWithCarClasses.SelectedIndex = 0;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Действия при изменении класса автомобилей.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCarClass = (CarClassesEnum)Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), listWithCarClasses.SelectedItem.ToString());
            OptionsPresenter.ChangeCarClass(listWithCarClasses.SelectedItem.ToString());
        }

        /// <summary>
        /// Действия при нажатии пользователем кнопки закрыть окно натсроек.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Действия при нажатии пользоателем на кнопку применить (произвести сохранение всех опций в БД).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            var res = OptionsPresenter.SaveOptions();

            if (res.Result)
            {
                Close();
            }
        }

        /// <summary>
        /// Действия происходящие при изменении значения времени задержки для ложных отсечек.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DelayTime = double.Parse(delay_time_tb.Value.ToString());
        }

        #endregion     
     
    }
}
