using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Sprint.Managers;
using Sprint.Models;
using Sprint.Views;
using Sprint.Interfaces;

namespace Sprint.Presenters
{
    /// <summary>
    /// Презентер окна ввода гонщиков.
    /// </summary>
    public class NewRacerPresenter
    {
        #region Свойства

        /// <summary>
        /// Задать или получить интерфейс на визуальное представление формы.
        /// </summary>
        private INewRacer NewRacerView { get; set; }

        /// <summary>
        /// Задать или получить список гонщиков.
        /// </summary>
        public List<RacerModel> Racers { get; set; }

        /// <summary>
        /// Получить список слассов автомобилей.
        /// </summary>
        private List<string> CarClasses
        {
            get
            {
                return Enum.GetNames(Type.GetType("Sprint.Models.CarClassesEnum")).ToList();
            }
        }

        /// <summary>
        /// Задать или получить номер следующего добавляемого гонщика.
        /// </summary>
        private int NextRacerNumber { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="newRaserForm">Ссылка на форму.</param>
        public NewRacerPresenter(INewRacer newRaserForm)
        {
            NewRacerView = newRaserForm;

            if (NewRacerView != null)
            {
                NewRacerView.CarClasses = CarClasses;                

                Racers = new List<RacerModel>();

                NewRacerView.RacersTable = CreateTable();
            }

            NextRacerNumber = RacersDbManager.GetRacers().Count() + 1;
        }

        #endregion

        #region Методы

        /// <summary>
        ///  Добавление нового участника.
        /// </summary>
        public void AddNewRacer()
        {
            // Добавлениее новго гонщика в структуру
            Racers.Add(new RacerModel(NewRacerView.FirstName, NewRacerView.LastName, NewRacerView.MiddleName,
                                        NewRacerView.Manufacturer, NewRacerView.Model,
                                        NewRacerView.EngineSize, NewRacerView.EnginePower,
                                        NewRacerView.CarClass)
                            {
                                RacerNumber = NextRacerNumber
                            });

            // Добавление нового гонщика в таблицу
            var racer   = Racers.Last();
            var table   = NewRacerView.RacersTable;
            var row     = table.NewRow();

            row[0] = racer.RacerNumber;
            row[1] = racer.FirstName;
            row[2] = racer.LastName;
            row[3] = racer.MiddleName;
            row[4] = racer.Car.ToString();
            row[5] = racer.Car.CarClass.ToString();

            table.Rows.Add(row);

            NewRacerView.FirstName  = string.Empty;
            NewRacerView.LastName   = string.Empty;
            NewRacerView.MiddleName = string.Empty;
            NewRacerView.Manufacturer    = string.Empty;
            NewRacerView.Model = string.Empty;
            NewRacerView.EngineSize = 0.0;
            NewRacerView.EnginePower = 0.0;

            NextRacerNumber++;
        }

        /// <summary>
        /// Сгенерировть заголовок таблицы.
        /// </summary>
        /// <returns>Сгенерированная таблица.</returns>
        private DataTable CreateTable()
        {
            var table = new DataTable();

            var column = new DataColumn("№");
            table.Columns.Add(column);

            column = new DataColumn("Фамилия");
            table.Columns.Add(column);

            column = new DataColumn("Имя");
            table.Columns.Add(column);

            column = new DataColumn("Отчетсво");
            table.Columns.Add(column);

            column = new DataColumn("Автомобиль");
            table.Columns.Add(column);

            column = new DataColumn("Класс");
            table.Columns.Add(column);

            return table;
        }

        /// <summary>
        /// Удалить гонщика из списка участников.
        /// </summary>
        /// <param name="index"></param>
        public void DeleteRacer(int index)
        {
            Racers.RemoveAt(index);
        }

        #endregion
    }
}
