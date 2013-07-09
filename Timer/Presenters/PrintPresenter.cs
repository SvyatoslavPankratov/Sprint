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

            PrintView.CarClasses = Enum.GetNames(typeof(CarClassesEnum)).ToList();
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

            foreach (var cc in Enum.GetNames(typeof(CarClassesEnum)))
            {
                var carClass = (CarClassesEnum)Enum.Parse(typeof(CarClassesEnum), cc);

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
                        RaceCount = ConstantsModel.RaceCount,
                        LidersCount = ConstantsModel.LidersCount
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
                            new RaceOptionsModel(CarClassesEnum.FWD) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount },
                            new RaceOptionsModel(CarClassesEnum.RWD) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount },
                            new RaceOptionsModel(CarClassesEnum.AWD) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount },
                            new RaceOptionsModel(CarClassesEnum.Sport) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount },
                            new RaceOptionsModel(CarClassesEnum.K100) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount },
                            new RaceOptionsModel(CarClassesEnum.K160) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount },
                            new RaceOptionsModel(CarClassesEnum.KA) { RaceCount = ConstantsModel.RaceCount, LidersCount = ConstantsModel.LidersCount }
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
        /// <param name="raceNumber">Номер тура, для которого получаем отчет, начиная с 0.</param>
        /// <returns>Результаты заезда.</returns>
        public static IEnumerable<ResultsForReport> GetResults(string carClass, int raceNumber)
        {
            var results = new List<ResultsForReport>();
            var cc = ParseCarClass(carClass);
            var racers = RacersDbManager.GetRacers(cc, raceNumber);
            
            // Вначале добавяем участников у ктороых есть результаты
            var racers_with_results = from racer in racers
                                      where racer.Results.HasValues(raceNumber)
                                      orderby racer.Results.GetMinTime(raceNumber)
                                      select racer;

            if (racers_with_results.Any())
            {
                foreach (var racer in racers_with_results)
                {
                    AddNewRacer(results, racer, cc, raceNumber);
                }
            }

            // Затем добавляем участников у ктороых нету результатов
            var racers_without_results = from racer in racers
                                         where !racer.Results.HasValues(raceNumber)
                                         orderby racer.RacerNumber
                                         select racer;

            if (racers_without_results.Any())
            {
                foreach (var racer in racers_without_results)
                {
                    AddNewRacer(results, racer, cc, raceNumber);
                }
            }
            return results;
        }

        /// <summary>
        /// Добавить в список участников нового участника на базе модели участника из БД.
        /// </summary>
        /// <param name="results">Список участников, в который будет добавлен новый участник.</param>
        /// <param name="racer">Модель из БД добавляемого участника.</param>
        /// <param name="carClass">Класс автомобилей.</param>
        /// <param name="raceNumber">Номер тура, для которого получаем отчет, начиная с 0.</param>
        private static void AddNewRacer(List<ResultsForReport> results, RacerModel racer, CarClassesEnum carClass, int raceNumber)
        {
            var result = new ResultsForReport
                                {
                                    RacerName = racer.FirstName + " " + racer.LastName + " " + racer.MiddleName,
                                    CarName = racer.Car.Name,
                                    RacerNumber = racer.RacerNumber
                                };

            // Добавить результаты кругов
            TimeModel time = null;

            var min_time = racer.Results.GetMinTime(raceNumber);

            time = min_time.HasValue ? new TimeModel(min_time.Value) : null;
            result.MinTime = time == null ? string.Empty : time.ToString();

            time = racer.Results.ResultsList.ElementAt(raceNumber).ElementAt(1);
            result.Time1 = time == null ? string.Empty : time.ToString();

            time = racer.Results.ResultsList.ElementAt(raceNumber).ElementAt(2);
            result.Time2 = time == null ? string.Empty : time.ToString();

            time = racer.Results.ResultsList.ElementAt(raceNumber).ElementAt(3);
            result.Time3 = time == null ? string.Empty : time.ToString();

            results.Add(result);
        }

        /// <summary>
        /// Отпарсить класс автомобилей в виде строки.
        /// </summary>
        /// <param name="carClass">Класс автомобилей.</param>
        /// <returns></returns>
        public static CarClassesEnum ParseCarClass(string carClass)
        {
            return (CarClassesEnum)Enum.Parse(typeof(CarClassesEnum), carClass);
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
