using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Sprint.Interfaces;
using Sprint.Managers;
using Sprint.Models;

namespace Sprint.Presenters
{
    /// <summary>
    /// Презентер окна восстановления данных приложения из резервной копии.
    /// </summary>
    public class RestoreAppDateFromBackupPresenter
    {
        #region Свойства

        /// <summary>
        /// Задать или получить интерфейс на представление диалога для 
        /// восстановления данных приложеня из резервной копии.
        /// </summary>
        private IRestoreAppDateFromBackup View { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="view">Представление диалога для восстановления данных приложеня из резервной копии.</param>
        public RestoreAppDateFromBackupPresenter(IRestoreAppDateFromBackup view)
        {
            View = view;

            LoadBackupFileList();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Восстановить данные приложения из файла резервной копии.
        /// </summary>
        /// <returns></returns>
        public OperationResult Restore()
        {
            if (View.SelectedFile != null)
            {
                DatabaseBackupManager.RestoreDatabaseBackups(View.SelectedFile.Name);

                return new OperationResult(true);
            }

            return new OperationResult(false);
        }

        /// <summary>
        /// Загрузить список всех файлов-резервных копий.
        /// </summary>
        private void LoadBackupFileList()
        {
            var backup_dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Backups");

            View.Files = backup_dir.GetFiles().OrderBy(f => f.CreationTime).ToList();
        }

        #endregion
    }
}
