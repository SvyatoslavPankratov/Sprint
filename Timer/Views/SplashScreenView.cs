using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Sprint.Interfaces;

namespace Sprint.Views
{
    public partial class SplashScreenView : Form, ISplashScreen
    {
        #region Реализация интерфейса ISplashScreen

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public SplashScreenView()
        {
            InitializeComponent();
        } 

        #endregion

        #region Методы

        /// <summary>
        /// Действия при закрытии формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplashScreenView_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity != 0)
            {
                Opacity -= 0.1;
                Thread.Sleep(100);
            }
        }

        #endregion
    }
}
