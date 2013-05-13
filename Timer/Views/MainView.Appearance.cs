using System;
using System.Drawing;
using System.Windows.Forms;

using Sprint.Models;
using Sprint.Views.Interfaces;

namespace Sprint.Views
{
    public partial class MainView
    {
        #region Свойства

        #region Свойства доступности по редактированию списка участников

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// первого заезда на переднеприводных автомобилях.
        /// </summary>
        public bool Editable_fwdR1DGV { get; set; }

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// первого заезда на заднеприводных автомобилях.
        /// </summary>
        public bool Editable_rwdR1DGV { get; set; }

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// первого заезда на полноприводных автомобилях.
        /// </summary>
        public bool Editable_awdR1DGV { get; set; }

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// первого заезда на спортивных автомобилях.
        /// </summary>
        public bool Editable_sportR1DGV { get; set; }

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// второго заезда на переднеприводных автомобилях.
        /// </summary>
        public bool Editable_fwdR2DGV { get; set; }

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// второго заезда на заднеприводных автомобилях.
        /// </summary>
        public bool Editable_rwdR2DGV { get; set; }

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// второго заезда на полноприводных автомобилях.
        /// </summary>
        public bool Editable_awdR2DGV { get; set; }

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// второго заезда на спортивных автомобилях.
        /// </summary>
        public bool Editable_sportR2DGV { get; set; }

        #endregion

        #endregion

        #region Методы

        #region Внешний вид

        #region Таблица первого заезда переднеприводных автомобилей

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками первого заезда на переднеприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fwdR1DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in fwdR1DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (fwdR1DGV.Columns.Count > 0)
            {
                fwdR1DGV.Columns[0].Width = 50;
                fwdR1DGV.Columns[1].Width = 350;
                fwdR1DGV.Columns[2].Width = 250;
                fwdR1DGV.Columns[3].Width = 150;
                fwdR1DGV.Columns[4].Width = 150;
                fwdR1DGV.Columns[5].Width = 150;
                fwdR1DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками первого заезда на переднеприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fwdR1DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in fwdR1DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// в первом заезде на переднеприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton49_Click(object sender, EventArgs e)
        {
            Editable_fwdR1DGV = !Editable_fwdR1DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.FWD;
            MainPresenter.CurrentEditRaceNumber = 1;

            addRacer_Btn_fwdR1DGV.Enabled = Editable_fwdR1DGV;
            upRacer_Btn_fwdR1DGV.Enabled = Editable_fwdR1DGV;
            downRacer_Btn_fwdR1DGV.Enabled = Editable_fwdR1DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на переднеприводных автомобилях вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (fwdR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = fwdR1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                fwdR1DGV.ClearSelection();

                if (index > 0)
                {
                    fwdR1DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    fwdR1DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на переднеприводных автомобилях вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            if (fwdR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = fwdR1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                fwdR1DGV.ClearSelection();

                if (index < fwdR1DGV.Rows.Count - 1)
                {
                    fwdR1DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    fwdR1DGV.Rows[index].Selected = true;
                }
            }
        } 

        /// <summary>
        /// Действия при нажатии на кнопку добавить новых участников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addRacer_Btn_fwdR1DGV_Click(object sender, EventArgs e)
        {
            AddNewRacers(CarClassesEnum.FWD);
        }

        #endregion

        #region Таблица первого заезда заднеприводных автомобилей

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками первого заезда на заднеприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rwdR1DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in rwdR1DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (rwdR1DGV.Columns.Count > 0)
            {
                rwdR1DGV.Columns[0].Width = 50;
                rwdR1DGV.Columns[1].Width = 350;
                rwdR1DGV.Columns[2].Width = 250;
                rwdR1DGV.Columns[3].Width = 150;
                rwdR1DGV.Columns[4].Width = 150;
                rwdR1DGV.Columns[5].Width = 150;
                rwdR1DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками первого заезда на заднеприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rwdR1DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in rwdR1DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// в первом заезде на заднеприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            Editable_rwdR1DGV = !Editable_rwdR1DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.FWD;
            MainPresenter.CurrentEditRaceNumber = 1;

            addRacer_Btn_rwdR1DGV.Enabled = Editable_rwdR1DGV;
            upRacer_Btn_rwdR1DGV.Enabled = Editable_rwdR1DGV;
            downRacer_Btn_rwdR1DGV.Enabled = Editable_rwdR1DGV;
        }

        #endregion

        #region Таблица первого заезда полноприводных автомобилей

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками первого заезда на полноприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void awdR1DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in awdR1DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (awdR1DGV.Columns.Count > 0)
            {
                awdR1DGV.Columns[0].Width = 50;
                awdR1DGV.Columns[1].Width = 350;
                awdR1DGV.Columns[2].Width = 250;
                awdR1DGV.Columns[3].Width = 150;
                awdR1DGV.Columns[4].Width = 150;
                awdR1DGV.Columns[5].Width = 150;
                awdR1DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками первого заезда на полноприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void awdR1DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in awdR1DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// в первом заезде на полноприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            Editable_awdR1DGV = !Editable_awdR1DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.FWD;
            MainPresenter.CurrentEditRaceNumber = 1;

            addRacer_Btn_awdR1DGV.Enabled = Editable_awdR1DGV;
            upRacer_Btn_awdR1DGV.Enabled = Editable_awdR1DGV;
            downRacer_Btn_awdR1DGV.Enabled = Editable_awdR1DGV;
        }

        #endregion

        #region Таблица первого заезда спортивных автомобилей

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками первого заезда на спортивных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sportR1DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in sportR1DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (sportR1DGV.Columns.Count > 0)
            {
                sportR1DGV.Columns[0].Width = 50;
                sportR1DGV.Columns[1].Width = 350;
                sportR1DGV.Columns[2].Width = 250;
                sportR1DGV.Columns[3].Width = 150;
                sportR1DGV.Columns[4].Width = 150;
                sportR1DGV.Columns[5].Width = 150;
                sportR1DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками первого заезда на спортивных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sportR1DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in sportR1DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// в первом заезде на спортивных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            Editable_sportR1DGV = !Editable_sportR1DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.FWD;
            MainPresenter.CurrentEditRaceNumber = 1;

            addRacer_Btn_sportR1DGV.Enabled = Editable_sportR1DGV;
            upRacer_Btn_sportR1DGV.Enabled = Editable_sportR1DGV;
            downRacer_Btn_sportR1DGV.Enabled = Editable_sportR1DGV;
        }

        #endregion

        #region Таблица второго заезда переднеприводных автомобилей

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками второго заезда на переднеприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fwdR2DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in fwdR2DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (fwdR2DGV.Columns.Count > 0)
            {
                fwdR2DGV.Columns[0].Width = 50;
                fwdR2DGV.Columns[1].Width = 350;
                fwdR2DGV.Columns[2].Width = 250;
                fwdR2DGV.Columns[3].Width = 150;
                fwdR2DGV.Columns[4].Width = 150;
                fwdR2DGV.Columns[5].Width = 150;
                fwdR2DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками второго заезда на переднеприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fwdR2DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in fwdR2DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// во втором заезде на переднеприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton36_Click(object sender, EventArgs e)
        {
            Editable_fwdR2DGV = !Editable_fwdR2DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.FWD;
            MainPresenter.CurrentEditRaceNumber = 1;

            addRacer_Btn_fwdR2DGV.Enabled = Editable_fwdR2DGV;
            upRacer_Btn_fwdR2DGV.Enabled = Editable_fwdR2DGV;
            downRacer_Btn_fwdR2DGV.Enabled = Editable_fwdR2DGV;
        }

        #endregion

        #region Таблица второго заезда заднеприводных автомобилей

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками второго заезда на заднеприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rwdR2DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in rwdR2DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (rwdR2DGV.Columns.Count > 0)
            {
                rwdR2DGV.Columns[0].Width = 50;
                rwdR2DGV.Columns[1].Width = 350;
                rwdR2DGV.Columns[2].Width = 250;
                rwdR2DGV.Columns[3].Width = 150;
                rwdR2DGV.Columns[4].Width = 150;
                rwdR2DGV.Columns[5].Width = 150;
                rwdR2DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками второго заезда на заднеприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rwdR2DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in rwdR2DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// во втором заезде на заднеприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Editable_rwdR2DGV = !Editable_rwdR2DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.FWD;
            MainPresenter.CurrentEditRaceNumber = 1;

            addRacer_Btn_rwdR2DGV.Enabled = Editable_rwdR2DGV;
            upRacer_Btn_rwdR2DGV.Enabled = Editable_rwdR2DGV;
            downRacer_Btn_rwdR2DGV.Enabled = Editable_rwdR2DGV;
        }

        #endregion

        #region Таблица второго заезда полноприводных автомобилей

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками второго заезда на полноприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void awdR2DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in awdR2DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            awdR2DGV.Columns[0].Width = 50;
            awdR2DGV.Columns[1].Width = 350;
            awdR2DGV.Columns[2].Width = 250;
            awdR2DGV.Columns[3].Width = 150;
            awdR2DGV.Columns[4].Width = 150;
            awdR2DGV.Columns[5].Width = 150;
            awdR2DGV.Columns[6].Width = 150;
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками второго заезда на полноприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void awdR2DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in awdR2DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// во втором заезде на полноприводных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            Editable_awdR1DGV = !Editable_awdR2DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.FWD;
            MainPresenter.CurrentEditRaceNumber = 1;

            addRacer_Btn_awdR2DGV.Enabled = Editable_awdR2DGV;
            upRacer_Btn_awdR2DGV.Enabled = Editable_awdR2DGV;
            downRacer_Btn_awdR2DGV.Enabled = Editable_awdR2DGV;
        }

        #endregion

        #region Таблица второго заезда спортивных автомобилей

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками второго заезда на спортивных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sportR2DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in sportR2DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (sportR2DGV.Columns.Count > 0)
            {
                sportR2DGV.Columns[0].Width = 50;
                sportR2DGV.Columns[1].Width = 350;
                sportR2DGV.Columns[2].Width = 250;
                sportR2DGV.Columns[3].Width = 150;
                sportR2DGV.Columns[4].Width = 150;
                sportR2DGV.Columns[5].Width = 150;
                sportR2DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками второго заезда на спортивных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sportR2DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in sportR2DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// во втором заезде на спортивных автомобилях.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            Editable_sportR2DGV = !Editable_sportR2DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.FWD;
            MainPresenter.CurrentEditRaceNumber = 1;

            addRacer_Btn_sportR2DGV.Enabled = Editable_sportR2DGV;
            upRacer_Btn_sportR2DGV.Enabled = Editable_sportR2DGV;
            downRacer_Btn_sportR2DGV.Enabled = Editable_sportR2DGV;
        }

        #endregion

        #region Таблица первого заезда автомобилей мощностью до 100 л/с



        #endregion

        #region Таблица второго заезда автомобилей мощностью до 100 л/с



        #endregion

        #region Таблица первого заезда автомобилей мощностью от 100 л/с до 160 л/с



        #endregion

        #region Таблица второго заезда автомобилей мощностью от 100 л/с до 160 л/с



        #endregion

        #region Таблица первого заезда автомобилей мощностью свыше 160 л/с



        #endregion

        #region Таблица второго заезда автомобилей мощностью свыше 160 л/с



        #endregion

        #endregion

        #region Общие методы

        /// <summary>
        /// Показать диалог добавления новых участников после чего 
        /// добавить новых участников к списку уже зарегистрированных участников.
        /// </summary>
        /// <param name="car_class_selected">Выбранный по умолчанию класс автомобилей.</param>
        private void AddNewRacers(CarClassesEnum car_class_selected)
        {
            var newRacerView = new NewRacerView();
            ((INewRacerView)newRacerView).CarClass = car_class_selected;
            var res = newRacerView.ShowDialog();

            if (res == DialogResult.OK)
            {
                var wnd = new AddedRacersProcessView();
                Invoke(new Action(() => wnd.Show()));

                MainPresenter.AddRacerAddNewRacer(newRacerView.NewRacerPresenter.Racers);

                Invoke(new Action(() => wnd.Close()));
            }
        }

        #endregion

        #endregion
    }
}
