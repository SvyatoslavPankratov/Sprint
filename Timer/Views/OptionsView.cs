using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sprint.Views
{
    public partial class OptionsView : Form
    {
        #region Свойства



        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public OptionsView()
        {
            InitializeComponent();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Действия при нажатии пользователем кнопки закрыть окно натсроек.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
