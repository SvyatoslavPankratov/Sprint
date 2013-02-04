using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    /// <summary>
    /// Модель человека.
    /// </summary>
    public class PeopleModel
    {
        #region Properties

        /// <summary>
        /// Задать или получить имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Задать или получить фамилию.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Задать или получить отчество.
        /// </summary>
        public string MiddleName { get; set; }

        #endregion
    }
}
