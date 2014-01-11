using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Models;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class DeleteResultsDialogView : Form, IDeleteResultsDialog
    {
        #region Реализация интерфейса IDeleteResultsDialog

        /// <summary>
        /// Задать список классов автомобилей.
        /// </summary>
        public IEnumerable<CarClassesEnum?> CarClasses
        {
            set
            {
                car_classes_cb.Items.Clear();
                
                foreach (var cc in value)
                {
                    if (cc != null)
                    {
                        car_classes_cb.Items.Add(cc.ToString());
                    }
                    else
                    {
                        car_classes_cb.Items.Add(string.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Получить выбранный класс автомобилей для 
        /// которых будет производиться удаление результатов.
        /// </summary>
        public CarClassesEnum? SelectedCarClass
        {
            get
            {
                if (car_classes_cb.SelectedItem == null)
                {
                    return null;
                }

                return (CarClassesEnum)Enum.Parse(typeof(CarClassesEnum), car_classes_cb.SelectedItem.ToString());
            }
        }

        /// <summary>
        /// Задать или получить номер заезда, в выбранном классе автомобилей, у 
        /// всех учатсников которого будут удалены все результаты.
        /// </summary>
        public int RaceNumber
        {
            get
            {
                if (first_rb.Checked)
                {
                    return 1;
                }

                if (second_rb.Checked)
                {
                    return 2;
                }

                return 0;
            }
        }

        /// <summary>
        /// Получить флаг, говорящий о том надо ли сделать резервную 
        /// копию данных перед удалением результатов.
        /// </summary>
        public bool BackupData
        {
            get { return backup_data_cb.Checked; }
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить презентер диалога удаления результатов участников.
        /// </summary>
        private DeleteResultsDialogPresenter DeleteResultsDialogPresenter { get; set; }

        #endregion

        #region Конструтокры

        /// <summary>
        /// Конструктор.
        /// </summary>
        public DeleteResultsDialogView()
        {
            InitializeComponent();

            DeleteResultsDialogPresenter = new DeleteResultsDialogPresenter(this);
        }

        #endregion
        
        #region Методы

        /// <summary>
        /// Действия при нажатии пользователем на кнопку удалния результатов у участников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!first_rb.Checked && !second_rb.Checked)
            {
                MessageBox.Show("Выберите, пожалуйста номер заезда у участников которого будут удалены результаты.", "Валидация введенных данных", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion                
    }
}
