using System;
using System.Windows.Forms;

namespace Sprint.Views
{
    /// <summary>
    /// Часть класса отвечающая за работу с верхней строкой меню.
    /// </summary>
    public partial class MainView
    {
        #region Методы

        /// <summary>
        /// Действия при нажатии пользователем в меню проверки датчика отсечки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void проверкаДатчикаОтсечкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowHookManager.UnregisterHooks();

            var checkSensorView = new CheckSensorView();
            checkSensorView.ShowDialog();

            WindowHookManager.RegisterHooks(false, true);
        }

        /// <summary>
        /// Действия при нажатии пользователя в меню выгрузки всех данных в Excel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wnd = new ExportProcessView();

            BeginInvoke(new Action(wnd.Show));
            MainPresenter.ExportToExcel();
            BeginInvoke(new Action(wnd.Close));
        }

        /// <summary>
        /// Действия при нажатии пользователем кнопки очистки данных программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void очиститьДанныеПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var q_res = MessageBox.Show("Вы уверены, что хотите удалить все данные программы?", "Подтверждение действия",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (q_res == System.Windows.Forms.DialogResult.Yes)
            {
                var res = MainPresenter.DeleteData();

                if (!res.Result)
                {
                    MessageBox.Show("Не удалось удалить данные программы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                foreach (var window in Application.OpenForms)
                {
                    if (window != null && window is MainView)
                    {
                        ((MainView)window).AutomationResetOrClose = true;
                    }
                }

                Application.Restart();
            }
        }

        /// <summary>
        /// Действия при нажатии пользователем кнопки очистки логов программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void очиститьЛогиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var q_res = MessageBox.Show("Вы уверены, что хотите удалить все логи программы?", "Подтверждение действия",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (q_res == System.Windows.Forms.DialogResult.Yes)
            {
                var res = MainPresenter.DeleteLogs();

                if (!res.Result)
                {
                    MessageBox.Show("Не удалось удалить логи программы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Действия при нажатии пользователем в меню настройки программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void опцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wnd = new OptionsView();
            wnd.ShowDialog();
        }

        /// <summary>
        /// Действия при нажатии пользователем в меню печати результатов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void печатьРезультатовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wnd = new PrintView();
            wnd.Show();
        }

        /// <summary>
        /// Действия при нажатии пользователем по кнопке удаления всех резервных копий данных приложения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void удалитьРезервныеКопииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var q_res = MessageBox.Show("Вы уверены, что хотите удалить все резервные копии данных программы?", "Подтверждение действия",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (q_res == System.Windows.Forms.DialogResult.Yes)
            {
                var res_1 = MainPresenter.DeleteBackupFiles();
                //var res_2 удалим все резервные копии файлов Excel

                if (!res_1.Result)
                {
                    MessageBox.Show("Не удалось удалить резервные копии базы данных приложения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Действия при нажатии пользователем по кнопке восстановления данных программы из файла резервной копии.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void восстановитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new RestoreAppDateFromBackupView();
            dialog.ShowDialog();
        }

        #endregion
    }
}
