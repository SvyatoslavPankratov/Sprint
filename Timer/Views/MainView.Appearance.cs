using System;
using System.Drawing;
using System.Windows.Forms;

using Sprint.Models;
using Sprint.Interfaces;

namespace Sprint.Views
{
    /// <summary>
    /// Часть класса для работы с содержимым табличек.
    /// </summary>
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

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// первого заезда на автомобилях с мощностью до 100 л/с.
        /// </summary>
        public bool Editable_K100R1DGV { get; set; }

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// первого заезда на автомобилях с мощностью от 100 л/с до 160 л/с.
        /// </summary>
        public bool Editable_K160R1DGV { get; set; }

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// первого заезда на автомобилях с мощностью свыше 160 л/с.
        /// </summary>
        public bool Editable_KAR1DGV { get; set; }

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// второго заезда на автомобилях с мощностью до 100 л/с.
        /// </summary>
        public bool Editable_K100R2DGV { get; set; }

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// второго заезда на автомобилях с мощностью от 100 л/с до 160 л/с.
        /// </summary>
        public bool Editable_K160R2DGV { get; set; }

        /// <summary>
        /// Задать или получить возможность редактирования списка участников 
        /// второго заезда на автомобилях с мощностью свыше 160 л/с.
        /// </summary>
        public bool Editable_KAR2DGV { get; set; }

        #endregion

        #endregion

        #region Методы

        #region Внешний вид

        #region Фиксация выбранного класса автомобилей

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (передний привод 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_fwdR1DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.FWD, 0);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (задний привод 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_rwdR1DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.RWD, 0);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (полный привод 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Select_awdR1DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.AWD, 0);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (спорт 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_sportR1DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.Sport, 0);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (передний привод 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_fwdR2DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.FWD, 1);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (задний привод 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_rwdR2DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.RWD, 1);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (полный привод 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_awdR2DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.AWD, 1);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (спорт 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_sport2DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.Sport, 1);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (до 100 л/с 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_k100R1DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.K100, 0);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (до 100 л/с 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_k100R2DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.K100, 1);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (от 100 л/с до 160 л/с 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_k160R1DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.K160, 0);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (от 100 л/с до 160 л/с 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_k160R2DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.K160, 1);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (свыше 160 л/с 1 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_kaR1DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.KA, 0);
        }

        /// <summary>
        /// Фиксация текущего класса автомобилей, которые поедут заезд (свыше 160 л/с 2 заезд).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_kaR2DGV_Click(object sender, EventArgs e)
        {
            SetCurrentCarClass(CarClassesEnum.KA, 1);
        }

        /// <summary>
        /// Задать текущий класс автомобилей в презентере, которые поедут.
        /// </summary>
        /// <param name="car_class">Класс автомобилей.</param>
        /// <param name="race_number">Номер заезда.</param>
        private void SetCurrentCarClass(CarClassesEnum car_class, int race_number)
        {
            var info_message_text = GetMessageAboutFinishedCarClass(car_class, race_number);

            if (MainPresenter.CarClassIsFinished(car_class, race_number).Result)
            {
                MessageBox.Show(info_message_text, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GlobalCarClassSelectBtnEnable = !GlobalCarClassSelectBtnEnable;
            SetValueForAllButtonsForSelectCarClass(GlobalCarClassSelectBtnEnable);
            SetEnableValueSelectCarClassBtn(car_class, race_number);
            MainPresenter.CurrentCarClass = car_class;
            MainPresenter.CurrentRaceNum = race_number;
            ReverseStartBtnEnable();
        }

        /// <summary>
        /// Получить сообщение для диалогового окна о том, что в заданном классе автомобилей 
        /// в заданном заезде все участники уже завершили заезд.
        /// </summary>
        /// <param name="car_class">Класс автомобилей.</param>
        /// <param name="race_number">Номер заезда (начиная с 0).</param>
        /// <returns>Сообщение для вывода его в окно с сообщением.</returns>
        private string GetMessageAboutFinishedCarClass(CarClassesEnum car_class, int race_number)
        {
            switch (car_class)
            {
                case CarClassesEnum.FWD:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    return "Класс автомобилей с передним приводом уже закончил первый заезд.";
                                }
                            case 1:
                                {
                                    return "Класс автомобилей с передним приводом уже закончил второй заезд.";
                                }
                        }
                    } break;
                case CarClassesEnum.RWD:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    return "Класс автомобилей с задним приводом уже закончил первый заезд.";
                                }
                            case 1:
                                {
                                    return "Класс автомобилей с задним приводом уже закончил второй заезд.";
                                }
                        }
                    } break;
                case CarClassesEnum.AWD:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    return "Класс автомобилей с полним приводом уже закончил первый заезд.";
                                }
                            case 1:
                                {
                                    return "Класс автомобилей с полним приводом уже закончил второй заезд.";
                                }
                        }
                    } break;
                case CarClassesEnum.Sport:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    return "Класс спортивных автомобилей уже закончил первый заезд.";
                                }
                            case 1:
                                {
                                    return "Класс спортивных автомобилей уже закончил второй заезд.";
                                }
                        }
                    } break;
                case CarClassesEnum.K100:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    return "Класс автомобилей с мощностью до 100 л/с уже закончил первый заезд.";
                                }
                            case 1:
                                {
                                    return "Класс автомобилей с мощностью до 100 л/с уже закончил второй заезд.";
                                }
                        }
                    } break;
                case CarClassesEnum.K160:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    return "Класс автомобилей с мощностью от 100 л/с до 160 л/с уже закончил первый заезд.";
                                }
                            case 1:
                                {
                                    return "Класс автомобилей с мощностью от 100 л/с до 160 л/с уже закончил второй заезд.";
                                }
                        }
                    } break;
                case CarClassesEnum.KA:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    return "Класс автомобилей с мощностью свыше 160 л/с уже закончил первый заезд.";
                                }
                            case 1:
                                {
                                    return "Класс автомобилей с мощностью свыше 160 л/с уже закончил второй заезд.";
                                }
                        }
                    } break;
            }

            return string.Empty;
        }

        /// <summary>
        /// Задать значение доступности кнопки выбора класса автомобилей, которые будут выполнять заезд.
        /// </summary>
        /// <param name="car_class">Класс автомобилей.</param>
        /// <param name="race_number">Номер заезда (начиная с 0).</param>
        /// <returns>Сообщение для вывода его в окно с сообщением.</returns>
        private void SetEnableValueSelectCarClassBtn(CarClassesEnum car_class, int race_number)
        {
            switch (car_class)
            {
                case CarClassesEnum.FWD:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    selectFwdCarClassR1_BT.Enabled = true;
                                } break;
                            case 1:
                                {
                                    selectFwdCarClass2_BT.Enabled = true;
                                } break;
                        }
                    } break;
                case CarClassesEnum.RWD:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    selectRwdCarClass1_BT.Enabled = true;
                                } break;
                            case 1:
                                {
                                    selectRwdCarClass2_BT.Enabled = true;
                                } break;
                        }
                    } break;
                case CarClassesEnum.AWD:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    selectAwdCarClass1_BT.Enabled = true;
                                } break;
                            case 1:
                                {
                                    selectAwdCarClass2_BT.Enabled = true;
                                } break;
                        }
                    } break;
                case CarClassesEnum.Sport:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    selectSportCarClass1_BT.Enabled = true;
                                } break;
                            case 1:
                                {
                                    selectSportCarClass2_BT.Enabled = true;
                                } break;
                        }
                    } break;
                case CarClassesEnum.K100:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    selectK100CarClass1_BT.Enabled = true;
                                } break;
                            case 1:
                                {
                                    selectK100CarClass2_BT.Enabled = true;
                                } break;
                        }
                    } break;
                case CarClassesEnum.K160:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    selectK160CarClass1_BT.Enabled = true;
                                } break;
                            case 1:
                                {
                                    selectK160CarClass2_BT.Enabled = true;
                                } break;
                        }
                    } break;
                case CarClassesEnum.KA:
                    {
                        switch (race_number)
                        {
                            case 0:
                                {
                                    selectKACarClass1_BT.Enabled = true;
                                } break;
                            case 1:
                                {
                                    selectKACarClass2_BT.Enabled = true;
                                } break;
                        }
                    } break;
            }
        }

        /// <summary>
        /// Задать значение доступности у всех кнопок выбора текущего класса автомобилей.
        /// </summary>
        private void SetValueForAllButtonsForSelectCarClass(bool value)
        {
            selectFwdCarClassR1_BT.Enabled = value;
            selectFwdCarClass2_BT.Enabled = value;
            selectRwdCarClass1_BT.Enabled = value;
            selectRwdCarClass2_BT.Enabled = value;
            selectAwdCarClass1_BT.Enabled = value;
            selectAwdCarClass2_BT.Enabled = value;
            selectSportCarClass1_BT.Enabled = value;
            selectSportCarClass2_BT.Enabled = value;
            selectK100CarClass1_BT.Enabled = value;
            selectK100CarClass2_BT.Enabled = value;
            selectK160CarClass1_BT.Enabled = value;
            selectK160CarClass2_BT.Enabled = value;
            selectKACarClass1_BT.Enabled = value;
            selectKACarClass2_BT.Enabled = value;
        }

        /// <summary>
        /// Сделать кнопку запуска секундомера доступной.
        /// </summary>
        private void ReverseStartBtnEnable()
        {
            startBtn.Enabled = !startBtn.Enabled;
        }

        #endregion

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
        private void OnOffEditTable_Btn_fwdR1DGV_Click(object sender, EventArgs e)
        {
            if (!Editable_fwdR1DGV)
            {
                OffAllEditMode();
            }

            Editable_fwdR1DGV = !Editable_fwdR1DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.FWD;
            MainPresenter.CurrentEditRaceNumber = 0;

            editRacer_Btn_fwdR1DGV.Enabled = Editable_fwdR1DGV;
            addRacer_Btn_fwdR1DGV.Enabled = Editable_fwdR1DGV;
            upRacer_Btn_fwdR1DGV.Enabled = Editable_fwdR1DGV;
            downRacer_Btn_fwdR1DGV.Enabled = Editable_fwdR1DGV;
        } 

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на переднеприводных автомобилях вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_fwdR1DGV_Click(object sender, EventArgs e)
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
        private void downRacer_Btn_fwdR1DGV_Click(object sender, EventArgs e)
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

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_fwdR1DGV_Click(object sender, EventArgs e)
        {
            if (fwdR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = fwdR1DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            fwdR1DGV.ClearSelection();
            fwdR1DGV.Rows[index].Selected = true;
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
        private void selectRwdCarClass1_BT_Click(object sender, EventArgs e)
        {
            if (!Editable_fwdR2DGV)
            {
                OffAllEditMode();
            }

            Editable_fwdR2DGV = !Editable_fwdR2DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.FWD;
            MainPresenter.CurrentEditRaceNumber = 1;

            editRacer_Btn_fwdR2DGV.Enabled = Editable_fwdR2DGV;
            upRacer_Btn_fwdR2DGV.Enabled = Editable_fwdR2DGV;
            downRacer_Btn_fwdR2DGV.Enabled = Editable_fwdR2DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника второго заезда на переднеприводных автомобилях вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_fwdR2DGV_Click(object sender, EventArgs e)
        {
            if (fwdR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = fwdR2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                fwdR2DGV.ClearSelection();

                if (index > 0)
                {
                    fwdR2DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    fwdR2DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника второго заезда на переднеприводных автомобилях вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_fwdR2DGV_Click(object sender, EventArgs e)
        {
            if (fwdR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = fwdR2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                fwdR2DGV.ClearSelection();

                if (index < fwdR2DGV.Rows.Count - 1)
                {
                    fwdR2DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    fwdR2DGV.Rows[index].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_fwdR2DGV_Click(object sender, EventArgs e)
        {
            if (fwdR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = fwdR2DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            fwdR2DGV.ClearSelection();
            fwdR2DGV.Rows[index].Selected = true;
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
            if (!Editable_rwdR1DGV)
            {
                OffAllEditMode();
            }

            Editable_rwdR1DGV = !Editable_rwdR1DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.RWD;
            MainPresenter.CurrentEditRaceNumber = 0;

            editRacer_Btn_rwdR1DGV.Enabled = Editable_rwdR1DGV;
            addRacer_Btn_rwdR1DGV.Enabled = Editable_rwdR1DGV;
            upRacer_Btn_rwdR1DGV.Enabled = Editable_rwdR1DGV;
            downRacer_Btn_rwdR1DGV.Enabled = Editable_rwdR1DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_rwdR1DGV_Click(object sender, EventArgs e)
        {
            if (rwdR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = rwdR1DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            rwdR1DGV.ClearSelection();
            rwdR1DGV.Rows[index].Selected = true;
        }

        /// <summary>
        /// Действия при нажатии на кнопку добавить новых участников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addRacer_Btn_rwdR1DGV_Click(object sender, EventArgs e)
        {
            AddNewRacers(CarClassesEnum.RWD);
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на заднеприводных автомобилях вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_rwdR1DGV_Click(object sender, EventArgs e)
        {
            if (rwdR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = rwdR1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                rwdR1DGV.ClearSelection();

                if (index > 0)
                {
                    rwdR1DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    rwdR1DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на заднеприводных автомобилях вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_rwdR1DGV_Click(object sender, EventArgs e)
        {
            if (rwdR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = rwdR1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                rwdR1DGV.ClearSelection();

                if (index < fwdR1DGV.Rows.Count - 1)
                {
                    rwdR1DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    rwdR1DGV.Rows[index].Selected = true;
                }
            }
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
            if (!Editable_rwdR2DGV)
            {
                OffAllEditMode();
            }

            Editable_rwdR2DGV = !Editable_rwdR2DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.RWD;
            MainPresenter.CurrentEditRaceNumber = 1;

            editRacer_Btn_rwdR2DGV.Enabled = Editable_rwdR2DGV;
            upRacer_Btn_rwdR2DGV.Enabled = Editable_rwdR2DGV;
            downRacer_Btn_rwdR2DGV.Enabled = Editable_rwdR2DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_rwdR2DGV_Click(object sender, EventArgs e)
        {
            if (rwdR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = rwdR2DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            rwdR2DGV.ClearSelection();
            rwdR2DGV.Rows[index].Selected = true;
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника второго заезда на заднеприводных автомобилях вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_rwdR2DGV_Click(object sender, EventArgs e)
        {
            if (rwdR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = rwdR2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                rwdR2DGV.ClearSelection();

                if (index > 0)
                {
                    rwdR2DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    rwdR2DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника второго заезда на заднеприводных автомобилях вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_rwdR2DGV_Click(object sender, EventArgs e)
        {
            if (rwdR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = rwdR2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                rwdR2DGV.ClearSelection();

                if (index < rwdR2DGV.Rows.Count - 1)
                {
                    rwdR2DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    rwdR2DGV.Rows[index].Selected = true;
                }
            }
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
            if (!Editable_awdR1DGV)
            {
                OffAllEditMode();
            }

            Editable_awdR1DGV = !Editable_awdR1DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.AWD;
            MainPresenter.CurrentEditRaceNumber = 0;

            editRacer_Btn_awdR1DGV.Enabled = Editable_awdR1DGV;
            addRacer_Btn_awdR1DGV.Enabled = Editable_awdR1DGV;
            upRacer_Btn_awdR1DGV.Enabled = Editable_awdR1DGV;
            downRacer_Btn_awdR1DGV.Enabled = Editable_awdR1DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_awdR1DGV_Click(object sender, EventArgs e)
        {
            if (awdR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = awdR1DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            awdR1DGV.ClearSelection();
            awdR1DGV.Rows[index].Selected = true;
        }

        /// <summary>
        /// Действия при нажатии на кнопку добавить новых участников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addRacer_Btn_awdR1DGV_Click(object sender, EventArgs e)
        {
            AddNewRacers(CarClassesEnum.AWD);
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на полноприводных автомобилях вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_awdR1DGV_Click(object sender, EventArgs e)
        {
            if (awdR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = awdR1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                awdR1DGV.ClearSelection();

                if (index > 0)
                {
                    awdR1DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    awdR1DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на полноприводных автомобилях вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_awdR1DGV_Click(object sender, EventArgs e)
        {
            if (awdR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = awdR1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                awdR1DGV.ClearSelection();

                if (index < awdR1DGV.Rows.Count - 1)
                {
                    awdR1DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    awdR1DGV.Rows[index].Selected = true;
                }
            }
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
            if (!Editable_awdR2DGV)
            {
                OffAllEditMode();
            }

            Editable_awdR2DGV = !Editable_awdR2DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.AWD;
            MainPresenter.CurrentEditRaceNumber = 1;

            editRacer_Btn_awdR2DGV.Enabled = Editable_awdR2DGV;
            upRacer_Btn_awdR2DGV.Enabled = Editable_awdR2DGV;
            downRacer_Btn_awdR2DGV.Enabled = Editable_awdR2DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_awdR2DGV_Click(object sender, EventArgs e)
        {
            if (awdR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = awdR2DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            awdR2DGV.ClearSelection();
            awdR2DGV.Rows[index].Selected = true;
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника второго заезда на полноприводных автомобилях вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_awdR2DGV_Click(object sender, EventArgs e)
        {
            if (awdR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = awdR2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                awdR2DGV.ClearSelection();

                if (index > 0)
                {
                    awdR2DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    awdR2DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника второго заезда на полноприводных автомобилях вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_awdR2DGV_Click(object sender, EventArgs e)
        {
            if (awdR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = awdR2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                awdR2DGV.ClearSelection();

                if (index < awdR2DGV.Rows.Count - 1)
                {
                    awdR2DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    awdR2DGV.Rows[index].Selected = true;
                }
            }
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
            if (!Editable_sportR1DGV)
            {
                OffAllEditMode();
            }

            Editable_sportR1DGV = !Editable_sportR1DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.Sport;
            MainPresenter.CurrentEditRaceNumber = 0;

            editRacer_Btn_sportR1DGV.Enabled = Editable_sportR1DGV;
            addRacer_Btn_sportR1DGV.Enabled = Editable_sportR1DGV;
            upRacer_Btn_sportR1DGV.Enabled = Editable_sportR1DGV;
            downRacer_Btn_sportR1DGV.Enabled = Editable_sportR1DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку добавить новых участников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addRacer_Btn_sportR1DGV_Click(object sender, EventArgs e)
        {
            AddNewRacers(CarClassesEnum.Sport);
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_sportR1DGV_Click(object sender, EventArgs e)
        {
            if (sportR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = sportR1DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            sportR1DGV.ClearSelection();
            sportR1DGV.Rows[index].Selected = true;
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на спортивных автомобилях вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_sportR1DGV_Click(object sender, EventArgs e)
        {
            if (sportR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = sportR1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                sportR1DGV.ClearSelection();

                if (index > 0)
                {
                    sportR1DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    sportR1DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на спортивных автомобилях вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_sportR1DGV_Click(object sender, EventArgs e)
        {
            if (sportR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = sportR1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                sportR1DGV.ClearSelection();

                if (index < sportR1DGV.Rows.Count - 1)
                {
                    sportR1DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    sportR1DGV.Rows[index].Selected = true;
                }
            }
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
            if (!Editable_sportR2DGV)
            {
                OffAllEditMode();
            }

            Editable_sportR2DGV = !Editable_sportR2DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.Sport;
            MainPresenter.CurrentEditRaceNumber = 1;

            editRacer_Btn_sportR2DGV.Enabled = Editable_sportR2DGV;
            upRacer_Btn_sportR2DGV.Enabled = Editable_sportR2DGV;
            downRacer_Btn_sportR2DGV.Enabled = Editable_sportR2DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_sportR2DGV_Click(object sender, EventArgs e)
        {
            if (sportR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = sportR2DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            sportR2DGV.ClearSelection();
            sportR2DGV.Rows[index].Selected = true;
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника второго заезда на спортивных автомобилях вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_sportR2DGV_Click(object sender, EventArgs e)
        {
            if (sportR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = sportR2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                sportR2DGV.ClearSelection();

                if (index > 0)
                {
                    sportR2DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    sportR2DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника второго заезда на спортивных автомобилях вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_sportR2DGV_Click(object sender, EventArgs e)
        {
            if (sportR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = sportR2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                sportR2DGV.ClearSelection();

                if (index < sportR2DGV.Rows.Count - 1)
                {
                    sportR2DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    sportR2DGV.Rows[index].Selected = true;
                }
            }
        }

        #endregion

        #region Таблица первого заезда автомобилей мощностью до 100 л/с

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками первого заезда автомобилей мощностью до 100 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void k100R1DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in K100R1DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (K100R1DGV.Columns.Count > 0)
            {
                K100R1DGV.Columns[0].Width = 50;
                K100R1DGV.Columns[1].Width = 350;
                K100R1DGV.Columns[2].Width = 250;
                K100R1DGV.Columns[3].Width = 150;
                K100R1DGV.Columns[4].Width = 150;
                K100R1DGV.Columns[5].Width = 150;
                K100R1DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками первого заезда автомобилей мощностью до 100 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K100R1DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in K100R1DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// в первом заезде на автомобилях мощностью до 100 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOffEditTable_Btn_k100R1DGV_Click(object sender, EventArgs e)
        {
            if (!Editable_K100R1DGV)
            {
                OffAllEditMode();
            }

            Editable_K100R1DGV = !Editable_K100R1DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.K100;
            MainPresenter.CurrentEditRaceNumber = 0;

            editRacer_Btn_K100R1DGV.Enabled = Editable_K100R1DGV;
            addRacer_Btn_K100R1DGV.Enabled = Editable_K100R1DGV;
            upRacer_Btn_K100R1DGV.Enabled = Editable_K100R1DGV;
            downRacer_Btn_K100R1DGV.Enabled = Editable_K100R1DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку добавить новых участников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addRacer_Btn_K100R1DGV_Click(object sender, EventArgs e)
        {
            AddNewRacers(CarClassesEnum.K100);
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_K100R1DGV_Click(object sender, EventArgs e)
        {
            if (K100R1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = K100R1DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            K100R1DGV.ClearSelection();
            K100R1DGV.Rows[index].Selected = true;
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на автомобилях c мощностью до 100 л/с вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_k100R1DGV_Click(object sender, EventArgs e)
        {
            if (K100R1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = K100R1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                K100R1DGV.ClearSelection();

                if (index > 0)
                {
                    K100R1DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    K100R1DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на автомобилях c мощностью до 100 л/с вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_k100R1DGV_Click(object sender, EventArgs e)
        {
            if (K100R1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = K100R1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                K100R1DGV.ClearSelection();

                if (index < K100R1DGV.Rows.Count - 1)
                {
                    K100R1DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    K100R1DGV.Rows[index].Selected = true;
                }
            }
        }
        
        #endregion

        #region Таблица второго заезда автомобилей мощностью до 100 л/с

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками второго заезда автомобилей мощностью до 100 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void k100R2DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in K100R2DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (K100R2DGV.Columns.Count > 0)
            {
                K100R2DGV.Columns[0].Width = 50;
                K100R2DGV.Columns[1].Width = 350;
                K100R2DGV.Columns[2].Width = 250;
                K100R2DGV.Columns[3].Width = 150;
                K100R2DGV.Columns[4].Width = 150;
                K100R2DGV.Columns[5].Width = 150;
                K100R2DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками второго заезда автомобилей мощностью до 100 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K100R2DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in K100R2DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// в первом заезде на автомобилях мощностью до 100 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOffEditTable_Btn_k100R2DGV_Click(object sender, EventArgs e)
        {
            if (!Editable_K100R2DGV)
            {
                OffAllEditMode();
            }

            Editable_K100R2DGV = !Editable_K100R2DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.K100;
            MainPresenter.CurrentEditRaceNumber = 1;

            editRacer_Btn_K100R2DGV.Enabled = Editable_K100R2DGV;
            upRacer_Btn_K100R2DGV.Enabled = Editable_K100R2DGV;
            downRacer_Btn_K100R2DGV.Enabled = Editable_K100R2DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_K100R2DGV_Click(object sender, EventArgs e)
        {
            if (K100R2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = K100R2DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            K100R2DGV.ClearSelection();
            K100R2DGV.Rows[index].Selected = true;
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника второго заезда на автомобилях c мощностью до 100 л/с вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_k100R2DGV_Click(object sender, EventArgs e)
        {
            if (K100R2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = K100R2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                K100R2DGV.ClearSelection();

                if (index > 0)
                {
                    K100R2DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    K100R2DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника второго заезда на автомобилях c мощностью до 100 л/с вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_k100R2DGV_Click(object sender, EventArgs e)
        {
            if (K100R2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = K100R2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                K100R2DGV.ClearSelection();

                if (index < K100R2DGV.Rows.Count - 1)
                {
                    K100R2DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    K100R2DGV.Rows[index].Selected = true;
                }
            }
        }

        #endregion

        #region Таблица первого заезда автомобилей мощностью от 100 л/с до 160 л/с

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками первого заезда автомобилей мощностью от 100 л/с до 160 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void k160R1DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in K160R1DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (K160R1DGV.Columns.Count > 0)
            {
                K160R1DGV.Columns[0].Width = 50;
                K160R1DGV.Columns[1].Width = 350;
                K160R1DGV.Columns[2].Width = 250;
                K160R1DGV.Columns[3].Width = 150;
                K160R1DGV.Columns[4].Width = 150;
                K160R1DGV.Columns[5].Width = 150;
                K160R1DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками первого заезда автомобилей с мощностью от 100 л/с до 160 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K160R1DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in K160R1DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// в первом заезде на автомобилях мощностью от 100 л/с до 160 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOffEditTable_Btn_k160R1DGV_Click(object sender, EventArgs e)
        {
            if (!Editable_K160R1DGV)
            {
                OffAllEditMode();
            }

            Editable_K160R1DGV = !Editable_K160R1DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.K160;
            MainPresenter.CurrentEditRaceNumber = 0;

            editRacer_Btn_K160R1DGV.Enabled = Editable_K160R1DGV;
            addRacer_Btn_K160R1DGV.Enabled = Editable_K160R1DGV;
            upRacer_Btn_K160R1DGV.Enabled = Editable_K160R1DGV;
            downRacer_Btn_K160R1DGV.Enabled = Editable_K160R1DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку добавить новых участников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addRacer_Btn_K160R1DGV_Click(object sender, EventArgs e)
        {
            AddNewRacers(CarClassesEnum.K160);
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_K160R1DGV_Click(object sender, EventArgs e)
        {
            if (K160R1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = K160R1DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            K160R1DGV.ClearSelection();
            K160R1DGV.Rows[index].Selected = true;
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на автомобилях с мощностью от 100 л/с до 160 л/с вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_K160R1DGV_Click(object sender, EventArgs e)
        {
            if (K160R1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = K160R1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                K160R1DGV.ClearSelection();

                if (index > 0)
                {
                    K160R1DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    K160R1DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на автомобилях с мощностью от 100 л/с до 160 л/с вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_K160R1DGV_Click(object sender, EventArgs e)
        {
            if (K160R1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = K160R1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                K160R1DGV.ClearSelection();

                if (index < K160R1DGV.Rows.Count - 1)
                {
                    K160R1DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    K160R1DGV.Rows[index].Selected = true;
                }
            }
        }

        #endregion

        #region Таблица второго заезда автомобилей мощностью от 100 л/с до 160 л/с

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками второго заезда автомобилей мощностью от 100 л/с до 160 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void k160R2DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in K160R2DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (K160R2DGV.Columns.Count > 0)
            {
                K160R2DGV.Columns[0].Width = 50;
                K160R2DGV.Columns[1].Width = 350;
                K160R2DGV.Columns[2].Width = 250;
                K160R2DGV.Columns[3].Width = 150;
                K160R2DGV.Columns[4].Width = 150;
                K160R2DGV.Columns[5].Width = 150;
                K160R2DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками второго заезда автомобилей мощностью от 100 л/с до 160 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K160R2DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in K160R2DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// в первом заезде на автомобилях мощностью от 100 л/с до 160 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOffEditTable_Btn_k160R2DGV_Click(object sender, EventArgs e)
        {
            if (!Editable_K160R2DGV)
            {
                OffAllEditMode();
            }

            Editable_K160R2DGV = !Editable_K160R2DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.K160;
            MainPresenter.CurrentEditRaceNumber = 1;

            editRacer_Btn_K160R2DGV.Enabled = Editable_K160R2DGV;
            upRacer_Btn_K160R2DGV.Enabled = Editable_K160R2DGV;
            downRacer_Btn_K160R2DGV.Enabled = Editable_K160R2DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_K160R2DGV_Click(object sender, EventArgs e)
        {
            if (K160R2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = K160R2DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            K160R2DGV.ClearSelection();
            K160R2DGV.Rows[index].Selected = true;
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на автомобилях с мощностью от 100 л/с до 160 л/с вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_K160R2DGV_Click(object sender, EventArgs e)
        {
            if (K160R2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = K160R2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                K160R2DGV.ClearSelection();

                if (index > 0)
                {
                    K160R2DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    K160R2DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на автомобилях с мощностью от 100 л/с до 160 л/с вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_K160R2DGV_Click(object sender, EventArgs e)
        {
            if (K160R2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = K160R2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                K160R2DGV.ClearSelection();

                if (index < K160R2DGV.Rows.Count - 1)
                {
                    K160R2DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    K160R2DGV.Rows[index].Selected = true;
                }
            }
        }

        #endregion

        #region Таблица первого заезда автомобилей мощностью свыше 160 л/с

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками первого заезда автомобилей мощностью свыше 160 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kaR1DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in KAR1DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (KAR1DGV.Columns.Count > 0)
            {
                KAR1DGV.Columns[0].Width = 50;
                KAR1DGV.Columns[1].Width = 350;
                KAR1DGV.Columns[2].Width = 250;
                KAR1DGV.Columns[3].Width = 150;
                KAR1DGV.Columns[4].Width = 150;
                KAR1DGV.Columns[5].Width = 150;
                KAR1DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками первого заезда автомобилей мощностью свыше 160 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kaR1DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in KAR1DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// в первом заезде на автомобилях мощностью свыше 160 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOffEditTable_Btn_kaR1DGV_Click(object sender, EventArgs e)
        {
            if (!Editable_KAR1DGV)
            {
                OffAllEditMode();
            }

            Editable_KAR1DGV = !Editable_KAR1DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.KA;
            MainPresenter.CurrentEditRaceNumber = 0;

            editRacer_Btn_KAR1DGV.Enabled = Editable_KAR1DGV;
            addRacer_Btn_KAR1DGV.Enabled = Editable_KAR1DGV;
            upRacer_Btn_KAR1DGV.Enabled = Editable_KAR1DGV;
            downRacer_Btn_KAR1DGV.Enabled = Editable_KAR1DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку добавить новых участников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addRacer_Btn_KAR1DGV_Click(object sender, EventArgs e)
        {
            AddNewRacers(CarClassesEnum.KA);
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_KAR1DGV_Click(object sender, EventArgs e)
        {
            if (KAR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = KAR1DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            KAR1DGV.ClearSelection();
            KAR1DGV.Rows[index].Selected = true;
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на автомобилях с мощностью свыше 160 л/с вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_KAR1DGV_Click(object sender, EventArgs e)
        {
            if (KAR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = KAR1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                KAR1DGV.ClearSelection();

                if (index > 0)
                {
                    KAR1DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    KAR1DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на автомобилях с мощностью свыше 160 л/с вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_KAR1DGV_Click(object sender, EventArgs e)
        {
            if (KAR1DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = KAR1DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                KAR1DGV.ClearSelection();

                if (index < KAR1DGV.Rows.Count - 1)
                {
                    KAR1DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    KAR1DGV.Rows[index].Selected = true;
                }
            }
        }

        #endregion

        #region Таблица второго заезда автомобилей мощностью свыше 160 л/с

        /// <summary>
        /// Действия после изменении привязки данных к таблице с участниками второго заезда автомобилей мощностью свыше 160 л/с вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kaR2DGV_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in KAR2DGV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Padding = new Padding(2);
            }

            if (KAR2DGV.Columns.Count > 0)
            {
                KAR2DGV.Columns[0].Width = 50;
                KAR2DGV.Columns[1].Width = 350;
                KAR2DGV.Columns[2].Width = 250;
                KAR2DGV.Columns[3].Width = 150;
                KAR2DGV.Columns[4].Width = 150;
                KAR2DGV.Columns[5].Width = 150;
                KAR2DGV.Columns[6].Width = 150;
            }
        }

        /// <summary>
        /// Действия при перересовке таблицы с участниками второго заезда автомобилей мощностью свыше 160 л/с вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kaR2DGV_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in KAR2DGV.Rows)
            {
                row.Cells[0].Style.BackColor = Color.MediumAquamarine;
                row.Cells[1].Style.BackColor = Color.PowderBlue;
                row.Height = 30;
            }
        }

        /// <summary>
        /// Включить или отключить возможность редактирования списка гонщиков 
        /// в первом заезде на автомобилях мощностью свыше 160 л/с.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOffEditTable_Btn_kaR2DGV_Click(object sender, EventArgs e)
        {
            if (!Editable_KAR2DGV)
            {
                OffAllEditMode();
            }

            Editable_KAR2DGV = !Editable_KAR2DGV;
            MainPresenter.CurrentEditCarClass = CarClassesEnum.KA;
            MainPresenter.CurrentEditRaceNumber = 0;

            editRacer_Btn_KAR2DGV.Enabled = Editable_KAR2DGV;
            upRacer_Btn_KAR2DGV.Enabled = Editable_KAR2DGV;
            downRacer_Btn_KAR2DGV.Enabled = Editable_KAR2DGV;
        }

        /// <summary>
        /// Действия при нажатии на кнопку редактирования данных выделенного участника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRacer_Btn_KAR2DGV_Click(object sender, EventArgs e)
        {
            if (KAR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = KAR2DGV.SelectedRows[0].Index;
            EditRacerInfo(MainPresenter.GetRacerFromIndex(index));
            KAR2DGV.ClearSelection();
            KAR2DGV.Rows[index].Selected = true;
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на автомобилях с мощностью свыше 160 л/с вверх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upRacer_Btn_KAR2DGV_Click(object sender, EventArgs e)
        {
            if (KAR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = KAR2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveUpRacer(index))
            {
                KAR2DGV.ClearSelection();

                if (index > 0)
                {
                    KAR2DGV.Rows[index - 1].Selected = true;
                }
                else
                {
                    KAR2DGV.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии на кнопку передвинуть участника первого заезда на автомобилях с мощностью свыше 160 л/с вниз.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downRacer_Btn_KAR2DGV_Click(object sender, EventArgs e)
        {
            if (KAR2DGV.SelectedRows.Count == 0)
            {
                return;
            }

            var index = KAR2DGV.SelectedRows[0].Index;

            if (MainPresenter.MoveDownRacer(index))
            {
                KAR2DGV.ClearSelection();

                if (index < KAR2DGV.Rows.Count - 1)
                {
                    KAR2DGV.Rows[index + 1].Selected = true;
                }
                else
                {
                    KAR2DGV.Rows[index].Selected = true;
                }
            }
        }

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

        /// <summary>
        /// Редактировать данные участника.
        /// </summary>ч
        /// <param name="racer">Редактируемый участник.</param>
        private void EditRacerInfo(RacerModel racer)
        {
            if (racer == null)
            {
                return;
            }

            var dialog = new EditRacerView(racer);
            dialog.ShowDialog();

            var result = MainPresenter.UpdateRacer(racer);

            if (!result.Result)
            {
                MessageBox.Show("Не удалось обновить данные участника в базе данных приложения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MainPresenter.DataBind();
        }

        /// <summary>
        /// Выключить у всех таблиц режим редактирования.
        /// </summary>
        private void OffAllEditMode()
        {
            // Передний привод
            Editable_fwdR1DGV = false;
            editRacer_Btn_fwdR1DGV.Enabled = Editable_fwdR1DGV;
            addRacer_Btn_fwdR1DGV.Enabled = Editable_fwdR1DGV;
            upRacer_Btn_fwdR1DGV.Enabled = Editable_fwdR1DGV;
            downRacer_Btn_fwdR1DGV.Enabled = Editable_fwdR1DGV;
            
            Editable_fwdR2DGV = false;
            editRacer_Btn_fwdR2DGV.Enabled = Editable_fwdR1DGV;
            upRacer_Btn_fwdR2DGV.Enabled = Editable_fwdR1DGV;
            downRacer_Btn_fwdR2DGV.Enabled = Editable_fwdR1DGV;

            // Задний привод
            Editable_rwdR1DGV = false;
            editRacer_Btn_rwdR1DGV.Enabled = Editable_rwdR1DGV;
            addRacer_Btn_rwdR1DGV.Enabled = Editable_rwdR1DGV;
            upRacer_Btn_rwdR1DGV.Enabled = Editable_rwdR1DGV;
            downRacer_Btn_rwdR1DGV.Enabled = Editable_rwdR1DGV;

            Editable_rwdR2DGV = false;
            editRacer_Btn_rwdR2DGV.Enabled = Editable_rwdR2DGV;
            upRacer_Btn_rwdR2DGV.Enabled = Editable_rwdR2DGV;
            downRacer_Btn_rwdR2DGV.Enabled = Editable_rwdR2DGV;

            // Полний привод
            Editable_awdR1DGV = false;
            editRacer_Btn_awdR1DGV.Enabled = Editable_awdR1DGV;
            addRacer_Btn_awdR1DGV.Enabled = Editable_awdR1DGV;
            upRacer_Btn_awdR1DGV.Enabled = Editable_awdR1DGV;
            downRacer_Btn_awdR1DGV.Enabled = Editable_awdR1DGV;

            Editable_awdR2DGV = false;
            editRacer_Btn_awdR2DGV.Enabled = Editable_awdR2DGV;
            upRacer_Btn_awdR2DGV.Enabled = Editable_awdR2DGV;
            downRacer_Btn_awdR2DGV.Enabled = Editable_awdR2DGV;

            // Спорт
            Editable_sportR1DGV = false;
            editRacer_Btn_sportR1DGV.Enabled = Editable_sportR1DGV;
            addRacer_Btn_sportR1DGV.Enabled = Editable_sportR1DGV;
            upRacer_Btn_sportR1DGV.Enabled = Editable_sportR1DGV;
            downRacer_Btn_sportR1DGV.Enabled = Editable_sportR1DGV;

            Editable_sportR2DGV = false;
            editRacer_Btn_sportR2DGV.Enabled = Editable_sportR2DGV;
            upRacer_Btn_sportR2DGV.Enabled = Editable_sportR2DGV;
            downRacer_Btn_sportR2DGV.Enabled = Editable_sportR2DGV;
            
            //  До 100 л/с
            Editable_K100R1DGV = false;
            editRacer_Btn_K100R1DGV.Enabled = Editable_K100R1DGV;
            addRacer_Btn_K100R1DGV.Enabled = Editable_K100R1DGV;
            upRacer_Btn_K100R1DGV.Enabled = Editable_K100R1DGV;
            downRacer_Btn_K100R1DGV.Enabled = Editable_K100R1DGV;

            Editable_K100R2DGV = false;
            editRacer_Btn_K100R2DGV.Enabled = Editable_K100R2DGV;
            upRacer_Btn_K100R2DGV.Enabled = Editable_K100R2DGV;
            downRacer_Btn_K100R2DGV.Enabled = Editable_K100R2DGV;

            // От 100 л/с до 160 л/с
            Editable_K160R1DGV = false;
            editRacer_Btn_K160R1DGV.Enabled = Editable_K160R1DGV;
            addRacer_Btn_K160R1DGV.Enabled = Editable_K160R1DGV;
            upRacer_Btn_K160R1DGV.Enabled = Editable_K160R1DGV;
            downRacer_Btn_K160R1DGV.Enabled = Editable_K160R1DGV;

            Editable_K160R2DGV = false;
            editRacer_Btn_K160R2DGV.Enabled = Editable_K160R2DGV;
            upRacer_Btn_K160R2DGV.Enabled = Editable_K160R2DGV;
            downRacer_Btn_K160R2DGV.Enabled = Editable_K160R2DGV;

            // Свыше 160 л/с
            Editable_KAR1DGV = false;
            editRacer_Btn_KAR1DGV.Enabled = Editable_KAR1DGV;
            addRacer_Btn_KAR1DGV.Enabled = Editable_KAR1DGV;
            upRacer_Btn_KAR1DGV.Enabled = Editable_KAR1DGV;
            downRacer_Btn_KAR1DGV.Enabled = Editable_KAR1DGV;

            Editable_KAR2DGV = false;
            editRacer_Btn_KAR2DGV.Enabled = Editable_KAR2DGV;
            upRacer_Btn_KAR2DGV.Enabled = Editable_KAR2DGV;
            downRacer_Btn_KAR2DGV.Enabled = Editable_KAR2DGV;
        }

        #endregion

        #endregion
    }
}
