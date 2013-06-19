using System;

using Sprint.Models;

namespace Sprint.Views
{
    /// <summary>
    /// Часть класса, отвечающая за печать.
    /// </summary>
    public partial class MainView
    {
        #region Методы

        /// <summary>
        /// Действия при нажатии на печать результатов переднеприводных автомобилей 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
                {
                    var wnd = new PrintView(CarClassesEnum.FWD.ToString(), 1, false);
                    wnd.Show();
                }));
        }

        /// <summary>
        /// Действия при нажатии на печать результатов переднеприводных автомобилей 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.FWD.ToString(), 2, false);
            this.Invoke(new Action(wnd.Show));
        }

        /// <summary>
        /// Действия при нажатии на печать результатов заднеприводных автомобилей 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.RWD.ToString(), 1, false);
            this.Invoke(new Action(wnd.Show));
        }

        /// <summary>
        /// Действия при нажатии на печать результатов заднеприводных автомобилей 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.RWD.ToString(), 2, false);
            this.Invoke(new Action(wnd.Show));
        }

        /// <summary>
        /// Действия при нажатии на печать результатов полноприводных автомобилей 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.AWD.ToString(), 1, false);
            this.Invoke(new Action(wnd.Show));
        }

        /// <summary>
        /// Действия при нажатии на печать результатов полноприводных автомобилей 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.AWD.ToString(), 2, false);
            this.Invoke(new Action(wnd.Show));
        }

        /// <summary>
        /// Действия при нажатии на печать результатов спортивных автомобилей 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.Sport.ToString(), 1, false);
            this.Invoke(new Action(wnd.Show));
        }

        /// <summary>
        /// Действия при нажатии на печать результатов спортивных автомобилей 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView(CarClassesEnum.Sport.ToString(), 2, false);
            this.Invoke(new Action(wnd.Show));
        }

        #endregion        
    }
}
