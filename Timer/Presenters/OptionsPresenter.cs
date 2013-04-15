﻿using System;
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
        #region Константы

        /// <summary>
        /// Количество проводимых туров по умолчанию в каждом классе автомобилей (максимум 2).
        /// </summary>
        private const int RaceCount = 2;

        /// <summary>
        /// Количество отбираемых лидеров по результатам первого тура для проведения финального тура.
        /// </summary>
        private const int LidersCount = 5;

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить интерфейс на форму с опциями.
        /// </summary>
        private IOptionsView OptionsView { get; set; }

        /// <summary>
        /// Задать или получить опции гонок по классам автомобилей.
        /// </summary>
        private IEnumerable<RaceOptionsModel> RaceOptions { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="optionsView">Интерфейс на форму с опциями.</param>
        public OptionsPresenter(IOptionsView optionsView)
        {
            OptionsView = optionsView;
            RaceOptions = GetOptions();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Изменить класс автомобиля.
        /// </summary>
        /// <param name="carClass"></param>
        public void ChangeCarClass(string carClass)
        {
            var cc = (CarClassesEnum)Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), carClass);

            OptionsView.RaceOptionsForCarClass = RaceOptions.FirstOrDefault(ro => ro.CarClass == cc);
        }

        /// <summary>
        /// Загрузить все опции из БД.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<RaceOptionsModel> GetOptions()
        {
            var db_o = OptionsDbManager.GetOptions();

            if (!db_o.Any())
            {
                return CreateEmptyOptions();
            }

            var options = new List<RaceOptionsModel>();

            foreach (var cc in Enum.GetNames(Type.GetType("Sprint.Models.CarClassesEnum")))
            {
                var carClass = (CarClassesEnum)Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), cc);

                if(db_o.Any(o => o.CarClass == carClass))
                {
                    var option = db_o.First(o => o.CarClass == carClass);                    

                    options.Add(new RaceOptionsModel(carClass)
                                    {
                                        RaceCount = option.RaceCount,
                                        LidersCount = option.LidersCount
                                    });
                }
                else
                {
                    options.Add(new RaceOptionsModel(carClass)
                                    {
                                        RaceCount = RaceCount,
                                        LidersCount = LidersCount
                                    });
                }
            }

            return options;
        }

        /// <summary>
        /// Сохранить опции.
        /// </summary>
        /// <returns></returns>
        public OperationResult SaveOptions()
        {
            try
            {
                foreach (var options in RaceOptions)
                {
                    var res = OptionsDbManager.SetOptions(options);

                    if (!res.Result)
                    {
                        throw new Exception(res.Details, res.Exception);
                    }
                }
                return new OperationResult(true);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось сохранить настройки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new OperationResult(false, ex);
            }
        }

        /// <summary>
        /// Сгенерировать список пустых опций по классам автомобилей.
        /// </summary>
        /// <returns>Список пустых опций по классам автомобилей.</returns>
        private IEnumerable<RaceOptionsModel> CreateEmptyOptions()
        {
            return new List<RaceOptionsModel> 
                        { 
                            new RaceOptionsModel(CarClassesEnum.FWD) { RaceCount = RaceCount, LidersCount = LidersCount },
                            new RaceOptionsModel(CarClassesEnum.RWD) { RaceCount = RaceCount, LidersCount = LidersCount },
                            new RaceOptionsModel(CarClassesEnum.AWD) { RaceCount = RaceCount, LidersCount = LidersCount },
                            new RaceOptionsModel(CarClassesEnum.Sport) { RaceCount = RaceCount, LidersCount = LidersCount },
                            new RaceOptionsModel(CarClassesEnum.K100) { RaceCount = RaceCount, LidersCount = LidersCount },
                            new RaceOptionsModel(CarClassesEnum.K160) { RaceCount = RaceCount, LidersCount = LidersCount },
                            new RaceOptionsModel(CarClassesEnum.KA) { RaceCount = RaceCount, LidersCount = LidersCount }
                        };
        }

        #endregion
    }
}
