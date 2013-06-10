using System;
using System.IO;

using NLog;

using Sprint.Exceptions;
using Sprint.Models;

namespace Sprint.Managers
{

    /// <summary>
    /// Менеджер резервного архивирования базы данных приложения.
    /// </summary>
    public static class DatabaseBackupManager
    {
        #region Поля только для чтения

        /// <summary>
        /// Логгер.
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Методы

        /// <summary>
        /// Произвести резервное копирование базы данных приложения.
        /// </summary>
        /// <returns>Результат выполнения операции.</returns>
        public static OperationResult CreateNewDatabaseBackup()
        {
            try
            {
                var app_dir = AppDomain.CurrentDomain.BaseDirectory;

#if UT
                var backup_dir = new DirectoryInfo(@"..\..\..\UnitTest\bin\Debug\Backup");
                var db_dir = new DirectoryInfo(@"..\..\..\UnitTest\bin\Debug\Data");
#else
                var backup_dir = new DirectoryInfo(app_dir + @"\Backup");
                var db_dir = new DirectoryInfo(app_dir + @"\Data");
#endif

                // Если директории для резервных копий нету, то попробуем ее создать
                if (!backup_dir.Exists)
                {
                    backup_dir = Directory.CreateDirectory(backup_dir.FullName);

                    if (!backup_dir.Exists)
                    {
                        throw new SprintException("Не удалось создать директорию для создания резервной копии базы данных приложения.",
                                                    "Sprint.Managers.DatabaseBackupManager.CreateNewDatabaseBackup()");
                    }
                }

                var source_file = string.Format(@"{0}\Sprint.sdf", db_dir.FullName);
                var target_file = string.Format(@"{0}\Sprint [ {1} ].sdf", backup_dir.FullName, DateTime.Now.ToString("dd MMM yyyy HH-mm-ss"));

                File.Copy(source_file, target_file);

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось создать резервную копию базы данных приложения.",
                                                    "Sprint.Managers.DatabaseBackupManager.CreateNewDatabaseBackup()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Удалить все резервные копии базы данных приложения.
        /// </summary>
        /// <returns>Результат выполнения операции.</returns>
        public static OperationResult DeleteDatabaseBackups()
        {
            try
            {
                var app_dir = AppDomain.CurrentDomain.BaseDirectory;

#if UT
                var backup_dir = new DirectoryInfo(@"..\..\..\UnitTest\bin\Debug\Backup");
#else
                var backup_dir = new DirectoryInfo(app_dir + @"\Backup");
#endif

                // Если директории для резервных копий нету, то и удалять нечего, а если есть, то почитсим из нее все
                if (backup_dir.Exists)
                {
                    foreach (var file in backup_dir.GetFiles())
                    {
                        File.Delete(file.FullName);
                    }
                }

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось удалить резервные копии базы данных приложения.",
                                                    "Sprint.Managers.DatabaseBackupManager.DeleteDatabaseBackups()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Восстановить бд приложения из заданной резервной копии.
        /// </summary>
        /// <param name="backup_file_name">Имя резервной копии из которой будет восстанавливаться бд приложения.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static OperationResult RestoreDatabaseBackups(string backup_file_name)
        {
            try
            {
                if (string.IsNullOrEmpty(backup_file_name) || string.IsNullOrWhiteSpace(backup_file_name))
                {
                    var ex = new SprintException("Не задано имя файла с резервной копией.",
                                                    "Sprint.Managers.DatabaseBackupManager.RestoreDatabaseBackups()");
                }

                var app_dir = AppDomain.CurrentDomain.BaseDirectory;

#if UT
                var backup_dir = new DirectoryInfo(@"..\..\..\UnitTest\bin\Debug\Backup");
                var db_dir = new DirectoryInfo(@"..\..\..\UnitTest\bin\Debug\Data");
#else
                var backup_dir = new DirectoryInfo(app_dir + @"\Backup");
                var db_dir = new DirectoryInfo(app_dir + @"\Data");
#endif

                // Если директории для бд приложения нету, то попробуем ее создать
                if (!db_dir.Exists)
                {
                    backup_dir = Directory.CreateDirectory(db_dir.FullName);

                    if (!db_dir.Exists)
                    {
                        throw new SprintException("Не удалось создать директорию для базы данных приложения.",
                                                    "Sprint.Managers.DatabaseBackupManager.RestoreDatabaseBackups()");
                    }
                }

                var target_file = string.Format(@"{0}\Sprint.sdf", db_dir.FullName);
                var source_file = string.Format(@"{0}\{1}", backup_dir.FullName, backup_file_name);

                File.Copy(source_file, target_file, true);

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось восстановить базу данных приложения из резервной копии.",
                                                    "Sprint.Managers.DatabaseBackupManager.RestoreDatabaseBackups()", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        #endregion
    }
}
