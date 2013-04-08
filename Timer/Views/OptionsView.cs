using System;
using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Models;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class OptionsView : Form, IOptionsView
    {
        #region Fields

        private RaceOptionsModel _raceOptionsForCarClass;

        #endregion

        #region Реализация интерфейса IOptionView

        /// <summary>
        /// Задать или получить модель с опциями.
        /// </summary>
        public RaceOptionsModel RaceOptionsForCarClass
        {
            get { return _raceOptionsForCarClass; }
            set
            {
                _raceOptionsForCarClass = value;

                optionsPropertyGrid.SelectedObject = value;
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
            var cc = (CarClassesEnum) Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), listWithCarClasses.SelectedItem.ToString());

            OptionsPresenter.ChangeCarClass(cc);
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
            OptionsPresenter.SaveOptions();
        }

        #endregion
    }
}
