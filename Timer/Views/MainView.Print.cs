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
        private void Print_Btn_fwdR1DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.FWD, 0);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов переднеприводных автомобилей 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_fwdR2DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.FWD, 1);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов заднеприводных автомобилей 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_rwdR1DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.RWD, 0);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов заднеприводных автомобилей 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_rwdR2DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.RWD, 1);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов полноприводных автомобилей 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_awdR1DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.AWD, 0);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов полноприводных автомобилей 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_awdR2DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.AWD, 1);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов спортивных автомобилей 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_sportR1DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.Sport, 0);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов спортивных автомобилей 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_sportR2DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.Sport, 1);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов автомобилей с мощностью до 100 л/с 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_k100R1DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.K100, 0);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов автомобилей с мощностью до 100 л/с 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_k100R2DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.K100, 1);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов автомобилей с мощностью от 100 л/с до 160 л/с 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_k160R1DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.K160, 0);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов автомобилей с мощностью от 100 л/с до 160 л/с 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_k160R2DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.K160, 1);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов автомобилей с мощностью свыше 160 л/с 1-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_kaR1DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.KA, 0);
        }

        /// <summary>
        /// Действия при нажатии на печать результатов автомобилей с мощностью свыше 160 л/с 2-го тура.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Btn_kaR2DGV_Click(object sender, EventArgs e)
        {
            ShowPrintDialog(CarClassesEnum.KA, 1);
        }

        /// <summary>
        /// Показать диалог печати результатов.
        /// </summary>
        /// <param name="car_class">Класс автомобилей.</param>
        /// <param name="race_number">Номер заезда (начиная с 0).</param>
        private void ShowPrintDialog(CarClassesEnum car_class, int race_number)
        {
            Invoke(new Action(() =>
                        {
                            var wnd = new PrintView(car_class.ToString(), race_number, false);
                            wnd.Show();
                        }));
        }

        #endregion        
    }
}
