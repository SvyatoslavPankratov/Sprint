using System.ComponentModel;

namespace Sprint.Models
{
    /// <summary>
    /// Модель опций гонок для заданного класса автомобилей.
    /// </summary>
    public class RaceOptionsModel
    {
        #region Свойства

        /// <summary>
        /// Задать или получить количество проводимых туров в текущем классе автомобилей.
        /// </summary>
        [Category("Заезды"), 
         DisplayName("Количество туров"), 
         Description("Количество проводимых туров в выбранном классе автомобилей. Минимум 0, максимум 2.")]
        public int RaceCount { get; set; }

        /// <summary>
        /// Задать или получить количество отбираемых лидеров для проведения финального тура.
        /// </summary>
        [Category("Заезды"), 
         DisplayName("Количество лидеров"), 
         Description("Количество отбираемых лидеров по результатам первого тура для проведения финального тура.")]
        public int LidersCount { get; set; }

        /// <summary>
        /// Задать или получить класс автомобилей для которых действуют данные опции.
        /// </summary>
        [Category("Заезды"), 
         DisplayName("Класс автомобилей"), 
         ReadOnly(true),
         Description("Класс автомобилей для которых действуют данные настройки.")]
        public CarClassesEnum CarClass { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="carClass">Класс автомобилей, для которых предназначены данные опции.</param>
        public RaceOptionsModel(CarClassesEnum carClass)
        {
            CarClass = carClass;
        }

        #endregion

        #region Методы



        #endregion							
    }
}
