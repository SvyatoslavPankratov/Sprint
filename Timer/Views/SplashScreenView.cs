using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sprint.Interfaces;

namespace Sprint.Views
{
    public partial class SplashScreenView : Form, ISplashScreenView
    {
        #region Реализация интерфейса ISplashScreenView

        /// <summary>
        /// Закрыть сплеш экран.
        /// </summary>
        public void CloseSplashScreen()
        {
            Close();
        }

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
    }
}
