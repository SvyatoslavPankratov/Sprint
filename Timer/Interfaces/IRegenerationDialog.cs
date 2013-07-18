using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Models;

namespace Sprint.Interfaces
{
    public interface IRegenerationDialog
    {
        #region Свойства

        /// <summary>
        /// Получить тип восстановления состояния приложения.
        /// </summary>
        AppRegenerationTypesEnum SelectedAppRegenerationType { get; }

        #endregion
    }
}
