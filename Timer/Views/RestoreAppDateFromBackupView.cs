using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

using Sprint.Interfaces;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class RestoreAppDateFromBackupView : Form, IRestoreAppDateFromBackup
    {
        #region Реализация интерфейса IRestoreAppDateFromBackup

        /// <summary>
        /// Задать или получить список резервных копий базы данных приложения.
        /// </summary>
        public IEnumerable<FileInfo> Files
        {
            get { return BackupFiles; }
            set
            {
                BackupFiles = value;

                foreach (var backupFile in BackupFiles)
                {
                    fileNames_lb.Items.Add(backupFile.CreationTime.ToString("dd MMMM yyyy   HH:mm:ss"));
                }
            }
        }

        /// <summary>
        /// Получить выбранный файл резервной копии.
        /// </summary>
        public FileInfo SelectedFile
        {
            get { return BackupFiles.ElementAt(fileNames_lb.SelectedIndex); }
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить информацию о файлах резервных копий.
        /// </summary>
        private IEnumerable<FileInfo> BackupFiles { get; set; }

        /// <summary>
        /// Презентер окна восстановления данных приложения из резервной копии.
        /// </summary>
        private RestoreAppDateFromBackupPresenter Presenter { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public RestoreAppDateFromBackupView()
        {
            InitializeComponent();

            Presenter = new RestoreAppDateFromBackupPresenter(this);
        }

        #endregion
        
        #region Методы

        /// <summary>
        /// Действия при закрытии окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Действия при нажатии пользователем кнопки восстановления данных.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            var res = Presenter.Restore();

            if (res.Result)
            {
                Close();
                ((MainView) Application.OpenForms[0]).AutomationResetOrClose = true;
                Application.Restart();
                return;
            }

            MessageBox.Show("Не удалось произвести восстановление данных программы из заданного файла с резервной копией.", "Ошибка", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}
