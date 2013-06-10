using System.Collections.Generic;
using System.IO;

namespace Sprint.Interfaces
{
    public interface IRestoreAppDateFromBackup
    {
        #region Свойства

        /// <summary>
        /// Задать или получить список резервных копий базы данных приложения.
        /// </summary>
        IEnumerable<FileInfo> Files { get; set; }

        /// <summary>
        /// Получить выбранный файл резервной копии.
        /// </summary>
        FileInfo SelectedFile { get; }

        #endregion
    }
}
