using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using NLog;
using Sprint.Data;

namespace Sprint.Managers
{
    /// <summary>
    /// Менеджер для доступа к данным опций системы.
    /// </summary>
    public static class OptionsManager
    {
        #region Поля только для чтения

        /// <summary>
        /// Контекст для работы с данными.
        /// </summary>
        private static readonly SprintEntities1 dc = new SprintEntities1();

        /// <summary>
        /// Логгер.
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Методы

        /// <summary>
        /// Получить список всех опций гонок во всех классах автомобилей.
        /// </summary>
        /// <returns>Списко всех опций.</returns>
        public static IEnumerable<RacesOption> GetOptions()
        {
            throw new NotImplementedException();
            
            try
            {
                return dc.RacesOptions.ToList();
            }
            catch (Exception ex)
            {
                var message =   ex.TargetSite.Name + Environment.NewLine + 
                                ex.Message + Environment.NewLine + 
                                ex.InnerException + Environment.NewLine + 
                                ex.StackTrace;

                logger.Error(message);

                throw new Exception(message);
            }
        }

        /// <summary>
        /// Получить список всек опций гонок в заданном классе автомобилей.
        /// </summary>
        /// <param name="carClass"></param>
        /// <returns>Список опций в заданном классе автомобилей.</returns>
        public static IEnumerable<RacesOption> GetOptions(CarClass carClass)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Задать опции гонок.
        /// </summary>
        /// <param name="raceOption">Задаваемые опции.</param>
        /// <returns>Результат задания опций.</returns>
        public static bool SetOptions(RacesOption raceOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Удалить опции гонок.
        /// </summary>
        /// <param name="Id">Идентификатор удаляемых опций.</param>
        /// <returns>Результат удаления опций.</returns>
        public static bool DeleteOptions(int Id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
