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
                            new RaceOptionsModel(CarClassesEnum.Sport) { RaceCount = RaceCount, LidersCount = LidersCount },
                            new RaceOptionsModel(CarClassesEnum.K100) { RaceCount = RaceCount, LidersCount = LidersCount },
                            new RaceOptionsModel(CarClassesEnum.K160) { RaceCount = RaceCount, LidersCount = LidersCount },
                            new RaceOptionsModel(CarClassesEnum.KA) { RaceCount = RaceCount, LidersCount = LidersCount }
                        };
        }

        /// <summary>
        /// Задать номера заездов для заданного класса автомобилей.
        /// </summary>
        public void SetRaceNumbers()
        {
            var cc = ParseCarClass(PrintView.SelectedCarClass);
            var options = Options.FirstOrDefault(o => o.CarClass == cc);
            var list = new List<int>(options.RaceCount);

            for (int i = 1; i <= options.RaceCount; i++)
            {
                list.Add(i);
            }

            PrintView.RacesNumbers = list;
        }

        /// <summary>
        /// Получить результаты для подачи их на отчет.
        /// </summary>
        /// <param name="carClass">Класс автомобилей для которы получаем отчет.</param>
        /// <param name="raceNumber">Номер тура, для которого получаем отчет.</param>
        /// <returns>Результаты заезда.</returns>
        public static IEnumerable<ResultsForReport> GetResults(string carClass, int raceNumber)
        {
            var results = new List<ResultsForReport>();
            var cc = ParseCarClass(carClass);
            var racers = RacersDbManager.GetRacers(cc);
            
            // Вначале добавяем участников у ктороых есть результаты
            foreach (var racer in racers.Where(r => r.Results != null).OrderBy(r => r.Results.GetMinTime(raceNumber)))
            {
                AddNewRacer(results, racer, cc);
            }

            // Затем добавляем участников у ктороых нету результатов
            foreach (var racer in racers.Where(r => r.Results == null))
            {
                AddNewRacer(results, racer, cc);
            }
            return results;
        }

        /// <summary>
        /// Добавить в список участников нового участника на базе модели участника из БД.
        /// </summary>
        /// <param name="results">Список участников, в который будет добавлен новый участник.</param>
        /// <param name="racer">Модель из БД добавляемого участника.</param>
        /// <param name="carClass">Класс автомобилей.</param>
        private static void AddNewRacer(List<ResultsForReport> results, RacerModel racer, CarClassesEnum carClass)
        {
            var result = new ResultsForReport
                                {
                                    RacerName = racer.FirstName + " " + racer.LastName + " " + racer.MiddleName,
                                    CarName = racer.Car.Name
                                };

            // Добавить результаты кругов


            results.Add(result);
        }

        /// <summary>
        /// Отпарсить класс автомобилей в виде строки.
        /// </summary>
        /// <param name="carClass">Класс автомобилей.</param>
        /// <returns></returns>
        public static CarClassesEnum ParseCarClass(string carClass)
        {
            return (CarClassesEnum)Enum.Parse(Type.GetType("Sprint.Models.CarClassesEnum"), carClass);
        }

        /// <summary>
        /// Перевести класс автомобилей в полное имя.
        /// </summary>
        /// <param name="carClass">Класс автомобилей.</param>
        /// <returns></returns>
        public static string ConvertCarClass(CarClassesEnum carClass)
        {
            switch (carClass)
            {
                case CarClassesEnum.FWD:
                    {
                        return "Передний привод";
                    }
                case CarClassesEnum.RWD:
                    {
                        return "Задний привод";
                    }
                case CarClassesEnum.AWD:
                    {
                        return "Полный привод";
                    }
                case CarClassesEnum.Sport:
                    {
                        return "Спорт";
                    }
                case CarClassesEnum.K100:
                    {
                        return "До 100 л.с.";
                    }
                case CarClassesEnum.K160:
                    {
                        return "От 100 л.с. до 160 л.с.";
                    }
                case CarClassesEnum.KA:
                    {
                        return "Свыше 160 л.с.";
                    }
                default: return "";
            }
        }

        #endregion				
    }
}
