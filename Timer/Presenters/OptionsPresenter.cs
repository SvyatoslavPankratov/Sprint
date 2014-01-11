using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using Sprint.Interfaces;
using Sprint.Managers;
using Sprint.Models;

namespace Sprint.Presenters
{
    public class OptionsPresenter
    {
        #region Свойства

        /// <summary>
        /// Задать или получить интерфейс на форму с опциями.
        /// </summary>
        private IOptions OptionsView { get; set; }
        
        /// <summary>
        /// Задать или получить опции приложения.
        /// </summary>
        private AppOptionsModel AppOptions { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="optionsView">Интерфейс на форму с опциями.</param>
        public OptionsPresenter(IOptions optionsView)
        {
            OptionsView = optionsView;
            AppOptions = GetOptions();

            OptionsView.DelayTime = AppOptions.DelayTime;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Изменить класс автомобиля.
        /// </summary>
        /// <param name="carClass"></param>
        public void ChangeCarClass(string carClass)
        {
            if (AppOptions != null && AppOptions.RaceOptions != null)
            {
                OptionsView.RaceOptionsForCarClass = AppOptions.RaceOptions.FirstOrDefault(ro => ro.CarClass == OptionsView.SelectedCarClass);
            }
        }

        /// <summary>
        /// Загрузить все опции из БД.
        /// </summary>
        /// <returns></returns>
        private AppOptionsModel GetOptions()
        {
            var db_o = OptionsDbManager.GetOptions();

            if (db_o == null || db_o.RaceOptions == null || !db_o.RaceOptions.Any())
            {
                return CreateEmptyOptions();
            }
            
            var race_options = new List<RaceOptionsModel>();

            foreach (var cc in Enum.GetNames(Type.GetType("Sprint.Models.CarClassesEnum")))
            {
                var carClass = (CarClassesEnum)Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), cc);

                if(db_o.RaceOptions.Any(o => o.CarClass == carClass))
                {
                    var option = db_o.RaceOptions.First(o => o.CarClass == carClass);                    

                    race_options.Add(new RaceOptionsModel(carClass)
                                    {
                                        RaceCount = option.RaceCount,
                                        LidersCount = option.LidersCount
                                    });
                }
                else
                {
                    race_options.Add(new RaceOptionsModel(carClass)
                                    {
                                        RaceCount = ConstantsModel.RaceCount,
                                        LidersCount = ConstantsModel.LidersCount
                                    });
                }
            }

            var app_options = new AppOptionsModel { RaceOptions = race_options, DelayTime = db_o.DelayTime };

            return app_options;
        }

        /// <summary>
        /// Сохранить опции.
        /// </summary>
        /// <returns></returns>
        public OperationResult SaveOptions()
        {
            try
            {
                AppOptions.DelayTime = OptionsView.DelayTime;
                return OptionsDbManager.SetOptions(AppOptions);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось сохранить настройки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new OperationResult(false, ex);
            }
        }

        /// <summary>
        /// Сгенерировать пустые опции для приложения.
        /// </summary>
        /// <returns>Пустые опции для приложения.</returns>
        private AppOptionsModel CreateEmptyOptions()
        {
            var race_options = new List<RaceOptionsModel> 
                                    { 
                                        new RaceOptionsModel(CarClassesEnum.FWD) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount },
                                        new RaceOptionsModel(CarClassesEnum.RWD) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount },
                                        new RaceOptionsModel(CarClassesEnum.AWD) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount },
                                        new RaceOptionsModel(CarClassesEnum.Sport) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount },
                                        new RaceOptionsModel(CarClassesEnum.K100) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount },
                                        new RaceOptionsModel(CarClassesEnum.K160) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount },
                                        new RaceOptionsModel(CarClassesEnum.KA) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount }
                                    };

            var app_option = new AppOptionsModel
                                    {
                                        DelayTime = 0.0,
                                        RaceOptions = race_options
                                    };

            return app_option;
        }

        #endregion
    }
}
