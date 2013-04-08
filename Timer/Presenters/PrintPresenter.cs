using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sprint.Interfaces;
using Sprint.Managers;
using Sprint.Models;

namespace Sprint.Presenters
{
    public class PrintPresenter
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
        /// Задать или получить представление печати.
        /// </summary>
        private IPrintView PrintView { get; set; }

        /// <summary>
        /// Задать или получить опции гонок по классам автомобилей.
        /// </summary>
        private IEnumerable<RaceOptionsModel> Options { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="printView">Представление печати.</param>
        public PrintPresenter(IPrintView printView)
        {
            PrintView = printView;
            Options = GetOptions();

            PrintView.CarClasses = Enum.GetNames(Type.GetType("Sprint.Models.CarClassesEnum")).ToList();
        }

        #endregion

        #region Методы

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

                if (db_o.Any(o => o.CarClass == carClass))
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
                            new RaceOptionsModel(CarClassesEnum.Sport) { RaceCount = RaceCount, LidersCount = LidersCount }
                        };
        }

        /// <summary>
        /// Задать номера заездов для заданного класса автомобилей.
        /// </summary>
        public void SetRaceNumbers()
        {
            var cc = (CarClassesEnum) Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), PrintView.SelectedCarClass);
            var options = Options.FirstOrDefault(o => o.CarClass == cc);
            var list = new List<int>(options.RaceCount);

            for (int i = 1; i <= options.RaceCount; i++)
            {
                list.Add(i);
            }

            PrintView.RacesNumbers = list;
        }

        #endregion				
    }
}
